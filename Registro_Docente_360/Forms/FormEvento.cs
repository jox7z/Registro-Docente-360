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
    public partial class FormEvento : Form
    {
        public FormEvento()
        {
            InitializeComponent();
            this.Load += FormEvento_Load;
        }
        private void FormEvento_Load(object sender, EventArgs e)
        {
            txtFecha.Text = $"{UcCalendario.static_month}/{UcDias.static_day}/{UcCalendario.static_year}";
        }

        private void FormEvento_Click(object sender, EventArgs e)
        {
        }
    }
}
