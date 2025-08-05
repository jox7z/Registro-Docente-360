using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Interfaces;
using Registro_Docente_360.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcNotas : UserControl, IModoEdicion
    {
        private bool modoEdicion = false;
        private ToolTip tooltipNotas = new ToolTip();
        private bool huboCambios = false;
        private decimal valorTareas = 0;
        private decimal valorAsistencia = 0;
        private decimal valorCotidiano = 0;
        private Dictionary<string, decimal> tareasPorEstudiante = new Dictionary<string, decimal>();
        private Dictionary<string, decimal> asistenciaPorEstudiante = new Dictionary<string, decimal>();
        private Dictionary<string, decimal> cotidianoPorEstudiante = new Dictionary<string, decimal>();
        public bool EstaEnModoEdicion => modoEdicion;

        public void CancelarModoEdicion()
        {
            btnCancelar.PerformClick(); 
        }

        public UcNotas()
        {
            InitializeComponent();
            this.VisibleChanged += UcNotas_VisibleChanged;
            this.Load += UcNotas_Load;



        }


        private void UcNotas_Load(object sender, EventArgs e)
        {
            tablaNotas.Grid.CellEndEdit += Grid_CellEndEdit;
            tablaNotas.Grid.EditingControlShowing += Grid_EditingControlShowing;

            tablaNotas.Grid.Columns.Clear();

            tablaNotas.Grid.Columns.Add("colCedula", "Cédula");
            tablaNotas.Grid.Columns.Add("colNombre", "Nombre");
            tablaNotas.Grid.Columns.Add("colPrimerExamen", "Primer Examen 10%");
            tablaNotas.Grid.Columns.Add("colSegundoExamen", "Segundo Examen 10%");
            tablaNotas.Grid.Columns.Add("colTareas", "Tareas 10%");
            tablaNotas.Grid.Columns.Add("colAsistencia", "Asistencia 10%");
            tablaNotas.Grid.Columns.Add("colCotidiano", "Cotidiano 60%");
            tablaNotas.Grid.Columns.Add("colNotaFinal", "Nota Final");

            // Solo lectura por defecto
            tablaNotas.Grid.ReadOnly = true;
            tablaNotas.Grid.AllowUserToAddRows = false;
            tablaNotas.Grid.AllowUserToDeleteRows = false;

            tablaNotas.Grid.Columns["colCedula"].ReadOnly = true;
            tablaNotas.Grid.Columns["colNombre"].ReadOnly = true;
            tablaNotas.Grid.Columns["colNotaFinal"].ReadOnly = true;

            // Color visual opcional para distinguir nota final
            tablaNotas.Grid.Columns["colNotaFinal"].DefaultCellStyle.BackColor = Color.LightGray;

            // Ocultar acciones hasta que se active el botón
            PanelAcciones.Visible = false;

            using(var contexto = new RegistroDocenteEntities())
{
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);
                var rol = contexto.Roles.FirstOrDefault(r => r.id_rol == usuario.id_rol);

                if (rol != null && rol.nombre_rol == "Administrador")
                {
                    // Admin: puede elegir un docente
                    cmbDocentes.Visible = true;
                    lblNomDocente.Visible = false;
                    lblSecc.Visible = false;
                    lblSeccion.Text = "Materia: ";

                    var docentes = contexto.Usuarios
                        .Where(u => u.Roles.nombre_rol == "Docente")
                        .Select(u => new
                        {
                            u.id_usuario,
                            NombreCompleto = u.nombre_usuario + " " + u.apellido_usuario
                        })
                        .ToList();

                    cmbDocentes.DisplayMember = "NombreCompleto";
                    cmbDocentes.ValueMember = "id_usuario";
                    cmbDocentes.DataSource = docentes;

                    cmbDocentes.SelectedIndexChanged += CmbDocentes_SelectedIndexChanged;

                    if (cmbDocentes.Items.Count > 0)
                        cmbDocentes.SelectedIndex = 0; // Carga automática
                }
                else
                {
                    // Docente: solo su propia sección y nombre
                    cmbDocentes.Visible = false;

                    lblNomDocente.Visible = true;
                    lblSecc.Visible = true;

                    lblNomDocente.Text = usuario.nombre_usuario;

                    var seccion = contexto.Secciones.FirstOrDefault(s => s.id_seccion == usuario.id_seccion);
                    lblSecc.Text = seccion?.nombre_seccion ?? "Sin sección";

                    CargarNotasDocente(usuario.id_usuario); // método que vos definís para cargar las notas
                }
            }

        }

        private void btnGestionarNotas_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                lblNotas.Text = "MODO GESTIÓN ACTIVADO";
                lblNotas.ForeColor = Color.Black;

                tablaNotas.Grid.ReadOnly = false;

                foreach (DataGridViewColumn col in tablaNotas.Grid.Columns)
                {
                    // Solo permitir edición en estas columnas
                    if (col.Name == "colPrimerExamen" ||
                        col.Name == "colSegundoExamen" ||
                        col.Name == "colCotidiano" ||
                        col.Name == "colTareas" ||
                        col.Name == "colAsistencia")
                        col.ReadOnly = false;
                    else
                        col.ReadOnly = true;
                }

                btnGestionarNotas.Text = "Terminar Edición";
                tooltipNotas.SetToolTip(btnGestionarNotas, "Haz clic para terminar la edición");
                PanelAcciones.Visible = true;
                modoEdicion = true;
            }
            else
            {
                if (huboCambios)
                {
                    var confirm = MessageBox.Show(
                        "Hay cambios no guardados. ¿Estás seguro de que deseas salir del modo edición y perder los cambios?",
                        "Confirmar salida",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirm == DialogResult.No)
                    {
                        return; // No salir
                    }
                }

                // Restablecer modo visual
                lblNotas.Text = "Listado de Notas";
                lblNotas.ForeColor = Color.Teal;

                tablaNotas.Grid.ReadOnly = true;
                foreach (DataGridViewColumn col in tablaNotas.Grid.Columns)
                    col.ReadOnly = true;

                btnGestionarNotas.Text = "Gestionar Notas";
                tooltipNotas.SetToolTip(btnGestionarNotas, "Haz clic para editar las notas");

                PanelAcciones.Visible = false;
                modoEdicion = false;
                huboCambios = false;

                // Recargar datos originales
                tablaNotas.Grid.Rows.Clear();
                if (cmbMateria.SelectedItem is Materias materiaSeleccionada)
                {
                    cmbMateria_SelectedIndexChanged(null, null);
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (huboCambios)
            {
                var confirm = MessageBox.Show("Hay cambios no guardados. ¿Estás seguro de que deseas cancelar la edición y perder los cambios?",
                                              "Confirmar cancelación",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                if (confirm == DialogResult.No)
                {
                    return; // El usuario decidió quedarse en modo edición
                }
            }

            // Restaurar estado visual
            lblNotas.Text = "Listado de Notas";
            lblNotas.ForeColor = Color.Teal;

            tablaNotas.Grid.ReadOnly = true;
            foreach (DataGridViewColumn col in tablaNotas.Grid.Columns)
            {
                col.ReadOnly = true;
            }

            btnGestionarNotas.Text = "Gestionar Notas";
            tooltipNotas.SetToolTip(btnGestionarNotas, "Haz clic para editar las notas");

            PanelAcciones.Visible = false;
            modoEdicion = false;
            huboCambios = false;

            tablaNotas.Grid.Rows.Clear();

            // Recargar datos originales desde la base
            if (cmbMateria.SelectedItem is Materias materiaSeleccionada)
            {
                cmbMateria_SelectedIndexChanged(null, null); // vuelve a cargar los datos para la materia actual
            }
        }


        private void cmbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMateria.SelectedItem is Materias materiaSeleccionada)
            {
                tablaNotas.Grid.Rows.Clear();

                using (var contexto = new RegistroDocenteEntities())
                {
                    int idDocente = Sesion.IdUsuario; // por defecto es el usuario actual

                    // Si es admin y hay un docente seleccionado, usar ese ID
                    if (cmbDocentes.Visible && cmbDocentes.SelectedItem != null)
                    {
                        idDocente = (int)cmbDocentes.SelectedValue;
                    }

                    var clases = (from c in contexto.Clases
                                  join est in contexto.Estudiantes on c.id_estudiante equals est.id_estudiante
                                  where c.id_usuario == idDocente
                                  && c.id_materia == materiaSeleccionada.id_materia
                                  select new
                                  {
                                      Clase = c,
                                      Estudiante = est,
                                      Nota = contexto.Notas.FirstOrDefault(n => n.id_clase == c.id_clase)
                                  }).ToList();

                    foreach (var item in clases)
                    {
                        string cedula = item.Estudiante.cedula_estudiante;

                        decimal examen1 = item.Nota?.primer_examen ?? 0;
                        decimal examen2 = item.Nota?.segundo_examen ?? 0;
                        decimal tareas = item.Nota?.tareas ?? 0;
                        decimal asistencia = item.Nota?.asistencia ?? 0;
                        decimal cotidiano = item.Nota?.cotidiano ?? 0;

                        tareasPorEstudiante[cedula] = tareas;
                        asistenciaPorEstudiante[cedula] = asistencia;
                        cotidianoPorEstudiante[cedula] = cotidiano;

                        decimal notaFinal = examen1 + examen2 + tareas + asistencia + cotidiano;

                        tablaNotas.Grid.Rows.Add(
                            cedula,
                            $"{item.Estudiante.nombre_estudiante} {item.Estudiante.primer_apellido}",
                            examen1.ToString("0.##"),
                            examen2.ToString("0.##"),
                            tareas.ToString("0.##"),
                            asistencia.ToString("0.##"),
                            cotidiano.ToString("0.##"),
                            notaFinal.ToString("0.##")
                        );
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var materiaSeleccionada = cmbMateria.SelectedItem as Materias;
            if (materiaSeleccionada == null) return;

            using (var contexto = new RegistroDocenteEntities())
            {
                foreach (DataGridViewRow fila in tablaNotas.Grid.Rows)
                {
                    if (fila.IsNewRow) continue;

                    string cedula = fila.Cells["colCedula"].Value?.ToString();
                    var estudiante = contexto.Estudiantes.FirstOrDefault(x => x.cedula_estudiante == cedula);
                    if (estudiante == null) continue;

                    int idDocente = Sesion.IdUsuario;
                    if (cmbDocentes.Visible && cmbDocentes.SelectedItem != null)
                    {
                        idDocente = (int)cmbDocentes.SelectedValue;
                    }

                    var clase = contexto.Clases.FirstOrDefault(c =>
                        c.id_usuario == idDocente &&
                        c.id_estudiante == estudiante.id_estudiante &&
                        c.id_materia == materiaSeleccionada.id_materia);



                    if (clase == null) continue;

                    decimal.TryParse(fila.Cells["colPrimerExamen"].Value?.ToString(), out decimal examen1);
                    decimal.TryParse(fila.Cells["colSegundoExamen"].Value?.ToString(), out decimal examen2);
                    decimal.TryParse(fila.Cells["colTareas"].Value?.ToString(), out decimal tareas);
                    decimal.TryParse(fila.Cells["colAsistencia"].Value?.ToString(), out decimal asistencia);
                    decimal.TryParse(fila.Cells["colCotidiano"].Value?.ToString(), out decimal cotidiano);

                    decimal notaFinal = examen1 + examen2 + tareas + asistencia + cotidiano;

                    var notaExistente = contexto.Notas.FirstOrDefault(n => n.id_clase == clase.id_clase);

                    if (notaExistente != null)
                    {
                        // Actualizar la nota existente
                        notaExistente.primer_examen = examen1;
                        notaExistente.segundo_examen = examen2;
                        notaExistente.tareas = tareas;
                        notaExistente.asistencia = asistencia;
                        notaExistente.cotidiano = cotidiano;
                        notaExistente.nota_final = notaFinal;

                        string descripcion = $"Se actualizo la nota del estudiante: {estudiante.nombre_estudiante} {estudiante.primer_apellido}";
                        string accion = "Actualizar nota";
                        string modulo = "Notas";

                        AlumnoController controlador = new AlumnoController();
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
                    }
                    else
                    {
                        // Insertar nueva nota
                        contexto.Notas.Add(new Notas
                        {
                            id_clase = clase.id_clase,
                            primer_examen = examen1,
                            segundo_examen = examen2,
                            asistencia = asistencia,
                            tareas = tareas,
                            cotidiano = cotidiano,
                            nota_final = notaFinal


                        });

                        string descripcion = $"Se agrego la nota del estudiante: {estudiante.nombre_estudiante} {estudiante.primer_apellido}";
                        string accion = "Nueva nota";
                        string modulo = "Notas";

                        AlumnoController controlador = new AlumnoController();
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
                    }
                }

                contexto.SaveChanges();
                MessageBox.Show("Notas guardadas exitosamente");

                
            }

            huboCambios = false;
        }


        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            huboCambios = true;

            var fila = tablaNotas.Grid.Rows[e.RowIndex];
            var columna = tablaNotas.Grid.Columns[e.ColumnIndex].Name;
            string cedula = fila.Cells["colCedula"].Value?.ToString();

            // Validaciones de rango
            if (columna == "colPrimerExamen" || columna == "colSegundoExamen" || columna == "colTareas")
            {
                if (decimal.TryParse(fila.Cells[columna].Value?.ToString(), out decimal valor) && (valor < 0 || valor > 10))
                {
                    MessageBox.Show("El valor debe estar entre 0 y 10.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fila.Cells[columna].Value = 0;
                }
            }
            else if (columna == "colAsistencia")
            {
                if (decimal.TryParse(fila.Cells[columna].Value?.ToString(), out decimal valor) && (valor < 0 || valor > 10))
                {
                    MessageBox.Show("El valor debe estar entre 0 y 10.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fila.Cells[columna].Value = 0;
                }
            }
            else if (columna == "colCotidiano")
            {
                if (decimal.TryParse(fila.Cells[columna].Value?.ToString(), out decimal valor) && (valor < 0 || valor > 60))
                {
                    MessageBox.Show("El valor debe estar entre 0 y 60.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fila.Cells[columna].Value = 0;
                }
            }

            // Actualizar valores por estudiante
            if (!string.IsNullOrWhiteSpace(cedula))
            {
                if (decimal.TryParse(fila.Cells["colTareas"].Value?.ToString(), out decimal t))
                    tareasPorEstudiante[cedula] = t;

                if (decimal.TryParse(fila.Cells["colAsistencia"].Value?.ToString(), out decimal a))
                    asistenciaPorEstudiante[cedula] = a;

                if (decimal.TryParse(fila.Cells["colCotidiano"].Value?.ToString(), out decimal c))
                    cotidianoPorEstudiante[cedula] = c;
            }

            // Recalcular nota final
            decimal.TryParse(fila.Cells["colPrimerExamen"].Value?.ToString(), out decimal examen1);
            decimal.TryParse(fila.Cells["colSegundoExamen"].Value?.ToString(), out decimal examen2);
            decimal.TryParse(fila.Cells["colTareas"].Value?.ToString(), out decimal tareas);
            decimal.TryParse(fila.Cells["colAsistencia"].Value?.ToString(), out decimal asistencia);
            decimal.TryParse(fila.Cells["colCotidiano"].Value?.ToString(), out decimal cotidiano);

            decimal notaFinal = examen1 + examen2 + tareas + asistencia + cotidiano;
            fila.Cells["colNotaFinal"].Value = Math.Round(notaFinal, 2);
        }




        private void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.KeyPress -= TextBox_KeyPressSoloNumeros;
                textBox.KeyPress += TextBox_KeyPressSoloNumeros;
            }
        }

        private void TextBox_KeyPressSoloNumeros(object sender, KeyPressEventArgs e)
        {
            // Solo números, backspace y punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Solo un punto decimal permitido
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void UcNotas_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && cmbMateria.SelectedItem != null)
            {
                // Fuerza la recarga de estudiantes y notas
                cmbMateria_SelectedIndexChanged(cmbMateria, EventArgs.Empty);
            }
        }

        // Evento que se ejecuta al hacer clic en el botón "Exportar"
        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Obtener datos del formulario
            string nombreDocente = lblNomDocente.Text;
            string seccion = lblSecc.Text;
            string materia = cmbMateria.Text;

            // Mostrar cuadro para guardar el archivo
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"Notas_{nombreDocente.Replace(" ", "_")}_{materia}.pdf"
            };

            // Si el usuario confirma guardar
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Exportar el contenido a PDF
                ExportadorPDF.Exportar(tablaNotas.Grid, nombreDocente, seccion, materia, sfd.FileName);

                // Confirmación
                MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el archivo automáticamente
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void CmbDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocentes.SelectedItem != null)
            {
                int idDocenteSeleccionado = (int)cmbDocentes.SelectedValue;
                CargarNotasDocente(idDocenteSeleccionado);
            }
        }



        private void CargarNotasDocente(int idDocente)
        {
            tablaNotas.Grid.Rows.Clear();

            using (var contexto = new RegistroDocenteEntities())
            {
                var materias = (from h in contexto.Horarios
                                join m in contexto.Materias on h.id_materia equals m.id_materia
                                where h.id_usuario == idDocente
                                select m).Distinct().ToList();

                cmbMateria.DataSource = materias;
                cmbMateria.DisplayMember = "nombre_materia";
                cmbMateria.ValueMember = "id_materia";

                if (cmbMateria.Items.Count > 0)
                    cmbMateria_SelectedIndexChanged(null, null); // recarga las notas
            }
        }


    }
}
