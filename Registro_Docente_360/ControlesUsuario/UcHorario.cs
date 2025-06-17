using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcHorario : UserControl
    {
        private bool modoEdicion = false;
        private ToolTip tooltipHorario = new ToolTip();

        public UcHorario()
        {
            InitializeComponent();
            this.Load += UcHorario_Load;
        }

        // Inicializa columnas y filas del horario
        private void UcHorario_Load(object sender, EventArgs e)
        {
            string[] horas = {
                "7:00 A 7:40", "7:40 A 8:20", "8:35 A 9:15", "9:15 A 9:55",
                "10:05 A 10:45", "10:45 A 11:25", "11:30 A 12:10",
                "12:30 A 1:10", "1:10 A 1:50", "2:00 A 2:40",
                "2:40 A 3:20", "3:35 A 4:15", "4:15 A 4:55", "5:00 A 5:40"
            };

            string[] materias = { "", "Español", "Matemáticas", "Ciencias", "Est. Sociales" };
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };

            dataGridPerso1.Grid.Columns.Clear();

            // Lección y horario (columnas fijas)
            dataGridPerso1.Grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Lección", Name = "colLeccion", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 10 });
            dataGridPerso1.Grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Horario", Name = "colHorario", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 30 });

            // Columnas con ComboBox para cada día
            foreach (var dia in dias)
            {
                dataGridPerso1.Grid.Columns.Add(new DataGridViewComboBoxColumn
                {
                    HeaderText = dia,
                    Name = "col" + dia,
                    DataSource = materias.ToList(),
                    FlatStyle = FlatStyle.Flat,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 20
                });
            }

            // Cargar las filas con horario
            dataGridPerso1.Grid.Rows.Clear();
            for (int i = 0; i < horas.Length; i++)
            {
                var fila = new DataGridViewRow();
                fila.CreateCells(dataGridPerso1.Grid);
                fila.Cells[0].Value = (i + 1);
                fila.Cells[1].Value = horas[i];
                dataGridPerso1.Grid.Rows.Add(fila);
            }

            // Pintar horario de la tarde
            for (int i = 7; i < dataGridPerso1.Grid.Rows.Count; i++)
                dataGridPerso1.Grid.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;

            // Eventos
            dataGridPerso1.Grid.CellValueChanged += dataGridPerso1_CellValueChanged;
            dataGridPerso1.Grid.CellEndEdit += dataGridPerso1_CellEndEdit;

            // Estado inicial
            dataGridPerso1.Grid.ReadOnly = true;
            txtSeccion.ReadOnly = true;
            txtSeccion.KeyPress += txtSeccion_KeyPress;

            // TODO: Cargar datos de BD aquí si se quiere mostrar horario previamente guardado
        }

        // Pinta celdas según la materia
        private void dataGridPerso1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 2) return;

            var celda = dataGridPerso1.Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string valor = celda.Value?.ToString().Trim().ToLower() ?? "";

            if (valor == "español") celda.Style.BackColor = Color.IndianRed;
            else if (valor == "matemáticas" || valor == "matematicas") celda.Style.BackColor = Color.Khaki;
            else if (valor == "ciencias") celda.Style.BackColor = Color.LightGreen;
            else if (valor == "inglés" || valor == "ingles") celda.Style.BackColor = Color.DodgerBlue;
            else if (valor == "est. sociales" || valor == "estudios sociales") celda.Style.BackColor = Color.DeepSkyBlue;
            else celda.Style.BackColor = Color.White;
        }

        private void dataGridPerso1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridPerso1.Grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dataGridPerso1.Grid.Invalidate();
        }

        // Solo números en el campo sección
        private void txtSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
            if (e.KeyChar == '-' && (sender as TextBox).Text.Contains("-")) e.Handled = true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void dataGridPerso1_Load(object sender, EventArgs e) { }

        // Activa/Desactiva modo edición
        private void btnEditarHorario_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                lblHorario.Text = "MODO EDICIÓN ACTIVADO";
                lblHorario.ForeColor = Color.Black;
                dataGridPerso1.Grid.ReadOnly = false;

                // Mantener columnas Lección y Horario como solo lectura
                if (dataGridPerso1.Grid.Columns.Count > 1)
                {
                    dataGridPerso1.Grid.Columns[0].ReadOnly = true;
                    dataGridPerso1.Grid.Columns[1].ReadOnly = true;
                }

                for (int i = 2; i < dataGridPerso1.Grid.Columns.Count; i++)
                    dataGridPerso1.Grid.Columns[i].ReadOnly = false;

                this.BackColor = Color.FromArgb(220, 250, 253);
                btnEditarHorario.Text = "GUARDAR HORARIO";
                tooltipHorario.SetToolTip(btnEditarHorario, "Haz clic para guardar los cambios");
                txtSeccion.ReadOnly = false;
                modoEdicion = true;
            }
            else
            {
                // TODO Aquí guardar a base de datos:
                // 1. txtSeccion.Text guardarla como sección
                // 2. Recorrer dataGridPerso1.Grid para extraer materias por día y hora

                lblHorario.Text = "Horario del Docente";
                lblHorario.ForeColor = Color.Teal;
                lblHorario.Font = new Font("Segoe UI", 21, FontStyle.Bold);
                this.BackColor = SystemColors.Control;

                dataGridPerso1.Grid.ReadOnly = true;
                txtSeccion.ReadOnly = true;
                btnEditarHorario.Text = "EDITAR HORARIO";
                tooltipHorario.SetToolTip(btnEditarHorario, "Haz clic para editar el horario");
                modoEdicion = false;
            }
        }
    }
}
