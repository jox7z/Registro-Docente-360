using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Registro_Docente_360.Forms
{
    public partial class FormAgregarAlumno : Form
    {
        public Estudiantes Alumno { get; private set; }
        private bool esNuevo;
        private int _idDocenteSeleccionado;
        public FormAgregarAlumno(Estudiantes alumno = null, int? idDocente = null)
        {
            InitializeComponent();

            // Configurar eventos de validación
            txtCedula.KeyPress += TxtCedula_KeyPress;
            txtTelefono.KeyPress += TxtTelefono_KeyPress;

            // Si se pasa un idDocente, lo usamos (para administrador)
            _idDocenteSeleccionado = idDocente ?? Sesion.IdUsuario;

            if (alumno == null)
            {
                Alumno = new Estudiantes();
                esNuevo = true;
                Text = "Agregar Nuevo Alumno";
                
            }
            else
            {
                Alumno = alumno;
                esNuevo = false;
                Text = "Editar Alumno";
                lblAgregarAlumno.Text = "Editar Alumno";
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            txtCedula.Text = Alumno.cedula_estudiante;
            txtNombre.Text = Alumno.nombre_estudiante;
            txtApellido1.Text = Alumno.primer_apellido;
            txtApellido2.Text = Alumno.segundo_apellido;
            txtTelefono.Text = Alumno.telefono_encargado;
        }

        private bool ValidarDatos()
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtCedula.Text))
            {
                MostrarError("La cédula es obligatoria", txtCedula);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarError("El nombre es obligatorio", txtNombre);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido1.Text))
            {
                MostrarError("El primer apellido es obligatorio", txtApellido1);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido2.Text))
            {
                MostrarError("El segundo apellido es obligatorio", txtApellido2);
                return false;
            }

            // Validar formato de cédula
            AlumnoController controlador = new AlumnoController();
            if (!controlador.ValidarCedula(txtCedula.Text, out string mensajeError))
            {
                MostrarError(mensajeError, txtCedula);
                return false;
            }

            // Validar teléfono (8 dígitos)
            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) &&
                (txtTelefono.Text.Length != 8 || !txtTelefono.Text.All(char.IsDigit)))
            {
                MostrarError("El teléfono debe tener exactamente 8 dígitos", txtTelefono);
                return false;
            }

            // Validar duplicados solo si es nuevo o cambió la cédula
            if (esNuevo || Alumno.cedula_estudiante != txtCedula.Text.Trim())
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    if (contexto.Estudiantes.Any(e => e.cedula_estudiante == txtCedula.Text.Trim() &&
                                                     (esNuevo || e.id_estudiante != Alumno.id_estudiante)))
                    {
                        MostrarError("Esta cédula ya está registrada", txtCedula);
                        return false;
                    }
                }
            }

            return true;
        }

        private void MostrarError(string mensaje, Control control)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            control.Focus();
        }

        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, guiones y teclas de control
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Autoformatear cédula (1-2345-678)
            if (e.KeyChar == (char)Keys.Back) return;

            var txt = sender as TextBox;
            string text = txt.Text.Replace("-", "");

            if (text.Length == 0 && char.IsDigit(e.KeyChar))
            {
                // No hacer nada, permitir primer dígito
            }
            else if (text.Length == 1 && char.IsDigit(e.KeyChar))
            {
                txt.Text = $"{text}{e.KeyChar}-";
                txt.SelectionStart = txt.Text.Length;
                e.Handled = true;
            }
            else if (text.Length >= 2 && text.Length < 4 && char.IsDigit(e.KeyChar))
            {
                // Continuar escribiendo después del guión
            }
            else if (text.Length == 4 && char.IsDigit(e.KeyChar))
            {
                txt.Text = $"{text.Substring(0, 1)}-{text.Substring(1, 3)}{e.KeyChar}-";
                txt.SelectionStart = txt.Text.Length;
                e.Handled = true;
            }
            else if (text.Length >= 6 && text.Length < 8 && char.IsDigit(e.KeyChar))
            {
                // Continuar escribiendo después del segundo guión
            }
            else if (char.IsDigit(e.KeyChar) && text.Length >= 9)
            {
                e.Handled = true; // No permitir más de 9 dígitos
            }
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y teclas de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limitar a 8 dígitos
            var txt = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && txt.Text.Length >= 8)
            {
                e.Handled = true;
            }
        }

        private void btnListo_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            Alumno.cedula_estudiante = txtCedula.Text.Trim();
            Alumno.nombre_estudiante = FormatearNombre(txtNombre.Text.Trim());
            Alumno.primer_apellido = FormatearNombre(txtApellido1.Text.Trim());
            Alumno.segundo_apellido = FormatearNombre(txtApellido2.Text.Trim());
            Alumno.telefono_encargado = txtTelefono.Text.Trim();

            try
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    var docente = contexto.Usuarios.Find(_idDocenteSeleccionado);

                    if (docente?.id_seccion == null)
                    {
                        MessageBox.Show("El docente no tiene sección asignada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verificar si el estudiante ya existe
                    var existe = contexto.Estudiantes
                        .FirstOrDefault(es => es.cedula_estudiante == Alumno.cedula_estudiante);

                    if (existe == null)
                    {
                        // Es un nuevo estudiante - asignar sección
                        Alumno.id_seccion = docente.id_seccion.Value;

                        // Guardar el estudiante primero
                        contexto.Estudiantes.Add(Alumno);
                        contexto.SaveChanges();

                        // Obtener las materias del docente
                        var materiasDocente = contexto.Horarios
                            .Where(h => h.id_usuario == _idDocenteSeleccionado)
                            .Select(h => h.id_materia)
                            .Distinct()
                            .ToList();

                        // Crear clases solo si el docente tiene materias asignadas
                        if (materiasDocente.Any())
                        {
                            foreach (var idMateria in materiasDocente)
                            {
                                contexto.Clases.Add(new Clases
                                {
                                    id_usuario = _idDocenteSeleccionado,
                                    id_materia = idMateria,
                                    id_estudiante = Alumno.id_estudiante,
                                });
                            }
                            contexto.SaveChanges();
                        }
                        string descripcion = $"Nuevo alumno: {txtNombre.Text}";
                        string accion = "Nuevo alumno";
                        string modulo = "Alumnos";

                        AlumnoController controlador = new AlumnoController();
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);

                        MessageBox.Show("Alumno guardado correctamente",
                                      "Éxito",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Actualizar estudiante existente
                        existe.nombre_estudiante = Alumno.nombre_estudiante;
                        existe.primer_apellido = Alumno.primer_apellido;
                        existe.segundo_apellido = Alumno.segundo_apellido;
                        existe.telefono_encargado = Alumno.telefono_encargado;
                        contexto.SaveChanges();

                        string descripcion = $"Edicion alumno: {txtNombre.Text}";
                        string accion = "Edicion alumno";
                        string modulo = "Alumnos";

                        AlumnoController controlador = new AlumnoController();
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);

                        MessageBox.Show("Datos del alumno actualizados correctamente",
                                      "Éxito",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el alumno: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private string FormatearNombre(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // Convertir a título (primera letra mayúscula, resto minúsculas)
            var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }

        private void txtCedula_Leave(object sender, EventArgs e)
        {
            // Validar formato completo al salir del campo
            TextBox txt = sender as TextBox;
            string cedula = txt.Text.Replace("-", "");

            if (cedula.Length > 0 && cedula.Length != 9)
            {
                MessageBox.Show("La cédula debe tener 9 dígitos en formato: 1-1111-1111",
                              "Formato incorrecto",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                txt.Focus();
            }
            else if (cedula.Length == 9)
            {
                // Aplicar formato estándar si no está completo
                if (!txt.Text.Contains("-") || txt.Text.Length != 10)
                {
                    txt.Text = $"{cedula[0]}-{cedula.Substring(1, 4)}-{cedula.Substring(5)}";
                }
            }
        }
    }
}