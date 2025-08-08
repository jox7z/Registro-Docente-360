using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Registro_Docente_360.Forms
{
    public partial class UcAlumnos : UserControl , IModoEdicion
    {
        private bool modoEdicion = false;
        private ToolTip tooltipAlumnos = new ToolTip();
        private AlumnoController alumnoController = new AlumnoController();
        private bool evitarValidacion = false;
        private bool estaCancelando = false;
        private bool hayCambios = false;
        public bool EstaEnModoEdicion => modoEdicion;

        public UcAlumnos()
        {
            InitializeComponent();
            this.Load += UcAlumnos_Load;
            tablaAlumnos.Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Configura la tabla de alumnos al cargar el control.
        /// </summary>
        private void UcAlumnos_Load(object sender, EventArgs e)
        {
            tablaAlumnos.Grid.Columns.Clear();

            tablaAlumnos.Grid.Columns.Add("colCedula", "Cedula");
            tablaAlumnos.Grid.Columns.Add("colApellido1", "Primer Apellido");
            tablaAlumnos.Grid.Columns.Add("colApellido2", "Segundo Apellido");
            tablaAlumnos.Grid.Columns.Add("colNombre", "Nombre");
            tablaAlumnos.Grid.Columns.Add("colTelefono", "Telefono Encargado");

            tablaAlumnos.Grid.ReadOnly = true;
            tablaAlumnos.Grid.AllowUserToAddRows = false;
            tablaAlumnos.Grid.AllowUserToDeleteRows = false;

            PanelAcciones.Visible = false;
            tablaAlumnos.Grid.EditingControlShowing += Grid_EditingControlShowing;
            tablaAlumnos.Grid.DataError += Grid_DataError;

            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);
                AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);
                if (esAdministrador) //administrador
                {
                    // Mostrar ComboBox y ocultar Label
                    cmbDocentes.Visible = true;
                    label1.Visible = false;
                    lblSeccion.Visible = false;
                    lblNomDocente.Visible = false;

                    // Cargar docentes
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
                    {
                        cmbDocentes.SelectedIndex = 0; // Dispara carga automática
                    }
                    int idDocenteSeleccionado = (int)cmbDocentes.SelectedValue;
                    CargarEstudiantes(idDocenteSeleccionado);
                }
                else
                {
                    // Usuario docente
                    cmbDocentes.Visible = false;
                    label1.Visible = true;

                    lblNomDocente.Text = usuario.nombre_usuario;

                    var seccion = contexto.Secciones.FirstOrDefault(s => s.id_seccion == usuario.id_seccion);
                    label1.Text = $"{seccion?.nombre_seccion ?? "Sin sección"}";

                    CargarEstudiantes(Sesion.IdUsuario); // Cargar estudiantes del docente actual
                }
            }
        }


        /// <summary>
        /// Controla la edición de la celda y restringe la entrada si es necesario.
        /// </summary>
        private void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                // Limpia todos los eventos previos para evitar duplicaciones o efectos cruzados
                tb.KeyPress -= Cedula_KeyPress;
                tb.KeyPress -= Telefono_KeyPress;

                var currentColumn = tablaAlumnos.Grid.Columns[tablaAlumnos.Grid.CurrentCell.ColumnIndex].Name;

                if (currentColumn == "colCedula")
                {
                    tb.CharacterCasing = CharacterCasing.Upper;
                    tb.KeyPress += Cedula_KeyPress;
                }
                else if (currentColumn == "colTelefono")
                {
                    tb.CharacterCasing = CharacterCasing.Normal;
                    tb.KeyPress += Telefono_KeyPress;
                }
                else
                {
                    tb.CharacterCasing = CharacterCasing.Normal;
                }
            }
        }

        /// <summary>
        /// Cuando uno termina de editar una celda de cédula, se valida el dato escrito.
        /// </summary>
        private void Grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // 🔁 No validar si estamos cancelando
            if (evitarValidacion || estaCancelando) return;

            if (tablaAlumnos.Grid.Columns[e.ColumnIndex].Name == "colCedula")
            {
                string cedula = e.FormattedValue?.ToString()?.Trim() ?? "";

                if (!alumnoController.ValidarCedula(cedula, out string mensajeError))
                {
                    MessageBox.Show(mensajeError, "Cédula inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }




        /// <summary>
        /// Restringe los caracteres permitidos en la cédula.
        /// </summary>
        private void Cedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            // Solo permitir números y backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limitar a 8 dígitos
            if (!char.IsControl(e.KeyChar) && tb.Text.Length >= 8)
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Activa y desactiva el modo de edición en la tabla.
        /// </summary>
        private void btnEditarAlumnos_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                lblAlumnos.Text = "MODO EDICIÓN ACTIVADO";
                lblAlumnos.ForeColor = Color.Black;

                tablaAlumnos.Grid.ReadOnly = false;
                tablaAlumnos.Grid.AllowUserToAddRows = false;
                tablaAlumnos.Grid.AllowUserToDeleteRows = true;

                for (int i = 0; i < tablaAlumnos.Grid.Columns.Count; i++)
                {
                    if (tablaAlumnos.Grid.Columns[i].Name != "colCedula")
                        tablaAlumnos.Grid.Columns[i].ReadOnly = false;
                }

                this.BackColor = Color.FromArgb(230, 255, 245);
                btnEditarAlumnos.Text = "TERMINAR EDICIÓN";
                tooltipAlumnos.SetToolTip(btnEditarAlumnos, "Haz clic para guardar los cambios");
                modoEdicion = true;
                PanelAcciones.Visible = true;
            }
            else
            {
                if (hayCambios)
                {
                    DialogResult result = MessageBox.Show(
                        "Hay cambios sin guardar. ¿Estás seguro de salir del modo edición y perder los cambios?",
                        "Confirmar salida",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    // Si el usuario acepta salir, recargamos los datos originales
                    tablaAlumnos.Grid.Rows.Clear();
                    UcAlumnos_Load(this, EventArgs.Empty); // recarga los datos desde cero
                    hayCambios = false;
                }

                lblAlumnos.Text = "Listado de Alumnos";
                lblAlumnos.ForeColor = Color.Teal;
                lblAlumnos.Font = new Font("Segoe UI", 21, FontStyle.Bold);

                tablaAlumnos.Grid.ReadOnly = true;
                tooltipAlumnos.SetToolTip(btnEditarAlumnos, "Haz clic para editar los datos");
                this.BackColor = SystemColors.Control;

                PanelAcciones.Visible = false;
                btnEditarAlumnos.Text = "EDITAR ALUMNOS";
                modoEdicion = false;
            }
        }


        /// <summary>
        /// Agrega una nueva fila vacía.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            tablaAlumnos.Grid.Rows.Add();
            hayCambios = false;
        }

        /// <summary>
        /// Elimina la fila seleccionada previa confirmación.
        /// </summary>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (tablaAlumnos.Grid.SelectedRows.Count > 0)
            {
                var fila = tablaAlumnos.Grid.SelectedRows[0];

                var cedula = fila.Cells["colCedula"].Value?.ToString()?.Trim();

                // Si no tiene cédula, asumimos que es una fila nueva, y la borramos directamente
                if (string.IsNullOrWhiteSpace(cedula))
                {
                    tablaAlumnos.Grid.Rows.Remove(fila);
                    hayCambios = true;
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "¿Está seguro que desea eliminar este estudiante?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool eliminado = alumnoController.EliminarEstudiantePorCedula(cedula);
                    if (eliminado)
                    {
                        MessageBox.Show("Estudiante eliminado correctamente.");
                        tablaAlumnos.Grid.Rows.Remove(fila); // Elimina visualmente

                        
                        string accion = "Eliminar estudiante";
                        string descripcion = $"Se eliminó el estudiante con cédula: {cedula}";
                        string modulo = "Alumnos";

                        AlumnoController controlador = new AlumnoController();
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el estudiante. Verifique si tiene datos relacionados.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante para eliminar.");
            }
            modoEdicion = false;
        }








        /// <summary>
        /// Acción de guardar alumnos. 
        /// Debe validarse desde AlumnoController antes de guardar en la base.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Asegurar que cualquier edición activa se termine
            tablaAlumnos.Grid.EndEdit();
            tablaAlumnos.Grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            tablaAlumnos.Grid.CurrentCell = null;

            List<Estudiantes> listaEstudiantes = new List<Estudiantes>();

            foreach (DataGridViewRow fila in tablaAlumnos.Grid.Rows)
            {
                if (fila.IsNewRow) continue;

                var cedula_estudiante = fila.Cells["colCedula"].Value?.ToString()?.Trim();
                var nombre_estudiante = fila.Cells["colNombre"].Value?.ToString()?.Trim();
                var primer_apellido = fila.Cells["colApellido1"].Value?.ToString()?.Trim();
                var segundo_apellido = fila.Cells["colApellido2"].Value?.ToString()?.Trim();
                var telefono_encargado = fila.Cells["colTelefono"].Value?.ToString()?.Trim();

                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(cedula_estudiante) ||
                    string.IsNullOrWhiteSpace(primer_apellido) ||
                    string.IsNullOrWhiteSpace(segundo_apellido) ||
                    string.IsNullOrWhiteSpace(nombre_estudiante) ||
                    string.IsNullOrWhiteSpace(telefono_encargado))
                {
                    MessageBox.Show("Todos los campos son obligatorios. Verifique que no haya campos vacíos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tablaAlumnos.Grid.CurrentCell = fila.Cells["colCedula"];
                    fila.Selected = true;
                    return; // Cancelar guardado
                }

                listaEstudiantes.Add(new Estudiantes
                {
                    cedula_estudiante = cedula_estudiante,
                    nombre_estudiante = nombre_estudiante,
                    primer_apellido = primer_apellido,
                    segundo_apellido = segundo_apellido,
                    telefono_encargado = telefono_encargado
                });
            }

            try
            {
                int idDocente = Sesion.IdUsuario;

                if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
                {
                    idDocente = (int)cmbDocentes.SelectedValue;
                }

                int idSeccionDocente;
                using (var contexto = new RegistroDocenteEntities())
                {
                    idSeccionDocente = contexto.Usuarios
                        .Where(u => u.id_usuario == idDocente)
                        .Select(u => u.id_seccion ?? 0)
                        .FirstOrDefault();
                }


                var nuevos = alumnoController.GuardarEstudiantes(listaEstudiantes, idSeccionDocente,idDocente);

                MessageBox.Show("Estudiantes guardados correctamente");


                foreach (var estudiante in nuevos)
                {
                    string descripcion = $"Se guardó el estudiante: {estudiante.nombre_estudiante} {estudiante.primer_apellido}";
                    string accion = "Nuevo estudiante";
                    string modulo = "Alumnos";

                    alumnoController.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los estudiantes: " + ex.Message);
            }

            hayCambios = false;
        }




        /// <summary>
        /// Cancela los cambios realizados y recarga los datos (a implementar).
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // 🔒 Forzar fin de edición para evitar validación molesta
            if (tablaAlumnos.Grid.IsCurrentCellInEditMode)
            {
                tablaAlumnos.Grid.EndEdit();
            }

            var confirm = MessageBox.Show("¿Desea descartar todos los cambios no guardados?", "Cancelar edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                evitarValidacion = true;

                // 🔁 Salir de modo edición
                lblAlumnos.Text = "Listado de Alumnos";
                lblAlumnos.ForeColor = Color.Teal;
                lblAlumnos.Font = new Font("Segoe UI", 21, FontStyle.Bold);
                this.BackColor = SystemColors.Control;
                PanelAcciones.Visible = false;
                btnEditarAlumnos.Text = "EDITAR ALUMNOS";
                modoEdicion = false;

                // 🧹 Eliminar filas completamente vacías
                foreach (DataGridViewRow fila in tablaAlumnos.Grid.Rows.Cast<DataGridViewRow>().ToList())
                {
                    bool vacia = true;
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                        {
                            vacia = false;
                            break;
                        }
                    }

                    if (vacia && !fila.IsNewRow)
                    {
                        tablaAlumnos.Grid.Rows.Remove(fila);
                    }
                }

                // 🔄 Recargar datos desde base
                tablaAlumnos.Grid.Rows.Clear();
                var estudiantes = alumnoController.ObtenerEstudiantesPorDocente(Sesion.IdUsuario);
                foreach (var estudiante in estudiantes)
                {
                    tablaAlumnos.Grid.Rows.Add(
                        estudiante.cedula_estudiante,
                        estudiante.primer_apellido,
                        estudiante.segundo_apellido,
                        estudiante.nombre_estudiante,
                        estudiante.telefono_encargado);
                }

                evitarValidacion = false;
            }
        }

        public void CancelarModoEdicion()
        {
            // Simula un clic en el botón de cancelar si hay cambios
            if (modoEdicion)
            {
                btnCancelar.PerformClick();
            }
        }


        private void CancelarEdicionSinValidacion()
        {
            // 🔒 Eliminar el manejador de validación temporalmente
            tablaAlumnos.Grid.CellValidating -= Grid_CellValidating;

            // Forzar salida de edición y validación
            if (tablaAlumnos.Grid.IsCurrentCellInEditMode)
            {
                tablaAlumnos.Grid.CancelEdit();
                tablaAlumnos.Grid.EndEdit();
            }

            // 🔁 Volver a suscribirse
            tablaAlumnos.Grid.CellValidating += Grid_CellValidating;
        }

        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Suprime errores por valores inválidos
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void CargarEstudiantes(int idDocente)
        {
            tablaAlumnos.Grid.Rows.Clear();

            var estudiantes = alumnoController.ObtenerEstudiantesPorDocente(idDocente);
            foreach (var estudiante in estudiantes)
            {
                tablaAlumnos.Grid.Rows.Add(
                    estudiante.cedula_estudiante,
                    estudiante.primer_apellido,
                    estudiante.segundo_apellido,
                    estudiante.nombre_estudiante,
                    estudiante.telefono_encargado);
            }
        }

        private void CmbDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocentes.SelectedItem != null)
            {
                int idDocenteSeleccionado = (int)cmbDocentes.SelectedValue;
                CargarEstudiantes(idDocenteSeleccionado);
            }
        }
    }
}
