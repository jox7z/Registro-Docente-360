using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Registro_Docente_360.ControlesUsuario;

namespace Registro_Docente_360
{
    public partial class UcCalendario : UserControl
    {
        private int month;
        private int year;

        public static int static_month, static_year;

        public UcCalendario()
        {
            InitializeComponent();

            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            displayDays();
        }

        private void UcCalendario_Load(object sender, EventArgs e)
        {
            displayDays();
            CentrarPanel();
            this.Resize += (s, ev) => CentrarPanel();
        }

        private void CentrarPanel()
        {
            int x = (this.ClientSize.Width - panelminiContenedor.Width) / 2;
            int y = (this.ClientSize.Height - panelminiContenedor.Height) / 2;

            // Asegura que no se salga de la vista si la ventana es más pequeña
            panelminiContenedor.Location = new Point(Math.Max(0, x), Math.Max(0, y));
        }

        private void displayDays()
        {
            daycontainer.Controls.Clear();

            DateTime startOfMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int startDay = Convert.ToInt32(startOfMonth.DayOfWeek.ToString("d")) + 1;
            static_month = month;
            static_year = year;



            for (int i = 1; i < startDay; i++)
            {
                UserControlBlank blank = new UserControlBlank();
                daycontainer.Controls.Add(blank);
            }

            for (int i = 1; i <= days; i++)
            {
                UcDias day = new UcDias();
                day.days(i);
                daycontainer.Controls.Add(day);
            }

            
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month).ToUpper();
            lblTitulo.Text = monthname + " " + year;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            static_month = month;
            static_year = year;

            month--;
            if (month < 1)
            {
                month = 12;
                year--;
            }
            displayDays();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {

            static_month = month;
            static_year = year;

            month++;
            if (month > 12)
            {
                month = 1;
                year++;
            }
            displayDays();
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e) { }

    }
}
