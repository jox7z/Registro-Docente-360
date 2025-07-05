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
    public partial class UcAcercaDe : UserControl
    {
        public UcAcercaDe()
        {
            InitializeComponent();

            this.Resize += (s, e) => CentrarPanelMiniContenedor();
            CentrarPanelMiniContenedor();
        }

        

        private void siticonePanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lbllogin2_Click(object sender, EventArgs e)
        {

        }

        private void lblAcercaDe1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelminiContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CentrarPanelMiniContenedor()
        {
            panelminiContenedor.Left = (this.ClientSize.Width - panelminiContenedor.Width) / 2;
            panelminiContenedor.Top = (this.ClientSize.Height - panelminiContenedor.Height) / 2;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
