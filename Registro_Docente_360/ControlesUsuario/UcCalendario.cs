using Registro_Docente_360.ControlesUsuario;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Registro_Docente_360
{
    public partial class UcCalendario : UserControl
    {
        // Variables para el mes y año actual mostrados en el calendario
        private int month;
        private int year;

        // Variables estáticas accesibles desde otros UserControls (por ejemplo, UcDias)
        public static int static_month, static_year;

        // Constructor: inicializa el calendario con el mes y año actuales
        public UcCalendario()
        {
            InitializeComponent();

            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            displayDays(); // Carga los días del mes actual al iniciar
        }

        // Evento que se lanza cuando el UserControl se carga
        private void UcCalendario_Load(object sender, EventArgs e)
        {
            displayDays(); // Muestra los días del mes
            CentrarPanel(); // Centra el contenedor visual
            this.Resize += (s, ev) => CentrarPanel(); // Recalcula posición al redimensionar
        }

        // Centra el panel que contiene todo el calendario dentro del UserControl
        private void CentrarPanel()
        {
            int x = (this.ClientSize.Width - panelminiContenedor.Width) / 2;
            int y = (this.ClientSize.Height - panelminiContenedor.Height) / 2;

            // Asegura que el panel no se salga de los bordes si el tamaño es muy pequeño
            panelminiContenedor.Location = new Point(Math.Max(0, x), Math.Max(0, y));
        }

        // Muestra los días del mes actual en el contenedor
        private void displayDays()
        {
            daycontainer.Controls.Clear(); // Limpia cualquier día anterior

            DateTime startOfMonth = new DateTime(year, month, 1); // Primer día del mes
            int days = DateTime.DaysInMonth(year, month); // Cantidad de días del mes
            int startDay = Convert.ToInt32(startOfMonth.DayOfWeek.ToString("d")) + 1; // Día de la semana del 1ro
            static_month = month;
            static_year = year;

            // Añade espacios en blanco antes del primer día del mes
            for (int i = 1; i < startDay; i++)
            {
                UserControlBlank blank = new UserControlBlank();
                daycontainer.Controls.Add(blank);
            }

            // Añade cada día con su número y evento (si existe)
            for (int i = 1; i <= days; i++)
            {
                UcDias day = new UcDias();
                day.days(i); // Asigna el número del día
                day.displayEvent(); // Carga eventos si los hay
                daycontainer.Controls.Add(day); // Añade al contenedor
            }

            // Muestra el nombre del mes en mayúsculas junto con el año
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month).ToUpper();
            lblTitulo.Text = monthname + " " + year;
        }

        // Navegar al mes anterior
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            static_month = month;
            static_year = year;

            month--;
            if (month < 1)
            {
                month = 12; // Regresa a diciembre
                year--;     // Resta un año
            }
            displayDays(); // Refresca la vista con el nuevo mes
        }

        // Navegar al mes siguiente
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            static_month = month;
            static_year = year;

            month++;
            if (month > 12)
            {
                month = 1;  // Vuelve a enero
                year++;     // Suma un año
            }
            displayDays(); // Actualiza la vista
        }

       
        private void panelContenedor_Paint(object sender, PaintEventArgs e) { }
    }
}
