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
using Registro_Docente_360.ControlesUsuario;
using Registro_Docente_360.Eventos;

namespace Registro_Docente_360.Forms
{
    public partial class UcConfiguracion : UserControl
    {



        public UcConfiguracion()
        {
            InitializeComponent();

            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();

            panelCambiarContra.Click += panelCambiarContra_Click;
            lblCambiaContra.Click += panelCambiarContra_Click;
            lblCambiaContra2.Click += panelCambiarContra_Click;
            pictureflecha1.Click += panelCambiarContra_Click;

            panelInfoCuenta.Click += panelInfoCuenta_Click;
            lblinfoCuenta.Click += panelInfoCuenta_Click;
            lblinfoCuenta2.Click += panelInfoCuenta_Click;
            pictureflecha2.Click += panelInfoCuenta_Click;

            EventHandler cerrarSesion = (s, e) => EjecutarCerrarSesion();
            pnCerrarSesion.Click += cerrarSesion;
            lblCerrarSesion.Click += cerrarSesion;
            lblCerrarSesion2.Click += cerrarSesion;
            pictureflecha3.Click += cerrarSesion;
            pnCerrarSesion.MouseDown += pnCerrarSesion_MouseDown;

        }



        private void CentrarMiniContenedor()
        {
            int anchoContenedor = panelMiniContenedor.Width;
            int altoContenedor = panelMiniContenedor.Height;

            panelMiniContenedor.Left = (this.ClientSize.Width - anchoContenedor) / 2;
            panelMiniContenedor.Top = (this.ClientSize.Height - altoContenedor) / 2;
        }

        public event EventHandler OnSolicitarCambioContrasena;
        public event EventHandler OnSolicitarInfoCuenta;

        private void panelCambiarContra_Click(object sender, EventArgs e)
        {
            OnSolicitarCambioContrasena?.Invoke(this, EventArgs.Empty);
        }

        private void panelInfoCuenta_Click(object sender, EventArgs e)
        {
            OnSolicitarInfoCuenta?.Invoke(this, EventArgs.Empty);
        }


        private bool cerrandoSesion = false;

        private void pnCerrarSesion_MouseDown(object sender, MouseEventArgs e)
        {
            // Validar que solo sea clic izquierdo (opcional)
            if (e.Button == MouseButtons.Left)
            {
                EjecutarCerrarSesion();
            }
        }



        private void EjecutarCerrarSesion()
        {
            if (cerrandoSesion) return;
            cerrandoSesion = true;

            DialogResult resultado = MessageBox.Show("¿Deseas cerrar sesión y volver al login?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Debug: Verificar el valor de Sesion.IdUsuario
                System.Diagnostics.Debug.WriteLine($"Cerrando sesión para el usuario con ID: {Sesion.IdUsuario}");

                // Registrar el LOGOUT en la bitácora
                RegistrarAcceso(Sesion.IdUsuario, "LOGOUT");

                // Limpiar sesión
                Sesion.IdUsuario = 0;
                Sesion.NombreUsuario = null;
                Sesion.UltimoAnhoSeleccionado = 0;
                Sesion.UltimoMesIndex = 0;
                Sesion.UltimaSemanaIndex = 0;

                // Abrir Login
                Form formularioPadre = this.FindForm();
                if (formularioPadre != null)
                {
                    formularioPadre.Hide();
                }

                Login loginForm = new Login();
                loginForm.Show();

                formularioPadre?.Close();
            }

            cerrandoSesion = false;
        }


        private void RegistrarAcceso(int idUsuario, string tipoAcceso)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var acceso = new Bitacora_Accesos
                {
                    id_usuario = idUsuario,
                    tipo_acceso = tipoAcceso,
                    fecha_acceso = DateTime.Now
                };

                contexto.Bitacora_Accesos.Add(acceso);
                contexto.SaveChanges();
            }
        }





        private void panelInfoCuenta_Paint(object sender, PaintEventArgs e) { }


        private void panelCambiarContra_Paint(object sender, PaintEventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e) { }


    }
}
