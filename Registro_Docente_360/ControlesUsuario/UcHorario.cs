using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            cmbDocentes.SelectedIndexChanged += cmbDocentes_SelectedIndexChanged;
        }

        private void UcHorario_Load(object sender, EventArgs e)
        {
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

            dataGridPerso1.Grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Lección", Name = "colLeccion", ReadOnly = true });
            dataGridPerso1.Grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Horario", Name = "colHorario", ReadOnly = true });

            foreach (var dia in dias)
            {
                dataGridPerso1.Grid.Columns.Add(new DataGridViewComboBoxColumn
                {
                    HeaderText = dia,
                    Name = "col" + dia,
                    DataSource = materias.ToList(),
                    FlatStyle = FlatStyle.Flat
                });
            }

            dataGridPerso1.Grid.Rows.Clear();
            for (int i = 0; i < horas.Length; i++)
            {
                var fila = new DataGridViewRow();
                fila.CreateCells(dataGridPerso1.Grid);
                fila.Cells[0].Value = (i + 1);
                fila.Cells[1].Value = horas[i];
                dataGridPerso1.Grid.Rows.Add(fila);
            }

            for (int i = 7; i < dataGridPerso1.Grid.Rows.Count; i++)
                dataGridPerso1.Grid.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;

            dataGridPerso1.Grid.ReadOnly = true;

            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);

                if (esAdministrador)
                {
                    // Modo administrador
                    lblNomDocente.Visible = false;
                    lblSecc.Visible = false;
                    cmbDocentes.Visible = true;
                    lblSeccion.Visible = false;

                    // Cargar docentes
                    var docentes = contexto.Usuarios
                        .Where(u => u.Roles.nombre_rol == "Docente" && u.estado_usuario == "A") //cambiar a que agarre el permiso no el rol
                        .Select(u => new
                        {
                            u.id_usuario,
                            NombreCompleto = u.nombre_usuario + " " + u.apellido_usuario
                        }).ToList();

                    cmbDocentes.DataSource = docentes;
                    cmbDocentes.DisplayMember = "NombreCompleto";
                    cmbDocentes.ValueMember = "id_usuario";
                }
                else
                {
                    var seccion = contexto.Secciones.FirstOrDefault(s => s.id_seccion == usuario.id_seccion);
                    lblNomDocente.Text = usuario.nombre_usuario;
                    lblSecc.Text = seccion?.nombre_seccion ?? "";
                    cmbDocentes.Visible = false;

                    CargarHorario(usuario.id_usuario);
                }
            }
        }

        private void CargarHorario(int idUsuario)
        {
            foreach (DataGridViewRow fila in dataGridPerso1.Grid.Rows)
                for (int col = 2; col < dataGridPerso1.Grid.Columns.Count; col++)
                    fila.Cells[col].Value = "";

            using (var contexto = new RegistroDocenteEntities())
            {
                var horarios = (from h in contexto.Horarios
                                join m in contexto.Materias on h.id_materia equals m.id_materia
                                where h.id_usuario == idUsuario
                                select new
                                {
                                    h.dia,
                                    h.hora_inicio,
                                    h.hora_fin,
                                    m.nombre_materia
                                }).ToList();

                foreach (DataGridViewRow fila in dataGridPerso1.Grid.Rows)
                {
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

        private void cmbDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocentes.SelectedValue is int idDocente)
            {
                CargarHorario(idDocente);
            }
        }

        private void dataGridPerso1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 2) return;

            var celda = dataGridPerso1.Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string valor = celda.Value?.ToString().Trim().ToLower() ?? "";

            if (valor == "español") celda.Style.BackColor = Color.IndianRed;
            else if (valor == "matemáticas" || valor == "matematicas") celda.Style.BackColor = Color.Khaki;
            else if (valor == "ciencias") celda.Style.BackColor = Color.LightGreen;
            else if (valor == "estudios sociales") celda.Style.BackColor = Color.DeepSkyBlue;
            else if (valor == "complementarias") celda.Style.BackColor = Color.MediumPurple;
            else celda.Style.BackColor = e.RowIndex >= 7 ? Color.LightYellow : Color.White;
        }

        private void dataGridPerso1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridPerso1.Grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dataGridPerso1.Grid.Invalidate();
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

        public void CancelarModoEdicion()
        {
            if (modoEdicion)
            {
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

                    if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
                    {
                        idUsuario = (int)cmbDocentes.SelectedValue;
                    }
                   
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

                    string descripcion = "Horario Guardado";
                    string accion = "Nuevo horario";
                    string modulo = "Horario";

                    AlumnoController controlador = new AlumnoController();
                    controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
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
    }
}


