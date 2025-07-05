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
using Registro_Docente_360.Forms;

namespace Registro_Docente_360
{
    public partial class UcDias : UserControl
    {
        public static string static_day;
        public UcDias()
        {
            InitializeComponent();
        }

        private void UcDias_Load(object sender, EventArgs e)
        {

        }

        public void days(int numday)
        {
            lbldias.Text = numday+"";
        }

        private void UcDias_Click(object sender, EventArgs e)
        {
            static_day = lbldias.Text;
            FormEvento formEvento = new FormEvento();
            formEvento.Show();
        }
    }
}
