using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Registro_Docente_360_2025
{
    public partial class VentanaFechas : Form
    {
        public VentanaFechas()
        {
            InitializeComponent();
            comboMeses.SelectedIndexChanged += comboMeses_SelectedIndexChanged;
        }

        private void VentanaFechas_Load(object sender, EventArgs e)
        {

            //Llenar años
            for (int anho = 2025; anho <= DateTime.Now.Year + 5; anho++)
            {
                comboAnhos.Items.Add(anho.ToString());
            }
            comboAnhos.SelectedItem = DateTime.Now.Year.ToString(); //este selecciona el ano actual



            //Llenar meses
            string[] meses = { "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                               "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            comboMeses.Items.AddRange(meses);
            comboMeses.SelectedIndex = 0; // Selecciona Febrero por defecto

            LlenarComboSemanas();
        }


        private void LlenarComboSemanas()
        {
            int mesSeleccionado = comboMeses.SelectedIndex + 2;
            int anhoSeleccionado = int.Parse(comboAnhos.SelectedItem.ToString());

            List<string> semanas = ObtenerSemanasMes(anhoSeleccionado, mesSeleccionado);
            comboSemanas.Items.Clear();
            comboSemanas.Items.AddRange(semanas.ToArray());

            comboSemanas.SelectedIndex = -1;
            if (semanas.Count > 0)
                comboSemanas.SelectedIndex = 0;
        }

        public List<string> ObtenerSemanasMes(int año, int mes)
        {
            List<string> semanas = new List<string>();
            DateTime primerDiaMes = new DateTime(año, mes, 1);
            DateTime ultimoDiaMes = new DateTime(año, mes, DateTime.DaysInMonth(año, mes));

            DateTime lunesActual = primerDiaMes;
            while (lunesActual.DayOfWeek != DayOfWeek.Monday && lunesActual.Month == mes)
            {
                lunesActual = lunesActual.AddDays(1);
            }

            while (lunesActual.Month == mes)
            {
                DateTime viernes = lunesActual.AddDays(4);

                // Si el viernes sobrepasa el mes, permitir que la semana continúe en el mes siguiente
                if (viernes.Month != mes)
                {
                    viernes = lunesActual.AddDays(4);
                }

                semanas.Add($"{lunesActual:dd/MM} - {viernes:dd/MM}");
                lunesActual = lunesActual.AddDays(7);
            }

            return semanas;
        }

        private void comboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Mes seleccionado: " + (comboMeses.SelectedIndex + 1));
            LlenarComboSemanas();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuPrincipal menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
            this.Hide();
        }

        private void comboAnhos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboSemanas();
        }
    }
}
