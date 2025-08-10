using Modelos;
using Modelos.EntityFramework;
using Registro_Docente_360.Eventos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;


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

     

        public List<Estudiantes> GuardarEstudiantes(List<Estudiantes> estudiantes, int idSeccion,int docente)
        {
            var nuevosEstudiantes = new List<Estudiantes>();

            using (var contexto = new RegistroDocenteEntities())
            {
                foreach (var estudiante in estudiantes)
                {
                    estudiante.id_seccion = idSeccion;
                    var existe = contexto.Estudiantes
                        .FirstOrDefault(e => e.cedula_estudiante == estudiante.cedula_estudiante);

                    if (existe == null)
                    {
                        contexto.Estudiantes.Add(estudiante);
                        contexto.SaveChanges();

                        var materiasDocente = contexto.Horarios
                            .Where(h => h.id_usuario == docente)
                            .Select(h => h.id_materia)
                            .Distinct()
                            .ToList();

                        foreach (var idMateria in materiasDocente)
                        {
                            contexto.Clases.Add(new Clases
                            {
                                id_usuario = docente,
                                id_materia = idMateria,
                                id_estudiante = estudiante.id_estudiante
                            });
                        }

                        nuevosEstudiantes.Add(estudiante);
                    }
                }

                contexto.SaveChanges(); // Guardar clases nuevas
            }

            return nuevosEstudiantes;
        }


        public List<Estudiantes> ObtenerEstudiantesPorDocente(int idDocente)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                return contexto.Clases
                    .Where(c => c.id_usuario == idDocente)
                    .Select(c => c.Estudiantes)
                    .Distinct()
                    .ToList();
            }
        }

        public Estudiantes ObtenerEstudiantesPorCedula(string cedula)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                return contexto.Estudiantes.FirstOrDefault(e => e.cedula_estudiante == cedula);
            }
        }




        public bool EliminarEstudiantePorCedula(string cedula)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var estudiante = contexto.Estudiantes.FirstOrDefault(e => e.cedula_estudiante == cedula);
                if (estudiante == null) return false;

                var clases = contexto.Clases
                    .Where(c => c.id_estudiante == estudiante.id_estudiante)
                    .ToList();

                foreach (var clase in clases)
                {
                    // ✅ 1. Eliminar asistencias

                    // ✅ 2. Eliminar notas
                    var notas = contexto.Notas.Where(n => n.id_clase == clase.id_clase).ToList();
                    contexto.Notas.RemoveRange(notas);
                }

                // ✅ 3. Eliminar clases
                contexto.Clases.RemoveRange(clases);

                // ✅ 4. Eliminar estudiante
                contexto.Estudiantes.Remove(estudiante);

                contexto.SaveChanges();
                return true;
            }
        }

        //usuario
        public List<Clases> ObtenerClasesDelDocenteYEstudiante(int idDocente, int idEstudiante)
        {
            using (var db = new RegistroDocenteEntities())
            {
                return db.Clases
                    .Where(c => c.id_usuario == idDocente && c.id_estudiante == idEstudiante)
                    .ToList();
            }
        }

        public List<Usuarios> ObtenerUsuarios()
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                return contexto.Usuarios.Include("Roles").Include("Secciones").ToList();
            }

        }
        public void GuardarUsuarios(List<(Usuarios usuario, string nombreSeccion)> usuariosConSeccion)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                foreach (var (u, nombreSeccion) in usuariosConSeccion)
                {
                    // 1. Verificar si la sección existe, si no, crearla
                    int? idSeccion = null;

                    if (!string.IsNullOrWhiteSpace(nombreSeccion))
                    {
                        var seccionExistente = contexto.Secciones
                            .FirstOrDefault(s => s.nombre_seccion == nombreSeccion);

                        if (seccionExistente != null)
                        {
                            idSeccion = seccionExistente.id_seccion;
                        }
                        else
                        {
                            // Crear nueva sección
                            var nuevaSeccion = new Secciones
                            {
                                nombre_seccion = nombreSeccion
                            };
                            contexto.Secciones.Add(nuevaSeccion);
                            contexto.SaveChanges(); // importante para obtener el ID generado
                            idSeccion = nuevaSeccion.id_seccion;
                        }
                    }

                    u.id_seccion = idSeccion;

                    // 2. Insertar o actualizar usuario
                    var existente = contexto.Usuarios.FirstOrDefault(x => x.cedula_usuario == u.cedula_usuario);

                    if (existente != null)
                    {
                        existente.nombre_usuario = u.nombre_usuario;
                        existente.apellido_usuario = u.apellido_usuario;
                        existente.estado_usuario = u.estado_usuario;
                        existente.contraseña = u.contraseña;
                        existente.id_rol = u.id_rol;
                        existente.id_seccion = u.id_seccion;
                    }
                    else
                    {
                        contexto.Usuarios.Add(u);
                    }
                }

                contexto.SaveChanges();
            }
        }




        public int ObtenerIdRolDesdeNombre(string nombreRol)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var rol = contexto.Roles.FirstOrDefault(r => r.nombre_rol == nombreRol);
                return rol != null ? rol.id_rol : 0;
            }
        }


        public int ObtenerIdSeccionDesdeNombre(string nombreSeccion)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var seccion = contexto.Secciones.FirstOrDefault(s => s.nombre_seccion == nombreSeccion);
                return seccion?.id_seccion ?? 0;
            }

        }


        public string EncriptarContrasena(string contrasena)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(contrasena);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public List<Roles> ObtenerRoles()
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                return contexto.Roles.ToList();
            }
        }


        public void RegistrarMovimiento(int idUsuario, string accion, string descripcion, string modulo)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var movimiento = new Bitacora_Movimientos
                {
                    id_usuario = idUsuario,
                    accion = accion,
                    descripcion = descripcion,
                    fecha_hora = DateTime.Now,
                    modulo = modulo
                };

                contexto.Bitacora_Movimientos.Add(movimiento);
                contexto.SaveChanges();
            }
        }

        public string ObtenerContrasenaPorCedula(string cedula)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.cedula_usuario == cedula);
                return usuario?.contraseña ?? "";
            }
        }

        public bool ExisteUsuario(string cedula)
        {
            using (var context = new RegistroDocenteEntities())
            {
                return context.Usuarios.Any(u => u.cedula_usuario == cedula);
            }
        }

        public Usuarios ObtenerUsuarioPorCedula(string cedula)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                return contexto.Usuarios.FirstOrDefault(u => u.cedula_usuario == cedula);
            }
        }

        public void MarcarUsuarioComoInactivo(string cedula)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.cedula_usuario == cedula);
                if (usuario != null)
                {
                    usuario.estado_usuario = "I";
                    contexto.SaveChanges();
                }
            }
        }

        public List<Secciones> ObtenerSecciones()
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                return contexto.Secciones.ToList();
            }
        }

        public void AsignarPermisoARol(int rolId, int permisoId)
        {
            try
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    // Ejecutar el procedimiento almacenado AgregarPermisoARol
                    contexto.Database.ExecuteSqlCommand(
                        "EXEC AgregarPermisoARol @RolId, @PermisoId",
                        new SqlParameter("@RolId", rolId),
                        new SqlParameter("@PermisoId", permisoId)
                    );
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (puedes hacer logging aquí si lo deseas)
                MessageBox.Show("Ocurrió un error al asignar el permiso: " + ex.Message);
            }
        }

        public List<int> ObtenerPermisosPorRol(int rolId)
        {
            try
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    // Ejecutar el procedimiento almacenado ObtenerPermisosPorRol
                    var permisos = contexto.Database.SqlQuery<int>(
                        "EXEC ObtenerPermisosPorRol @RolId",
                        new SqlParameter("@RolId", rolId)
                    ).ToList();

                    return permisos;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (puedes hacer logging aquí si lo deseas)
                MessageBox.Show("Ocurrió un error al obtener los permisos: " + ex.Message);
                return new List<int>(); // Retorna una lista vacía en caso de error
            }
        }

        public void EliminarPermisosDeRol(int rolId)
        {
            try
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    // Ejecutar el procedimiento almacenado EliminarPermisosDeRol
                    contexto.Database.ExecuteSqlCommand(
                        "EXEC EliminarPermisosDeRol @RolId",
                        new SqlParameter("@RolId", rolId)
                    );
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                MessageBox.Show("Ocurrió un error al eliminar los permisos: " + ex.Message);
            }
        }


        public static List<int> PermisosRolActual { get; private set; } = new List<int>();

        public static void CargarPermisosRolActual(int idRol)
        {
            try
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    var permisos = contexto.Database.SqlQuery<int>(
                        "EXEC ObtenerPermisosPorRol @RolId",
                        new SqlParameter("@RolId", idRol)
                    ).ToList();

                    PermisosRolActual = permisos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al obtener los permisos: " + ex.Message);
                PermisosRolActual = new List<int>();
            }
        }

        // Función para verificar si el usuario es administrador
        public static bool VerificarSiEsAdministrador(int idUsuario)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                return contexto.Usuarios
                    .Include(u => u.Roles.Roles_Permisos)
                    .Any(u => u.id_usuario == idUsuario &&
                           u.Roles.Roles_Permisos.Any(rp => rp.id_permiso == 2));
            }
        }

        // Función para verificar si el usuario es administrador
        public static bool VerificarSiEsDocente(int idUsuario)
        {
            AlumnoController.CargarPermisosRolActual(Sesion.IdRol);
            if (AlumnoController.PermisosRolActual.Contains(1))
            {
                return true;
            }
            return false;
        }

        // Función para verificar si el usuario puede modificar usuarios
        public static bool VerificarSiModificaUsuario(int idUsuario)
        {
            AlumnoController.CargarPermisosRolActual(Sesion.IdRol);
            if (AlumnoController.PermisosRolActual.Contains(3))
            {
                return true;
            }
            return false;
        }

        // Función para verificar si el usuario puede acceder a las configuraciones
        public static bool VerificarSiAccedeConfig(int idUsuario)
        {
            AlumnoController.CargarPermisosRolActual(Sesion.IdRol);
            if (AlumnoController.PermisosRolActual.Contains(5))
            {
                return true;
            }
            return false;
        }

        // Función para verificar si el usuario puede acceder a las bitácoras
        public static bool VerificarSiAccedeBitacoras(int idUsuario)
        {
            AlumnoController.CargarPermisosRolActual(Sesion.IdRol);
            if (AlumnoController.PermisosRolActual.Contains(6))
            {
                return true;
            }
            return false;
        }

      

    }
}

