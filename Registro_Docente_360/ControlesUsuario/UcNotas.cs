using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Interfaces;
using Registro_Docente_360.Utilidades;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcNotas : UserControl, IModoEdicion
    {
        private bool modoEdicion = false;
        private ToolTip tooltipNotas = new ToolTip();
        private bool huboCambios = false;

        private string periodoAnterior;
        private Materias materiaAnterior;
        private bool evitandoEvento = false;

        private bool alertaMostrada = false;

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

            // Todos ReadOnly por defecto
            tablaNotas.Grid.ReadOnly = true;
            tablaNotas.Grid.AllowUserToAddRows = false;
            tablaNotas.Grid.AllowUserToDeleteRows = false;
            tablaNotas.Grid.Columns["colCedula"].ReadOnly = true;
            tablaNotas.Grid.Columns["colNombre"].ReadOnly = true;
            tablaNotas.Grid.Columns["colNotaFinal"].ReadOnly = true;
            tablaNotas.Grid.Columns["colAsistencia"].ReadOnly = true; // 👈 SIEMPRE solo lectura
            tablaNotas.Grid.Columns["colNotaFinal"].DefaultCellStyle.BackColor = Color.LightGray;
            tablaNotas.Grid.CellValidating += Grid_CellValidating;

            cmbPeriodo.Items.Clear();
            cmbPeriodo.Items.Add("Primer Periodo");
            cmbPeriodo.Items.Add("Segundo Periodo");
            cmbPeriodo.SelectedIndex = 0;  // Asignamos "Primer Periodo" como valor por defecto
            cmbPeriodo.SelectedIndexChanged += CmbPeriodo_SelectedIndexChanged;
            PanelAcciones.Visible = false;

            // Inicializar variables de control
            huboCambios = false;
            evitandoEvento = false;
            periodoAnterior = cmbPeriodo.SelectedItem?.ToString();
            materiaAnterior = cmbMateria.SelectedItem as Materias;

            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);

                if (esAdministrador)
                {
                    cmbDocentes.Visible = true;
                    lblNomDocente.Visible = false;
                    lblDocente.Visible = false;
                    lblSecc.Visible = false;
                    lblSeccion.Text = "Materia: ";

                    var docentes = contexto.Usuarios
                   .Where(u => u.Roles != null &&
                              u.Roles.Roles_Permisos.Any(rp => rp.id_permiso == 1) &&  // Permiso docente
                              !u.Roles.Roles_Permisos.Any(rp => rp.id_permiso == 2))   // No es admin
                   .Select(u => new
                   {
                       u.id_usuario,
                       NombreCompleto = u.nombre_usuario + " " + u.apellido_usuario
                   })
                   .OrderBy(d => d.NombreCompleto)  // Orden alfabético
                   .ToList();

                    cmbDocentes.DisplayMember = "NombreCompleto";
                    cmbDocentes.ValueMember = "id_usuario";
                    cmbDocentes.DataSource = docentes;
                    cmbDocentes.SelectedIndexChanged += CmbDocentes_SelectedIndexChanged;

                    if (cmbDocentes.Items.Count > 0)
                        cmbDocentes.SelectedIndex = 0;
                }
                else
                {
                    cmbDocentes.Visible = false;
                    lblNomDocente.Visible = true;
                    lblSecc.Visible = true;
                    lblNomDocente.Text = usuario.nombre_usuario;
                    var seccion = contexto.Secciones.FirstOrDefault(s => s.id_seccion == usuario.id_seccion);
                    lblSecc.Text = seccion?.nombre_seccion ?? "Sin sección";

                    // Aquí obtenemos el periodo seleccionado y lo pasamos a CargarNotasDocente
                    string periodoSeleccionado = cmbPeriodo.SelectedItem.ToString();
                    CargarNotasDocente(usuario.id_usuario, periodoSeleccionado); // Llamada a CargarNotasDocente con el periodo
                }
            }
        }


        private void btnGestionarNotas_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                // Entrar en modo edición
                modoEdicion = true;
                lblNotas.Text = "MODO GESTIÓN ACTIVADO";
                lblNotas.ForeColor = Color.Black;
                tablaNotas.Grid.ReadOnly = false;

                foreach (DataGridViewColumn col in tablaNotas.Grid.Columns)
                {
                    col.ReadOnly = !(col.Name == "colPrimerExamen" ||
                                    col.Name == "colSegundoExamen" ||
                                    col.Name == "colCotidiano" ||
                                    col.Name == "colTareas");
                }

                btnGestionarNotas.Text = "Terminar Edición";
                PanelAcciones.Visible = true;
            }
            else
            {
                // Salir del modo edición
                if (huboCambios)
                {
                    var confirm = MessageBox.Show(
                        "Hay cambios no guardados. ¿Estás seguro de que deseas salir del modo edición y perder los cambios?",
                        "Confirmar salida",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm == DialogResult.No) return;
                }

                modoEdicion = false;
                huboCambios = false;
                lblNotas.Text = "Listado de Notas";
                lblNotas.ForeColor = Color.Teal;
                tablaNotas.Grid.ReadOnly = true;
                btnGestionarNotas.Text = "Gestionar Notas";
                PanelAcciones.Visible = false;

                // Recargar datos
                CargarNotasAutomatico(cmbPeriodo.SelectedItem?.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (huboCambios)
            {
                var confirm = MessageBox.Show(
                    "Hay cambios no guardados. ¿Estás seguro de que deseas cancelar la edición y perder los cambios?",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.No) return;
            }

            modoEdicion = false;
            huboCambios = false;

            // Cambiar texto de la etiqueta y el botón
            lblNotas.Text = "Listado de Notas";
            lblNotas.ForeColor = Color.Teal;
            btnGestionarNotas.Text = "Gestionar Notas";

            // Volver a modo solo lectura en el DataGrid
            tablaNotas.Grid.ReadOnly = true;
            PanelAcciones.Visible = false;

            // Recargar los datos si es necesario
            CargarNotasAutomatico(periodoAnterior);
        }



        private void CmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (evitandoEvento) return;

            string nuevoPeriodo = cmbPeriodo.SelectedItem?.ToString();
            if (nuevoPeriodo == periodoAnterior) return;

            if (huboCambios && modoEdicion)
            {
                var resultado = MessageBox.Show(
                    "Hay cambios no guardados. ¿Estás seguro de que deseas cambiar el período y perder los cambios?",
                    "Confirmar cambio de período",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.No)
                {
                    evitandoEvento = true;
                    cmbPeriodo.SelectedItem = periodoAnterior;
                    evitandoEvento = false;
                    return;
                }
                huboCambios = false;
            }

            periodoAnterior = nuevoPeriodo;
            CargarNotasAutomatico(nuevoPeriodo);
        }

        private void cmbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (evitandoEvento) return;

            var nuevaMateria = cmbMateria.SelectedItem as Materias;
            if (nuevaMateria?.id_materia == materiaAnterior?.id_materia) return;

            if (huboCambios && modoEdicion)
            {
                var resultado = MessageBox.Show(
                    "Hay cambios no guardados. ¿Estás seguro de que deseas cambiar la materia y perder los cambios?",
                    "Confirmar cambio de materia",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.No)
                {
                    evitandoEvento = true;
                    cmbMateria.SelectedItem = materiaAnterior;
                    evitandoEvento = false;
                    return;
                }
                huboCambios = false;
            }

            materiaAnterior = nuevaMateria;
            CargarNotasAutomatico(cmbPeriodo.SelectedItem?.ToString());
        }



        private void CargarMateriasDocente(int idDocente)
        {
            try
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    var materias = (from h in contexto.Horarios
                                    join m in contexto.Materias on h.id_materia equals m.id_materia
                                    where h.id_usuario == idDocente
                                    select m)
                                  .Distinct()
                                  .OrderBy(m => m.nombre_materia)
                                  .ToList();

                    // Guardar selección actual
                    var materiaActual = cmbMateria.SelectedItem as Materias;
                    int? idMateriaActual = materiaActual?.id_materia;

                    cmbMateria.DataSource = materias;
                    cmbMateria.DisplayMember = "nombre_materia";
                    cmbMateria.ValueMember = "id_materia";

                    // Restaurar selección si existe
                    if (idMateriaActual != null && materias.Any(m => m.id_materia == idMateriaActual))
                    {
                        cmbMateria.SelectedValue = idMateriaActual;
                    }
                    else if (materias.Count > 0)
                    {
                        cmbMateria.SelectedIndex = 0;
                    }

                    materiaAnterior = cmbMateria.SelectedItem as Materias;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar materias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void CargarNotasAutomatico(string periodoSeleccionado)
        {
            tablaNotas.Grid.Rows.Clear();  // Limpiar las filas previas

            var materiaSeleccionada = cmbMateria.SelectedItem as Materias;
            if (materiaSeleccionada == null) return;

            using (var contexto = new RegistroDocenteEntities())
            {
                int idDocente = Sesion.IdUsuario; // Por defecto es el usuario actual

                // Si es admin y hay un docente seleccionado, usar ese ID
                if (cmbDocentes.Visible && cmbDocentes.SelectedItem != null)
                {
                    idDocente = (int)cmbDocentes.SelectedValue;
                }

                // Obtener las clases del docente para la materia seleccionada
                var clases = (from c in contexto.Clases
                              join est in contexto.Estudiantes on c.id_estudiante equals est.id_estudiante
                              where c.id_usuario == idDocente
                                    && c.id_materia == materiaSeleccionada.id_materia
                              select new
                              {
                                  Clase = c,
                                  Estudiante = est,
                                  Nota = contexto.Notas.FirstOrDefault(n => n.id_clase == c.id_clase && n.periodo == periodoSeleccionado) // Filtrar por el periodo seleccionado
                              }).ToList();

                foreach (var item in clases)
                {
                    string cedula = item.Estudiante.cedula_estudiante;

                    // 1. Calcular la asistencia automática
                    // 1.1 Obtener todas las asistencias de ese estudiante en ese periodo
                    var fechaInicio = periodoSeleccionado == "Primer Periodo" ? new DateTime(2025, 2, 3) : new DateTime(2025, 5, 26);
                    var fechaFin = periodoSeleccionado == "Primer Periodo" ? new DateTime(2025, 5, 25) : new DateTime(2025, 12, 10);

                    var clasesDocente = contexto.Clases
                        .Where(c => c.id_usuario == idDocente && c.id_materia == materiaSeleccionada.id_materia)
                        .Select(c => c.id_clase)
                        .ToList();

                    // Obtener las asistencias de ese estudiante en ese periodo
                    var asistencias = contexto.Asistencia
                        .Where(a => a.id_estudiante == item.Estudiante.id_estudiante
                                    && a.fecha >= fechaInicio && a.fecha <= fechaFin)  // Filtrar por fechas del periodo
                        .ToList();


                    // 2. Filtrar por día (solo un registro por fecha, el peor es "Ausente" > "Tarde" > "Presente")
                    var estadosPorDia = asistencias
                        .GroupBy(a => a.fecha.Value.Date)  // Usamos .Value para acceder al valor de fecha, ya que es nullable
                        .Select(g =>
                        {
                            if (g.Any(a => a.estado == "Ausente")) return "Ausente";
                            if (g.Any(a => a.estado == "Tarde")) return "Tarde";
                            return "Presente";
                        })
                        .ToList();


                    // 3. Calcular penalizaciones
                    int penalizaciones = estadosPorDia.Count(e => e == "Ausente" || e == "Tarde");
                    decimal asistenciaFinal = Math.Max(10 - (penalizaciones * 0.2m), 0);

                    // 4. Calcular las notas
                    decimal examen1 = item.Nota?.primer_examen ?? 0;
                    decimal examen2 = item.Nota?.segundo_examen ?? 0;
                    decimal tareas = item.Nota?.tareas ?? 10;
                    decimal asistencia = asistenciaFinal;  // Asistencia ya calculada
                    decimal cotidiano = item.Nota?.cotidiano ?? 60;

                    // Calcular la nota final
                    decimal notaFinal = examen1 + examen2 + tareas + asistencia + cotidiano;

                    // 5. Agregar los datos al DataGridView
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
                        idDocente = (int)cmbDocentes.SelectedValue;

                    var clases = contexto.Clases
                        .Where(c => c.id_usuario == idDocente && c.id_estudiante == estudiante.id_estudiante)
                        .ToList();

                    foreach (var clase in clases)
                    {
                        var periodo = cmbPeriodo.SelectedItem?.ToString();
                        if (string.IsNullOrEmpty(periodo)) continue;

                        decimal.TryParse(fila.Cells["colPrimerExamen"].Value?.ToString(), out decimal examen1);
                        decimal.TryParse(fila.Cells["colSegundoExamen"].Value?.ToString(), out decimal examen2);
                        decimal.TryParse(fila.Cells["colTareas"].Value?.ToString(), out decimal tareas);
                        decimal.TryParse(fila.Cells["colAsistencia"].Value?.ToString(), out decimal asistenciaFinal);
                        decimal.TryParse(fila.Cells["colCotidiano"].Value?.ToString(), out decimal cotidiano);

                        var notaExistente = contexto.Notas.FirstOrDefault(n =>
                            n.id_clase == clase.id_clase &&
                            n.periodo == periodo
                        );

                        if (notaExistente != null)
                        {
                            notaExistente.tareas = tareas;
                            notaExistente.asistencia = asistenciaFinal;
                            notaExistente.cotidiano = cotidiano;
                            if (clase.id_materia == materiaSeleccionada.id_materia)
                            {
                                notaExistente.primer_examen = examen1;
                                notaExistente.segundo_examen = examen2;
                            }
                            notaExistente.nota_final =
                                notaExistente.primer_examen + notaExistente.segundo_examen
                                + tareas + asistenciaFinal + cotidiano;
                        }
                        else
                        {
                            contexto.Notas.Add(new Notas
                            {
                                id_clase = clase.id_clase,
                                primer_examen = clase.id_materia == materiaSeleccionada.id_materia ? examen1 : 0,
                                segundo_examen = clase.id_materia == materiaSeleccionada.id_materia ? examen2 : 0,
                                tareas = tareas,
                                asistencia = asistenciaFinal,
                                cotidiano = cotidiano,
                                nota_final = (clase.id_materia == materiaSeleccionada.id_materia ? examen1 : 0)
                                            + (clase.id_materia == materiaSeleccionada.id_materia ? examen2 : 0)
                                            + tareas + asistenciaFinal + cotidiano,
                                periodo = periodo,
                                // Asigna el id_estudiante correctamente a la nota nueva
                                id_estudiante = estudiante.id_estudiante  // Esta es la clave para guardar el id_estudiante
                            });
                        }
                    }
                }
                contexto.SaveChanges();
                // Después de guardar exitosamente:
                huboCambios = false;
                periodoAnterior = cmbPeriodo.SelectedItem?.ToString();
                materiaAnterior = cmbMateria.SelectedItem as Materias;

                MessageBox.Show("Notas guardadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Después de guardar:
            huboCambios = false;

            // Actualizar estado de las notas guardadas
            periodoAnterior = cmbPeriodo.SelectedItem?.ToString();
            materiaAnterior = cmbMateria.SelectedItem as Materias;

            // Opcional: Recargar datos para asegurar consistencia
            CargarNotasAutomatico(periodoAnterior);
        }


        // El resto de eventos siguen igual (NO se puede editar asistencia, y todo se recalcula)
        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !modoEdicion) return;

            var fila = tablaNotas.Grid.Rows[e.RowIndex];
            var celda = fila.Cells[e.ColumnIndex];

            // Solo marcar cambios en columnas editables
            if (celda.OwningColumn.Name == "colPrimerExamen" ||
                celda.OwningColumn.Name == "colSegundoExamen" ||
                celda.OwningColumn.Name == "colTareas" ||
                celda.OwningColumn.Name == "colCotidiano")
            {
                huboCambios = true;
                alertaMostrada = false;  // Reiniciar la bandera al finalizar la edición de la celda

                // Recalcular nota final
                decimal.TryParse(fila.Cells["colPrimerExamen"].Value?.ToString(), out decimal examen1);
                decimal.TryParse(fila.Cells["colSegundoExamen"].Value?.ToString(), out decimal examen2);
                decimal.TryParse(fila.Cells["colTareas"].Value?.ToString(), out decimal tareas);
                decimal.TryParse(fila.Cells["colAsistencia"].Value?.ToString(), out decimal asistencia);
                decimal.TryParse(fila.Cells["colCotidiano"].Value?.ToString(), out decimal cotidiano);

                decimal notaFinal = examen1 + examen2 + tareas + asistencia + cotidiano;
                fila.Cells["colNotaFinal"].Value = Math.Round(notaFinal, 2);
            }
        }

        private void Grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Verifica si el valor está siendo editado en las celdas de los exámenes, tareas o cotidiano
            if (e.ColumnIndex == tablaNotas.Grid.Columns["colPrimerExamen"].Index ||
                e.ColumnIndex == tablaNotas.Grid.Columns["colSegundoExamen"].Index ||
                e.ColumnIndex == tablaNotas.Grid.Columns["colTareas"].Index ||
                e.ColumnIndex == tablaNotas.Grid.Columns["colCotidiano"].Index)
            {
                // Intenta convertir el valor a decimal
                if (decimal.TryParse(e.FormattedValue.ToString(), out decimal valor))
                {
                    bool esValido = true;
                    string mensaje = string.Empty;

                    // Comprobar los límites para los exámenes, tareas y cotidiano
                    if (e.ColumnIndex == tablaNotas.Grid.Columns["colPrimerExamen"].Index ||
                        e.ColumnIndex == tablaNotas.Grid.Columns["colSegundoExamen"].Index)
                    {
                        // Limitar el valor a 10 para los exámenes
                        if (valor < 0 || valor > 10)
                        {
                            esValido = false;
                            mensaje = "La nota debe estar entre 0 y 10";
                        }
                    }
                    else if (e.ColumnIndex == tablaNotas.Grid.Columns["colTareas"].Index)
                    {
                        // Limitar el valor a 10 para las tareas
                        if (valor < 0 || valor > 10)
                        {
                            esValido = false;
                            mensaje = "La tarea debe estar entre 0 y 10";
                        }
                    }
                    else if (e.ColumnIndex == tablaNotas.Grid.Columns["colCotidiano"].Index)
                    {
                        // Limitar el valor a 60 para el cotidiano
                        if (valor < 0 || valor > 60)
                        {
                            esValido = false;
                            mensaje = "La nota de cotidiano debe estar entre 0 y 60";
                        }
                    }

                    if (!esValido)
                    {
                        e.Cancel = true;  // Cancelar el cambio
                        tablaNotas.Grid.CancelEdit();  // Asegurarse de que no se guarde el valor incorrecto

                        // Solo mostrar la alerta una vez
                        if (!alertaMostrada)
                        {
                            alertaMostrada = true; // Evitar mostrar la alerta más de una vez
                            MessageBox.Show(mensaje, "Valor no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    // Si no es un número válido, cancelar el cambio
                    e.Cancel = true;
                    tablaNotas.Grid.CancelEdit();  // Asegurarse de que no se guarde el valor incorrecto

                    // Solo mostrar la alerta una vez
                    if (!alertaMostrada)
                    {
                        alertaMostrada = true; // Evitar mostrar la alerta más de una vez
                        MessageBox.Show("Por favor ingrese un valor numérico", "Valor no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void UcNotas_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && cmbMateria.SelectedItem != null)
            {
                // Asegúrate de obtener el valor de periodoSeleccionado
                string periodoSeleccionado = cmbPeriodo.SelectedItem?.ToString();
                CargarNotasAutomatico(periodoSeleccionado);  // Ahora pasas el periodo seleccionado
            }
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Obtener el periodo seleccionado
            string periodo = cmbPeriodo.SelectedItem.ToString();

            string nombreDocente = lblNomDocente.Text;
            string seccion = lblSecc.Text;
            string materia = cmbMateria.Text;

            // Resto del código para guardar el archivo PDF
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"{nombreDocente.Replace(" ", "_")}_{materia}.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExportadorPDF.Exportar(tablaNotas.Grid, nombreDocente, seccion, materia, periodo, sfd.FileName);
                MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }





        private void CmbDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocentes.SelectedItem != null)
            {
                int idDocenteSeleccionado = (int)cmbDocentes.SelectedValue;
                CargarMateriasDocente(idDocenteSeleccionado);

                // Cargar notas con el período actual
                string periodoSeleccionado = cmbPeriodo.SelectedItem?.ToString();
                CargarNotasAutomatico(periodoSeleccionado);
            }
        }



        private void CargarNotasDocente(int idDocente, string periodoSeleccionado)
        {
            tablaNotas.Grid.Rows.Clear();

            using (var contexto = new RegistroDocenteEntities())
            {
                // Obtener las materias que imparte el docente
                var materias = (from h in contexto.Horarios
                                join m in contexto.Materias on h.id_materia equals m.id_materia
                                where h.id_usuario == idDocente
                                select m).Distinct().ToList();

                // Asignar las materias al ComboBox de materias
                cmbMateria.DataSource = materias;
                cmbMateria.DisplayMember = "nombre_materia";
                cmbMateria.ValueMember = "id_materia";

                if (cmbMateria.Items.Count > 0)
                {
                    cmbMateria.SelectedIndex = 0;  // Seleccionar la primera materia si existe alguna
                    cmbMateria_SelectedIndexChanged(null, null);  // Actualiza las notas de esa materia
                }
            }

            // Después de asignar la materia, cargamos las notas por periodo
            CargarNotasPorPeriodo(periodoSeleccionado);
        }


        private void CargarNotasPorPeriodo(string periodoSeleccionado)
        {
            tablaNotas.Grid.Rows.Clear();  // Limpiar las filas previas

            var materiaSeleccionada = cmbMateria.SelectedItem as Materias;
            if (materiaSeleccionada == null) return;

            using (var contexto = new RegistroDocenteEntities())
            {
                var idDocente = Sesion.IdUsuario;  // Obtener el ID del docente

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
                                  Nota = contexto.Notas
                                          .FirstOrDefault(n => n.id_clase == c.id_clase && n.periodo == periodoSeleccionado)
                              }).ToList();

                foreach (var item in clases)
                {
                    string cedula = item.Estudiante.cedula_estudiante;

                    // Si no existen notas para el periodo seleccionado, inicializar con los valores máximos
                    decimal tareas = item.Nota?.tareas ?? 10;
                    decimal asistencia = item.Nota?.asistencia ?? 10;
                    decimal cotidiano = item.Nota?.cotidiano ?? 60;

                    // Si el periodo es el segundo y no hay nota, poner el valor máximo por defecto
                    if (periodoSeleccionado == "Segundo Periodo" && item.Nota == null)
                    {
                        tareas = 10;
                        asistencia = 10;
                        cotidiano = 60;
                    }

                    decimal examen1 = item.Nota?.primer_examen ?? 0;
                    decimal examen2 = item.Nota?.segundo_examen ?? 0;
                    decimal notaFinal = examen1 + examen2 + tareas + asistencia + cotidiano;

                    // Agregar los datos al DataGridView
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

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
