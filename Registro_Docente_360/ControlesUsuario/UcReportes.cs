using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class UcReportes : UserControl
    {
        private int anhoSeleccionado;
        private int mesSeleccionado;
        private int semanaSeleccionada;

        public UcReportes()
        {
            InitializeComponent();
            this.Resize += (s, e) => CentrarMiniContenedor();


            cmbMaterias.Visible = false;  // Inicialmente oculto
            cmbEstudiantes.Visible = false;  // Inicialmente oculto
            lblEstudiante.Visible = false;
            lblMateria.Visible = false;

            PanelFechas.Visible = false;
            tableFechas.Visible = false;
            panelPeriodo.Visible = false;

            CentrarMiniContenedor();

            cmbTiempo.SelectedIndexChanged += cbTiempoReporte_SelectedIndexChanged;

            cmbMeses.SelectedIndexChanged += cmbMeses_SelectedIndexChanged;
            cmbAnhos.SelectedIndexChanged += cmbAnhos_SelectedIndexChanged;
            cmbAnhos2.SelectedIndexChanged += cmbAnhos_SelectedIndexChanged;

        }

        private void InicializarFechas()
        {
            for (int anho = 2025; anho <= DateTime.Now.Year + 5; anho++)
            {
                cmbAnhos.Items.Add(anho.ToString());
                cmbAnhos2.Items.Add(anho.ToString());
            }


            cmbAnhos.SelectedItem = DateTime.Now.Year.ToString();
            cmbAnhos2.SelectedItem = DateTime.Now.Year.ToString();

            string[] meses = { "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio",
                       "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            cmbMeses.Items.AddRange(meses);
            cmbMeses.SelectedIndex = 0;

            LlenarComboSemanas();
        }

        private void LlenarComboSemanas()
        {
            if (cmbAnhos.SelectedItem == null || cmbMeses.SelectedIndex < 0)
                return;

            int mesSeleccionado = cmbMeses.SelectedIndex + 2;
            int anhoSeleccionado = int.Parse(cmbAnhos.SelectedItem.ToString());

            var semanas = ObtenerSemanasMes(anhoSeleccionado, mesSeleccionado);
            cmbSemanas.Items.Clear();
            cmbSemanas.Items.AddRange(semanas.ToArray());

            cmbSemanas.SelectedIndex = semanas.Count > 0 ? 0 : -1;
        }

        public List<string> ObtenerSemanasMes(int año, int mes)
        {
            var semanas = new List<string>();
            DateTime primerDiaMes = new DateTime(año, mes, 1);
            DateTime lunesActual = primerDiaMes;

            while (lunesActual.DayOfWeek != DayOfWeek.Monday && lunesActual.Month == mes)
                lunesActual = lunesActual.AddDays(1);

            while (lunesActual.Month == mes)
            {
                DateTime viernes = lunesActual.AddDays(4);
                semanas.Add($"{lunesActual:dd/MM} - {viernes:dd/MM}");
                lunesActual = lunesActual.AddDays(7);
            }

            return semanas;
        }

        private void AjustarColumnasTableFechas(bool mostrarSemana)
        {
            if (tableFechas.ColumnCount != 3)
                return;

            if (mostrarSemana)
            {
                tableFechas.ColumnStyles[0].Width = 33F;
                tableFechas.ColumnStyles[1].Width = 33F;
                tableFechas.ColumnStyles[2].Width = 33F;
            }
            else
            {
                tableFechas.ColumnStyles[0].Width = 50F;
                tableFechas.ColumnStyles[1].Width = 50F;
                tableFechas.ColumnStyles[2].Width = 0F;
            }
        }



        private void CentrarMiniContenedor()
        {
            panelminiContenedor.Left = (this.ClientSize.Width - panelminiContenedor.Width) / 2;
            panelminiContenedor.Top = (this.ClientSize.Height - panelminiContenedor.Height) / 2;
        }



        private void AjustarCentradoFechas()
        {
            cmbAnhos.Anchor = AnchorStyles.None;
            cmbMeses.Anchor = AnchorStyles.None;
            cmbSemanas.Anchor = AnchorStyles.None;
            lblAnho.Anchor = AnchorStyles.None;
            lblMeses.Anchor = AnchorStyles.None;
            lblSemanas.Anchor = AnchorStyles.None;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            int idDocente = Sesion.IdUsuario;

            if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
            {
                idDocente = (int)cmbDocentes.SelectedValue;
            }

            // Obtener los valores seleccionados 
            anhoSeleccionado = int.Parse(cmbAnhos.SelectedItem.ToString());
            mesSeleccionado = cmbMeses.SelectedIndex + 2;  // Meses se indexan desde 1
            semanaSeleccionada = cmbSemanas.SelectedIndex + 1;
            int tiposReporte = cmbTipoReporte.SelectedIndex;

            // Verificar si se ha seleccionado una semana válida
            if (tiposReporte >= 0)
            {
                // Obtener el tipo de reporte
                string tipoReporte = cmbTipoReporte.SelectedItem.ToString();
                DateTime fechaInicioSemana = ObtenerFechaInicio(anhoSeleccionado, mesSeleccionado, semanaSeleccionada);

                //*****************NOTAS*************************
                if (tipoReporte == "Notas")
                {
                    // Comprobar si no se ha seleccionado un filtro
                    if (cmbFiltro.SelectedItem == null)
                    {
                        MessageBox.Show("Debes de seleccionar un filtro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (cmbPeriodo.SelectedItem == null)
                    {
                        MessageBox.Show("Debes de seleccionar un periodo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Comprobar si el filtro es "Por Grupo" o "Por Estudiante"
                    if (cmbFiltro.SelectedItem.ToString() == "Por Grupo" && cmbMaterias.SelectedItem != null)
                    {
                        // Obtener la materia seleccionada
                        var materiaSeleccionada = cmbMaterias.SelectedItem as Materias;
                        if (materiaSeleccionada != null)
                        {
                            // Mostrar cuadro para guardar el archivo
                            SaveFileDialog sfd = new SaveFileDialog
                            {
                                Filter = "PDF files (*.pdf)|*.pdf",
                                FileName = $"Notas_{materiaSeleccionada.nombre_materia}.pdf" // Nombre basado en la materia
                            };

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                GenerarReportePorGrupo(materiaSeleccionada, sfd.FileName);
                            }
                        }
                    }
                    else if (cmbFiltro.SelectedItem.ToString() == "Por Estudiante" && cmbEstudiantes.SelectedItem != null)
                    {
                        // Obtener el estudiante seleccionado
                        var estudianteSeleccionado = cmbEstudiantes.SelectedItem as Estudiantes;
                        if (estudianteSeleccionado != null)
                        {
                            SaveFileDialog sfd = new SaveFileDialog
                            {
                                Filter = "PDF files (*.pdf)|*.pdf",
                                FileName = $"Notas_{estudianteSeleccionado.nombre_estudiante}_{estudianteSeleccionado.primer_apellido}.pdf"
                            };

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                GenerarReportePorEstudiante(estudianteSeleccionado, sfd.FileName);
                            }
                        }
                    }
                }
                //*****************ASISTENCIA*****************
                else if (tipoReporte == "Asistencia")
                {
                    if (cmbTiempo.SelectedItem == null)
                    {
                        MessageBox.Show("Debes seleccionar un tiempo del reporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (cmbTiempo.SelectedItem.ToString() == "Periodo académico" && cmbPeriodo.SelectedItem == null)
                    {
                        MessageBox.Show("Debes seleccionar un periodo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (cmbFiltro.SelectedItem == null)
                    {
                        MessageBox.Show("Debes de seleccionar un filtro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Comprobar si el filtro es "Por Grupo" o "Por Estudiante"
                    if (cmbFiltro.SelectedItem.ToString() == "Por Grupo" && cmbMaterias.SelectedItem != null)
                    {
                        var materiaSeleccionada = cmbMaterias.SelectedItem as Materias;
                        if (materiaSeleccionada != null)
                        {
                            // Obtener el periodo seleccionado (ej: "Primer Periodo" o "Segundo Periodo")
                            string periodoSeleccionado = cmbPeriodo.SelectedItem?.ToString();

                            SaveFileDialog sfd = new SaveFileDialog
                            {
                                Filter = "PDF files (*.pdf)|*.pdf",
                                // Nombre del archivo basado en el periodo: "Asistencia_PrimerPeriodo.pdf"
                                FileName = $"Asistencia_Por_Grupo.pdf"
                            };

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                GenerarReporteAsistenciaPorGrupo(materiaSeleccionada, sfd.FileName);
                            }
                        }
                    }
                    else if (cmbFiltro.SelectedItem.ToString() == "Por Estudiante" && cmbEstudiantes.SelectedItem != null)
                    {
                        var estudianteSeleccionado = cmbEstudiantes.SelectedItem as Estudiantes;
                        if (estudianteSeleccionado != null)
                        {
                            SaveFileDialog sfd = new SaveFileDialog
                            {
                                Filter = "PDF files (*.pdf)|*.pdf",
                                FileName = $"Asistencia_{estudianteSeleccionado.nombre_estudiante}_{estudianteSeleccionado.primer_apellido}.pdf"
                            };

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                GenerarReporteAsistenciaPorEstudiante(estudianteSeleccionado, sfd.FileName);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tipo de reporte.");
            }
        }

        // Generar reporte por grupo
        private void GenerarReportePorGrupo(Materias materia, string filePath)
        {
            int idDocente = Sesion.IdUsuario;

            if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
            {
                idDocente = (int)cmbDocentes.SelectedValue;
            }

            if (materia == null)
            {
                MessageBox.Show("Materia no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string periodoSeleccionado = cmbPeriodo.SelectedItem?.ToString();

                // Aquí obtendrás los datos de la base de datos para la materia seleccionada
                using (var contexto = new RegistroDocenteEntities())
                {
                    var estudiantes = contexto.Clases
                        .Join(contexto.Notas, c => c.id_clase, n => n.id_clase, (c, n) => new { c, n })
                        .Where(joined => joined.n.periodo == periodoSeleccionado && joined.c.id_materia == materia.id_materia && joined.c.id_usuario == idDocente)  // Asegurarse de que se filtra por periodo
                        .Select(joined => joined.c.Estudiantes)  // Seleccionar los estudiantes
                        .Distinct()
                        .ToList();
                    // Obtener la sección del docente
                    var docente = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idDocente);
                    var seccion = docente != null ? contexto.Secciones.FirstOrDefault(s => s.id_seccion == docente.id_seccion)?.nombre_seccion : "No asignada";

                    // Generar el PDF para todos los estudiantes en esta materia
                    ExportadorPDF.ExportarNotasPorGrupo(estudiantes, materia, filePath, seccion, docente.nombre_usuario, docente.apellido_usuario, periodoSeleccionado);

                    // Abrir el archivo automáticamente
                    System.Diagnostics.Process.Start(filePath);
            }
        }


        // Generar reporte por estudiante
        private void GenerarReportePorEstudiante(Estudiantes estudiante, string filePath)
        {
            int idDocente = Sesion.IdUsuario;

            if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
            {
                idDocente = (int)cmbDocentes.SelectedValue;
            }
            string periodoSeleccionado = cmbPeriodo.SelectedItem?.ToString();

            // Obtener los datos de la base de datos para el estudiante seleccionado
            using (var contexto = new RegistroDocenteEntities())
            {
                var clasesConNotas = (from c in contexto.Clases
                                      join n in contexto.Notas on c.id_clase equals n.id_clase
                                      where c.id_estudiante == estudiante.id_estudiante
                                      && n.periodo == periodoSeleccionado  // Filtrar por el periodo seleccionado
                                      select new
                                      {
                                          Clase = c,
                                          Nota = n
                                      }).ToList();

                var clases = clasesConNotas.Select(x => x.Clase).ToList(); // Obtener las clases del estudiante
                // Obtener la sección del docente
                var docente = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idDocente);
                var seccion = docente != null ? contexto.Secciones.FirstOrDefault(s => s.id_seccion == docente.id_seccion)?.nombre_seccion : "No asignada";

                // Generar el PDF para este estudiante, pasando la ruta donde se guardará
                ExportadorPDF.ExportarNotasPorEstudiante(clases, estudiante, filePath, seccion, docente?.nombre_usuario, docente?.apellido_usuario,periodoSeleccionado);

                // Abrir el archivo automáticamente
                System.Diagnostics.Process.Start(filePath);
            }
        }

        // Llenar el ComboBox con las materias disponibles para el docente seleccionado
        private void CargarMateriasDocente(int idDocente)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                // Obtener todas las materias asociadas al docente
                var materias = contexto.Horarios
                                       .Where(h => h.id_usuario == idDocente)
                                       .Join(contexto.Materias, h => h.id_materia, m => m.id_materia, (h, m) => m)
                                       .Distinct()
                                       .ToList();

                cmbMaterias.DataSource = materias;
                cmbMaterias.DisplayMember = "nombre_materia";  // Mostrar el nombre de la materia
                cmbMaterias.ValueMember = "id_materia";  // Usar el ID de la materia como el valor
            }
        }

        // Llenar el ComboBox con los estudiantes del docente
        private void CargarEstudiantesDocente(int idDocente)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var estudiantes = contexto.Clases
                                          .Where(c => c.id_usuario == idDocente)
                                          .Select(c => c.Estudiantes)
                                          .Distinct()
                                          .ToList();

                cmbEstudiantes.DataSource = estudiantes;
                cmbEstudiantes.DisplayMember = "nombre_estudiante";  // Mostrar el nombre del estudiante
                cmbEstudiantes.ValueMember = "id_estudiante";  // Usar el ID del estudiante como valor
            }
        }

        private void GenerarReporteAsistenciaPorGrupo(Materias materia, string filePath)
        {
            int idDocente = Sesion.IdUsuario;

            if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
            {
                idDocente = (int)cmbDocentes.SelectedValue;
            }

            if (materia == null)
            {
                MessageBox.Show("Materia no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string seleccion = cmbTiempo.SelectedItem?.ToString();
            if (seleccion == "Semanal")
            {
                // Aquí obtendrás los datos de la base de datos para la materia seleccionada
                using (var contexto = new RegistroDocenteEntities())
                {
                    // Obtener el primer día de la semana seleccionada
                    DateTime fechaInicioSemana = ObtenerFechaInicio(anhoSeleccionado, mesSeleccionado, semanaSeleccionada);

                    // Obtener los estudiantes en esta materia
                    var estudiantes = contexto.Clases
                        .Where(c => c.id_materia == materia.id_materia && c.id_usuario == idDocente)
                        .Select(c => c.Estudiantes)
                        .Distinct()
                        .ToList();

                    if (estudiantes.Count == 0)
                    {
                        MessageBox.Show("No hay estudiantes en esta materia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // Obtener la sección del docente
                    var docente = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idDocente);
                    var seccion = docente != null ? contexto.Secciones.FirstOrDefault(s => s.id_seccion == docente.id_seccion)?.nombre_seccion : "No asignada";
                    string mes = cmbMeses.SelectedItem.ToString();

                    // Generar el PDF para todos los estudiantes en esta materia
                    ExportadorPDF.ExportarAsistenciaPorGrupo(estudiantes, materia, filePath, fechaInicioSemana, null, seccion, docente.nombre_usuario, docente.apellido_usuario, mes, seleccion);
                    // Confirmación
                    MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el archivo automáticamente
                    System.Diagnostics.Process.Start(filePath);
                }
            }
            else if (seleccion == "Mensual")
            {
                // Asistencia mensual
                using (var contexto = new RegistroDocenteEntities())
                {
                    // Obtener el primer día del mes seleccionado
                    DateTime primerDiaMes = new DateTime(anhoSeleccionado, mesSeleccionado, 1);

                    // Obtener todos los días laborables del mes (lunes a viernes)
                    var diasDelMes = new List<DateTime>();
                    for (DateTime dia = primerDiaMes; dia.Month == mesSeleccionado; dia = dia.AddDays(1))
                    {
                        if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday) // Lunes a Viernes
                        {
                            diasDelMes.Add(dia);
                        }
                    }

                    // Obtener los estudiantes en esta materia
                    var estudiantes = contexto.Clases
                    .Where(c => c.id_materia == materia.id_materia && c.id_usuario == idDocente)
                    .Select(c => c.Estudiantes)
                    .Distinct()
                    .ToList();

                    if (estudiantes.Count == 0)
                    {
                        MessageBox.Show("No hay estudiantes en esta materia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Obtener la sección del docente
                    var docente = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idDocente);
                    var seccion = docente != null ? contexto.Secciones.FirstOrDefault(s => s.id_seccion == docente.id_seccion)?.nombre_seccion : "No asignada";
                    string mes = cmbMeses.SelectedItem.ToString();

                    // Generar el PDF para todos los estudiantes en esta materia
                    ExportadorPDF.ExportarAsistenciaPorGrupo(estudiantes, materia, filePath, primerDiaMes, diasDelMes, seccion, docente.nombre_usuario, docente.apellido_usuario, mes, seleccion);
                    // Confirmación
                    MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el archivo automáticamente
                    System.Diagnostics.Process.Start(filePath);
                }
            }
            else if (seleccion == "Periodo académico")
            {
                string periodoSeleccionado = cmbPeriodo.SelectedItem?.ToString();

                // Definir fechas exactas del periodo
                DateTime fechaInicioPeriodo = periodoSeleccionado == "Primer Periodo"
                    ? new DateTime(anhoSeleccionado, 2, 3)  // Primer Periodo: 3 de febrero
                    : new DateTime(anhoSeleccionado, 5, 26); // Segundo Periodo: 26 de mayo

                DateTime fechaFinPeriodo = periodoSeleccionado == "Primer Periodo"
                    ? new DateTime(anhoSeleccionado, 5, 25)  // Primer Periodo hasta 25 de mayo
                    : new DateTime(anhoSeleccionado, 12, 10); // Segundo Periodo hasta 10 de diciembre

                // Obtener los meses involucrados en el periodo
                List<int> mesesPeriodo = Enumerable.Range(fechaInicioPeriodo.Month,
                    (fechaFinPeriodo.Month - fechaInicioPeriodo.Month) + 1).ToList();

                using (var contexto = new RegistroDocenteEntities())
                {
                    var estudiantes = contexto.Clases
                        .Where(c => c.id_materia == materia.id_materia && c.id_usuario == idDocente)
                        .Select(c => c.Estudiantes)
                        .Distinct()
                        .ToList();

                    if (estudiantes.Count == 0)
                    {
                        MessageBox.Show("No hay estudiantes en esta materia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var docente = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idDocente);
                    var seccion = docente != null ? contexto.Secciones.FirstOrDefault(s => s.id_seccion == docente.id_seccion)?.nombre_seccion : "No asignada";

                    // Lista para almacenar todos los días laborables del periodo completo
                    var diasDelPeriodo = new List<DateTime>();

                    // Procesar cada mes del periodo
                    foreach (var mes in mesesPeriodo)
                    {
                        // Determinar el primer y último día del mes que caen dentro del periodo
                        DateTime primerDiaMes = new DateTime(anhoSeleccionado, mes, 1);
                        DateTime ultimoDiaMes = new DateTime(anhoSeleccionado, mes, DateTime.DaysInMonth(anhoSeleccionado, mes));

                        // Ajustar para que no salgan del rango del periodo
                        DateTime inicioMes = primerDiaMes < fechaInicioPeriodo ? fechaInicioPeriodo : primerDiaMes;
                        DateTime finMes = ultimoDiaMes > fechaFinPeriodo ? fechaFinPeriodo : ultimoDiaMes;

                        // Obtener días laborables del mes dentro del periodo
                        for (DateTime dia = inicioMes; dia <= finMes; dia = dia.AddDays(1))
                        {
                            if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday)
                            {
                                diasDelPeriodo.Add(dia);
                            }
                        }
                    }

                    // Generar un solo PDF con todos los días del periodo
                    ExportadorPDF.ExportarAsistenciaPorGrupo(
                        estudiantes,
                        materia,
                        filePath,
                        fechaInicioPeriodo,  // Fecha de inicio real del periodo
                        diasDelPeriodo,      // Todos los días laborables del periodo
                        seccion,
                        docente.nombre_usuario,
                        docente.apellido_usuario,
                        periodoSeleccionado,
                        "Periodo académico");

                    MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(filePath);
                }
            }
        }

        private void GenerarReporteAsistenciaPorEstudiante(Estudiantes estudiante, string filePath)
        {
            int idDocente = Sesion.IdUsuario;

            if (cmbDocentes.Visible && cmbDocentes.SelectedValue != null)
            {
                idDocente = (int)cmbDocentes.SelectedValue;
            }
            if (estudiante == null)
            {
                MessageBox.Show("Estudiante no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string seleccion = cmbTiempo.SelectedItem?.ToString();
            if (seleccion == "Semanal")
            {
                // Obtener el primer día de la semana seleccionada
                DateTime fechaInicioSemana = ObtenerFechaInicio(anhoSeleccionado, mesSeleccionado, semanaSeleccionada);

                // Aquí obtendrás los datos de la base de datos para el estudiante seleccionado
                using (var contexto = new RegistroDocenteEntities())
                {
                    var asistencia = contexto.Asistencia
                        .Where(a => a.id_estudiante == estudiante.id_estudiante)
                        .ToList();

                    if (asistencia.Count == 0)
                    {
                        MessageBox.Show("No hay clases para este estudiante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // Obtener la sección del docente
                    var docente = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idDocente);
                    var seccion = docente != null ? contexto.Secciones.FirstOrDefault(s => s.id_seccion == docente.id_seccion)?.nombre_seccion : "No asignada";
                    string mes = cmbMeses.SelectedItem.ToString();

                    var estudiantes = asistencia.Select(c => c.Estudiantes).ToList();

                    // Generar el PDF para todas las materias de este estudiante
                    ExportadorPDF.ExportarAsistenciaPorEstudiante(estudiantes, filePath, fechaInicioSemana, null, seccion, docente.nombre_usuario, docente.apellido_usuario, mes, seleccion);
                    // Confirmación
                    MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el archivo automáticamente
                    System.Diagnostics.Process.Start(filePath);
                }
            }
            else if (seleccion == "Mensual")
            {
                // Obtener el primer día de la semana seleccionada
                DateTime primerDiaMes = new DateTime(anhoSeleccionado, mesSeleccionado, 1);

                // Aquí obtendrás los datos de la base de datos para el estudiante seleccionado
                using (var contexto = new RegistroDocenteEntities())
                {
                    // Obtener todos los días laborables del mes (lunes a viernes)
                    var diasDelMes = new List<DateTime>();
                    for (DateTime dia = primerDiaMes; dia.Month == mesSeleccionado; dia = dia.AddDays(1))
                    {
                        if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday) // Lunes a Viernes
                        {
                            diasDelMes.Add(dia);
                        }
                    }

                    var clases = contexto.Clases
                        .Where(c => c.id_estudiante == estudiante.id_estudiante)
                        .ToList();

                    if (clases.Count == 0)
                    {
                        MessageBox.Show("No hay clases para este estudiante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // Obtener la sección del docente
                    var docente = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idDocente);
                    var seccion = docente != null ? contexto.Secciones.FirstOrDefault(s => s.id_seccion == docente.id_seccion)?.nombre_seccion : "No asignada";
                    string mes = cmbMeses.SelectedItem.ToString();
                    var estudiantes = clases.Select(c => c.Estudiantes).ToList();

                    // Generar el PDF para todas las materias de este estudiante
                    ExportadorPDF.ExportarAsistenciaPorEstudiante(estudiantes, filePath, primerDiaMes, diasDelMes, seccion, docente.nombre_usuario, docente.apellido_usuario, mes, seleccion);
                    // Confirmación
                    MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el archivo automáticamente
                    System.Diagnostics.Process.Start(filePath);
                }
            }
            else if (seleccion == "Periodo académico")
            {
                string periodoSeleccionado = cmbPeriodo.SelectedItem?.ToString();

                
                DateTime fechaInicioPeriodo = periodoSeleccionado == "Primer Periodo"
                    ? new DateTime(anhoSeleccionado, 2, 3)
                    : new DateTime(anhoSeleccionado, 5, 26);

                DateTime fechaFinPeriodo = periodoSeleccionado == "Primer Periodo"
                    ? new DateTime(anhoSeleccionado, 5, 25)
                    : new DateTime(anhoSeleccionado, 12, 10);

                List<int> mesesPeriodo = Enumerable.Range(fechaInicioPeriodo.Month,
                    (fechaFinPeriodo.Month - fechaInicioPeriodo.Month) + 1).ToList();
               

                // El resto del código se mantiene EXACTAMENTE igual
                using (var contexto = new RegistroDocenteEntities())
                {
                    var clases = contexto.Clases
                         .Where(c => c.id_estudiante == estudiante.id_estudiante)
                         .ToList();

                    if (clases.Count == 0)
                    {
                        MessageBox.Show("No hay estudiantes en esta materia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var docente = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idDocente);
                    var seccion = docente != null ? contexto.Secciones.FirstOrDefault(s => s.id_seccion == docente.id_seccion)?.nombre_seccion : "No asignada";
                    var estudiantes = clases.Select(c => c.Estudiantes).ToList();

                    foreach (var mes in mesesPeriodo)
                    {
                        DateTime primerDiaMes = new DateTime(anhoSeleccionado, mes, 1);

                        var diasDelMes = new List<DateTime>();
                        for (DateTime dia = primerDiaMes; dia.Month == mes; dia = dia.AddDays(1))
                        {
                            if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday)
                            {
                                diasDelMes.Add(dia);
                            }
                        }

                        string mesSeleccionado = new DateTime(anhoSeleccionado, mes, 1).ToString("MMMM");
                        ExportadorPDF.ExportarAsistenciaPorEstudiante(estudiantes, filePath, new DateTime(anhoSeleccionado, 1, 1), null, seccion, docente.nombre_usuario, docente.apellido_usuario, periodoSeleccionado, "Periodo académico", mesesPeriodo);
                    }

                    MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(filePath);
                }
            }
        }

        // Método para obtener la fecha de inicio (primer día de la semana)
        private DateTime ObtenerFechaInicio(int anho, int mes, int semana)
        {
            // Crear la fecha con el primer día del mes
            DateTime primerDiaDelMes = new DateTime(anho, mes, 1);

            // Ajustar al primer lunes del mes
            DateTime primerLunes = primerDiaDelMes.AddDays((DayOfWeek.Monday - primerDiaDelMes.DayOfWeek + 7) % 7);

            // Ahora movernos a la semana seleccionada, sumando 7 días por cada semana
            DateTime inicioSemanaSeleccionada = primerLunes.AddDays((semana - 1) * 7);

            return inicioSemanaSeleccionada;  // Devuelve el lunes de la semana seleccionada
        }

        private void cmbAnhos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboSemanas();
        }

        private void cmbMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboSemanas();
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = cmbTipoReporte.SelectedItem?.ToString();

            // Ocultar controles por defecto
            cmbMaterias.Visible = false;
            lblMateria.Visible = false;

            if (seleccion == "Notas")
            {
                PanelFechas.Visible = false;
                panelPeriodo.Visible = true;
                panelTiempoReport.Visible = false;
                lblAnho2.Visible = false;  
                cmbAnhos2.Visible = false;
            }
            else if (seleccion == "Asistencia")
            {
                PanelFechas.Visible = false;
                panelTiempoReport.Visible = true;
                cmbTiempo.SelectedIndex = 0;
                lblAnho2.Visible = true;
                cmbAnhos2.Visible = true;
                if (cmbTiempo.SelectedIndex == 0)
                {
                    panelPeriodo.Visible = true;
                }
            }

            // Forzar actualización del filtro
            cmbFiltro_SelectedIndexChanged(sender, e);

            AjustarCentradoFechas();
        }
        private void cbTiempoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {

            string seleccion = cmbTiempo.SelectedItem?.ToString();

            if (seleccion == "Periodo académico")
            {
                PanelFechas.Visible = false;
                tableFechas.Visible = false;
                panelPeriodo.Visible = true;
            }
            else
            {
                PanelFechas.Visible = true;
                tableFechas.Visible = true;
                panelPeriodo.Visible = false;

                cmbAnhos.Visible = true;
                cmbMeses.Visible = true;
                lblAnho.Visible = true;
                lblMeses.Visible = true;
                if (seleccion == "Semanal")
                {
                    cmbSemanas.Visible = true;
                    lblSemanas.Visible = true;
                    AjustarColumnasTableFechas(true);
                }
                else if (seleccion == "Mensual")
                {
                    cmbSemanas.Visible = false;
                    lblSemanas.Visible = false;
                    AjustarColumnasTableFechas(false);

                }
            }

            AjustarCentradoFechas();
        }
        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltro.SelectedItem == null) return;

            string tipoReporte = cmbTipoReporte.SelectedItem?.ToString();
            string filtroSeleccionado = cmbFiltro.SelectedItem.ToString();

            // Ocultar todos los controles primero
            cmbMaterias.Visible = false;
            lblMateria.Visible = false;
            cmbEstudiantes.Visible = false;
            lblEstudiante.Visible = false;

            // Mostrar los controles según el filtro seleccionado
            if (filtroSeleccionado == "Por Grupo")
            {
                // Solo mostrar materias si es reporte de Notas
                if (tipoReporte == "Notas")
                {
                    cmbMaterias.Visible = true;
                    lblMateria.Visible = true;
                }
            }
            else if (filtroSeleccionado == "Por Estudiante")
            {
                // Mostrar estudiantes para ambos tipos de reporte
                cmbEstudiantes.Visible = true;
                lblEstudiante.Visible = true;
            }
        }
        private void UcReportes_Load(object sender, EventArgs e)
        {
            cmbTipoReporte.Items.Add("Notas");
            cmbTipoReporte.Items.Add("Asistencia");
            cmbTipoReporte.SelectedIndex = 0;

            cmbFiltro.Items.Add("Por Grupo");
            cmbFiltro.Items.Add("Por Estudiante");
            cmbFiltro.SelectedIndex = 0;

            InicializarComboTiempo();

            cmbPeriodo.Items.Add("Primer Periodo");
            cmbPeriodo.Items.Add("Segundo Periodo");
            cmbPeriodo.SelectedIndex = 0;

            InicializarFechas();


            bool esAdministrador = AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);

            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == Sesion.IdUsuario);
                AlumnoController.VerificarSiEsAdministrador(Sesion.IdUsuario);
                if (esAdministrador) //administrador
                {
                    // Mostrar ComboBox y ocultar Label
                    cmbDocentes.Visible = true;

                    // Cargar docentes
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

                    cmbDocentes.SelectedIndexChanged += cmbDocentes_SelectedIndexChanged;

                    if (cmbDocentes.Items.Count > 0)
                    {
                        cmbDocentes.SelectedIndex = 0; // Dispara carga automática
                    }
                }
                else
                {
                    // Usuario docente
                    cmbDocentes.Visible = false;

                    CargarEstudiantesDocente(Sesion.IdUsuario);
                    CargarMateriasDocente(Sesion.IdUsuario);
                }
            }
        }

        private void cmbDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocentes.SelectedItem != null)
            {
                int idDocenteSeleccionado = (int)cmbDocentes.SelectedValue;
                CargarEstudiantesDocente(idDocenteSeleccionado);
                CargarMateriasDocente(idDocenteSeleccionado);
            }
        }

        private void InicializarComboTiempo()
        {
            cmbTiempo.Items.Clear();
            cmbTiempo.Items.Add("Periodo académico");
            cmbTiempo.Items.Add("Semanal");
            cmbTiempo.Items.Add("Mensual");
            cmbTiempo.SelectedIndex = 0;
        }

        private void panelContenidos_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
