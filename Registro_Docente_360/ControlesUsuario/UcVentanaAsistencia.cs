using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Linq;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcVentanaAsistencia : UserControl
    {

        private AlumnoController alumnoController = new AlumnoController();
        private List<Estudiantes> estudiantesCargados = new List<Estudiantes>();

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

        private void UcVentanaAsistencia_Load(object sender, EventArgs e)
        {
            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios
                    .Include(u => u.Roles)
                    .FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);

                if (esAdministrador)
                {
                    // Configuración UI para administradores
                    cmbDocentes.Visible = true;
                    lblGrupo.Visible = false;

                    // Carga optimizada de docentes (por permiso ID 1)
                    var docentes = contexto.Usuarios
                        .Include("Roles.Roles_Permisos")
                        .Include(u => u.Secciones) // Asegurar que carga la relación secciones
                        .Where(u =>
                         u.Roles.Roles_Permisos.Any(rp => rp.id_permiso == 1) && // Tiene permiso docente
                        !u.Roles.Roles_Permisos.Any(rp => rp.id_permiso == 2) && // No es admin
                         u.id_seccion != null // Tiene sección asignada
                        )
                        .Select(u => new
                         {
                         u.id_usuario,
                         NombreCompleto = u.nombre_usuario + " " + u.apellido_usuario,
                         Seccion = u.Secciones.nombre_seccion
                         })
                         .OrderBy(d => d.NombreCompleto)
                         .ToList();

                    // Configurar ComboBox
                    cmbDocentes.DisplayMember = "NombreCompleto";
                    cmbDocentes.ValueMember = "id_usuario";
                    cmbDocentes.DataSource = docentes;
                    cmbDocentes.SelectedIndexChanged += cmbDocentes_SelectedIndexChanged;

                    if (docentes.Any())
                    {
                        cmbDocentes.SelectedIndex = 0;
                        // Disparar carga inicial de asistencia
                        CargarAsistenciaDesdeBD((int)cmbDocentes.SelectedValue);
                    }
                }
                else
                {
                    // Configuración para docentes
                    cmbDocentes.Visible = false;
                    CargarAsistenciaDesdeBD(Sesion.IdUsuario);
                    dataGridPerso1.Grid.CellValueChanged += dataGridPerso1_Grid_CellValueChanged;
                }
            }
        }

        private void CargarAsistenciaDesdeBD(int docente)
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
                estudiantesCargados = alumnoController.ObtenerEstudiantesPorDocente(docente);
                DateTime fechaInicio = DateTime.ParseExact(fechaInicioSeleccionada, "dd/MM/yyyy", null);

                using (SqlConnection conn = new SqlConnection("data source=192.168.195.168\\SQLEXPRESS;Initial Catalog=RegistroDocente;User Id=Admin;Password=12345;MultipleActiveResultSets=True;Encrypt=False;Application Name=EntityFramework"))
                {

                    conn.Open();

                    foreach (var estudiante in estudiantesCargados)
                    {
                        string nombreCompleto = $"{estudiante.primer_apellido} {estudiante.segundo_apellido}, {estudiante.nombre_estudiante}";
                        object[] fila = new object[6];
                        fila[0] = nombreCompleto;

                        var clases = alumnoController.ObtenerClasesDelDocenteYEstudiante(docente, estudiante.id_estudiante);

                        for (int i = 0; i < 5; i++)
                        {
                            DateTime fechaDia = fechaInicio.AddDays(i);
                            string estado = "";

                            foreach (var clase in clases)
                            {
                                using (SqlCommand cmd = new SqlCommand(
                                    "SELECT estado FROM Asistencia WHERE id_estudiante = @idest AND fecha = @fecha", conn))
                                {
                                    cmd.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                                    cmd.Parameters.AddWithValue("@fecha", fechaDia);

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





        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Fechas de los periodos
                DateTime fechaInicioPrimerPeriodo = new DateTime(2025, 2, 3); // 3 de febrero
                DateTime fechaFinPrimerPeriodo = new DateTime(2025, 5, 25);  // 25 de mayo
                DateTime fechaInicioSegundoPeriodo = new DateTime(2025, 5, 26); // 26 de mayo
                DateTime fechaFinSegundoPeriodo = new DateTime(2025, 12, 10);  // 10 de diciembre

                // La fecha seleccionada para la semana (debe ser calculada correctamente)
                DateTime fechaInicioSemanaSeleccionada = DateTime.ParseExact(fechaInicioSeleccionada, "dd/MM/yyyy", null);

                // Determinar el periodo según la fecha seleccionada
                string periodoSeleccionado = "";
                DateTime fechaInicio = DateTime.MinValue;
                DateTime fechaFin = DateTime.MinValue;

                if (fechaInicioSemanaSeleccionada >= fechaInicioPrimerPeriodo && fechaInicioSemanaSeleccionada <= fechaFinPrimerPeriodo)
                {
                    periodoSeleccionado = "Primer Periodo";
                    fechaInicio = fechaInicioPrimerPeriodo;
                    fechaFin = fechaFinPrimerPeriodo;
                }
                else if (fechaInicioSemanaSeleccionada >= fechaInicioSegundoPeriodo && fechaInicioSemanaSeleccionada <= fechaFinSegundoPeriodo)
                {
                    periodoSeleccionado = "Segundo Periodo";
                    fechaInicio = fechaInicioSegundoPeriodo;
                    fechaFin = fechaFinSegundoPeriodo;
                }
                else
                {
                    MessageBox.Show("La fecha seleccionada está fuera de los periodos definidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Variables para contabilizar cambios
                int cambiosRealizados = 0;

                using (SqlConnection conn = new SqlConnection("data source=192.168.195.168\\SQLEXPRESS;Initial Catalog=RegistroDocente;User Id=Admin;Password=12345;MultipleActiveResultSets=True;Encrypt=False;Application Name=EntityFramework"))
                {
                    conn.Open();

                    foreach (DataGridViewRow fila in dataGridPerso1.Grid.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        string nombreEstudiante = fila.Cells[0].Value?.ToString()?.Trim().ToLower();

                        var estudiante = estudiantesCargados.FirstOrDefault(est =>
                            $"{est.primer_apellido} {est.segundo_apellido}, {est.nombre_estudiante}".ToLower() == nombreEstudiante);

                        if (estudiante == null)
                        {
                            continue;
                        }

                        // 1. Guardar o actualizar las asistencias
                        for (int i = 1; i <= 5; i++) // 5 días de la semana
                        {
                            string estado = fila.Cells[i].Value?.ToString()?.Trim(); // Obtener el estado de cada celda

                            if (!string.IsNullOrEmpty(estado))
                            {
                                // Aquí calculamos la fecha de cada día de la semana basado en la semana seleccionada
                                DateTime fechaDia = fechaInicioSemanaSeleccionada.AddDays(i - 1); // Ajustamos para cada día de la semana (Lunes-Viernes)

                                // Verificar si ya existe un registro de asistencia para ese estudiante y fecha
                                using (SqlCommand checkAsistencia = new SqlCommand(
                                    "SELECT COUNT(*) FROM Asistencia WHERE id_estudiante = @idest AND fecha = @fecha", conn))
                                {
                                    checkAsistencia.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                                    checkAsistencia.Parameters.AddWithValue("@fecha", fechaDia);

                                    int count = (int)checkAsistencia.ExecuteScalar();

                                    if (count > 0) // Si ya existe, actualizamos
                                    {
                                        using (SqlCommand updateAsistencia = new SqlCommand(
                                            "UPDATE Asistencia SET estado = @estado WHERE id_estudiante = @idest AND fecha = @fecha", conn))
                                        {
                                            updateAsistencia.Parameters.AddWithValue("@estado", estado);
                                            updateAsistencia.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                                            updateAsistencia.Parameters.AddWithValue("@fecha", fechaDia);

                                            int filasAfectadas = updateAsistencia.ExecuteNonQuery();
                                            if (filasAfectadas > 0)
                                            {
                                                cambiosRealizados += filasAfectadas;
                                            }
                                        }
                                    }
                                    else // Si no existe, insertamos
                                    {
                                        using (SqlCommand insertAsistencia = new SqlCommand(
                                            "INSERT INTO Asistencia (id_estudiante, fecha, estado) VALUES (@idest, @fecha, @estado)", conn))
                                        {
                                            insertAsistencia.Parameters.AddWithValue("@estado", estado);
                                            insertAsistencia.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                                            insertAsistencia.Parameters.AddWithValue("@fecha", fechaDia);

                                            int filasInsertadas = insertAsistencia.ExecuteNonQuery();
                                            if (filasInsertadas > 0)
                                            {
                                                cambiosRealizados += filasInsertadas;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        // 2. Calcular la penalización y la nueva nota de asistencia
                        string[] estadosDeAsistencia = { "Ausente", "Tarde" };
                        int penalizaciones = 0;

                        // Consultar las asistencias del estudiante en el rango de fechas
                        using (SqlCommand cmd = new SqlCommand(
                            "SELECT COUNT(*) FROM Asistencia WHERE id_estudiante = @idest AND fecha >= @fechaInicio AND fecha <= @fechaFin AND estado IN ('Ausente', 'Tarde')", conn))
                        {
                            cmd.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                            cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                            cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                            penalizaciones = (int)cmd.ExecuteScalar();
                        }

                        // 3. Calcular la nueva nota de asistencia
                        decimal asistenciaFinal = Math.Max(10 - (penalizaciones * 0.2m), 0); // Penalización

                        // 4. Actualizar la nota de asistencia en la tabla Notas
                        using (SqlCommand updateNota = new SqlCommand(
                            "UPDATE Notas SET asistencia = @asistencia WHERE id_estudiante = @idest AND periodo = @periodo", conn))
                        {
                            updateNota.Parameters.AddWithValue("@asistencia", asistenciaFinal);
                            updateNota.Parameters.AddWithValue("@idest", estudiante.id_estudiante);
                            updateNota.Parameters.AddWithValue("@periodo", periodoSeleccionado);

                            int filasAfectadasNota = updateNota.ExecuteNonQuery();
                            if (filasAfectadasNota > 0)
                            {
                                cambiosRealizados += filasAfectadasNota;
                            }
                        }
                    }

                    MessageBox.Show($"Asistencia guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar asistencia y actualizar la nota: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Dibuja el borde del item
            e.DrawFocusRectangle();
        }

        private void cmbDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocentes.SelectedItem != null)
            {
                int idDocenteSeleccionado = (int)cmbDocentes.SelectedValue;
                CargarAsistenciaDesdeBD(idDocenteSeleccionado);
            }
        }
    }
}
