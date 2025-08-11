using iTextSharp.text;
using iTextSharp.text.pdf;
using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Registro_Docente_360.Utilidades
{
    public static class ExportadorPDF
    {
        /// <summary>
        /// Exporta el contenido de un DataGridView a un archivo PDF con encabezado de docente, sección y materia.
        /// </summary>
        public static void Exportar(DataGridView dgv, string nombreDocente, string seccion, string materia, string periodo, string nombreArchivo)
        {
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(nombreArchivo, FileMode.Create));
                writer.PageEvent = new FooterHelper();
                doc.Open();

                // Agregar el periodo al encabezado
                Paragraph encabezado = new Paragraph(
                    $"Docente: {nombreDocente}\n" +
                    $"Sección: {seccion}\n" +
                    $"Materia: {materia}\n" +
                    $"Periodo: {periodo}\n\n",  // Aquí incluimos el periodo
                    FontFactory.GetFont("Arial", 12, Font.BOLD)
                );
                encabezado.Alignment = Element.ALIGN_LEFT;
                doc.Add(encabezado);

                // Línea divisoria
                doc.Add(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                doc.Add(new Paragraph(" "));

                // Tabla PDF con columnas del DataGridView
                PdfPTable tabla = new PdfPTable(dgv.Columns.Count);
                tabla.WidthPercentage = 100;

                // Encabezados de columna
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    PdfPCell celdaEncabezado = new PdfPCell(new Phrase(col.HeaderText, FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    celdaEncabezado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabla.AddCell(celdaEncabezado);
                }

                // Filas de datos
                foreach (DataGridViewRow fila in dgv.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        foreach (DataGridViewCell celda in fila.Cells)
                        {
                            tabla.AddCell(celda.Value?.ToString() ?? "");
                        }
                    }
                }

                doc.Add(tabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a PDF: " + ex.Message);
            }
            finally
            {
                doc.Close();
            }
        }

        public static void ExportarNotasPorGrupo(List<Estudiantes> estudiantes, Materias materia, string filePath, string Seccion, string nomDocente, string apeDocente, string periodoSeleccionado)
        {
            try
            {
                // SOLO CAMBIO 1: Documento en horizontal (Rotate)
                Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                writer.PageEvent = new FooterHelper();
                doc.Open();

                // Encabezado (igual que antes)
                Paragraph encabezado = new Paragraph(
                    "Reporte de notas\n" +
                    $"Periodo: {periodoSeleccionado}\n" +
                    $"Docente: {nomDocente} {apeDocente}\n" +
                    $"Sección: {Seccion}\n\n",
                    FontFactory.GetFont("Arial", 12, Font.BOLD)
                );
                encabezado.Alignment = Element.ALIGN_LEFT;
                doc.Add(encabezado);

                // Línea divisoria (igual que antes)
                doc.Add(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                doc.Add(new Paragraph(" "));

                // Tabla ORIGINAL (sin cambios en estructura)
                PdfPTable table = new PdfPTable(9); // Mismas 9 columnas
                table.WidthPercentage = 100;
                float alturaFila = 20f;

                // Encabezados (igual que antes)
                string[] titulos = new string[] {
            "Cedula","Nombre estudiante","Primer apellido","Primer Examen", "Segundo Examen", "Tareas", "Asistencia", "Cotidiano", "Nota Final"
        };

                foreach (var titulo in titulos)
                {
                    PdfPCell celdaEncabezado = new PdfPCell(new Phrase(titulo, FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    celdaEncabezado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    celdaEncabezado.FixedHeight = alturaFila;
                    table.AddCell(celdaEncabezado);
                }

                // Datos (igual que antes)
                foreach (var estudiante in estudiantes)
                {
                    var nota = ObtenerNotaPorEstudianteYMateria(estudiante, materia, periodoSeleccionado);

                    // Cedula
                    PdfPCell celdaCedula = new PdfPCell(new Phrase(estudiante.cedula_estudiante));
                    celdaCedula.FixedHeight = alturaFila;
                    table.AddCell(celdaCedula);

                    // Nombre estudiante
                    PdfPCell celdaNombre = new PdfPCell(new Phrase(estudiante.nombre_estudiante));
                    celdaNombre.FixedHeight = alturaFila;
                    table.AddCell(celdaNombre);

                    // Primer apellido
                    PdfPCell celdaApellido = new PdfPCell(new Phrase(estudiante.primer_apellido));
                    celdaApellido.FixedHeight = alturaFila;
                    table.AddCell(celdaApellido);

                    // Primer Examen
                    PdfPCell celdaPrimerExamen = new PdfPCell(new Phrase(nota?.primer_examen.ToString() ?? "0"));
                    celdaPrimerExamen.FixedHeight = alturaFila;
                    table.AddCell(celdaPrimerExamen);

                    // Segundo Examen
                    PdfPCell celdaSegundoExamen = new PdfPCell(new Phrase(nota?.segundo_examen.ToString() ?? "0"));
                    celdaSegundoExamen.FixedHeight = alturaFila;
                    table.AddCell(celdaSegundoExamen);

                    // Tareas
                    PdfPCell celdaTareas = new PdfPCell(new Phrase(nota?.tareas.ToString() ?? "0"));
                    celdaTareas.FixedHeight = alturaFila;
                    table.AddCell(celdaTareas);

                    // Asistencia
                    PdfPCell celdaAsistencia = new PdfPCell(new Phrase(nota?.asistencia.ToString() ?? "0"));
                    celdaAsistencia.FixedHeight = alturaFila;
                    table.AddCell(celdaAsistencia);

                    // Cotidiano
                    PdfPCell celdaCotidiano = new PdfPCell(new Phrase(nota?.cotidiano.ToString() ?? "0"));
                    celdaCotidiano.FixedHeight = alturaFila;
                    table.AddCell(celdaCotidiano);

                    // Nota Final
                    PdfPCell celdaNotaFinal = new PdfPCell(new Phrase(nota?.nota_final.ToString() ?? "0"));
                    celdaNotaFinal.FixedHeight = alturaFila;
                    table.AddCell(celdaNotaFinal);
                }

                // SOLO CAMBIO 2: Ajustar ancho de columnas para mejor visualización en horizontal
                float[] columnWidths = new float[] { 8f, 15f, 15f, 10f, 10f, 8f, 8f, 8f, 8f };
                table.SetWidths(columnWidths);

                doc.Add(table);
                doc.Close();
                MessageBox.Show("PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public static void ExportarNotasPorEstudiante(List<Clases> clases, Estudiantes estudiante, string filePath, string Seccion, string nomDocente, string apeDocente, string periodoSeleccionado)
        {
            // Crear documento en horizontal
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            writer.PageEvent = new FooterHelper();
            doc.Open();

            // Encabezado
            Paragraph encabezado = new Paragraph(
                $"Reporte de notas - {estudiante.nombre_estudiante} {estudiante.primer_apellido}\n" +
                $"Cédula: {estudiante.cedula_estudiante}\n" +
                $"Docente: {nomDocente} {apeDocente}\n" +
                $"Sección: {Seccion}\n" +
                $"Periodo: {periodoSeleccionado}\n\n",
                FontFactory.GetFont("Arial", 12, Font.BOLD)
            );
            doc.Add(encabezado);

            // Tabla principal: Materias en filas, evaluaciones en columnas
            PdfPTable table = new PdfPTable(7); // 6 evaluaciones + columna de materias
            table.WidthPercentage = 100;

            // Línea divisoria (se mantiene igual)
            doc.Add(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(new Paragraph(" "));

            // Encabezados
            string[] headers = { "Materia", "Primer Examen", "Segundo Examen", "Tareas", "Asistencia", "Cotidiano", "Nota Final" };
            foreach (string header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, FontFactory.GetFont("Arial", 10, Font.BOLD)));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
            }

            // Datos
            foreach (var clase in clases)
            {
                var materia = ObtenerMateriaPorClase(clase);
                var nota = ObtenerNotaPorClase(clase, periodoSeleccionado);

                // Materia
                table.AddCell(new Phrase(materia?.nombre_materia ?? "Sin nombre", FontFactory.GetFont("Arial", 9)));

                // Evaluaciones
                table.AddCell(new Phrase(nota?.primer_examen.ToString() ?? "0", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(nota?.segundo_examen.ToString() ?? "0", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(nota?.tareas.ToString() ?? "0", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(nota?.asistencia.ToString() ?? "0", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(nota?.cotidiano.ToString() ?? "0", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(nota?.nota_final.ToString() ?? "0", FontFactory.GetFont("Arial", 9)));
            }

            doc.Add(table);
            doc.Close();
            MessageBox.Show("PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static Notas ObtenerNotaPorEstudianteYMateria(Estudiantes estudiante, Materias materia, string periodo)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var clase = contexto.Clases
                    .FirstOrDefault(c => c.id_estudiante == estudiante.id_estudiante && c.id_materia == materia.id_materia);

                if (clase != null)
                {
                    // Filtrar por periodo también
                    return contexto.Notas
                        .FirstOrDefault(n => n.id_clase == clase.id_clase && n.periodo == periodo); // Filtrar por periodo
                }

                return null; // Si no hay nota para el estudiante en esta materia
            }
        }


        public static Notas ObtenerNotaPorClase(Clases clase, string periodo)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var notas = contexto.Notas.FirstOrDefault(n => n.id_clase == clase.id_clase);

                if (notas != null)
                {
                    // Filtrar por periodo también
                    return contexto.Notas
                        .FirstOrDefault(n => n.id_clase == clase.id_clase && n.periodo == periodo); // Filtrar por periodo
                }
                return null;
            }
        }

        public static Materias ObtenerMateriaPorClase(Clases clase)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                // Buscar la materia relacionada con la clase usando el id_materia
                var materia = contexto.Materias
                    .FirstOrDefault(m => m.id_materia == clase.id_materia);
                return materia; // Devuelve la materia encontrada o null si no se encuentra
            }
        }

        //**************************ASISTENCIA*********************************


        public static void ExportarAsistenciaPorGrupo(List<Estudiantes> estudiantes, Materias materia, string filePath, DateTime fechaInicioSemana, List<DateTime> diasDelMes, string Seccion, string nomDocente, string apeDocente, string mesSeleccionado, string Tiempo, List<int> mesesPeriodo = null)

        {
            // Crear el documento PDF
            Document doc = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            writer.PageEvent = new FooterHelper();
            doc.Open();
            // Crear el estilo para los titulos
            Font fontEncabezado = FontFactory.GetFont("Arial", 10, Font.BOLD);
            BaseColor colorFondo = BaseColor.LIGHT_GRAY;

            // Encabezado
            Paragraph encabezado = new Paragraph(
                "Reporte de asistencia\n" +
                $"Sección: {Seccion}\n" +
                $"Mes: {mesSeleccionado} \n" +
                $"Docente: {nomDocente} {apeDocente}\n\n",
                FontFactory.GetFont("Arial", 12, Font.BOLD)
            );
            encabezado.Alignment = Element.ALIGN_LEFT;
            doc.Add(encabezado);

            // Línea divisoria
            doc.Add(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(new Paragraph(" "));

            // Si es semanal, agregamos solo 5 columnas (Lunes a Viernes), si es mensual agregamos una columna por día
            if (Tiempo == "Semanal")
            {
                PdfPTable table = new PdfPTable(7); // Incluye cédula y nombre + Lunes a Viernes
                table.WidthPercentage = 110;
                float alturaFila = 20f;


                string titulo1 = "Cédula";
                PdfPCell celdaCedula = new PdfPCell(new Phrase(titulo1, fontEncabezado));
                celdaCedula.BackgroundColor = colorFondo;
                celdaCedula.FixedHeight = alturaFila;
                table.AddCell(celdaCedula);

                string titulo2 = "Nombre";
                PdfPCell celdaNombre = new PdfPCell(new Phrase(titulo2, fontEncabezado));
                celdaNombre.BackgroundColor = colorFondo;
                celdaNombre.FixedHeight = alturaFila;
                table.AddCell(celdaNombre);

                //titulos de los dias
                string[] diasSemana = new string[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
                foreach (var dia in diasSemana) //pone el estilo a cada titulo
                {
                    // Calcular la fecha de cada día basado en la fecha de inicio de la semana
                    DateTime fechaDia = fechaInicioSemana.AddDays(Array.IndexOf(diasSemana, dia)); // Calcula el día sumando días a la fecha de inicio de la semana

                    // Formatear el texto para mostrar "Lunes 3", "Martes 4", etc.
                    string titulos = $"{dia} {fechaDia.Day}";
                    PdfPCell celdaEncabezado = new PdfPCell(new Phrase(titulos, fontEncabezado));
                    celdaEncabezado.BackgroundColor = colorFondo;
                    celdaEncabezado.FixedHeight = alturaFila;
                    table.AddCell(celdaEncabezado);
                }

                foreach (var estudiante in estudiantes)
                {
                    var fila = new List<string> { estudiante.cedula_estudiante, estudiante.nombre_estudiante };
                    for (int i = 0; i < 5; i++)
                    {
                        DateTime fechaDia = fechaInicioSemana.AddDays(i); // Día correspondiente de la semana
                        string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, fechaDia);

                        fila.Add(estadoAsistencia);  // Agregar el estado de asistencia al día correspondiente
                    }

                    // Agregar la fila de datos a la tabla
                    foreach (var valor in fila)
                    {
                        PdfPCell celdaAsistencia = new PdfPCell(new Phrase(valor));
                        celdaAsistencia.FixedHeight = alturaFila;  // Altura fija para la celda de asistencia
                        table.AddCell(celdaAsistencia);
                    }
                }
                // Agregar la tabla al documento
                doc.Add(table);
            }
            else if (Tiempo == "Mensual")//si es mensual
            {

                // Agrupar los días en semanas
                List<List<DateTime>> semanas = AgruparDiasEnSemanas(diasDelMes);
                // Por cada semana, crear una nueva tabla
                foreach (var semana in semanas)
                {
                    doc.Add(new Paragraph(" "));
                    // Crear una nueva tabla para la semana actual
                    PdfPTable table = new PdfPTable(semana.Count + 2); // Incluye cédula, nombre y los días de la semana
                    table.WidthPercentage = 110;
                    float alturaFila = 20f;



                    // Títulos de columna
                    string[] titulos = { "Cédula", "Nombre" };
                    foreach (var titulo in titulos)
                    {
                        PdfPCell celdaEncabezado = new PdfPCell(new Phrase(titulo, fontEncabezado));
                        celdaEncabezado.BackgroundColor = colorFondo;
                        celdaEncabezado.FixedHeight = alturaFila;
                        table.AddCell(celdaEncabezado);
                    }

                    // Títulos de los días (por ejemplo "Lunes 1", "Martes 2", etc.)
                    foreach (var dia in semana)
                    {
                        string tituloDia = $"{dia:dddd dd}"; // Formato: "Lunes 01"
                        PdfPCell celdaEncabezado = new PdfPCell(new Phrase(tituloDia, fontEncabezado));
                        celdaEncabezado.BackgroundColor = colorFondo;
                        table.AddCell(celdaEncabezado);
                    }

                    // Obtener la asistencia para cada estudiante
                    foreach (var estudiante in estudiantes)
                    {
                        var fila = new List<string> { estudiante.cedula_estudiante, estudiante.nombre_estudiante };

                        // Agregar los días de la semana (en la semana actual)
                        foreach (var dia in semana)
                        {
                            string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, dia);
                            fila.Add(estadoAsistencia);  // Agregar el estado de asistencia para el día correspondiente
                        }

                        // Agregar la fila de datos a la tabla
                        foreach (var valor in fila)
                        {
                            PdfPCell celdaAsistencia = new PdfPCell(new Phrase(valor));
                            celdaAsistencia.FixedHeight = alturaFila;  // Altura fija para la celda de asistencia
                            table.AddCell(celdaAsistencia);
                        }
                    }

                    // Agregar la tabla al documento
                    doc.Add(table);

                }

            }
            else if (Tiempo == "Periodo académico")
            {

                // Obtener las fechas exactas del periodo
                DateTime fechaInicioPeriodo = mesSeleccionado == "Primer Periodo"
                    ? new DateTime(fechaInicioSemana.Year, 2, 3)
                    : new DateTime(fechaInicioSemana.Year, 5, 26);

                DateTime fechaFinPeriodo = mesSeleccionado == "Primer Periodo"
                    ? new DateTime(fechaInicioSemana.Year, 5, 25)
                    : new DateTime(fechaInicioSemana.Year, 12, 10);

                // Obtener los meses involucrados en el periodo
                List<int> mesesDelPeriodo = Enumerable.Range(fechaInicioPeriodo.Month,
                    (fechaFinPeriodo.Month - fechaInicioPeriodo.Month) + 1).ToList();

                foreach (var mes in mesesDelPeriodo)
                {
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Mes: {new DateTime(fechaInicioSemana.Year, mes, 1).ToString("MMMM", new System.Globalization.CultureInfo("es-ES"))}"));
                    doc.Add(new Paragraph(" "));

                    // Calcular el primer y último día del mes que caen dentro del periodo
                    DateTime primerDiaMes = new DateTime(fechaInicioSemana.Year, mes, 1);
                    DateTime ultimoDiaMes = new DateTime(fechaInicioSemana.Year, mes, DateTime.DaysInMonth(fechaInicioSemana.Year, mes));

                    // Ajustar para que no salgan del rango del periodo
                    DateTime inicioMes = primerDiaMes < fechaInicioPeriodo ? fechaInicioPeriodo : primerDiaMes;
                    DateTime finMes = ultimoDiaMes > fechaFinPeriodo ? fechaFinPeriodo : ultimoDiaMes;

                    List<DateTime> diasLaborables = new List<DateTime>();

                    for (DateTime dia = inicioMes; dia <= finMes; dia = dia.AddDays(1))
                    {
                        if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday)
                        {
                            diasLaborables.Add(dia);
                        }
                    }

                    diasLaborables = diasLaborables.OrderBy(d => d).ToList();

                    // Agrupar días por semana
                    List<List<DateTime>> semanas = AgruparDiasEnSemanasPeriodo(diasLaborables);

                    foreach (var semana in semanas)
                    {
                        doc.Add(new Paragraph(" "));
                        PdfPTable table = new PdfPTable(semana.Count + 2); // Cédula + Nombre + días
                        table.WidthPercentage = 110;
                        float alturaFila = 20f;

                        // Encabezados
                        string[] encabezados = { "Cédula", "Nombre" };
                        foreach (var encabezadoTexto in encabezados)
                        {
                            PdfPCell celda = new PdfPCell(new Phrase(encabezadoTexto, fontEncabezado));
                            celda.BackgroundColor = colorFondo;
                            celda.FixedHeight = alturaFila;
                            table.AddCell(celda);
                        }

                        foreach (var dia in semana)
                        {
                            string tituloDia = $"{dia:dddd dd}"; // Ej: "Lunes 03"
                            PdfPCell celda = new PdfPCell(new Phrase(tituloDia, fontEncabezado));
                            celda.BackgroundColor = colorFondo;
                            celda.FixedHeight = alturaFila;
                            table.AddCell(celda);
                        }

                        // Cargar asistencia
                        foreach (var estudiante in estudiantes)
                        {
                            var fila = new List<string> { estudiante.cedula_estudiante, estudiante.nombre_estudiante };

                            foreach (var dia in semana)
                            {
                                string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, dia);
                                fila.Add(estadoAsistencia);
                            }

                            foreach (var valor in fila)
                            {
                                PdfPCell celdaAsistencia = new PdfPCell(new Phrase(valor));
                                celdaAsistencia.FixedHeight = alturaFila;
                                table.AddCell(celdaAsistencia);
                            }
                        }
                        doc.Add(table);
                    }
                }
            }
            doc.Close();
        }

        public static List<List<DateTime>> AgruparDiasEnSemanas(List<DateTime> diasDelMes)
        {
            List<List<DateTime>> semanas = new List<List<DateTime>>();
            List<DateTime> semanaActual = new List<DateTime>();

            foreach (var dia in diasDelMes)
            {
                semanaActual.Add(dia);

                // Si ya tenemos 7 días, agregamos la semana y comenzamos una nueva
                if (semanaActual.Count == 5)
                {
                    semanas.Add(new List<DateTime>(semanaActual));
                    semanaActual.Clear();  // Limpiar la lista para la siguiente semana
                }
            }

            // Si quedan días en la última semana (menos de 7), agregamos la semana final
            if (semanaActual.Count > 0)
            {
                semanas.Add(semanaActual);
            }

            return semanas;
        }

        public static List<List<DateTime>> AgruparDiasEnSemanasPeriodo(List<DateTime> diasDelMes)
        {
            List<List<DateTime>> semanas = new List<List<DateTime>>();
            List<DateTime> semanaActual = new List<DateTime>();
            DayOfWeek primerDiaSemana = DayOfWeek.Monday;

            foreach (var dia in diasDelMes.OrderBy(d => d))
            {
                // Si la semana actual está vacía o el día es lunes, inicia una nueva semana
                if (semanaActual.Count == 0 || dia.DayOfWeek == primerDiaSemana)
                {
                    if (semanaActual.Count > 0)
                    {
                        semanas.Add(new List<DateTime>(semanaActual));
                        semanaActual.Clear();
                    }
                }

                semanaActual.Add(dia);
            }

            // Agregar la última semana si quedó algo pendiente
            if (semanaActual.Count > 0)
            {
                semanas.Add(semanaActual);
            }

            return semanas;
        }

        public static void ExportarAsistenciaPorEstudiante(List<Estudiantes> estudiantes, string filePath, DateTime fechaInicioSemana, List<DateTime> diasDelMes, string Seccion, string nomDocente, string apeDocente, string mesSeleccionado, string Tiempo, List<int> mesesPeriodo = null)
        {
            // Crear el documento PDF
            Document doc = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            writer.PageEvent = new FooterHelper();
            doc.Open();
            var estudiante = estudiantes.FirstOrDefault();

            // 1. ENCABEZADO PRINCIPAL
            Paragraph encabezado = new Paragraph(
                "REPORTE DE ASISTENCIA INDIVIDUAL\n\n" +
                $"Estudiante: {estudiante.nombre_estudiante} {estudiante.primer_apellido}\n" +
                $"Cédula: {estudiante.cedula_estudiante}\n" +
                $"Sección: {Seccion}\n" +
                $"Docente: {nomDocente} {apeDocente}\n" +
                $"Periodo: {mesSeleccionado}\n\n",
                FontFactory.GetFont("Arial", 12, Font.BOLD)
            );
            encabezado.Alignment = Element.ALIGN_LEFT;
            doc.Add(encabezado);

            // Línea divisoria
            doc.Add(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(new Paragraph(" "));

            // 2. INICIALIZAR CONTADORES
            int totalPresentes = 0;
            int totalAusentes = 0;
            int anho = fechaInicioSemana.Year;

            // 3. CONFIGURACIÓN DE ESTILOS
            Font fontEncabezado = FontFactory.GetFont("Arial", 10, Font.BOLD);
            BaseColor colorFondo = BaseColor.LIGHT_GRAY;

            // 4. PROCESAR POR TIPO DE REPORTE
            if (Tiempo == "Semanal")
            {
                // TABLA SEMANAL
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                float alturaFila = 20f;

                // Encabezados de días
                string[] diasSemana = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
                foreach (var dia in diasSemana)
                {
                    DateTime fechaDia = fechaInicioSemana.AddDays(Array.IndexOf(diasSemana, dia));
                    PdfPCell celda = new PdfPCell(new Phrase($"{dia} {fechaDia.Day}", fontEncabezado));
                    celda.BackgroundColor = colorFondo;
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.FixedHeight = alturaFila;
                    table.AddCell(celda);
                }

                // Datos de asistencia
                for (int i = 0; i < 5; i++)
                {
                    DateTime fechaDia = fechaInicioSemana.AddDays(i);
                    string estado = "";

                    // Obtener estado de asistencia directamente
                    using (var contexto = new RegistroDocenteEntities())
                    {
                        var asistencia = contexto.Asistencia
                            .FirstOrDefault(a => a.id_estudiante == estudiante.id_estudiante && a.fecha == fechaDia.Date);
                        estado = asistencia?.estado ?? "No registrado";
                    }

                    // Actualizar contadores
                    if (estado == "Presente" || estado == "Justificado") totalPresentes++;
                    else if (estado == "Ausente" || estado == "Tarde") totalAusentes++;

                    PdfPCell celda = new PdfPCell(new Phrase(estado, FontFactory.GetFont("Arial", 9)));
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.FixedHeight = alturaFila;
                    table.AddCell(celda);
                }

                doc.Add(table);
            }
            else if (Tiempo == "Mensual")
            {
                // TABLAS MENSUALES - Agrupación directa sin método auxiliar
                List<List<DateTime>> semanas = new List<List<DateTime>>();
                List<DateTime> semanaActual = new List<DateTime>();

                foreach (var dia in diasDelMes)
                {
                    semanaActual.Add(dia);

                    if (semanaActual.Count == 5) // Semana completa (L-V)
                    {
                        semanas.Add(new List<DateTime>(semanaActual));
                        semanaActual.Clear();
                    }
                }

                if (semanaActual.Count > 0)
                {
                    semanas.Add(semanaActual);
                }

                // Generar tablas
                foreach (var semana in semanas)
                {
                    PdfPTable table = new PdfPTable(semana.Count);
                    table.WidthPercentage = 100;
                    float alturaFila = 20f;

                    // Encabezados
                    foreach (var dia in semana)
                    {
                        PdfPCell celda = new PdfPCell(new Phrase($"{dia:dddd dd}", fontEncabezado));
                        celda.BackgroundColor = colorFondo;
                        celda.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda.FixedHeight = alturaFila;
                        table.AddCell(celda);
                    }

                    // Datos
                    foreach (var dia in semana)
                    {
                        string estado = "";

                        // Obtener estado de asistencia directamente
                        using (var contexto = new RegistroDocenteEntities())
                        {
                            var asistencia = contexto.Asistencia
                                .FirstOrDefault(a => a.id_estudiante == estudiante.id_estudiante && a.fecha == dia.Date);
                            estado = asistencia?.estado ?? "No registrado";
                        }

                        // Actualizar contadores
                        if (estado == "Presente" || estado == "Justificado") totalPresentes++;
                        else if (estado == "Ausente" || estado == "Tarde") totalAusentes++;

                        PdfPCell celda = new PdfPCell(new Phrase(estado, FontFactory.GetFont("Arial", 9)));
                        celda.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda.FixedHeight = alturaFila;
                        table.AddCell(celda);
                    }

                    doc.Add(table);
                    doc.Add(new Paragraph(" ")); // Espacio entre semanas
                }
            }
            else if (Tiempo == "Periodo académico")
            {
                // DEFINIR FECHAS EXACTAS DEL PERIODO
                DateTime fechaInicioPeriodo = mesSeleccionado == "Primer Periodo"
                    ? new DateTime(anho, 2, 3)   // Primer Periodo: 3 de febrero
                    : new DateTime(anho, 5, 26); // Segundo Periodo: 26 de mayo

                DateTime fechaFinPeriodo = mesSeleccionado == "Primer Periodo"
                    ? new DateTime(anho, 5, 25)   // Primer Periodo hasta 25 de mayo
                    : new DateTime(anho, 12, 10); // Segundo Periodo hasta 10 de diciembre

                // Obtener los meses involucrados en el periodo
                List<int> mesesDelPeriodo = Enumerable.Range(
                    fechaInicioPeriodo.Month,
                    (fechaFinPeriodo.Month - fechaInicioPeriodo.Month) + 1).ToList();

                foreach (var mes in mesesDelPeriodo)
                {
                    // TÍTULO DEL MES
                    doc.Add(new Paragraph($"MES: {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes).ToUpper()}",
                    FontFactory.GetFont("Arial", 11, Font.BOLD)));
                    doc.Add(new Paragraph(" "));


                    // Calcular el rango de fechas válido para este mes dentro del periodo
                    DateTime primerDiaMes = new DateTime(anho, mes, 1);
                    DateTime ultimoDiaMes = new DateTime(anho, mes, DateTime.DaysInMonth(anho, mes));

                    DateTime inicioMes = primerDiaMes < fechaInicioPeriodo ? fechaInicioPeriodo : primerDiaMes;
                    DateTime finMes = ultimoDiaMes > fechaFinPeriodo ? fechaFinPeriodo : ultimoDiaMes;

                    var diasLaborables = new List<DateTime>();
                    for (DateTime dia = inicioMes; dia <= finMes; dia = dia.AddDays(1))
                    {
                        if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday)
                        {
                            diasLaborables.Add(dia);
                        }
                    }

                    // AGRUPAR POR SEMANAS NATURALES (sin método auxiliar)
                    var semanas = new List<List<DateTime>>();
                    var semanaActual = new List<DateTime>();

                    foreach (var dia in diasLaborables)
                    {
                        semanaActual.Add(dia);

                        if (dia.DayOfWeek == DayOfWeek.Friday || dia == diasLaborables.Last())
                        {
                            semanas.Add(semanaActual);
                            semanaActual = new List<DateTime>();
                        }
                    }

                    // GENERAR TABLAS
                    foreach (var semana in semanas)
                    {
                        PdfPTable table = new PdfPTable(semana.Count);
                        table.WidthPercentage = 100;
                        float alturaFila = 20f;

                        // ENCABEZADOS
                        foreach (var dia in semana)
                        {
                            PdfPCell celda = new PdfPCell(new Phrase($"{dia:dddd dd}", fontEncabezado));
                            celda.BackgroundColor = colorFondo;
                            celda.HorizontalAlignment = Element.ALIGN_CENTER;
                            celda.FixedHeight = alturaFila;
                            table.AddCell(celda);
                        }

                        // DATOS
                        foreach (var dia in semana)
                        {
                            string estado = "";

                            // Obtener estado de asistencia directamente
                            using (var contexto = new RegistroDocenteEntities())
                            {
                                var asistencia = contexto.Asistencia
                                    .FirstOrDefault(a => a.id_estudiante == estudiante.id_estudiante && a.fecha == dia.Date);
                                estado = asistencia?.estado ?? "No registrado";
                            }

                            // Actualizar contadores
                            if (estado == "Presente" || estado == "Justificado") totalPresentes++;
                            else if (estado == "Ausente" || estado == "Tarde") totalAusentes++;

                            PdfPCell celda = new PdfPCell(new Phrase(estado, FontFactory.GetFont("Arial", 9)));
                            celda.HorizontalAlignment = Element.ALIGN_CENTER;
                            celda.FixedHeight = alturaFila;
                            table.AddCell(celda);
                        }

                        doc.Add(table);
                        doc.Add(new Paragraph(" ")); // Espacio entre semanas
                    }
                }
            }

            doc.Close();
        }

        // Clase para el footer
        public class FooterHelper : PdfPageEventHelper
        {
            private PdfTemplate template;
            private BaseFont baseFont;

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                template = writer.DirectContent.CreateTemplate(50, 50);
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                // Configurar el footer
                PdfContentByte cb = writer.DirectContent;
                cb.SetColorFill(BaseColor.DARK_GRAY);
                cb.SetFontAndSize(baseFont, 10);

                // Texto del footer (izquierda)
                string footerText = "Registro Docente 360";
                float x = document.LeftMargin;
                float y = document.BottomMargin - 10;
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, footerText, x, y, 0);
                cb.EndText();

                // Número de página (derecha)
                string pageText = "Página " + writer.PageNumber + " de ";
                float len = baseFont.GetWidthPoint(pageText, 10);
                float pageX = document.Right - len - 20;
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, pageText, pageX, y, 0);
                cb.EndText();

                // Agregar el total de páginas
                cb.AddTemplate(template, pageX + len, y);
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                // Establecer el total de páginas
                template.BeginText();
                template.SetFontAndSize(baseFont, 10);
                template.SetTextMatrix(0, 0);
                template.ShowText("" + (writer.PageNumber));
                template.EndText();
            }
        }

        public static string ObtenerEstadoAsistencia(int idEstudiante, DateTime fecha)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                // Comparar solo la fecha sin la parte de la hora
                var asistencia = contexto.Asistencia
                    .FirstOrDefault(a => a.id_estudiante == idEstudiante && a.fecha == fecha.Date);  // Usar .Date para ignorar la hora

                return asistencia?.estado ?? "No registrado";  // Si no tiene asistencia, mostrar "No registrado"
            }
        }
    }
}