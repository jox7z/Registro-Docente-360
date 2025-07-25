using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelos.EntityFramework;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Forms;

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
            string contraActual = txtContraActual.Text;

            using (var db = new RegistroDocenteEntities())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);

                if (usuario == null)
                {
                    MessageBox.Show("Error al cargar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (usuario.contraseña == contraActual)
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

            if (nueva != confirmar)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(nueva))
            {
                MessageBox.Show("La nueva contraseña no puede estar vacía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new RegistroDocenteEntities())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);

                if (usuario == null)
                {
                    MessageBox.Show("No se pudo encontrar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                usuario.contraseña = nueva;
                db.SaveChanges();

                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Limpiar campos
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

        private void CambiarAConfiguracion()
        {
            Control parent = this.Parent;
            if (parent != null)
            {
                parent.Controls.Clear(); // Elimina el UC actual
                var configuracionUC = new UcConfiguracion();
                parent.Controls.Add(configuracionUC);
                configuracionUC.Dock = DockStyle.Fill;
            }
        }

        private void panelCambiarContra_Paint(object sender, PaintEventArgs e) { }

        
    }
}
