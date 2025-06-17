using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Registro_Docente_360.Controladores
{
    /// <summary>
    /// Controlador responsable de las validaciones relacionadas con la entidad Alumno.
    /// </summary>
    public class AlumnoController
    {
        /// <summary>
        /// Valida el formato de una cédula costarricense.
        /// </summary>
        /// <param name="cedula">Cédula a validar.</param>
        /// <param name="mensajeError">Mensaje con la razón del fallo si aplica.</param>
        /// <returns>True si es válida, False si no.</returns>
        public bool ValidarCedula(string cedula, out string mensajeError)
        {
            mensajeError = "";

            if (string.IsNullOrWhiteSpace(cedula))
            {
                mensajeError = "La cédula no puede estar vacía.";
                return false;
            }

            if (cedula.Length > 20)
            {
                mensajeError = "La cédula no debe superar los 20 caracteres.";
                return false;
            }

            if (!Regex.IsMatch(cedula, @"^[A-Z0-9\-]+$"))
            {
                mensajeError = "La cédula solo puede contener letras, números y guiones.";
                return false;
            }

            return true;
        }

    }
}
