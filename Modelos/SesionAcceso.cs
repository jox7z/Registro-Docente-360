using System;

namespace Registro_Docente_360.Modelos
{
    public class SesionAcceso
    {
        public int ID { get; set; }
        public string Usuario { get; set; }
        public DateTime? Ingreso { get; set; }
        public DateTime? Salida { get; set; }
        public string Resultado { get; set; }
    }
}
