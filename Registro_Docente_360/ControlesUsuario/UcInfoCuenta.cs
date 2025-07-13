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
    public partial class UcInfoCuenta : UserControl
    {

        private string contraGuardada = "admin123";
        private bool edicionHabilitada = false;

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
            btnGuardarCambios.Visible = false;
            pnConfirmContra.Visible = false;
            txtNombre.ReadOnly = true;
            txtCorreo.ReadOnly = true;

            txtNombre.Text = "Jose Hernandez";
            txtCorreo.Text = "joseadrianhernandez07@gmail.com";
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
                pnConfirmContra.Visible=false;
                pnInfo.Visible =true;

                txtNombre.ReadOnly =false;
                txtCorreo.ReadOnly =false;

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
            

            //Aqui va para guardar en base de datos 

            string nuevoNombre = txtNombre.Text;
            string nuevoCorreo = txtCorreo.Text;

            // Simulación
            MessageBox.Show("Datos actualizados");

            txtNombre.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            edicionHabilitada = false;

        }

        public event EventHandler OnSolicitarInfoCuenta;

    }
}
