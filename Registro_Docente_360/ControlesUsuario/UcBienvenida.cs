using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Docente_360.Forms
{
    public partial class UcBienvenida : UserControl
    {
        public UcBienvenida()
        {
            InitializeComponent();
            MostrarFraseAleatoria();

            CentrarMiniContenedor();
        }

        private void CentrarMiniContenedor()
        {
            panelminiContenedor.Left = (this.ClientSize.Width - panelminiContenedor.Width) / 2;
            panelminiContenedor.Top = (this.ClientSize.Height - panelminiContenedor.Height) / 2;
        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private readonly List<string> frases = new List<string>
        {
                "La educación es el arma más poderosa para cambiar el mundo. — Nelson Mandela",
                "Educar la mente sin educar el corazón no es educar en absoluto. — Aristóteles",
                "Una inversión en conocimiento siempre paga los mejores intereses. — B. Franklin",
                "El aprendizaje es un tesoro que seguirá a su dueño a todas partes. — Proverbio chino",
                "Nunca se es demasiado viejo para aprender algo nuevo. — Proverbio latino",
                "Los niños deben aprender a pensar, no qué pensar. — Margaret Mead",
                "Un buen maestro puede inspirar esperanza y encender la imaginación. — Brad Henry",
                "La educación no es llenar un balde, sino encender un fuego. — W.B. Yeats",
                "Quien se atreve a enseñar nunca debe dejar de aprender. — John C. Dana",
                "Un libro abierto es un cerebro que habla; cerrado, un amigo que espera. — Proverbio árabe",
                "La raíz de la educación es amarga, pero su fruto es dulce. — Aristóteles",
                "Una mente educada siempre está lista para nuevas ideas. — Proverbio moderno",
                "El arte de enseñar es el arte de ayudar a descubrir. — Mark Van Doren",
                "Una palabra amable puede abrir incluso las puertas más cerradas. — Proverbio chino",
                "El principio de la educación es predicar con el ejemplo. — Turgot",
                "Aprender sin reflexionar es malgastar energía. — Confucio",
                "Educar es sembrar en el alma de los demás. — Platón",
                "Enseñar es dejar una huella en la vida de una persona. — Anónimo",
                "Sin educación, no hay libertad real ni verdadera democracia. — Anónimo",
                "La escuela es el lugar donde se cultivan los sueños del mañana. — Anónimo"
        };



        private void MostrarFraseAleatoria()
        {
            Random rnd = new Random();
            int index = rnd.Next(frases.Count);
            lblFrases.Text = frases[index];
        }

        private void lblFrase_Click(object sender, EventArgs e)
        {

        }

        private void lblFrase_Click_1(object sender, EventArgs e)
        {

        }
    }
}
