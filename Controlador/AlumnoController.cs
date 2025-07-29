using Modelos.EntityFramework;
using Registro_Docente_360.Eventos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Modelos;


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

        public void GuardarEstudiantes(List<Estudiantes> estudiantes, int idSeccion)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                foreach (var estudiante in estudiantes)
                {
                    estudiante.id_seccion = idSeccion;
                    var existe = contexto.Estudiantes.FirstOrDefault(e => e.cedula_estudiante == estudiante.cedula_estudiante);
                    if (existe == null)
                    {
                        contexto.Estudiantes.Add(estudiante);
                        contexto.SaveChanges();

                        var materiasDocente = contexto.Horarios
                            .Where(h => h.id_usuario == Sesion.IdUsuario)
                            .Select(h => h.id_materia).Distinct().ToList();

                        foreach (var idMateria in materiasDocente)
                        {
                            contexto.Clases.Add(new Clases
                            {
                                id_usuario = Sesion.IdUsuario,
                                id_materia = idMateria,
                                id_estudiante = estudiante.id_estudiante
                            });
                        }
                    }
                    else
                    {
                    }
                }
                contexto.SaveChanges();
            }
        }

        public List<Estudiantes> ObtenerEstudiantesPorDocente(int idDocente)
        {
            using (var db = new RegistroDocenteEntities())
            {
                return db.Clases
                    .Where(c => c.id_usuario == idDocente)
                    .Select(c => c.Estudiantes)
                    .Distinct()
                    .ToList();
            }
        }





        public bool EliminarEstudiantePorCedula(string cedula)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var estudiante = contexto.Estudiantes.FirstOrDefault(e => e.cedula_estudiante == cedula);
                if (estudiante == null) return false;

                // Buscar clases asociadas al estudiante
                var clases = contexto.Clases.Where(c => c.id_estudiante == estudiante.id_estudiante).ToList();

                foreach (var clase in clases)
                {
                    // Eliminar notas relacionadas a esa clase
                    var notas = contexto.Notas.Where(n => n.id_clase == clase.id_clase).ToList();
                    contexto.Notas.RemoveRange(notas);
                }

                // Eliminar las clases del estudiante
                contexto.Clases.RemoveRange(clases);

                // Finalmente, eliminar al estudiante
                contexto.Estudiantes.Remove(estudiante);

                contexto.SaveChanges();
                return true;
            }
        }


        public List<Clases> ObtenerClasesDelDocenteYEstudiante(int idDocente, int idEstudiante)
        {
            using (var db = new RegistroDocenteEntities())
            {
                return db.Clases
                    .Where(c => c.id_usuario == idDocente && c.id_estudiante == idEstudiante)
                    .ToList();
            }
        }



    }
}




    
