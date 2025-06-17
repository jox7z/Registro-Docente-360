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
    public partial class UcReportes : UserControl
    {
        public UcReportes()
        {
            InitializeComponent();

            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();
        }

        private void CentrarMiniContenedor()
        {
            panelminiContenedor.Left = (this.ClientSize.Width - panelminiContenedor.Width) / 2;
            panelminiContenedor.Top = (this.ClientSize.Height - panelminiContenedor.Height) / 2;
        }

        private void UcReportes_Load(object sender, EventArgs e) { }

        
    }
}
