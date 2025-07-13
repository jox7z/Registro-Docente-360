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

            pnCerrarSesion.Click += pnCerrarSesion_Click;
            lblCerrarSesion.Click += pnCerrarSesion_Click;
            lblCerrarSesion2.Click += pnCerrarSesion_Click;
            pictureflecha3.Click += pnCerrarSesion_Click; 
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


        private void pnCerrarSesion_Click(object sender, EventArgs e)
        {
            //Aqui va el Evento para cerrar sesion
        }

        private void panelInfoCuenta_Paint(object sender, PaintEventArgs e) { }
        

        private void panelCambiarContra_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        
    }
}
