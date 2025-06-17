using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro_Docente_360.Eventos
{
    public class FechaSeleccionadaEventArgs : EventArgs
    {
        public int Anho { get; set; }
        public string MesTexto { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}
