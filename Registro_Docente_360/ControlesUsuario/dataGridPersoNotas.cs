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
    public partial class dataGridPersoNotas : UserControl
    {
        public dataGridPersoNotas()
        {
            InitializeComponent();
        }

        public DataGridView Grid
        {
            get { return datagridview2; }
        }

        private void dataGridPersoNotas_Load(object sender, EventArgs e)
        {

        }
    }
}
