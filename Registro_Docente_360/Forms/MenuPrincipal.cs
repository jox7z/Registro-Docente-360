using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelos.EntityFramework;
using Registro_Docente_360.Forms;
using Registro_Docente_360.ControlesUsuario;
using Registro_Docente_360.Eventos;

namespace Registro_Docente_360
{
    public partial class MenuPrincipal : Form
    {
        // Referencias a los UserControls que se usan
        UcFechas ucFechas;
        UcHorario ucHorario;
        UcAlumnos ucAlumnos;
        UcReportes ucReportes;

        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            // No se usa por ahora
        }

        bool sidebarExpand = true;

        // Controla la animación del menú lateral (expandir/contraer)
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 5;
                if (sidebar.Width <= 50)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                    panelContenedor.Padding = new Padding(50, 50, 0, 20);
                }
            }
            else
            {
                sidebar.Width += 5;
                if (sidebar.Width >= 202)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                    panelContenedor.Padding = new Padding(202, 50, 0, 20);
                }
            }
        }

        // Carga cualquier UserControl dentro del contenedor principal
        private void MostrarUserControl(UserControl control)
        {
            foreach (Control c in panelContenedor.Controls)
                c.Visible = false;

            if (!panelContenedor.Controls.Contains(control))
            {
                control.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(control);
            }

            control.Visible = true;
            control.BringToFront();
        }

        // Abre la ventana para seleccionar fechas (vista de asistencia)
        private void button1_Click(object sender, EventArgs e) //BtnAsistencia, solo que nunca se le cambio el nombre aca
        {
            if (ucFechas == null)
            {
                ucFechas = new UcFechas();
                ucFechas.OnFechaSeleccionada += UcFechas_OnFechaSeleccionada;
            }

            MostrarUserControl(ucFechas);
        }

        // Se abre cuando se elige una fecha en UcFechas
        private void UcFechas_OnFechaSeleccionada(object sender, FechaSeleccionadaEventArgs e)
        {
            var ucAsistencia = new UcVentanaAsistencia
            {
                Dock = DockStyle.Fill
            };

            ucAsistencia.ActualizarCabecera("Tomar de la ventana horario", e.Anho, e.FechaInicio, e.FechaFin);

            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(ucAsistencia);
        }

        private void formFechas_FormClosed(object sender, EventArgs e)
        {
            // Sin uso actual
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            // Sin uso actual
        }

        // Controla el botón hamburguesa (abre/cierra el menú lateral)
        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void sidebar_Paint(object sender, PaintEventArgs e) { }
        private void panelContenedor_Paint_1(object sender, PaintEventArgs e) { }

        // Abre la vista del horario
        private void btnHorario_Click(object sender, EventArgs e)
        {
            if (ucHorario == null)
                ucHorario = new UcHorario();

            MostrarUserControl(ucHorario);
        }

        // Abre la vista de alumnos
        private void btnAlumnos_Click(object sender, EventArgs e)
        {
            if (ucAlumnos == null)
                ucAlumnos = new UcAlumnos();

            MostrarUserControl(ucAlumnos);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (ucReportes == null)
                ucReportes = new UcReportes();

            MostrarUserControl(ucReportes);
        }
    }
}
