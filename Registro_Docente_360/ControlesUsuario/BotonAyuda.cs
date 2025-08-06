using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Docente_360.Forms
{
    public partial class BotonAyuda : UserControl
    {
        public BotonAyuda()
        {
            InitializeComponent();
            InicializarToolTip();
        }

        private ToolTip ayudaToolTip;

        private void InicializarToolTip()
        {
            ayudaToolTip = new ToolTip
            {
                IsBalloon = true,
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 100,
                ShowAlways = true
            };

            ayudaToolTip.SetToolTip(pictureAyuda, "Haz clic para obtener ayuda");
        }

        private void pictureAyuda_Click(object sender, EventArgs e)
        {
            string urlArchivo = "https://drive.google.com/drive/folders/1VFuMI5kD7_Qa-1l3HJzJJz_PoyHMol8u?usp=drive_link"; // URL pública del manual

            try
            {
                System.Diagnostics.Process.Start(urlArchivo); // Esto abrirá el archivo en el navegador web predeterminado
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el archivo. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
