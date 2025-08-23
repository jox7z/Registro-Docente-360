using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Registro_Docente_360.Forms
{
    public partial class FormAgregarUsuario : Form
    {
        public Usuarios _usuario { get; private set; }
        public readonly RegistroDocenteEntities _contexto = new RegistroDocenteEntities();

        public FormAgregarUsuario(Usuarios usuario = null)
        {
            InitializeComponent();
            // Si usuario es null, es nuevo
            _usuario = usuario ?? new Usuarios
            {
                fecha_registro = DateTime.Now

            };


            if (usuario != null)
            {
                Text = "Editar Usuario";
                lblAgregarUsuario.Text = "Editar Usuario";
            }
            else
            {
                Text = "Agregar Usuario";
            }

            CargarComboboxes();
            MostrarDatosUsuario();

            cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;
        }


        private void CargarComboboxes()
        {
            try
            {
                // Cargar roles
                cmbRol.DataSource = new AlumnoController().ObtenerRoles();
                cmbRol.DisplayMember = "nombre_rol";
                cmbRol.ValueMember = "id_rol";

                // Cargar secciones
                cmbSeccion.DataSource = _contexto.Secciones.ToList();
                cmbSeccion.DisplayMember = "nombre_seccion";
                cmbSeccion.ValueMember = "id_seccion";

                // Limpiar items existentes
                cmbEstado.Items.Clear();

                // Agregar los estados disponibles
                cmbEstado.Items.Add("A"); // Activo
                cmbEstado.Items.Add("I"); // Inactivo

                // Opcional: Establecer valor por defecto
                cmbEstado.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        private void MostrarDatosUsuario()
        {
            try
            {
                if (_usuario == null) return;

                txtCedula.Text = _usuario.cedula_usuario;
                txtNombre.Text = _usuario.nombre_usuario;
                txtApellido.Text = _usuario.apellido_usuario;
                txtCorreo.Text = _usuario.correo;
                txtContra.Text = string.Empty; // No mostrar la contraseña

                // Cargar estado
                if (!string.IsNullOrEmpty(_usuario.estado_usuario))
                {
                    cmbEstado.SelectedItem = _usuario.estado_usuario;
                }
                else
                {
                    cmbEstado.SelectedIndex = 0; // Valor por defecto
                }

                // Seleccionar el rol correcto
                if (_usuario.id_rol.HasValue)
                {
                    cmbRol.SelectedValue = _usuario.id_rol.Value;
                }

                // Seleccionar sección si es docente
                if (_usuario.id_seccion.HasValue)
                {
                    cmbSeccion.SelectedValue = _usuario.id_seccion.Value;
                }

                // Verificar visibilidad inicial de sección
                VerificarVisibilidadSeccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar datos: {ex.Message}");
            }
        }
        private void VerificarVisibilidadSeccion()
        {
            try
            {
                if (cmbRol.SelectedValue == null) return;

                int idRol = (int)cmbRol.SelectedValue;
                bool esDocentePuro = EsDocentePuro(idRol, _contexto);

                label5.Visible = esDocentePuro;
                cmbSeccion.Visible = esDocentePuro;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar sección: {ex.Message}");
            }
        }

        private void btnListo_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            try
            {
                bool esNuevoUsuario = _usuario.id_usuario == 0;
                // Actualizar objeto usuario
                _usuario.cedula_usuario = txtCedula.Text.Trim();
                _usuario.nombre_usuario = txtNombre.Text.Trim();
                _usuario.apellido_usuario = txtApellido.Text.Trim();
                _usuario.correo = txtCorreo.Text.Trim();
                _usuario.estado_usuario = cmbEstado.SelectedItem.ToString();
                _usuario.id_rol = cmbRol.SelectedValue != null ? (int)cmbRol.SelectedValue : (int?)null;

                // Asignar sección solo si es docente puro
                if (cmbRol.SelectedValue != null &&
            EsDocentePuro((int)cmbRol.SelectedValue, _contexto) &&
            cmbSeccion.SelectedValue != null)
                {
                    _usuario.id_seccion = (int?)cmbSeccion.SelectedValue;
                }
                else
                {
                    _usuario.id_seccion = null; // Asegurarse que no sea docente
                }

                // Actualizar contraseña solo si se ingresó una nueva
                if (!string.IsNullOrEmpty(txtContra.Text))
                {
                    _usuario.contraseña = new AlumnoController().EncriptarContrasena(txtContra.Text);
                }

                if (esNuevoUsuario == true)
                {
                    string descripcion = $"Nuevo usuario : {txtNombre.Text}";
                    string accion = "Nuevo usuario";
                    string modulo = "Usuarios";

                    AlumnoController controlador = new AlumnoController();
                    controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
                }
                else if (esNuevoUsuario == false)
                {
                    string descripcion = $"Edicion usuario : {txtNombre.Text}";
                    string accion = "Edicion usuario";
                    string modulo = "Usuarios";

                    AlumnoController controlador = new AlumnoController();
                    controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            // Validar campos básicos primero
            if (!ValidarCamposObligatorios() || !ValidarCedula() || !ValidarCorreo() || !ValidarContrasena())
                return false;

            // Verificar que se haya seleccionado un rol válido
            if (cmbRol.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un rol válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int idRolSeleccionado = (int)cmbRol.SelectedValue;
            bool esDocentePuro = EsDocentePuro(idRolSeleccionado, _contexto);

            // Validación específica para docentes puros
            if (esDocentePuro)
            {
                if (cmbSeccion.SelectedValue == null)
                {
                    MessageBox.Show("Los docentes deben tener una sección asignada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int idSeccionSeleccionada = (int)cmbSeccion.SelectedValue;

                // Usar la función existente SeccionAsignadaAOtroDocente
                if (SeccionAsignadaAOtroDocente(idSeccionSeleccionada))
                {
                    string nombreSeccion = cmbSeccion.Text;
                    MessageBox.Show($"La sección {nombreSeccion} ya está asignada a otro docente",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay un ítem seleccionado
                if (cmbRol.SelectedItem == null) return;

                // Obtener el rol seleccionado como objeto
                var rolSeleccionado = cmbRol.SelectedItem as Roles;
                if (rolSeleccionado == null) return;

                // Verificar si es docente puro
                bool mostrarSeccion = EsDocentePuro(rolSeleccionado.id_rol, _contexto);

                // Actualizar visibilidad de controles
                label5.Visible = mostrarSeccion;
                cmbSeccion.Visible = mostrarSeccion;

                // Limpiar selección si no es docente
                if (!mostrarSeccion)
                {
                    cmbSeccion.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar rol: {ex.Message}");
            }
        }

        private bool EsDocentePuro(int? idRol, RegistroDocenteEntities contexto)
        {
            if (!idRol.HasValue) return false;

            return contexto.Roles_Permisos
                .Any(rp => rp.id_rol == idRol.Value && rp.id_permiso == 1) &&
                   !contexto.Roles_Permisos
                       .Any(rp => rp.id_rol == idRol.Value && rp.id_permiso == 2);
        }

        private bool ValidarCedula()
        {
            string cedula = txtCedula.Text.Trim();

            // Validar que no esté vacía
            if (string.IsNullOrWhiteSpace(cedula))
            {
                MessageBox.Show("La cédula es obligatoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validar longitud máxima
            if (cedula.Length > 9)
            {
                MessageBox.Show("La cédula no puede tener más de 9 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validar que solo contenga números (opcional)
            if (!Regex.IsMatch(cedula, @"^\d+$"))
            {
                MessageBox.Show("La cédula solo puede contener números", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validar que no exista en la base de datos (excepto para el usuario actual)
            using (var contexto = new RegistroDocenteEntities())
            {
                bool cedulaExiste = contexto.Usuarios.Any(u =>
                    u.cedula_usuario == cedula &&
                    u.id_usuario != _usuario.id_usuario);

                if (cedulaExiste)
                {
                    MessageBox.Show("La cédula ya está registrada por otro usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }


        private bool ValidarCorreo()
        {
            string correo = txtCorreo.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("El correo electrónico es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validar formato de correo
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(correo);
            }
            catch
            {
                MessageBox.Show("Ingrese un correo electrónico válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validar que no exista en la base de datos (excepto para el usuario actual)
            using (var contexto = new RegistroDocenteEntities())
            {
                bool correoExiste = contexto.Usuarios.Any(u =>
                    u.correo == correo &&
                    u.id_usuario != _usuario.id_usuario);

                if (correoExiste)
                {
                    MessageBox.Show("El correo electrónico ya está registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private bool ValidarContrasena()
        {
            // Si es edición y no se cambió la contraseña, es válido
            if (_usuario.id_usuario > 0 && string.IsNullOrEmpty(txtContra.Text))
                return true;

            // Validar longitud mínima
            if (txtContra.Text.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidarCamposObligatorios()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbEstado.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un estado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool SeccionAsignadaAOtroDocente(int? idSeccion)
        {
            if (!idSeccion.HasValue) return false;

            using (var contexto = new RegistroDocenteEntities())
            {
                return contexto.Usuarios.Any(u =>
                    u.id_seccion == idSeccion &&
                    u.id_usuario != _usuario.id_usuario &&
                    u.id_rol.HasValue &&
                    contexto.Roles_Permisos.Any(rp =>
                        rp.id_rol == u.id_rol.Value &&
                        rp.id_permiso == 1) &&
                    !contexto.Roles_Permisos.Any(rp =>
                        rp.id_rol == u.id_rol.Value &&
                        rp.id_permiso == 2));
            }
        }
    }
}