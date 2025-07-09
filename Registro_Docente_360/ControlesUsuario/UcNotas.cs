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
            tablaNotas.Grid.Columns.Clear();

            tablaNotas.Grid.Columns.Add("colCedula", "Cédula");
            tablaNotas.Grid.Columns.Add("colApellido1", "Primer Apellido");
            tablaNotas.Grid.Columns.Add("colApellido2", "Segundo Apellido");
            tablaNotas.Grid.Columns.Add("colNombre", "Nombre");
            tablaNotas.Grid.Columns.Add("colAsistencia", "Asistencia");
            tablaNotas.Grid.Columns.Add("colExamenes", "Exámenes");
            tablaNotas.Grid.Columns.Add("colTareas", "Tareas");
            tablaNotas.Grid.Columns.Add("colCotidiano", "Cotidiano");
            tablaNotas.Grid.Columns.Add("colNotaFinal", "Nota Final");

            //solo lectura por defecto
            tablaNotas.Grid.ReadOnly = true;
            tablaNotas.Grid.AllowUserToAddRows = false;
            tablaNotas.Grid.AllowUserToDeleteRows = false;

            //no se pueden editar
            tablaNotas.Grid.Columns["colAsistencia"].ReadOnly = true;
            tablaNotas.Grid.Columns["colNotaFinal"].ReadOnly = true;
            tablaNotas.Grid.Columns["colCedula"].ReadOnly = true;
            tablaNotas.Grid.Columns["colApellido1"].ReadOnly = true;
            tablaNotas.Grid.Columns["colApellido2"].ReadOnly = true;
            tablaNotas.Grid.Columns["colNombre"].ReadOnly = true;

            //ocultar gestiones, hasta que se active el boton
            PanelAcciones.Visible = false;

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
                    if (col.Name == "colExamenes" || col.Name == "colTareas" || col.Name == "colCotidiano")
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


        private void tablaNotas_Load(object sender, EventArgs e)
        {
        }
    }
}
