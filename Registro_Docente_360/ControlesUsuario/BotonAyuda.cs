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
    public partial class BotonAyuda : UserControl
    {
        public BotonAyuda()
        {
            InitializeComponent();
            InicializarToolTip();
        }

        private ToolTip ayudaToolTip;

        private void InicializarToolTip()
        {
            ayudaToolTip = new ToolTip
            {
                IsBalloon = true,
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 100,
                ShowAlways = true
            };

            ayudaToolTip.SetToolTip(pictureAyuda, "Haz clic para obtener ayuda");
        }

        private void pictureAyuda_Click(object sender, EventArgs e)
        {
            //Aqui cargan lo que sea que se vaya a cargar 
        }
    }
}
