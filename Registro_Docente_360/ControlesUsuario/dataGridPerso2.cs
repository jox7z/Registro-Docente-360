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
    public partial class dataGridPerso2 : UserControl
    {
        public dataGridPerso2()
        {
            InitializeComponent();
            datagridview2.RowPostPaint += dataGridView2_RowPostPaint;

        }

        public DataGridView Grid
        {
            get { return datagridview2;  }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string numero = (e.RowIndex + 1).ToString();
            var centro = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, datagridview2.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(numero, this.Font, SystemBrushes.ControlText, rect, centro);
        }

        private void datagridview2_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        
        
    }
}
