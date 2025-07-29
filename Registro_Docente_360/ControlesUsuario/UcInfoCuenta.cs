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


namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcInfoCuenta : UserControl
    {

        private Usuarios usuarioActual;

        private string contraGuardada; // 👈 Aquí la declaras
        private bool edicionHabilitada = false;
        public event EventHandler OnVolverAConfiguracion;

        public UcInfoCuenta()
        {
            InitializeComponent();
            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();

        }


        private void CentrarMiniContenedor()
        {
            int anchoContenedor = panelMiniContenedor.Width;
            int altoContenedor = panelMiniContenedor.Height;

            panelMiniContenedor.Left = (this.ClientSize.Width - anchoContenedor) / 2;
            panelMiniContenedor.Top = (this.ClientSize.Height - altoContenedor) / 2;
        }

        private void UcInfoCuenta_Load(object sender, EventArgs e)
        {
            // Ocultar elementos de edición al cargar
            btnGuardarCambios.Visible = false;
            pnConfirmContra.Visible = false;
            txtNombre.ReadOnly = true;
            txtCorreo.ReadOnly = true;

            // Asignar datos del usuario desde la sesión
            txtNombre.Text = Sesion.Nombre;
            txtCorreo.Text = Sesion.Correo;
            lblTipodeRol.Text = Sesion.Rol; // Asegúrate que lblRol exista y esté en tu diseñador
            lblFechaRegistro.Text = Sesion.FechaRegistro.ToString("dd/MM/yyyy");

            // Guardar la contraseña actual (simulación, normalmente estaría encriptada)
            contraGuardada = Sesion.Contrasena;
        }



        private void panelCambiarContra_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblEditarInfo1_Click(object sender, EventArgs e)
        {
            MostrarPanelConfirmacion();
        }

        private void lblEditarInfo2_Click(object sender, EventArgs e)
        {
            MostrarPanelConfirmacion();
        }

        private void MostrarPanelConfirmacion()
        {
            pnInfo.Visible = false;
            pnConfirmContra.Visible = true;
            txtContra.Clear();
            txtContra.Focus();
            
        }

        private void cbMostrarContra_CheckedChanged(object sender, EventArgs e)
        {
            bool mostrar = cbMostrarContra.Checked;
            txtContra.UseSystemPasswordChar = !mostrar;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtContra.Text == contraGuardada)
            {
                edicionHabilitada = true;
                pnConfirmContra.Visible = false;
                pnInfo.Visible = true;

                txtNombre.ReadOnly = false;
                txtCorreo.ReadOnly = false;

                txtNombre.BackColor = Color.White;
                txtCorreo.BackColor = Color.White;

                btnGuardarCambios.Visible = true;

                MessageBox.Show("Puedes editar tu información ahora.");
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContra.Clear();
                txtContra.Focus();
            }
        }


        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txtNombre.Text.Trim();
            string nuevoCorreo = txtCorreo.Text.Trim();

            using (var contexto = new RegistroDocenteEntities())
            {
                int idUsuario = Sesion.IdUsuario;
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idUsuario);

                if (usuario != null)
                {
                    usuario.nombre_usuario = nuevoNombre;
                    usuario.correo = nuevoCorreo;

                    contexto.SaveChanges();

                    // Actualizar la sesión
                    Sesion.Nombre = nuevoNombre;
                    Sesion.Correo = nuevoCorreo;

                    MessageBox.Show("Datos actualizados correctamente.");
                }
                else
                {
                    MessageBox.Show("Error: no se encontró el usuario en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Bloquear edición nuevamente
            txtNombre.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtNombre.BackColor = Color.White;
            txtCorreo.BackColor = Color.White;
            btnGuardarCambios.Visible = false;
            edicionHabilitada = false;
        }



        public event EventHandler OnSolicitarInfoCuenta;

        private void btnVolver_Click(object sender, EventArgs e)
        {
            OnVolverAConfiguracion?.Invoke(this, EventArgs.Empty);
        }

        private void panelMiniContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
