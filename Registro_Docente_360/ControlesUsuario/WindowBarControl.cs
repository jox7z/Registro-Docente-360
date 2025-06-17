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
    public partial class WindowBarControl : UserControl
    {
        public WindowBarControl()
        {
            InitializeComponent();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            if (parent != null)
            {
                if (parent.WindowState == FormWindowState.Maximized)
                    parent.WindowState = FormWindowState.Normal;
                else
                    parent.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            if (parent != null)
                parent.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            if (parent != null)
                parent.WindowState = FormWindowState.Minimized;
        }
    }
}
