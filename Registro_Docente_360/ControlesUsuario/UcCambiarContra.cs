using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Forms;
using SiticoneNetFrameworkUI.Helpers.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcCambiarContra : UserControl
    {
        public UcCambiarContra()
        {
            InitializeComponent();


            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();
        }

        public event EventHandler OnVolverAConfiguracion;


        private void CentrarMiniContenedor()
        {
            int anchoContenedor = panelMiniContenedor.Width;
            int altoContenedor = panelMiniContenedor.Height;

            panelMiniContenedor.Left = (this.ClientSize.Width - anchoContenedor) / 2;
            panelMiniContenedor.Top = (this.ClientSize.Height - altoContenedor) / 2;
        }


        private void UcCambiarContra_Load(object sender, EventArgs e)
        {
            pnNuevaContra.Visible = false;
        }


        private void cbMostrarContra_CheckedChanged(object sender, EventArgs e)
        {
            bool mostrar = cbMostrarContra.Checked;
            txtContraActual.UseSystemPasswordChar = !mostrar;
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            AlumnoController controlador = new AlumnoController();
            string clave = controlador.EncriptarContrasena(txtContraActual.Text);

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);

                if (usuario == null)
                {
                    MessageBox.Show("Error al cargar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (usuario.contraseña == clave)
                {
                    pnNuevaContra.Visible = true;
                    panelCambiarContra.Visible = false;
                    btnVolver.Visible = false;
                }
                else
                {
                    MessageBox.Show("La contraseña actual es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void cbMostrarContras_CheckedChanged(object sender, EventArgs e)
        {
            bool mostrar = cbMostrarContras.Checked;

            txtNuevaContra.UseSystemPasswordChar = !mostrar;
            txtConfirmacion.UseSystemPasswordChar = !mostrar;
        }


        private void btnGuardarNueva_Click(object sender, EventArgs e)
        {
            string nueva = txtNuevaContra.Text;
            string confirmar = txtConfirmacion.Text;

            // Validar si las contraseñas coinciden
            if (nueva != confirmar)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar si la nueva contraseña tiene al menos 6 caracteres
            if (nueva.Length < 6)
            {
                MessageBox.Show("La nueva contraseña debe tener al menos 6 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar si la nueva contraseña está vacía
            if (string.IsNullOrWhiteSpace(nueva))
            {
                MessageBox.Show("La nueva contraseña no puede estar vacía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);

                if (usuario == null)
                {
                    MessageBox.Show("No se pudo encontrar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AlumnoController controlador = new AlumnoController();
                string nuevaEncriptada = controlador.EncriptarContrasena(nueva);

                usuario.contraseña = nuevaEncriptada;
                contexto.SaveChanges();

                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Registrar la acción realizada
                string accion = "Actualizar contraseña";
                string descripcion = $"Actualización de contraseña del usuario: {usuario.nombre_usuario}";
                string modulo = "Alumnos";
                controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
            }

            // Limpiar campos después de la actualización
            txtContraActual.Clear();
            txtNuevaContra.Clear();
            txtConfirmacion.Clear();
            pnNuevaContra.Visible = false;
            panelCambiarContra.Visible = true;
            btnVolver.Visible = true;
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {
            OnVolverAConfiguracion?.Invoke(this, EventArgs.Empty);
        }


    }
}
