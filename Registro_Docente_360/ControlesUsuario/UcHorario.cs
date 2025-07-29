using Modelos.EntityFramework;
using Registro_Docente_360.Eventos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Registro_Docente_360.Interfaces;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcHorario : UserControl, IModoEdicion
    {
        private bool modoEdicion = false;
        private ToolTip tooltipHorario = new ToolTip();
        public bool EstaEnModoEdicion => modoEdicion;

        public UcHorario()
        {
            InitializeComponent();
            this.Load += UcHorario_Load;
        }

      

        // Inicializa columnas y filas del horario
        private void UcHorario_Load(object sender, EventArgs e)
        {

            // Eventos
            dataGridPerso1.Grid.CellValueChanged += dataGridPerso1_CellValueChanged;
            dataGridPerso1.Grid.CellEndEdit += dataGridPerso1_CellEndEdit;
            dataGridPerso1.Grid.CellMouseMove += Grid_CellMouseMove;

            string[] horas = {
                "7:00 A 7:40", "7:40 A 8:20", "8:35 A 9:15", "9:15 A 9:55",
                "10:05 A 10:45", "10:45 A 11:25", "11:30 A 12:10",
                "12:30 A 1:10", "1:10 A 1:50", "2:00 A 2:40",
                "2:40 A 3:20", "3:35 A 4:15", "4:15 A 4:55", "5:00 A 5:40"
            };

            string[] materias = { "", "Español", "Matemáticas", "Ciencias", "Estudios Sociales", "Complementarias" };
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

            // TODO: Cargar datos de BD aquí si se quiere mostrar horario previamente guardado

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);
                var seccion = contexto.Secciones.FirstOrDefault(s => s.id_seccion == usuario.id_seccion);

                lblNomDocente.Text = usuario.nombre_usuario;
                lblSecc.Text = $"{seccion.nombre_seccion}";


                var horarios = (from h in contexto.Horarios
                                join m in contexto.Materias on h.id_materia equals m.id_materia
                                where h.id_usuario == Sesion.IdUsuario
                                select new
                                {
                                    h.dia,
                                    h.hora_inicio,
                                    h.hora_fin,
                                    m.nombre_materia
                                }).ToList();

                foreach (DataGridViewRow fila in dataGridPerso1.Grid.Rows)
                {
                    if (fila.IsNewRow) continue;

                    string horaTexto = fila.Cells["colHorario"].Value?.ToString()?.Trim() ?? "";
                    string[] partesHora = horaTexto.Split(new string[] { " A " }, StringSplitOptions.None);

                    if (partesHora.Length != 2) continue;

                    if (!TimeSpan.TryParse(partesHora[0], out TimeSpan horaInicio)) continue;
                    if (!TimeSpan.TryParse(partesHora[1], out TimeSpan horaFin)) continue;

                    foreach (var horario in horarios)
                    {
                        if (horario.hora_inicio == horaInicio && horario.hora_fin == horaFin)
                        {
                            string colName = "col" + horario.dia;
                            if (dataGridPerso1.Grid.Columns.Contains(colName))
                            {
                                fila.Cells[colName].Value = horario.nombre_materia;
                            }
                        }
                    }

                }
            }
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
            else if (valor == "est. sociales" || valor == "estudios sociales") celda.Style.BackColor = Color.DeepSkyBlue;
            else if (valor == "complementarias" ) celda.Style.BackColor = Color.MediumPurple;
            else
            {
                // Si es una fila de la tarde (de la 8 en adelante)
                if (e.RowIndex >= 7)
                    celda.Style.BackColor = Color.LightYellow;
                else
                    celda.Style.BackColor = Color.White;
            }
  
        }

        private void dataGridPerso1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridPerso1.Grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dataGridPerso1.Grid.Invalidate();
        }

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
                modoEdicion = true;
            }
            else
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    int idUsuario = Sesion.IdUsuario;

                    var existentes = contexto.Horarios.Where(h => h.id_usuario == idUsuario).ToList();
                    contexto.Horarios.RemoveRange(existentes);
                    contexto.SaveChanges();

                    foreach (DataGridViewRow fila in dataGridPerso1.Grid.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        string horaTexto = fila.Cells["colHorario"].Value?.ToString()?.Trim() ?? "";
                        string[] partesHora = horaTexto.Split(new string[] { " A " }, StringSplitOptions.None);

                        if (partesHora.Length != 2) continue;

                        TimeSpan horaInicio = TimeSpan.Parse(partesHora[0]);
                        TimeSpan horaFin = TimeSpan.Parse(partesHora[1]);

                        for (int col = 2; col < dataGridPerso1.Grid.Columns.Count; col++)
                        {
                            string dia = dataGridPerso1.Grid.Columns[col].HeaderText;
                            string materiaNombre = fila.Cells[col].Value?.ToString();

                            if (!string.IsNullOrWhiteSpace(materiaNombre))
                            {
                                var materia = contexto.Materias.FirstOrDefault(m => m.nombre_materia.ToLower() == materiaNombre.ToLower());

                                if (materia != null)
                                {
                                    Horarios nuevo = new Horarios
                                    {
                                        id_usuario = idUsuario,
                                        id_materia = materia.id_materia,
                                        dia = dia,
                                        hora_inicio = horaInicio,
                                        hora_fin = horaFin
                                    };

                                    contexto.Horarios.Add(nuevo);
                                }
                            }
                        }
                    }

                    contexto.SaveChanges();
                    MessageBox.Show("Horario Guardado correctamente");
                }

                lblHorario.Text = "Horario del Docente";
                lblHorario.ForeColor = Color.Teal;
                lblHorario.Font = new Font("Segoe UI", 21, FontStyle.Bold);
                this.BackColor = SystemColors.Control;

                dataGridPerso1.Grid.ReadOnly = true;
                btnEditarHorario.Text = "EDITAR HORARIO";
                tooltipHorario.SetToolTip(btnEditarHorario, "Haz clic para editar el horario");
                modoEdicion = false;
            }
        }

        public void CancelarModoEdicion()
        {
            if (modoEdicion)
            {
                // Restablecer UI y lógica de salida del modo edición
                lblHorario.Text = "Horario del Docente";
                lblHorario.ForeColor = Color.Teal;
                lblHorario.Font = new Font("Segoe UI", 21, FontStyle.Bold);
                this.BackColor = SystemColors.Control;

                dataGridPerso1.Grid.ReadOnly = true;
                btnEditarHorario.Text = "EDITAR HORARIO";
                tooltipHorario.SetToolTip(btnEditarHorario, "Haz clic para editar el horario");

                modoEdicion = false;
            }
        }

        private void Grid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!modoEdicion)
            {
                dataGridPerso1.Grid.Cursor = Cursors.Default;
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex >= 2)
            {
                var col = dataGridPerso1.Grid.Columns[e.ColumnIndex];
                if (col is DataGridViewComboBoxColumn)
                {
                    dataGridPerso1.Grid.Cursor = Cursors.Hand;
                    return;
                }
            }

            dataGridPerso1.Grid.Cursor = Cursors.Default;
        }
    }
}
