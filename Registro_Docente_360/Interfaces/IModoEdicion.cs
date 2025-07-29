using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Registro_Docente_360.Interfaces
{
    public interface IModoEdicion
    {
        bool EstaEnModoEdicion { get; }
        void CancelarModoEdicion();
    }

}
