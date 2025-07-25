using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using Registro_Docente_360.ControlesUsuario;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Forms;
using System.Runtime.InteropServices;

namespace Registro_Docente_360
{
    public partial class MenuPrincipal : Form
    {
        // ========================
        // CAMPOS Y REFERENCIAS
        // ========================
        public UcFechas ucFechas;
        UcHorario ucHorario;
        UcAlumnos ucAlumnos;
        UcReportes ucReportes;
        UcAcercaDe ucAcercaDe;
        UcCalendario ucCalendario;
        UcNotas ucNotas;
        UcConfiguracion ucConfiguracion;
        UcCambiarContra ucCambiarContra;
        UcInfoCuenta ucInfoCuenta;
        UcBienvenida ucBienvenida;
        BotonAyuda botonAyuda;
        UcVentanaAsistencia ucAsistencia;
        bool sidebarExpand = true;

        // ======================
        // API para mover ventana
        // ======================
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        // ========================
        // CONSTRUCTOR
        // ========================
        public MenuPrincipal()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.Size = new System.Drawing.Size(1280, 720);

            botonAyuda = new BotonAyuda();
            botonAyuda.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(botonAyuda);
            botonAyuda.BringToFront();
            botonAyuda.Location = new System.Drawing.Point(
                this.ClientSize.Width - botonAyuda.Width - 20,
                this.ClientSize.Height - botonAyuda.Height - 20
            );

            this.Resize += (s, e) =>
            {
                botonAyuda.Location = new System.Drawing.Point(
                    this.ClientSize.Width - botonAyuda.Width - 20,
                    this.ClientSize.Height - botonAyuda.Height - 20
                );
            };

            AjustarPaddingContenedor();

            // Cargar bienvenida al iniciar
            ucBienvenida = new UcBienvenida();
            MostrarUserControl(ucBienvenida);
            ucBienvenida.OnVerHorario += AbrirHorarioDesdeBienvenida;
        }


        private void AjustarPaddingContenedor()
        {
            if (sidebar.Width <= 50)
                panelContenedor.Padding = new Padding(50, 50, 0, 20);
            else 
                panelContenedor.Padding = new Padding(202,50,0,20);
        }


        // ========================
        // EVENTO LOAD DEL FORM
        // ========================
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            if (Sesion.IdRol == 1) //admin
            {

            }

            else if (Sesion.IdRol == 2)//docente
            {

            }
        }

        // ========================
        // MÉTODOS DE UI - SIDEBAR
        // ========================
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

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        // ========================
        // MÉTODOS DE NAVEGACIÓN
        // ========================

       

        public void MostrarUserControl(UserControl control)
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

     

        // ========================
        // EVENTOS DE BOTONES DEL MENÚ
        // ========================
        private void button1_Click(object sender, EventArgs e) // BtnAsistencia (nombre sin cambiar)
        {
            if (ucFechas == null)
            {
                ucFechas = new UcFechas();
                ucFechas.OnFechaSeleccionada += UcFechas_OnFechaSeleccionada;
            }

            MostrarUserControl(ucFechas);
        }

        public void UcFechas_OnFechaSeleccionada(object sender, FechaSeleccionadaEventArgs e)
        {
            var ucAsistencia = new UcVentanaAsistencia
            {
                Dock = DockStyle.Fill
            };

            if (ucAsistencia == null)
            {
                ucAsistencia = new UcVentanaAsistencia();
            }


            // Actualizar la cabecera
            ucAsistencia.ActualizarCabecera(e.Anho, e.FechaInicio, e.FechaFin);


            // Mostrar control
            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(ucAsistencia);

        }


        private void btnHorario_Click(object sender, EventArgs e)
        {
            if (ucHorario == null)
                ucHorario = new UcHorario();

            MostrarUserControl(ucHorario);
        }

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

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            if (ucAcercaDe == null)
                ucAcercaDe = new UcAcercaDe();

            MostrarUserControl(ucAcercaDe);
        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            if (ucCalendario == null)
                ucCalendario = new UcCalendario();

            MostrarUserControl(ucCalendario);
        }

        private void btnNotas_Click(object sender, EventArgs e)
        {
            if (ucNotas == null)
                ucNotas = new UcNotas();

            MostrarUserControl(ucNotas);
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            if (ucConfiguracion == null)
            {
                ucConfiguracion = new UcConfiguracion();
                ucConfiguracion.OnSolicitarCambioContrasena += UcConfiguracion_OnSolicitarCambioContrasena;
                ucConfiguracion.OnSolicitarInfoCuenta += UcConfiguracion_OnSolicitarInfoCuenta;
            }

            MostrarUserControl(ucConfiguracion);
        }
        private void UcConfiguracion_OnSolicitarCambioContrasena(object sender, EventArgs e)
        {
            ucCambiarContra = new UcCambiarContra();

            // Suscribirse al evento de regreso
            ucCambiarContra.OnVolverAConfiguracion += (s2, e2) =>
            {
                if (ucConfiguracion == null)
                {
                    ucConfiguracion = new UcConfiguracion();
                    ucConfiguracion.OnSolicitarCambioContrasena += UcConfiguracion_OnSolicitarCambioContrasena;
                    ucConfiguracion.OnSolicitarInfoCuenta += UcConfiguracion_OnSolicitarInfoCuenta;
                }

                MostrarUserControl(ucConfiguracion);
            };

            MostrarUserControl(ucCambiarContra);
        }


        private void UcConfiguracion_OnSolicitarInfoCuenta(object sender, EventArgs e)
        {
            ucInfoCuenta = new UcInfoCuenta();

            ucInfoCuenta.OnVolverAConfiguracion += (s2, e2) =>
            {
                if (ucConfiguracion == null)
                {
                    ucConfiguracion = new UcConfiguracion();
                    ucConfiguracion.OnSolicitarCambioContrasena += UcConfiguracion_OnSolicitarCambioContrasena;
                    ucConfiguracion.OnSolicitarInfoCuenta += UcConfiguracion_OnSolicitarInfoCuenta;
                }

                MostrarUserControl(ucConfiguracion);
            };

            MostrarUserControl(ucInfoCuenta);
        }

        private void CargarHorario()
        {
            var horario = new UcHorario();
            horario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(horario);
        }




        private void AbrirHorarioDesdeBienvenida(object sender, EventArgs e)
        {
            var ucHorario = new UcHorario();
            ucHorario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(ucHorario);
        }



        // ========================
        // EVENTOS DE OTROS COMPONENTES
        // ========================
        private void pictureAyuda_Click(object sender, EventArgs e)
        {
            // aquí va lo que vaya a cargar en Ayuda
        }

        private void paneltop_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void formFechas_FormClosed(object sender, EventArgs e){ }

        private void paneltop_Paint(object sender, PaintEventArgs e) { }

        private void sidebar_Paint(object sender, PaintEventArgs e) { }

        private void panelContenedor_Paint_1(object sender, PaintEventArgs e) { }

        
    }
}
