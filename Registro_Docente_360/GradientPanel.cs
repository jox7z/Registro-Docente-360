using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Docente_360
{
    public class GradientPanel : Panel
    {
        // Propiedades públicas para definir los colores del degradado (arriba y abajo)
        public Color gradientTop { get; set; }
        public Color gradientBottom { get; set; }

        // Constructor del panel con degradado
        public GradientPanel()
        {
            // Se suscribe al evento Resize para redibujar cuando cambia el tamaño
            this.Resize += GradientPanel_Resize;
        }

        private void GradientPanel_Resize(object sender, EventArgs e)
        {
            // Marca el control como pendiente de redibujo
            this.Invalidate();
        }

        // Sobrescribe el método OnPaint para dibujar el fondo con degradado
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Crea un pincel lineal con los colores definidos
            LinearGradientBrush linear = new LinearGradientBrush(
                this.ClientRectangle,        // Área a rellenar
                this.gradientTop,            // Color inicial (parte superior)
                this.gradientBottom,         // Color final (parte inferior)
                90F                          // Ángulo del degradado (90° = vertical)
            );

            // Obtiene el contexto gráfico para dibujar
            Graphics g = e.Graphics;

            // Rellena el área del panel con el degradado
            g.FillRectangle(linear, this.ClientRectangle);

            // Llama a la implementación base para asegurar que se dibuje correctamente
            base.OnPaint(e);
        }
    }
}
