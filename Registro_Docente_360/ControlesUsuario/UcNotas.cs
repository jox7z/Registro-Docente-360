using Modelos.EntityFramework;
using Registro_Docente_360.Eventos;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcNotas : UserControl
    {
        private bool modoEdicion = false;
        private ToolTip tooltipNotas = new ToolTip();

        public UcNotas()
        {
            InitializeComponent();
            this.Load += UcNotas_Load;
        }

        private void UcNotas_Load(object sender, EventArgs e)
        {
            tablaNotas.Grid.CellEndEdit += Grid_CellEndEdit;
            tablaNotas.Grid.EditingControlShowing += Grid_EditingControlShowing;
            tablaNotas.Grid.CellValidating += tablaNotas_Grid_CellValidating;


            tablaNotas.Grid.Columns.Clear();

            tablaNotas.Grid.Columns.Add("colCedula", "Cédula");
            tablaNotas.Grid.Columns.Add("colNombre", "Nombre");
            tablaNotas.Grid.Columns.Add("colPrimerExamen", "Primer Examen");
            tablaNotas.Grid.Columns.Add("colSegundoExamen", "Segundo Examen");
            tablaNotas.Grid.Columns.Add("colTareas", "Tareas");
            tablaNotas.Grid.Columns.Add("colAsistencia", "Asistencia");
            tablaNotas.Grid.Columns.Add("colCotidiano", "Cotidiano");
            tablaNotas.Grid.Columns.Add("colNotaFinal", "Nota Final");

            //solo lectura por defecto
            tablaNotas.Grid.ReadOnly = true;
            tablaNotas.Grid.AllowUserToAddRows = false;
            tablaNotas.Grid.AllowUserToDeleteRows = false;

            //no se pueden editar
            tablaNotas.Grid.Columns["colCedula"].ReadOnly = true;
            tablaNotas.Grid.Columns["colNombre"].ReadOnly = true;

            //ocultar gestiones, hasta que se active el boton
            PanelAcciones.Visible = false;


            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);
                var seccion = contexto.Secciones.FirstOrDefault(s => s.id_seccion == usuario.id_seccion);

                lblNomDocente.Text = usuario.nombre_usuario;
                lblSecc.Text = $"{seccion.nombre_seccion}";

                var materias = (from h in contexto.Horarios
                                join m in contexto.Materias on h.id_materia equals m.id_materia
                                where h.id_usuario == Sesion.IdUsuario
                                select m).Distinct().ToList();
                cmbMateria.DataSource = materias;
                cmbMateria.DisplayMember = "nombre_materia";
                cmbMateria.ValueMember = "id_materia";

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
                lblNotas.Text = "Listado de Notas";
                lblNotas.ForeColor = Color.Teal;

                tablaNotas.Grid.ReadOnly = true;
                btnGestionarNotas.Text = "Gestionar Notas";
                tooltipNotas.SetToolTip(btnGestionarNotas, "Haz clic para editar las notas");
                PanelAcciones.Visible = false;
                modoEdicion = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Desea descartar todos los cambios no guardados?", "Cancelar edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // Desactiva modo edición visualmente
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

                // TODO: Recargar los datos originales desde la fuente (base de datos,)


                tablaNotas.Grid.Rows.Clear();

                //Aqui se deberia de llamar el metodo para cagar los datos a la base  (los datos anteriores sin los camibios)
            }
        }

        private void cmbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMateria.SelectedItem is Materias materiaSeleccionada)
            {
                tablaNotas.Grid.Rows.Clear();

                using (var contexto = new RegistroDocenteEntities())
                {
                    var clases = (from c in contexto.Clases
                                  join est in contexto.Estudiantes on c.id_estudiante equals est.id_estudiante
                                  where c.id_usuario == Sesion.IdUsuario
                                  && c.id_materia == materiaSeleccionada.id_materia
                                  select new
                                  {
                                      Clase = c,
                                      Estudiante = est,
                                      Nota = contexto.Notas.FirstOrDefault(n => n.id_clase == c.id_clase)
                                  }).ToList();

                    foreach (var item in clases)
                    {
                        tablaNotas.Grid.Rows.Add(
                            item.Estudiante.cedula_estudiante,
                            $"{item.Estudiante.nombre_estudiante} {item.Estudiante.primer_apellido}",
                            item.Nota?.primer_examen?.ToString("0.##") ?? "",
                            item.Nota?.segundo_examen?.ToString("0.##") ?? "",
                            item.Nota?.tareas?.ToString("0.##") ?? "",
                            item.Nota?.asistencia?.ToString("0.##") ?? "",
                            item.Nota?.cotidiano?.ToString("0.##") ?? "",
                            item.Nota?.nota_final?.ToString("0.##") ?? ""
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
                var clases = contexto.Clases.
                    Where(c => c.id_usuario == Sesion.IdUsuario && c.id_materia == materiaSeleccionada.id_materia).ToList();

                foreach (var clase in clases)
                {
                    var notasExistentes = contexto.Notas.Where(n => n.id_clase == clase.id_clase);
                    contexto.Notas.RemoveRange(notasExistentes);
                }


                foreach (DataGridViewRow fila in tablaNotas.Grid.Rows)
                {
                    if (fila.IsNewRow) continue;

                    string cedula = fila.Cells["colCedula"].Value?.ToString();
                    var estudiante = contexto.Estudiantes.FirstOrDefault(x => x.cedula_estudiante == cedula);
                    if (estudiante == null) continue;

                    var clase = contexto.Clases.FirstOrDefault(c =>
                        c.id_usuario == Sesion.IdUsuario &&
                        c.id_estudiante == estudiante.id_estudiante &&
                        c.id_materia == materiaSeleccionada.id_materia);

                    if (clase == null) continue;

                    decimal.TryParse(fila.Cells["colPrimerExamen"].Value?.ToString(), out decimal examen1);
                    decimal.TryParse(fila.Cells["colSegundoExamen"].Value?.ToString(), out decimal examen2);
                    decimal.TryParse(fila.Cells["colTareas"].Value?.ToString(), out decimal tareas);
                    decimal.TryParse(fila.Cells["colAsistencia"].Value?.ToString(), out decimal asistencia);
                    decimal.TryParse(fila.Cells["colCotidiano"].Value?.ToString(), out decimal cotidiano);

                    decimal notaFinal = (examen1 + examen2 + tareas + asistencia + cotidiano) / 5;

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
                }

                contexto.SaveChanges();
                MessageBox.Show("Notas guardadas exitosamente");
            }
        }

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = tablaNotas.Grid.Rows[e.RowIndex];

                decimal.TryParse(fila.Cells["colPrimerExamen"].Value?.ToString(), out decimal examen1);
                decimal.TryParse(fila.Cells["colSegundoExamen"].Value?.ToString(), out decimal examen2);
                decimal.TryParse(fila.Cells["colTareas"].Value?.ToString(), out decimal tareas);
                decimal.TryParse(fila.Cells["colAsistencia"].Value?.ToString(), out decimal asistencia);
                decimal.TryParse(fila.Cells["colCotidiano"].Value?.ToString(), out decimal cotidiano);

                // Si todos están llenos, actualiza la nota final
                int countFilled = 0;
                if (!string.IsNullOrWhiteSpace(fila.Cells["colPrimerExamen"].Value?.ToString())) countFilled++;
                if (!string.IsNullOrWhiteSpace(fila.Cells["colSegundoExamen"].Value?.ToString())) countFilled++;
                if (!string.IsNullOrWhiteSpace(fila.Cells["colTareas"].Value?.ToString())) countFilled++;
                if (!string.IsNullOrWhiteSpace(fila.Cells["colAsistencia"].Value?.ToString())) countFilled++;
                if (!string.IsNullOrWhiteSpace(fila.Cells["colCotidiano"].Value?.ToString())) countFilled++;

                if (countFilled == 5)
                {
                    decimal notaFinal = (examen1 + examen2 + tareas + asistencia + cotidiano) / 5;
                    fila.Cells["colNotaFinal"].Value = Math.Round(notaFinal, 2);
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

        private void tablaNotas_Grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = tablaNotas.Grid.Columns[e.ColumnIndex].Name;

            // Validar solo las columnas editables
            if (columnName == "colPrimerExamen" ||
                columnName == "colSegundoExamen" ||
                columnName == "colTareas" ||
                columnName == "colAsistencia" ||
                columnName == "colCotidiano")
            {
                if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal valor) ||
                    valor < 0 || valor > 100)
                {
                    e.Cancel = true;
                    MessageBox.Show("Ingrese un número válido entre 0 y 100", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


    }
}
