using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                DialogResult resultado = MessageBox.Show("¿Deseas cerrar sesión y volver al login?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Limpiar sesión
                    Sesion.IdUsuario = 0;
                    Sesion.NombreUsuario = null;
                    Sesion.UltimoAnhoSeleccionado = 0;
                    Sesion.UltimoMesIndex = 0;
                    Sesion.UltimaSemanaIndex = 0;

                    // Abrir Login
                    Form padre = this.FindForm();
                    if (padre != null)
                    {
                        padre.Hide();
                        new Login().Show();
                        padre.Close();
                    }
                }
            }
        }


        private void EjecutarCerrarSesion()
        {
            if (cerrandoSesion) return;
            cerrandoSesion = true;

            DialogResult resultado = MessageBox.Show("¿Deseas cerrar sesión y volver al login?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Sesion.IdUsuario = 0;
                Sesion.NombreUsuario = null;
                Sesion.UltimoAnhoSeleccionado = 0;
                Sesion.UltimoMesIndex = 0;
                Sesion.UltimaSemanaIndex = 0;

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


        private void panelInfoCuenta_Paint(object sender, PaintEventArgs e) { }
        

        private void panelCambiarContra_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        
    }
}
