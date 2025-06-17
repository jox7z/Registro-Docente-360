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
    public partial class UcVentanaAsistencia : UserControl
    {
        public UcVentanaAsistencia()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ActualizarCabecera(string grupo, int anho, string fechaInicio, string fechaFin)
        {
            lblGrupo.Text = $"Sección: {grupo} - Año Lectivo: {anho}";
            lblSemana.Text = $"Semana Seleccionada: del {fechaInicio} al {fechaFin}";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
           
            UcFechas ucFechas = new UcFechas();       
            ucFechas.Dock = DockStyle.Fill;           
            
        }
    }
}
