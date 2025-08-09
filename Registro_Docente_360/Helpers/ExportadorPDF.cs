using iTextSharp.text;
using iTextSharp.text.pdf;
using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using System;
using System.Collections.Generic;
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
                PdfWriter.GetInstance(doc, new FileStream(nombreArchivo, FileMode.Create));
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
                // Crear el documento PDF
                Document doc = new Document(PageSize.A4.Rotate());
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create)); // Usa la ruta proporcionada

                // Abrir el documento para escribir
                doc.Open();
                // Encabezado con nombre, sección y materia
                Paragraph encabezado = new Paragraph(
                    "Reporte de notas\n" +
                    $"Periodo: {periodoSeleccionado}\n"+
                    $"Docente: {nomDocente} {apeDocente}\n" +
                    $"Sección: {Seccion}\n\n",
                    FontFactory.GetFont("Arial", 12, Font.BOLD)
                );

                encabezado.Alignment = Element.ALIGN_LEFT;
                doc.Add(encabezado);

                // Crear la tabla
                PdfPTable table = new PdfPTable(9);
                table.WidthPercentage = 100;
                float alturaFila = 20f;

                // Agregar encabezados de columna para las notas
                string[] titulos = new string[] {
            "Cedula","Nombre estudiante","Primer apellido","Primer Examen", "Segundo Examen", "Tareas", "Asistencia", "Cotidiano", "Nota Final"};

                foreach (var titulo in titulos)
                {
                    PdfPCell celdaEncabezado = new PdfPCell(new Phrase(titulo, FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    celdaEncabezado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    celdaEncabezado.FixedHeight = alturaFila;
                    table.AddCell(celdaEncabezado);
                }

                // Lógica para manejar los periodos
                foreach (var estudiante in estudiantes)
                {
                    var nota = ObtenerNotaPorEstudianteYMateria(estudiante, materia, periodoSeleccionado); // Llamada para obtener las notas del periodo seleccionado

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

                // Agregar la tabla al documento
                doc.Add(table);
                doc.Close();

                MessageBox.Show("PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (var estudiante in estudiantes)
            {
                var nota = ObtenerNotaPorEstudianteYMateria(estudiante, materia, periodoSeleccionado);
            }
        }




        public static void ExportarNotasPorEstudiante(List<Clases> clases, Estudiantes estudiante, string filePath, string Seccion, string nomDocente, string apeDocente, string periodoSeleccionado)
        {
            // Crear el documento PDF
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));  // Usar la ruta proporcionada

            // Abrir el documento para escribir
            doc.Open();

            // Encabezado con nombre, sección y materia
            Paragraph encabezado = new Paragraph(
                "Reporte de notas del " + $"estudiante: {estudiante.nombre_estudiante} {estudiante.primer_apellido}\n" +
                $"{periodoSeleccionado}\n"+
                $"Cedula estudiante: {estudiante.cedula_estudiante}\n" +
                $"Docente: {nomDocente} {apeDocente}\n" +
                $"Sección: {Seccion}\n\n",
                FontFactory.GetFont("Arial", 12, Font.BOLD)
            );

            encabezado.Alignment = Element.ALIGN_LEFT;
            doc.Add(encabezado);

            // Crear la tabla
            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100;
            float alturaFila = 20f;

            // Agregar encabezados de columna para las notas
            string[] titulos = new string[]
            {
        "Materia", "Primer Examen", "Segundo Examen", "Tareas", "Asistencia", "Cotidiano", "Nota Final"
            };

            foreach (var titulo in titulos)
            {
                PdfPCell celdaEncabezado = new PdfPCell(new Phrase(titulo, FontFactory.GetFont("Arial", 10, Font.BOLD)));
                celdaEncabezado.BackgroundColor = BaseColor.LIGHT_GRAY;
                celdaEncabezado.FixedHeight = alturaFila;  // Aquí aplicamos el mismo FixedHeight para los encabezados
                table.AddCell(celdaEncabezado);
            }

            // Agregar las clases del estudiante
            foreach (var clase in clases)
            {
                var materia = ObtenerMateriaPorClase(clase); // Obtener la materia asociada a la clase
                var nota = ObtenerNotaPorClase(clase, periodoSeleccionado); // Obtener la nota de la clase

                // Agregar cada celda para el estudiante
                PdfPCell celdaMateria = new PdfPCell(new Phrase(materia?.nombre_materia ?? "Sin materia"));
                celdaMateria.FixedHeight = alturaFila;
                table.AddCell(celdaMateria);

                PdfPCell celdaPrimerExamen = new PdfPCell(new Phrase(nota?.primer_examen.ToString() ?? "0"));
                celdaPrimerExamen.FixedHeight = alturaFila;
                table.AddCell(celdaPrimerExamen);

                PdfPCell celdaSegundoExamen = new PdfPCell(new Phrase(nota?.segundo_examen.ToString() ?? "0"));
                celdaSegundoExamen.FixedHeight = alturaFila;
                table.AddCell(celdaSegundoExamen);

                PdfPCell celdaTareas = new PdfPCell(new Phrase(nota?.tareas.ToString() ?? "0"));
                celdaTareas.FixedHeight = alturaFila;
                table.AddCell(celdaTareas);

                PdfPCell celdaAsistencia = new PdfPCell(new Phrase(nota?.asistencia.ToString() ?? "0"));
                celdaAsistencia.FixedHeight = alturaFila;
                table.AddCell(celdaAsistencia);

                PdfPCell celdaCotidiano = new PdfPCell(new Phrase(nota?.cotidiano.ToString() ?? "0"));
                celdaCotidiano.FixedHeight = alturaFila;
                table.AddCell(celdaCotidiano);

                PdfPCell celdaNotaFinal = new PdfPCell(new Phrase(nota?.nota_final.ToString() ?? "0"));
                celdaNotaFinal.FixedHeight = alturaFila;
                table.AddCell(celdaNotaFinal);
            }

            // Agregar la tabla al documento
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
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
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
                int anho = fechaInicioSemana.Year;
                // Agrupar los meses según el periodo seleccionado
                List<int> mesesDelPeriodo = new List<int>();

                if (mesSeleccionado == "Primer Periodo")
                {
                    mesesDelPeriodo.AddRange(new List<int> { 2, 3, 4, 5, 6 });
                }
                else if (mesSeleccionado == "Segundo Periodo")
                {
                    mesesDelPeriodo.AddRange(new List<int> { 7, 8, 9, 10, 11, 12 });
                }


                foreach (var mes in mesesDelPeriodo)
                {
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Mes: {new DateTime(fechaInicioSemana.Year, mes, 1).ToString("MMMM", new System.Globalization.CultureInfo("es-ES"))}"));
                    doc.Add(new Paragraph(" "));
                    // Obtener todos los días laborables del mes
                    DateTime primerDiaMes = new DateTime(anho, mes, 1);


                    List<DateTime> diasLaborables = new List<DateTime>();

                    for (DateTime dia = primerDiaMes; dia.Month == mes; dia = dia.AddDays(1))
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
                                celdaAsistencia.FixedHeight = alturaFila;  // Altura fija para la celda de asistencia
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
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();
            var estudiante = estudiantes.FirstOrDefault();

            // Inicializar contadores de presencia y ausencia
            int totalPresentes = 0;
            int totalAusentes = 0;

            // Contar los días presentes y ausentes en el caso de "Semanal", "Mensual" y "Periodo académico"
            if (Tiempo == "Semanal")
            {
                // Calcular los días presentes y ausentes para la semana
                for (int i = 0; i < 5; i++)
                {
                    DateTime fechaDia = fechaInicioSemana.AddDays(i); // Día correspondiente de la semana
                    string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, fechaDia);
                    if (estadoAsistencia == "Presente")
                        totalPresentes++;
                    else if (estadoAsistencia == "Ausente")
                        totalAusentes++;
                    else if (estadoAsistencia == "Tarde")
                        totalAusentes++;
                    else if (estadoAsistencia == "Justificado")
                        totalPresentes++;
                }
            }
            else if (Tiempo == "Mensual")
            {
                foreach (var dia in diasDelMes)
                {
                    string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, dia);
                    if (estadoAsistencia == "Presente")
                        totalPresentes++;
                    else if (estadoAsistencia == "Ausente")
                        totalAusentes++;
                }
            }
            else if (Tiempo == "Periodo académico")
            {
                foreach (var mes in mesesPeriodo)
                {
                    DateTime primerDiaMes = new DateTime(fechaInicioSemana.Year, mes, 1);
                    var diasLaborables = new List<DateTime>();

                    for (DateTime dia = primerDiaMes; dia.Month == mes; dia = dia.AddDays(1))
                    {
                        if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday)
                        {
                            diasLaborables.Add(dia);
                            string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, dia);
                            if (estadoAsistencia == "Presente")
                                totalPresentes++;
                            else if (estadoAsistencia == "Ausente")
                                totalAusentes++;
                        }
                    }
                }
            }

            // Encabezado con los totales de presencia y ausencia
            Paragraph encabezado = new Paragraph(
                $"Reporte de asistencia\n" +
                $"Estudiante: {estudiante.nombre_estudiante} {estudiante.primer_apellido}\n" +
                $"Sección: {Seccion}\n" +
                $"Mes: {mesSeleccionado} \n" +
                $"Docente: {nomDocente} {apeDocente}\n" +
                $"Ausencias: {totalAusentes}  |  Presentes: {totalPresentes}\n\n" ,// Mostrar los totales
                FontFactory.GetFont("Arial", 12, Font.BOLD)
            );
            encabezado.Alignment = Element.ALIGN_LEFT;
            doc.Add(encabezado);

            // Crear el estilo para los titulos
            Font fontEncabezado = FontFactory.GetFont("Arial", 10, Font.BOLD);
            BaseColor colorFondo = BaseColor.LIGHT_GRAY;

            if (Tiempo == "Semanal")
            {
                // Si es semanal, se necesita solo una tabla que tenga Lunes a Viernes
                PdfPTable table = new PdfPTable(5); // Incluye cédula, nombre y los 5 días de la semana (lunes a viernes)
                table.WidthPercentage = 110;
                float alturaFila = 20f;


                // Títulos de columna
                string[] diasSemana = new string[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
                foreach (var dia in diasSemana) //pone el estilo a cada titulo
                {
                    // Calcular la fecha de cada día basado en la fecha de inicio de la semana
                    DateTime fechaDia = fechaInicioSemana.AddDays(Array.IndexOf(diasSemana, dia)); // Calcula el día sumando días a la fecha de inicio de la semana
                    string titulos = $"{dia} {fechaDia.Day}";
                    PdfPCell celdaEncabezado = new PdfPCell(new Phrase(titulos, fontEncabezado));
                    celdaEncabezado.BackgroundColor = colorFondo;
                    celdaEncabezado.FixedHeight = alturaFila;
                    table.AddCell(celdaEncabezado);
                }
                if (estudiante != null)
                {
                    // Crear la fila con los días de la semana (lunes a viernes)
                    var fila = new List<string>();

                    // Agregar los días de la semana (lunes a viernes) a la fila
                    for (int i = 0; i < 5; i++)
                    {
                        DateTime fechaDia = fechaInicioSemana.AddDays(i); // Día correspondiente de la semana
                        string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, fechaDia);
                        fila.Add(estadoAsistencia);  // Agregar el estado de asistencia al día correspondiente
                    }

                    // Agregar los valores a la tabla
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
            else if (Tiempo == "Mensual")// Si es mensual
            {
                // Agrupar los días en semanas
                List<List<DateTime>> semanas = AgruparDiasEnSemanas(diasDelMes);

                // Por cada semana, crear una nueva tabla
                foreach (var semana in semanas)
                {
                    // Crear una nueva tabla para la semana actual
                    PdfPTable table = new PdfPTable(semana.Count);

                    table.WidthPercentage = 110;
                    float alturaFila = 20f;


                    // Títulos de los días (por ejemplo "Lunes 1", "Martes 2", etc.)
                    foreach (var dia in semana)
                    {
                        string tituloDia = $"{dia:dddd dd}"; // Formato: "Lunes 01"
                        PdfPCell celdaEncabezado = new PdfPCell(new Phrase(tituloDia, fontEncabezado));
                        celdaEncabezado.BackgroundColor = colorFondo;
                        celdaEncabezado.FixedHeight = alturaFila;
                        table.AddCell(celdaEncabezado);
                    }

                    if (estudiante != null)
                    {
                        // Crear la fila con los días de la semana (lunes a viernes)
                        var fila = new List<string>();

                        // Agregar los días de la semana (en la semana actual)
                        foreach (var dia in semana)
                        {
                            string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, dia);
                            fila.Add(estadoAsistencia);  // Agregar el estado de asistencia para el día correspondiente
                        }

                        // Agregar los valores a la tabla
                        foreach (var valor in fila)
                        {
                            PdfPCell celdaAsistencia = new PdfPCell(new Phrase(valor));
                            celdaAsistencia.FixedHeight = alturaFila;  // Altura fija para la celda de asistencia
                            table.AddCell(celdaAsistencia);
                        }
                    }

                    // Agregar la tabla al documento
                    doc.Add(table);

                    // Agregar un espacio (salto de línea) entre las tablas de cada semana
                    doc.Add(new Paragraph(" "));  // Esto agrega un salto de línea entre las tablas de las semanas
                }
            }
            else if (Tiempo == "Periodo académico")
            {
                int anho = fechaInicioSemana.Year;
                // Agrupar los meses según el periodo seleccionado
                List<int> mesesDelPeriodo = new List<int>();

                if (mesSeleccionado == "Primer Periodo")
                {
                    mesesDelPeriodo.AddRange(new List<int> { 2, 3, 4, 5, 6 });
                }
                else if (mesSeleccionado == "Segundo Periodo")
                {
                    mesesDelPeriodo.AddRange(new List<int> { 7, 8, 9, 10, 11, 12 });
                }

                foreach (var mes in mesesDelPeriodo)
                {
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Mes: {new DateTime(fechaInicioSemana.Year, mes, 1).ToString("MMMM", new System.Globalization.CultureInfo("es-ES"))}\n\n"));

                    // Obtener todos los días laborables del mes
                    DateTime primerDiaMes = new DateTime(anho, mes, 1);


                    List<DateTime> diasLaborables = new List<DateTime>();

                    for (DateTime dia = primerDiaMes; dia.Month == mes; dia = dia.AddDays(1))
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
                        PdfPTable table = new PdfPTable(semana.Count); // Cédula + Nombre + días
                        table.WidthPercentage = 110;
                        float alturaFila = 20f;

                        foreach (var dia in semana)
                        {
                            string tituloDia = $"{dia:dddd dd}"; // Ej: "Lunes 03"
                            PdfPCell celda = new PdfPCell(new Phrase(tituloDia, fontEncabezado));
                            celda.BackgroundColor = colorFondo;
                            celda.FixedHeight = alturaFila;
                            table.AddCell(celda);
                        }

                        // Cargar asistencia
                        if (estudiante != null)
                        {
                            var fila = new List<string>();

                            foreach (var dia in semana)
                            {
                                string estadoAsistencia = ObtenerEstadoAsistencia(estudiante.id_estudiante, dia);
                                fila.Add(estadoAsistencia);


                            }
                            foreach (var valor in fila)
                            {
                                PdfPCell celdaAsistencia = new PdfPCell(new Phrase(valor));
                                celdaAsistencia.FixedHeight = alturaFila;  // Altura fija para la celda de asistencia
                                table.AddCell(celdaAsistencia);
                            }
                            doc.Add(table);
                        }
                    }

                }

            }

            // Cerrar el documento
            doc.Close();
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
