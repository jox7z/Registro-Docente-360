using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Registro_Docente_360.Eventos;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcFechas : UserControl
    {
        public UcFechas()
        {
            InitializeComponent();

            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();

            comboMeses.SelectedIndexChanged += comboMeses_SelectedIndexChanged;
            comboAnhos.SelectedIndexChanged += comboAnhos_SelectedIndexChanged;

            UcFechas_Load(this, EventArgs.Empty);
        }

        private void CentrarMiniContenedor()
        {
            panelminiContenedor.Left = (this.ClientSize.Width - 431) / 2;
            panelminiContenedor.Top = (this.ClientSize.Height - 523) / 2;
        }




        /// <summary>
        /// Carga inicial: llena combos de años, meses y semanas.
        /// </summary>
        private void UcFechas_Load(object sender, EventArgs e)
        {
            for (int anho = 2025; anho <= DateTime.Now.Year + 5; anho++)
                comboAnhos.Items.Add(anho.ToString());

            comboAnhos.SelectedItem = DateTime.Now.Year.ToString();

            string[] meses = { "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio",
                               "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            comboMeses.Items.AddRange(meses);
            comboMeses.SelectedIndex = 0;

            LlenarComboSemanas();
        }

        /// <summary>
        /// Llena el combo de semanas según el mes y año seleccionados.
        /// </summary>
        private void LlenarComboSemanas()
        {
            int mesSeleccionado = comboMeses.SelectedIndex + 2;
            int anhoSeleccionado = int.Parse(comboAnhos.SelectedItem.ToString());

            var semanas = ObtenerSemanasMes(anhoSeleccionado, mesSeleccionado);
            comboSemanas.Items.Clear();
            comboSemanas.Items.AddRange(semanas.ToArray());

            comboSemanas.SelectedIndex = semanas.Count > 0 ? 0 : -1;
        }

        /// <summary>
        /// Genera las semanas (lunes a viernes) para un mes específico.
        /// </summary>
        public List<string> ObtenerSemanasMes(int año, int mes)
        {
            var semanas = new List<string>();
            DateTime primerDiaMes = new DateTime(año, mes, 1);
            DateTime lunesActual = primerDiaMes;

            while (lunesActual.DayOfWeek != DayOfWeek.Monday && lunesActual.Month == mes)
                lunesActual = lunesActual.AddDays(1);

            while (lunesActual.Month == mes)
            {
                DateTime viernes = lunesActual.AddDays(4);
                semanas.Add($"{lunesActual:dd/MM} - {viernes:dd/MM}");
                lunesActual = lunesActual.AddDays(7);
            }

            return semanas;
        }

        /// <summary>
        /// Actualiza las semanas al cambiar el mes.
        /// </summary>
        private void comboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboSemanas();
        }

        /// <summary>
        /// Actualiza las semanas al cambiar el año.
        /// </summary>
        private void comboAnhos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboSemanas();
        }

        private void btnAboutUs_Click(object sender, EventArgs e) { }

        /// <summary>
        /// Evento que se dispara al hacer clic en "Seleccionar".
        /// Envía los datos seleccionados (año, mes y semana).
        /// </summary>
        public event EventHandler<FechaSeleccionadaEventArgs> OnFechaSeleccionada;

        /// <summary>
        /// Toma los valores seleccionados y lanza el evento con la información.
        /// </summary>
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (comboAnhos.SelectedItem == null || comboMeses.SelectedItem == null || comboSemanas.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione el año, mes y semana.");
                return;
            }

            int anho = int.Parse(comboAnhos.SelectedItem.ToString());
            string mesTexto = comboMeses.SelectedItem.ToString();
            int mesNumero = comboMeses.SelectedIndex + 2;

            string[] fechas = comboSemanas.SelectedItem.ToString().Split('-');
            string fechaInicio = fechas[0].Trim();
            string fechaFin = fechas[1].Trim();

            OnFechaSeleccionada?.Invoke(this, new FechaSeleccionadaEventArgs
            {
                Anho = anho,
                MesTexto = mesTexto,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            });
        }
    }
}
