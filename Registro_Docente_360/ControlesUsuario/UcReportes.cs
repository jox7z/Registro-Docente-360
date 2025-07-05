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
    public partial class UcReportes : UserControl
    {
        public UcReportes()
        {
            InitializeComponent();
            InicializarComboTiempo();

            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();

            comboTiempo.SelectedIndexChanged += cbTiempoReporte_SelectedIndexChanged;

            cbMeses.SelectedIndexChanged += cbMeses_SelectedIndexChanged;
            cbAnhos.SelectedIndexChanged += cbAnhos_SelectedIndexChanged;

            InicializarFechas();

            PanelFechas.Visible = false;
            tableFechas.Visible = false;  
            panelPeriodo.Visible = false;
        }

        private void InicializarComboTiempo()
        {
            comboTiempo.Items.Clear();
            comboTiempo.Items.Add("Periodo académico");
            comboTiempo.Items.Add("Semanal");
            comboTiempo.Items.Add("Mensual");
            comboTiempo.SelectedIndex = -1;
        }

        private void UcReportes_Load(object sender, EventArgs e) { }

        private void InicializarFechas()
        {
            for (int anho = 2025; anho <= DateTime.Now.Year + 5; anho++)
                cbAnhos.Items.Add(anho.ToString());

            cbAnhos.SelectedItem = DateTime.Now.Year.ToString();

            string[] meses = { "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio",
                       "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            cbMeses.Items.AddRange(meses);
            cbMeses.SelectedIndex = 0;

            LlenarComboSemanas();
        }

        private void LlenarComboSemanas()
        {
            if (cbAnhos.SelectedItem == null || cbMeses.SelectedIndex < 0)
                return;

            int mesSeleccionado = cbMeses.SelectedIndex + 2;
            int anhoSeleccionado = int.Parse(cbAnhos.SelectedItem.ToString());

            var semanas = ObtenerSemanasMes(anhoSeleccionado, mesSeleccionado);
            cbSemanas.Items.Clear();
            cbSemanas.Items.AddRange(semanas.ToArray());

            cbSemanas.SelectedIndex = semanas.Count > 0 ? 0 : -1;
        }

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

        private void AjustarColumnasTableFechas(bool mostrarSemana)
        {
            if (tableFechas.ColumnCount != 3)
                return;

            if (mostrarSemana)
            {
                tableFechas.ColumnStyles[0].Width = 33F;
                tableFechas.ColumnStyles[1].Width = 33F;
                tableFechas.ColumnStyles[2].Width = 33F;
            }
            else
            {
                tableFechas.ColumnStyles[0].Width = 50F;
                tableFechas.ColumnStyles[1].Width = 50F;
                tableFechas.ColumnStyles[2].Width = 0F;
            }
        }

      

        private void CentrarMiniContenedor()
        {
            panelminiContenedor.Left = (this.ClientSize.Width - panelminiContenedor.Width) / 2;
            panelminiContenedor.Top = (this.ClientSize.Height - panelminiContenedor.Height) / 2;
        }


        private void cbTiempoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string seleccion = comboTiempo.SelectedItem?.ToString();

            if (seleccion == "Periodo académico")
            {
                PanelFechas.Visible = false;
                tableFechas.Visible = false;
                panelPeriodo.Visible = true;
            }
            else
            {
                PanelFechas.Visible = true;
                tableFechas.Visible = true;
                panelPeriodo.Visible = false;

                cbAnhos.Visible = true; 
                cbMeses.Visible = true; 
                lblAnho.Visible = true;
                lblMeses.Visible = true;

                if (seleccion == "Semanal")
                {
                    cbSemanas.Visible = true;   
                    lblSemanas.Visible = true;
                    AjustarColumnasTableFechas(true);
                }
                else if (seleccion == "Mensual")
                {
                    cbSemanas.Visible = false;
                    lblSemanas.Visible = false ;
                    AjustarColumnasTableFechas(false);
                }
            }

            AjustarCentradoFechas(); 
        }

        private void AjustarCentradoFechas()
        {
            cbAnhos.Anchor = AnchorStyles.None;
            cbMeses.Anchor = AnchorStyles.None;
            cbSemanas.Anchor = AnchorStyles.None;
            lblAnho.Anchor = AnchorStyles.None;
            lblMeses.Anchor = AnchorStyles.None;
            lblSemanas.Anchor = AnchorStyles.None;
        }

        private void cbAnhos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboSemanas();
        }

        private void cbMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboSemanas();
        }
    }
}
