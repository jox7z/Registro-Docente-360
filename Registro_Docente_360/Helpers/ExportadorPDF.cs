using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

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
    }
}
