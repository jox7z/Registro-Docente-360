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
    public partial class dataGridPerso : UserControl
    {
        public dataGridPerso()
        {
            InitializeComponent();

            datagridview1.ReadOnly = false;
            datagridview1.EditMode = DataGridViewEditMode.EditOnEnter;
            datagridview1.AllowUserToAddRows = false;
            datagridview1.AllowUserToDeleteRows = false;
        }
    

        [Browsable(true)]
        [Category("Encabezados")]
        [Description("Texto para la columna 0")]

        public string EncabezadoColumna0
        {
            get => datagridview1.Columns.Count > 0 ? datagridview1.Columns[0].HeaderText : "";
            set
            {
                if (datagridview1.Columns.Count > 0)
                    datagridview1.Columns[0].HeaderText = value;
            }

        }
        public DataGridView Grid
        {
            get { return datagridview1; }
        }


    }
}
