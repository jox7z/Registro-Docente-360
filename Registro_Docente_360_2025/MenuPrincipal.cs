using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Docente_360_2025
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnAsistencia_Click(object sender, EventArgs e)
        {
            VentanaFechas asistencia = new VentanaFechas();
            asistencia.Show();
            this.Hide();
        }

        private void btnHorario_Click(object sender, EventArgs e)
        {
            Horario horario = new Horario();
            horario.Show();
            this.Hide();
        }

        private void btnAlumnos_Click(object sender, EventArgs e)
        {
            Alumnos alumnos = new Alumnos();
            alumnos.Show();
            this.Hide();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            Reportes reportes = new Reportes();
            reportes.Show();
            this.Hide();
        }

        
    }
}
