using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registro_Docente_360.ControlesUsuario;
using Registro_Docente_360.Controladores;

namespace Registro_Docente_360.Forms
{
    public partial class UcAlumnos : UserControl
    {
        private bool modoEdicion = false;
        private ToolTip tooltipAlumnos = new ToolTip();
        private AlumnoController alumnoController = new AlumnoController();

        public UcAlumnos()
        {
            InitializeComponent();
            this.Load += UcAlumnos_Load;
        }

        /// <summary>
        /// Configura la tabla de alumnos al cargar el control.
        /// </summary>
        private void UcAlumnos_Load(object sender, EventArgs e)
        {
            // Configuración inicial de columnas
            tablaAlumnos.Grid.Columns.Clear();

            tablaAlumnos.Grid.Columns.Add("colCedula", "Cedula");
            tablaAlumnos.Grid.Columns.Add("colApellido1", "Primer Apellido");
            tablaAlumnos.Grid.Columns.Add("colApellido2", "Segundo Apellido");
            tablaAlumnos.Grid.Columns.Add("colNombre", "Nombre");

            tablaAlumnos.Grid.ReadOnly = true;
            tablaAlumnos.Grid.AllowUserToAddRows = false;
            tablaAlumnos.Grid.AllowUserToDeleteRows = false;

            PanelAcciones.Visible = false;

            tablaAlumnos.Grid.EditingControlShowing += Grid_EditingControlShowing;
            tablaAlumnos.Grid.CellValidating += Grid_CellValidating;
        }

        /// <summary>
        /// Controla la edición de la celda y restringe la entrada si es necesario.
        /// </summary>
        private void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (tablaAlumnos.Grid.CurrentCell != null &&
                tablaAlumnos.Grid.CurrentCell.ColumnIndex == tablaAlumnos.Grid.Columns["colCedula"].Index)
            {
                if (e.Control is TextBox tb)
                {
                    tb.CharacterCasing = CharacterCasing.Upper;
                    tb.KeyPress -= Cedula_KeyPress;
                    tb.KeyPress += Cedula_KeyPress;
                }
            }
            else
            {
                if (e.Control is TextBox tb)
                {
                    tb.CharacterCasing = CharacterCasing.Normal;
                    tb.KeyPress -= Cedula_KeyPress;
                }
            }
        }

        /// <summary>
        /// Cuando uno termina de editar una celda de cédula, se valida el dato escrito.
        /// </summary>
        private void Grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
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

        /// <summary>
        /// Activa y desactiva el modo de edición en la tabla.
        /// </summary>
        private void btnEditarHorario_Click_1(object sender, EventArgs e)
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
                btnEditarHorario.Text = "TERMINAR EDICIÓN";
                tooltipAlumnos.SetToolTip(btnEditarHorario, "Haz clic para guardar los cambios");
                modoEdicion = true;
                PanelAcciones.Visible = true;
            }
            else
            {
                lblAlumnos.Text = "Listado de Alumnos";
                lblAlumnos.ForeColor = Color.Teal;
                lblAlumnos.Font = new Font("Segoe UI", 21, FontStyle.Bold);

                tablaAlumnos.Grid.ReadOnly = true;
                tooltipAlumnos.SetToolTip(btnEditarHorario, "Haz clic para editar los datos");
                this.BackColor = SystemColors.Control;

                PanelAcciones.Visible = false;
                btnEditarHorario.Text = "EDITAR ALUMNOS";
                modoEdicion = false;
            }
        }

        /// <summary>
        /// Agrega una nueva fila vacía.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            tablaAlumnos.Grid.Rows.Add();
        }

        /// <summary>
        /// Elimina la fila seleccionada previa confirmación.
        /// </summary>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var fila = tablaAlumnos.Grid.CurrentRow;

            if (fila != null && !fila.IsNewRow)
            {
                var confirm = MessageBox.Show("¿Seguro que quiere eliminar la fila?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    tablaAlumnos.Grid.Rows.Remove(fila);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila válida para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Acción de guardar alumnos. 
        /// Debe validarse desde AlumnoController antes de guardar en la base.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // TODO: Conectar este evento con un controlador que guarde los datos en la base de datos.
        }

        /// <summary>
        /// Cancela los cambios realizados y recarga los datos (a implementar).
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Desea descartar todos los cambios no guardados?", "Cancelar edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                // TODO: Implementar recarga de datos desde la base de datos.
            }
        }

        private void tablaAlumnos_Load(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void dataGridPerso1_Load(object sender, EventArgs e) { }
        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e) { }
    }
}
