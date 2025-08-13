using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.Entity;

namespace Registro_Docente_360.Forms
{
    public partial class UcAlumnos : UserControl
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
            ConfigurarAutoformateoCedula();
            this.Load += UcAlumnos_Load;
            tablaAlumnos.Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            tablaAlumnos.Grid.CellEndEdit += (sender, e) =>
            {
                if (e.ColumnIndex == tablaAlumnos.Grid.Columns["colCedula"].Index)
                {
                    var celda = tablaAlumnos.Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string cedula = celda.Value?.ToString()?.Replace("-", "") ?? "";
                    if (cedula.Length == 9)
                        celda.Value = $"{cedula[0]}-{cedula.Substring(1, 4)}-{cedula.Substring(5)}";
                }
            };
        }

        private void UcAlumnos_Load(object sender, EventArgs e)
        {
            tablaAlumnos.Grid.Columns.Clear();
            tablaAlumnos.Grid.Columns.Add("colId", "ID");
            //tablaAlumnos.Grid.Columns["colId"].Visible = false;
            tablaAlumnos.Grid.Columns.Add("colCedula", "Cédula");
            tablaAlumnos.Grid.Columns.Add("colApellido1", "Primer Apellido");
            tablaAlumnos.Grid.Columns.Add("colApellido2", "Segundo Apellido");
            tablaAlumnos.Grid.Columns.Add("colNombre", "Nombre");
            tablaAlumnos.Grid.Columns.Add("colTelefono", "Teléfono Encargado");

            tablaAlumnos.Grid.ReadOnly = true;
            tablaAlumnos.Grid.AllowUserToAddRows = false;
            tablaAlumnos.Grid.AllowUserToDeleteRows = false;

            PanelAcciones.Visible = false;
            tablaAlumnos.Grid.EditingControlShowing += Grid_EditingControlShowing;
            tablaAlumnos.Grid.DataError += Grid_DataError;

            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios
                    .Include(u => u.Roles)
                    .FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);

                if (esAdministrador)
                {
                    cmbDocentes.Visible = true;
                    label1.Visible = false;
                    lblSeccion.Visible = false;
                    lblNomDocente.Visible = false;

                    var docentes = contexto.Usuarios
                     .Where(u => u.Roles != null &&
                                 u.Roles.Roles_Permisos.Any(rp => rp.id_permiso == 1) &&
                                 !u.Roles.Roles_Permisos.Any(rp => rp.id_permiso == 2))
                     .Select(u => new
                     {
                         u.id_usuario,
                         NombreCompleto = u.nombre_usuario + " " + u.apellido_usuario
                     })
                     .OrderBy(d => d.NombreCompleto)
                     .ToList();

                    cmbDocentes.DisplayMember = "NombreCompleto";
                    cmbDocentes.ValueMember = "id_usuario";
                    cmbDocentes.DataSource = docentes;
                    cmbDocentes.SelectedIndexChanged += CmbDocentes_SelectedIndexChanged;

                    if (docentes.Any())
                    {
                        cmbDocentes.SelectedIndex = 0;
                        CargarEstudiantes((int)cmbDocentes.SelectedValue);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron docentes con permiso docente registrados",
                                      "Información",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                }
                else
                {
                    cmbDocentes.Visible = false;
                    label1.Visible = true;

                    if (usuario != null)
                    {
                        lblNomDocente.Text = usuario.nombre_usuario;

                        var seccion = contexto.Secciones.FirstOrDefault(s => s.id_seccion == usuario.id_seccion);
                        label1.Text = seccion?.nombre_seccion ?? "Sin sección";

                        CargarEstudiantes(Sesion.IdUsuario);
                    }
                }
            }
        }

        private void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                tb.KeyPress -= Cedula_KeyPress;
                tb.KeyPress -= Telefono_KeyPress;
                tb.KeyPress -= Nombres_KeyPress;
                tb.Leave -= Nombres_Leave;

                var currentColumn = tablaAlumnos.Grid
                    .Columns[tablaAlumnos.Grid.CurrentCell.ColumnIndex].Name;

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
                else if (currentColumn == "colNombre" ||
                         currentColumn == "colApellido1" ||
                         currentColumn == "colApellido2")
                {
                    tb.CharacterCasing = CharacterCasing.Normal;
                    tb.KeyPress += Nombres_KeyPress;
                    tb.Leave += Nombres_Leave;
                }
                else
                {
                    tb.CharacterCasing = CharacterCasing.Normal;
                }
            }
        }

        private void Grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
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

        private void Cedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }

            if (!char.IsControl(e.KeyChar) && tb.Text.Length >= 20)
            {
                e.Handled = true;
            }
        }

        private void Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && tb.Text.Length >= 8)
            {
                e.Handled = true;
            }
        }

        private void btnEditarAlumnos_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                lblAlumnos.Text = "MODO EDICIÓN ACTIVADO";
                lblAlumnos.ForeColor = Color.Black;

                tablaAlumnos.Grid.AllowUserToAddRows = false;
                tablaAlumnos.Grid.AllowUserToDeleteRows = true;

                for (int i = 0; i < tablaAlumnos.Grid.Columns.Count; i++)
                {
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

                    tablaAlumnos.Grid.Rows.Clear();
                    UcAlumnos_Load(this, EventArgs.Empty);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int idDocente;
            if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
            {
                idDocente = (int)cmbDocentes.SelectedValue;
            }
            else
            {
                idDocente = Sesion.IdUsuario;
            }
            using (var form = new FormAgregarAlumno(null,idDocente))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    using (var contexto = new RegistroDocenteEntities())
                    {
                        
                        int idSeccion = contexto.Usuarios
                            .Where(u => u.id_usuario == idDocente)
                            .Select(u => u.id_seccion ?? 0)
                            .FirstOrDefault();

                        if (idSeccion == 0)
                        {
                            MessageBox.Show("No tiene sección asignada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        form.Alumno.id_seccion = idSeccion;
                        contexto.Estudiantes.Add(form.Alumno);
                        CargarEstudiantes(idDocente);
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (tablaAlumnos.Grid.SelectedRows.Count == 0) return;

            var fila = tablaAlumnos.Grid.SelectedRows[0];
            int idEstudiante = Convert.ToInt32(fila.Cells["colId"].Value);
            string nombre = $"{fila.Cells["colNombre"].Value} {fila.Cells["colApellido1"].Value}";
            int idDocente;
            if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
            {
                idDocente = (int)cmbDocentes.SelectedValue;
            }
            else
            {
                idDocente = Sesion.IdUsuario;
            }
            if (MessageBox.Show($"¿Eliminar al estudiante {nombre}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    var alumno = contexto.Estudiantes.Find(idEstudiante);
                    if (alumno != null)
                    {
                        contexto.Estudiantes.Remove(alumno);
                        contexto.SaveChanges();
                        CargarEstudiantes(idDocente);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (tablaAlumnos.Grid.SelectedRows.Count == 0) return;

            var fila = tablaAlumnos.Grid.SelectedRows[0];
            int idEstudiante = Convert.ToInt32(fila.Cells["colId"].Value);
            int idDocente;
            if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
            {
                idDocente = (int)cmbDocentes.SelectedValue;
            }
            else
            {
                idDocente = Sesion.IdUsuario;
            }
            using (var contexto = new RegistroDocenteEntities())
            {
                var alumno = contexto.Estudiantes.Find(idEstudiante);
                if (alumno == null) return;

                using (var form = new FormAgregarAlumno(alumno,idDocente))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        contexto.SaveChanges();
                        CargarEstudiantes(idDocente);
                    }
                    else
                    {
                        // Deshacer cambios si se canceló
                        contexto.Entry(alumno).Reload();
                    }
                }
            }
        }

        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void CargarEstudiantes(int idDocente)
        {
            try
            {
                tablaAlumnos.Grid.Rows.Clear();

                var estudiantes = alumnoController.ObtenerEstudiantesPorDocente(idDocente);

                if (estudiantes == null || estudiantes.Count == 0)
                {
                    MessageBox.Show("No se encontraron estudiantes para este docente",
                                  "Información",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;
                }

                foreach (var estudiante in estudiantes)
                {
                    tablaAlumnos.Grid.Rows.Add(
                        estudiante.id_estudiante,
                        estudiante.cedula_estudiante,
                        estudiante.primer_apellido,
                        estudiante.segundo_apellido,
                        estudiante.nombre_estudiante,
                        estudiante.telefono_encargado);
                }

                // Oculta la columna ID después de cargar los datos
                if (tablaAlumnos.Grid.Columns["colId"] != null)
                    tablaAlumnos.Grid.Columns["colId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estudiantes: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
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

        private void ConfigurarAutoformateoCedula()
        {
            tablaAlumnos.Grid.CellEndEdit += (sender, e) =>
            {
                if (e.ColumnIndex == tablaAlumnos.Grid.Columns["colCedula"].Index)
                {
                    var celda = tablaAlumnos.Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string cedula = celda.Value?.ToString()?.Replace("-", "") ?? "";

                    if (cedula.Length == 9)
                    {
                        celda.Value = $"{cedula[0]}-{cedula.Substring(1, 4)}-{cedula.Substring(5)}";
                        ValidarDuplicadosEnGrid(e.RowIndex, cedula);
                    }
                }
            };
        }

        private string NormalizarCedula(string cedula)
        {
            return Regex.Replace(cedula ?? "", "[^0-9]", "");
        }

        private void ValidarDuplicadosEnGrid(int filaActual, string cedulaNormalizada)
        {
            foreach (DataGridViewRow fila in tablaAlumnos.Grid.Rows)
            {
                if (fila.Index != filaActual && !fila.IsNewRow)
                {
                    string otraCedula = fila.Cells["colCedula"].Value?.ToString()?.Replace("-", "") ?? "";

                    if (otraCedula == cedulaNormalizada)
                    {
                        MessageBox.Show($"¡ALERTA! La cédula {cedulaNormalizada} ya existe en la fila {fila.Index + 1}",
                                      "Duplicado detectado",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);

                        tablaAlumnos.Grid.CurrentCell = tablaAlumnos.Grid.Rows[filaActual].Cells["colCedula"];
                        break;
                    }
                }
            }
        }

        private void Nombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsLetter(e.KeyChar) ||
                "áéíóúÁÉÍÓÚñÑ".Contains(e.KeyChar))
                return;

            if (e.KeyChar == ' ')
            {
                int pos = tb.SelectionStart;
                string text = tb.Text;

                if (pos == 0)
                {
                    e.Handled = true;
                    return;
                }

                if (pos > 0 && text.Length > 0 && text[pos - 1] == ' ')
                {
                    e.Handled = true;
                    return;
                }

                return;
            }

            if (e.KeyChar == '-' || e.KeyChar == '\'')
            {
                if ((sender as TextBox).SelectionStart == 0)
                {
                    e.Handled = true;
                    return;
                }
                return;
            }

            e.Handled = true;
        }

        private void Nombres_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;

            string texto = tb.Text.Trim();
            texto = Regex.Replace(texto, "\\s+", " ");

            var culture = new CultureInfo("es-CR");
            var ti = culture.TextInfo;
            texto = ti.ToTitleCase(texto.ToLower(culture));

            tb.Text = texto;
        }
    }
}