using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelos;
using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcVentanaAsistencia : UserControl
    {

        private AlumnoController alumnoController = new AlumnoController();
        private List<Estudiantes> estudiantesCargados = new List<Estudiantes>();
        public DataGridView dataGridPerso => dataGridPerso1.Grid;

        private int anhoSeleccionado;
        private string fechaInicioSeleccionada;
        private string fechaFinSeleccionada;
        private string SeccionGuardada;




        public UcVentanaAsistencia()
        {
            InitializeComponent();
            this.Load += UcVentanaAsistencia_Load;
            dataGridPerso1.Grid.EditingControlShowing += Grid_EditingControlShowing;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void CargarAsistenciaDesdeBD()
        {
            try
            {
                dataGridPerso1.Grid.Columns.Clear();
                dataGridPerso1.Grid.Rows.Clear();

                // Agregar columna de estudiante
                DataGridViewTextBoxColumn colEstudiante = new DataGridViewTextBoxColumn
                {
                    HeaderText = "Estudiante",
                    ReadOnly = true,
                    Width = 200
                };
                dataGridPerso1.Grid.Columns.Add(colEstudiante);

                // Columnas de días con opciones
                string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
                string[] opciones = { "Presente", "Ausente", "Justificado", "Tarde" };

                foreach (string dia in dias)
                {
                    DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn
                    {
                        HeaderText = dia,
                        Width = 100,
                        FlatStyle = FlatStyle.Flat,
                        DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                    };
                    col.Items.AddRange(opciones);
                    dataGridPerso1.Grid.Columns.Add(col);
                }

                // Obtener estudiantes
                estudiantesCargados = alumnoController.ObtenerEstudiantesPorDocente(Sesion.IdUsuario);
                DateTime fechaInicio = DateTime.ParseExact(fechaInicioSeleccionada, "dd/MM/yyyy", null);

                using (SqlConnection conn = new SqlConnection("Data Source=JOSE\\SQLEXPRESS;Initial Catalog=RegistroDocente;Integrated Security=True"))
                {
                    conn.Open();

                    foreach (var estudiante in estudiantesCargados)
                    {
                        string nombreCompleto = $"{estudiante.primer_apellido} {estudiante.segundo_apellido}, {estudiante.nombre_estudiante}";
                        object[] fila = new object[6];
                        fila[0] = nombreCompleto;

                        var clases = alumnoController.ObtenerClasesDelDocenteYEstudiante(Sesion.IdUsuario, estudiante.id_estudiante);

                        for (int i = 0; i < 5; i++)
                        {
                            DateTime fechaDia = fechaInicio.AddDays(i);
                            string estado = "";

                            foreach (var clase in clases)
                            {
                                using (SqlCommand cmd = new SqlCommand(
                                    "SELECT estado FROM Asistencia WHERE id_estudiante = @idest AND fecha = @fecha AND id_clase = @idclase", conn))
                                {
                                    cmd.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                                    cmd.Parameters.AddWithValue("@fecha", fechaDia);
                                    cmd.Parameters.AddWithValue("@idclase", clase.id_clase);

                                    var result = cmd.ExecuteScalar();
                                    if (result != null)
                                    {
                                        estado = result.ToString();
                                        break;
                                    }
                                }
                            }

                            fila[i + 1] = estado;
                        }

                        // Agregar fila y aplicar color
                        int rowIndex = dataGridPerso1.Grid.Rows.Add(fila);

                        // Aplicar color a las celdas
                        for (int colIndex = 1; colIndex <= 5; colIndex++)
                        {
                            var cell = dataGridPerso1.Grid.Rows[rowIndex].Cells[colIndex];
                            string valor = cell.Value?.ToString().Trim().ToLower() ?? "";

                            if (valor == "ausente") cell.Style.BackColor = Color.IndianRed;
                            else if (valor == "tarde") cell.Style.BackColor = Color.Khaki;
                            else if (valor == "presente") cell.Style.BackColor = Color.LightGreen;
                            else if (valor == "justificado") cell.Style.BackColor = Color.DeepSkyBlue;
                            else cell.Style.BackColor = Color.White;
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asistencia: " + ex.Message);
            }
        }






        public void ActualizarCabecera(int anho, string fechaInicio, string fechaFin)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);
                if (usuario != null)
                {
                    var seccion = contexto.Secciones.FirstOrDefault(s => s.id_seccion == usuario.id_seccion);
                    if (seccion != null)
                    {
                        lblGrupo.Text = $"Sección: {seccion.nombre_seccion} – Año Lectivo: {anho}";
                        SeccionGuardada = seccion.nombre_seccion;
                    }
                    else
                    {
                        lblGrupo.Text = $"Sección no asignada – Año Lectivo: {anho}";
                    }
                }
            }

            lblSemana.Text = $"Semana Seleccionada: del {fechaInicio} al {fechaFin}";

            anhoSeleccionado = anho;
            fechaInicioSeleccionada = fechaInicio;
            fechaFinSeleccionada = fechaFin;
        }




        private void btnVolver_Click(object sender, EventArgs e)
        {
            var formPadre = this.FindForm() as MenuPrincipal;

            if (formPadre != null)
            {
                if (formPadre.ucFechas == null)
                {
                    formPadre.ucFechas = new UcFechas();
                    formPadre.ucFechas.OnFechaSeleccionada += formPadre.UcFechas_OnFechaSeleccionada;
                }

                // ✅ Usar la última selección almacenada
                formPadre.ucFechas.InicializarFechas(
                    Sesion.UltimoAnhoSeleccionado,
                    Sesion.UltimoMesIndex,
                    Sesion.UltimaSemanaIndex
                );

                formPadre.MostrarUserControl(formPadre.ucFechas);
            }
        }



        private void dataGridPerso1_Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 1 || e.ColumnIndex > 5) return;

            var cell = dataGridPerso1.Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string valor = cell.Value?.ToString().Trim().ToLower() ?? "";

            if (valor == "ausente") cell.Style.BackColor = Color.IndianRed;
            else if (valor == "tarde") cell.Style.BackColor = Color.Khaki;
            else if (valor == "presente") cell.Style.BackColor = Color.LightGreen;
            else if (valor == "justificado") cell.Style.BackColor = Color.DeepSkyBlue;
            else cell.Style.BackColor = Color.White;
        }



        private void UcVentanaAsistencia_Load(object sender, EventArgs e)
        {
            CargarAsistenciaDesdeBD();
            dataGridPerso1.Grid.CellValueChanged += dataGridPerso1_Grid_CellValueChanged;

        }

        private void dataGridPerso1_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
                DateTime fechaInicio = DateTime.ParseExact(fechaInicioSeleccionada, "dd/MM/yyyy", null);
                int cambiosRealizados = 0;

                using (SqlConnection conn = new SqlConnection("Data Source=JOSE\\SQLEXPRESS;Initial Catalog=RegistroDocente;Integrated Security=True"))
                {
                    conn.Open();

                    StringBuilder debugLog = new StringBuilder();
                    debugLog.AppendLine($"Estudiantes cargados: {estudiantesCargados.Count}");

                    foreach (DataGridViewRow fila in dataGridPerso1.Grid.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        string nombreEstudiante = fila.Cells[0].Value?.ToString()?.Trim().ToLower();
                        debugLog.AppendLine($"\nFila: {nombreEstudiante}");

                        // Comparación corregida con coma y minúsculas
                        var estudiante = estudiantesCargados.FirstOrDefault(est =>
                            $"{est.primer_apellido} {est.segundo_apellido}, {est.nombre_estudiante}".ToLower() == nombreEstudiante);

                        if (estudiante == null)
                        {
                            debugLog.AppendLine("❌ No se encontró coincidencia en estudiantes.");
                            continue;
                        }

                        var clases = alumnoController.ObtenerClasesDelDocenteYEstudiante(Sesion.IdUsuario, estudiante.id_estudiante);

                        foreach (var clase in clases)
                        {
                            for (int i = 1; i <= 5; i++)
                            {
                                string estado = fila.Cells[i].Value?.ToString();
                                if (string.IsNullOrEmpty(estado)) continue;

                                DateTime fechaDia = fechaInicio.AddDays(i - 1);

                                using (SqlCommand check = new SqlCommand(
                                    "SELECT COUNT(*) FROM Asistencia WHERE id_estudiante = @idest AND fecha = @fecha AND id_clase = @idclase", conn))
                                {
                                    check.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                                    check.Parameters.AddWithValue("@fecha", fechaDia);
                                    check.Parameters.AddWithValue("@idclase", clase.id_clase);

                                    int count = (int)check.ExecuteScalar();

                                    if (count > 0)
                                    {
                                        using (SqlCommand update = new SqlCommand(
                                            "UPDATE Asistencia SET estado = @estado WHERE id_estudiante = @idest AND fecha = @fecha AND id_clase = @idclase", conn))
                                        {
                                            update.Parameters.AddWithValue("@estado", estado);
                                            update.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                                            update.Parameters.AddWithValue("@fecha", fechaDia);
                                            update.Parameters.AddWithValue("@idclase", clase.id_clase);
                                            cambiosRealizados += update.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        using (SqlCommand insert = new SqlCommand(
                                            "INSERT INTO Asistencia (id_estudiante, id_clase, fecha, estado) VALUES (@idest, @idclase, @fecha, @estado)", conn))
                                        {
                                            insert.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                                            insert.Parameters.AddWithValue("@idclase", clase.id_clase);
                                            insert.Parameters.AddWithValue("@fecha", fechaDia);
                                            insert.Parameters.AddWithValue("@estado", estado);
                                            cambiosRealizados += insert.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    MessageBox.Show("Asistencia guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar asistencia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox comboBox)
            {
                comboBox.Cursor = Cursors.Hand;
                comboBox.DrawMode = DrawMode.OwnerDrawFixed; // Habilitar el evento de dibujo
                comboBox.DrawItem += ComboBox_DrawItem; // Asignar el evento de dibujo
            }
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Verificar si el índice es válido
            if (e.Index < 0 || e.Index >= comboBox.Items.Count) return;

            string itemText = comboBox.Items[e.Index].ToString();

            // Define los colores según el texto del item
            Color itemColor = Color.White; // Color predeterminado

            if (itemText == "Presente") itemColor = Color.LightGreen;
            else if (itemText == "Ausente") itemColor = Color.IndianRed;
            else if (itemText == "Justificado") itemColor = Color.DeepSkyBlue;
            else if (itemText == "Tarde") itemColor = Color.Khaki;

            // Dibuja el fondo del item
            e.DrawBackground();

            // Dibuja el texto del item con el color de fondo definido
            using (Brush brush = new SolidBrush(itemColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            // Dibuja el texto
            using (Brush textBrush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(itemText, e.Font, textBrush, e.Bounds);
            }


        }
    }
}
