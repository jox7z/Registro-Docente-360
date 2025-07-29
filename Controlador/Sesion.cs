using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro_Docente_360.Eventos
{
    public static class Sesion
    {
        public static int IdUsuario { get; set; }
        public static int IdRol { get; set; }
        public static string NombreUsuario { get; set; }

        // Nuevas propiedades necesarias para UcInfoCuenta
        public static string Nombre { get; set; }
        public static string Correo { get; set; }
        public static string Rol { get; set; } // Ej: "Administrador" o "Docente"
        public static DateTime FechaRegistro { get; set; }
        public static string Contrasena { get; set; } // Solo para validación de contraseña en memoria

        // Para el módulo de fechas
        public static int? UltimoAnhoSeleccionado { get; set; }
        public static int? UltimoMesIndex { get; set; }
        public static int? UltimaSemanaIndex { get; set; }


    }
}
