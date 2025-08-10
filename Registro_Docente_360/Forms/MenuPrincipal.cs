using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.ControlesUsuario;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Forms;
using Registro_Docente_360.Interfaces;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

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
        UcRolesyPermisos ucRolesyPermisos;
        UcAccionesUsuario ucAccionesUsuario;
        UcRegistroAccesos ucRegistroAccesos;
        UcUsuarios ucUsuarios;

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
            this.Size = new System.Drawing.Size(1440, 800);


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

        // Actualiza el estado (visibilidad, habilitación, color) de los botones del menú principal
        // según los permisos del rol actual del usuario.
        // - Si tiene permiso, el botón está habilitado y en blanco.
        // - Si no tiene, el botón está deshabilitado y en gris.
        // - Algunos botones (como btnMantenimiento) solo se muestran si corresponde.
        private bool VerificarHorarioDocente(int idDocente)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                // Comprobar si hay un horario asignado al docente
                var horarioDocente = contexto.Horarios
                                              .FirstOrDefault(h => h.id_usuario == idDocente);
                return horarioDocente != null; // Retorna true si tiene horario, false si no
            }
        }
        private void ActualizarMenuPrincipal()
        {
            // --- Inicialización: Todos los botones deshabilitados y visibles, menos btnMantenimiento ---
            btnMantenimiento.Visible = false;
            btnMantenimiento.Enabled = false;
            btnMantenimiento.ForeColor = Color.Silver;

            // Resto de botones siempre visibles, pero deshabilitados y grises por defecto
            SetButtonState(btnUsuarios, false);
            SetButtonState(btnAccionesUsuario, false);
            SetButtonState(btnAccesos, false);
            SetButtonState(btnRolyPerm, false);
            SetButtonState(btnAsistencia, false);
            SetButtonState(btnHorario, false);
            SetButtonState(btnAlumnos, false);
            SetButtonState(btnReportes, false); // Deshabilitado por defecto
            SetButtonState(btnNotas, false);
            SetButtonState(btnCalendario, false);
            SetButtonState(btnAboutUs, false);
            SetButtonState(btnConfiguracion, false);


            // --- Cargar los permisos del rol actual desde la base de datos ---
            AlumnoController.CargarPermisosRolActual(Sesion.IdRol);

            // --- Mostrar btnMantenimiento SOLO si tiene admin, usuarios o bitácoras ---
            bool puedeVerMantenimiento =
                AlumnoController.PermisosRolActual.Contains(2) || // Admin
                AlumnoController.PermisosRolActual.Contains(3) || // Modificar Usuarios
                AlumnoController.PermisosRolActual.Contains(6);   // Bitácoras

            btnMantenimiento.Visible = puedeVerMantenimiento;
            btnMantenimiento.Enabled = puedeVerMantenimiento;
            btnMantenimiento.ForeColor = puedeVerMantenimiento ? Color.White : Color.Silver;

            // --- USUARIOS: Permiso de admin o modificar usuarios ---
            SetButtonState(btnUsuarios,
                AlumnoController.PermisosRolActual.Contains(2) ||
                AlumnoController.PermisosRolActual.Contains(3)
            );

            // --- ROLES Y PERMISOS: Solo admin ---
            SetButtonState(btnRolyPerm,
                AlumnoController.PermisosRolActual.Contains(2)
            );

            // --- BITÁCORAS: Admin o bitácoras ---
            SetButtonState(btnAccionesUsuario,
                AlumnoController.PermisosRolActual.Contains(2) ||
                AlumnoController.PermisosRolActual.Contains(6)
            );
            SetButtonState(btnAccesos,
                AlumnoController.PermisosRolActual.Contains(2) ||
                AlumnoController.PermisosRolActual.Contains(6)
            );

            // --- MÓDULO DOCENTE: Permiso de docente ---
            if (AlumnoController.PermisosRolActual.Contains(1))
            {
                SetButtonState(btnAsistencia, true);
                SetButtonState(btnHorario, true);
                SetButtonState(btnAlumnos, true);
                SetButtonState(btnNotas, true);
                SetButtonState(btnCalendario, true);
                SetButtonState(btnAboutUs, true);
            }

            // --- REPORTES: Solo visible y habilitado para Docente (1) y Admin (2) ---
            SetButtonState(btnReportes,
                AlumnoController.PermisosRolActual.Contains(1) ||  // Permiso para Docente
                AlumnoController.PermisosRolActual.Contains(2)    // Permiso para Admin
            );

            // --- CONFIGURACIÓN: Permiso de docente, admin o explícito de configuración ---
            SetButtonState(btnConfiguracion,
                AlumnoController.PermisosRolActual.Contains(1) ||
                AlumnoController.PermisosRolActual.Contains(2) ||
                AlumnoController.PermisosRolActual.Contains(5)
            );
        }

        // Helper universal: todos los botones se muestran pero se habilitan/cambian de color si tienen permiso.
        // enabled = true  -> botón habilitado y texto blanco
        // enabled = false -> botón deshabilitado y texto gris
        private void SetButtonState(Button btn, bool enabled)
        {
            btn.Visible = true;
            btn.Enabled = enabled;
            btn.ForeColor = enabled ? Color.White : Color.Silver;
        }



        // ========================
        // EVENTO LOAD DEL FORM
        // ========================

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            AlumnoController.CargarPermisosRolActual(Sesion.IdRol);

            ActualizarMenuPrincipal();

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
            // Ocultar el panel de mantenimiento al cambiar de vista
            pnMantenimiento.Visible = false;

            foreach (Control c in panelContenedor.Controls)
            {
                if (c is IModoEdicion editable && editable.EstaEnModoEdicion)
                {
                    MessageBox.Show("Debes salir del modo edición antes de cambiar de pestaña.",
                                   "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                c.Visible = false;
            }

            if (!panelContenedor.Controls.Contains(control))
            {
                control.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(control);
            }

            control.Visible = true;
            control.BringToFront();

            // Asegurar que el panel de mantenimiento esté siempre disponible
            pnMantenimiento.BringToFront();
        }



        // ========================
        // EVENTOS DE BOTONES DEL MENÚ 
        // MODULO DOCENTE
        // ========================
        private void btnAsistencia_Click(object sender, EventArgs e) // BtnAsistencia (nombre sin cambiar)
        {
            // Asegurarse de que el panel de opciones siempre esté al frente
            pnMantenimiento.BringToFront();

            // Verificar si el usuario es un administrador
            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            // Si no es administrador, verificar si tiene horario asignado
            if (!esAdministrador)
            {
                bool tieneHorario = VerificarHorarioDocente(Sesion.IdUsuario);  // Llamada a una función que verifica si el docente tiene horario

                if (!tieneHorario)
                {
                    // Si el docente no tiene horario asignado, mostrar mensaje
                    MessageBox.Show("Primero crea un horario para acceder a los alumnos",
                                     "Error de horario",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                    return; // Detener la ejecución del código si no tiene horario
                }
            }

            bool ExisteDocentes = ExistenDocentes();
            if (!ExisteDocentes)
            {
                // Si no existen docentes, mostrar mensaje
                MessageBox.Show("Primero debes ingresar un docente.",
                                 "Error de docente",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return; // Detener la ejecución del código si no existe el docente
            }

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


            foreach (Control control in panelContenedor.Controls)
            {
                control.Visible = false;
            }

            // Agregar/mostrar el nuevo
            if (!panelContenedor.Controls.Contains(ucAsistencia))
            {
                panelContenedor.Controls.Add(ucAsistencia);
            }

            ucAsistencia.Visible = true;
            ucAsistencia.BringToFront();

        }


        private void btnHorario_Click(object sender, EventArgs e)
        {
       
            bool ExisteDocentes = ExistenDocentes();
            if (!ExisteDocentes)
            {
                // Si no existen docentes, mostrar mensaje
                MessageBox.Show("Primero debes ingresar un docente.",
                                 "Error de docente",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return; // Detener la ejecución del código si no existe el docente
            }

            if (ucHorario == null)
                ucHorario = new UcHorario();

            MostrarUserControl(ucHorario);
        }

        private void btnAlumnos_Click(object sender, EventArgs e)
        {
            
            // Verificar si el usuario es un administrador
            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            // Si no es administrador, verificar si tiene horario asignado
            if (!esAdministrador)
            {
                bool tieneHorario = VerificarHorarioDocente(Sesion.IdUsuario);  // Llamada a una función que verifica si el docente tiene horario

                if (!tieneHorario)
                {
                    // Si el docente no tiene horario asignado, mostrar mensaje
                    MessageBox.Show("Primero crea un horario para acceder a los alumnos",
                                     "Error de horario",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                    return; // Detener la ejecución del código si no tiene horario
                }
            }

            bool ExisteDocentes = ExistenDocentes();
            if (!ExisteDocentes)
            {
                // Si no existen docentes, mostrar mensaje
                MessageBox.Show("Primero debes ingresar un docente.",
                                 "Error de docente",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return; // Detener la ejecución del código si no existe el docente
            }
            if (ucAlumnos == null)
                ucAlumnos = new UcAlumnos();
            MostrarUserControl(ucAlumnos);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            // Verificar si el usuario es un administrador
            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            // Si no es administrador, verificar si tiene horario asignado
            if (!esAdministrador)
            {
                bool tieneHorario = VerificarHorarioDocente(Sesion.IdUsuario);  // Llamada a una función que verifica si el docente tiene horario

                if (!tieneHorario)
                {
                    // Si el docente no tiene horario asignado, mostrar mensaje
                    MessageBox.Show("Primero crea un horario para acceder a los alumnos",
                                     "Error de horario",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                    return; // Detener la ejecución del código si no tiene horario
                }
            }

            bool ExisteDocentes = ExistenDocentes();
            if (!ExisteDocentes)
            {
                // Si no existen docentes, mostrar mensaje
                MessageBox.Show("Primero debes ingresar un docente.",
                                 "Error de docente",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return; // Detener la ejecución del código si no existe el docente
            }
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
            // Verificar si el usuario es un administrador
            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            // Si no es administrador, verificar si tiene horario asignado
            if (!esAdministrador)
            {
                bool tieneHorario = VerificarHorarioDocente(Sesion.IdUsuario);  // Llamada a una función que verifica si el docente tiene horario

                if (!tieneHorario)
                {
                    // Si el docente no tiene horario asignado, mostrar mensaje
                    MessageBox.Show("Primero crea un horario para acceder a los alumnos",
                                     "Error de horario",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                    return; // Detener la ejecución del código si no tiene horario
                }
            }

            bool ExisteDocentes = ExistenDocentes();
            if (!ExisteDocentes)
            {
                // Si no existen docentes, mostrar mensaje
                MessageBox.Show("Primero debes ingresar un docente.",
                                 "Error de docente",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return; // Detener la ejecución del código si no existe el docente
            }
            if (ucNotas == null)
                ucNotas = new UcNotas();

            MostrarUserControl(ucNotas);
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            // Verificar permisos antes de continuar
            if (!AlumnoController.VerificarSiAccedeConfig(Sesion.IdUsuario))
            {
                MessageBox.Show("No tienes permisos para acceder a esta configuración.",
                               "Acceso denegado",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return; // Salir del método si no tiene permisos
            }

            if (ucConfiguracion == null)
            {
                ucConfiguracion = new UcConfiguracion();
                ucConfiguracion.OnSolicitarCambioContrasena += UcConfiguracion_OnSolicitarCambioContrasena;
                ucConfiguracion.OnSolicitarInfoCuenta += UcConfiguracion_OnSolicitarInfoCuenta;
            }

            MostrarUserControl(ucConfiguracion);
        }


        //MODULO DE ADMINISTRADOR

        Panel p = new Panel();

        private void btnMantenimiento_MouseEnter(object sender, EventArgs e)
        {
            paneltop.Controls.Add(p);
            p.BackColor = Color.White;
            p.Size = new System.Drawing.Size(166, 26);
            p.Location = new
                System.Drawing.Point(btnMantenimiento.Location.X, btnMantenimiento.Location.Y + 26);
        }

        private void btnMantenimiento_MouseLeave(object sender, EventArgs e)
        {
            paneltop.Controls.Remove(p);

        }

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            // Forzar que el panel de mantenimiento esté siempre encima
            pnMantenimiento.BringToFront();

            pnMantenimiento.Visible = !pnMantenimiento.Visible;

            // Si se está mostrando, asegurarse que esté encima de todo
            if (pnMantenimiento.Visible)
            {
                pnMantenimiento.BringToFront();
            }
        }

        private void btnRolyPerm_Click(object sender, EventArgs e)
        {
            if (ucRolesyPermisos == null)
                ucRolesyPermisos = new UcRolesyPermisos();

            MostrarUserControl(ucRolesyPermisos);
        }

        private void btnAccesos_Click(object sender, EventArgs e)
        {
            // Validar permiso para bitácoras
            if (!AlumnoController.VerificarSiAccedeBitacoras(Sesion.IdUsuario))
            {
                MessageBox.Show("No tienes permiso para acceder a las bitácoras",
                               "Acceso denegado",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            if (ucRegistroAccesos == null)
                ucRegistroAccesos = new UcRegistroAccesos();

            MostrarUserControl(ucRegistroAccesos);
        }

        private void btnAccionesUsuario_Click(object sender, EventArgs e)
        {
            // Validar permiso para bitácoras (misma validación que el anterior)
            if (!AlumnoController.VerificarSiAccedeBitacoras(Sesion.IdUsuario))
            {
                MessageBox.Show("No tienes permiso para acceder a las bitácoras",
                               "Acceso denegado",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            if (ucAccionesUsuario == null)
                ucAccionesUsuario = new UcAccionesUsuario();

            MostrarUserControl(ucAccionesUsuario);
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            // Validar permiso para modificar usuarios
            if (!AlumnoController.VerificarSiModificaUsuario(Sesion.IdUsuario))
            {
                MessageBox.Show("No tienes permiso para modificar usuarios",
                               "Acceso denegado",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            if (ucUsuarios == null)
                ucUsuarios = new UcUsuarios();

            MostrarUserControl(ucUsuarios);
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

        private bool ExistenDocentes()
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                // Verificar si existe al menos un usuario cuyo rol tenga el permiso de docente (id_permiso == 1)
                // y asegurarse de que el usuario no tenga el permiso de administrador (id_permiso == 2)
                return contexto.Roles_Permisos
                .Any(rp => rp.id_permiso == 1 && contexto.Usuarios
                .Any(u => u.id_rol == rp.id_rol && // Verificar si el usuario tiene el rol asociado al permiso de docente
                !contexto.Roles_Permisos.Any(rpAdmin => rpAdmin.id_rol == u.id_rol && rpAdmin.id_permiso == 2))); // Excluir usuarios con el permiso de administrador
            }
        }


    }
}
