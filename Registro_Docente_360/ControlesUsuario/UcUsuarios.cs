using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Registro_Docente_360.Controladores;
using System.Text;




namespace Registro_Docente_360.Forms
{
    public partial class UcUsuarios : UserControl
    {
        public UcUsuarios()
        {
            InitializeComponent();
        }

        private void UcUsuarios_Load(object sender, System.EventArgs e)
        {
            datagridRoles.Columns.Clear();

            ConfigurarTabla();
            CargarUsuarios();

            datagridRoles.AllowUserToAddRows = false;
            datagridRoles.ReadOnly = false;
      
        }

        private void btnAgregar_Click(object sender, System.EventArgs e)
        {
            // Agregar nueva fila
            int rowIndex = datagridRoles.Rows.Add();

            // Establecer el estado "A" por defecto
            datagridRoles.Rows[rowIndex].Cells["colEstado"].Value = "A";

            // Posicionar el cursor en la celda de nombre y comenzar edición
            datagridRoles.CurrentCell = datagridRoles.Rows[rowIndex].Cells["colNombre"];
            datagridRoles.BeginEdit(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (datagridRoles.CurrentRow == null || datagridRoles.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Por favor, selecciona una fila válida para eliminar.",
                               "Advertencia",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            var fila = datagridRoles.CurrentRow;
            string cedula = fila.Cells["colCedula"].Value?.ToString();

            // Caso 1: Fila nueva sin datos (vacía)
            if (EsFilaVacia(fila))
            {
                datagridRoles.Rows.Remove(fila);
                MessageBox.Show("Fila vacía eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Caso 2: Fila con datos (guardada o no guardada)
            bool esUsuarioNuevoNoGuardado = string.IsNullOrEmpty(cedula) ||
                                           !new AlumnoController().ExisteUsuario(cedula);

            if (esUsuarioNuevoNoGuardado)
            {
                DialogResult confirmacion = MessageBox.Show(
                    "¿Deseas eliminar esta fila? Los cambios no guardados se perderán.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    datagridRoles.Rows.Remove(fila);
                }
                return;
            }

            // Caso 3: Usuario existente en la base de datos
            DialogResult confirmarInactivacion = MessageBox.Show(
                $"¿Estás seguro de que deseas inactivar al usuario con cédula {cedula}?",
                "Confirmar inactivación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmarInactivacion == DialogResult.Yes)
            {
                AlumnoController controlador = new AlumnoController();
                controlador.MarcarUsuarioComoInactivo(cedula);

                // Registrar en bitácora
                string accion = "Inactivación de usuario";
                string descripcion = $"Se marcó como inactivo al usuario con cédula: {cedula}";
                string modulo = "Usuarios";
                controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);

                MessageBox.Show("Usuario marcado como inactivo.", "Inactivado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios(); // Refrescar la tabla
            }
        }

        private bool EsFilaVacia(DataGridViewRow fila)
        {
            foreach (DataGridViewCell cell in fila.Cells)
            {
                if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private bool EsCorreoValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AlumnoController controlador = new AlumnoController();
            List<(Usuarios usuario, string nombreSeccion)> usuariosConSeccion = new List<(Usuarios, string)>();

            // Validar todas las filas primero
            foreach (DataGridViewRow fila in datagridRoles.Rows)
            {
                if (fila.IsNewRow || EsFilaVacia(fila)) continue;

                StringBuilder erroresFila = new StringBuilder();
                erroresFila.AppendLine($"Errores en fila {fila.Index + 1}:");

                bool filaValida = true;

                // Validar campos obligatorios
                if (fila.Cells["colNombre"].Value == null || string.IsNullOrWhiteSpace(fila.Cells["colNombre"].Value.ToString()))
                {
                    filaValida = false;
                    erroresFila.AppendLine("- Nombre es obligatorio");
                }

                if (fila.Cells["colApellido"].Value == null || string.IsNullOrWhiteSpace(fila.Cells["colApellido"].Value.ToString()))
                {
                    filaValida = false;
                    erroresFila.AppendLine("- Apellido es obligatorio");
                }

                if (fila.Cells["colCorreo"].Value == null || string.IsNullOrWhiteSpace(fila.Cells["colCorreo"].Value.ToString()))
                {
                    filaValida = false;
                    erroresFila.AppendLine("- Correo electrónico es obligatorio");
                }
                else if (!EsCorreoValido(fila.Cells["colCorreo"].Value.ToString()))
                {
                    filaValida = false;
                    erroresFila.AppendLine("- Formato de correo electrónico no válido");
                }

                if (fila.Cells["colContra"].Value == null || string.IsNullOrWhiteSpace(fila.Cells["colContra"].Value.ToString()))
                {
                    filaValida = false;
                    erroresFila.AppendLine("- Contraseña es obligatoria");
                }
                else if (fila.Cells["colContra"].Value.ToString().Length < 6)
                {
                    filaValida = false;
                    erroresFila.AppendLine("- La contraseña debe tener al menos 6 caracteres");
                }

                if (fila.Cells["colEstado"].Value == null)
                {
                    filaValida = false;
                    erroresFila.AppendLine("- Estado es obligatorio");
                }

                if (fila.Cells["colCedula"].Value == null || string.IsNullOrWhiteSpace(fila.Cells["colCedula"].Value.ToString()))
                {
                    filaValida = false;
                    erroresFila.AppendLine("- Cédula es obligatoria");
                }

                string cedula = fila.Cells["colCedula"].Value.ToString();

                // Verificar si la cédula ya existe en la base de datos (solo si es un usuario nuevo o se está cambiando la cédula)
                if (filaValida)
                {
                    string cedulaIngresada = cedula;

                    // Si no se está editando el registro, y si la cédula ya existe en la base de datos, mostrar el mensaje
                    var usuarioExistente = controlador.ObtenerUsuarioPorCedula(cedulaIngresada);

                    if (usuarioExistente != null)
                    {
                        // Verificar si el usuario está editando su propia cédula o si es un duplicado
                        string cedulaUsuarioActual = fila.Cells["colCedula"].Value?.ToString();
                        if (usuarioExistente.cedula_usuario != cedulaUsuarioActual)
                        {
                            MessageBox.Show($"El usuario con cédula {cedula} ya está registrado.", "Error de duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;  // Detener la ejecución y no guardar nada
                        }
                    }
                }

                if (!filaValida)
                {
                    MessageBox.Show(erroresFila.ToString(), "Error en los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Si todas las validaciones pasan, procesar los datos
            foreach (DataGridViewRow fila in datagridRoles.Rows)
            {
                if (fila.IsNewRow || EsFilaVacia(fila)) continue;

                string cedula = fila.Cells["colCedula"].Value.ToString();
                string contraIngresada = fila.Cells["colContra"].Value.ToString();

                var existente = controlador.ObtenerUsuarioPorCedula(cedula);

                string contraseñaFinal = contraIngresada;
                if (existente == null || existente.contraseña != contraIngresada)
                {
                    contraseñaFinal = controlador.EncriptarContrasena(contraIngresada);
                }

                // Datos nuevos del formulario
                var nuevo = new Usuarios
                {
                    nombre_usuario = fila.Cells["colNombre"].Value.ToString(),
                    apellido_usuario = fila.Cells["colApellido"].Value.ToString(),
                    correo = fila.Cells["colCorreo"].Value.ToString(),
                    contraseña = contraseñaFinal,
                    estado_usuario = fila.Cells["colEstado"].Value.ToString(),
                    cedula_usuario = cedula,
                    id_rol = controlador.ObtenerIdRolDesdeNombre(fila.Cells["colRol"].Value?.ToString()),
                    fecha_registro = existente?.fecha_registro ?? DateTime.Now
                };

                string rolNombre = fila.Cells["colRol"].Value?.ToString();
                string nombreSeccion = "";

                int idRol = controlador.ObtenerIdRolDesdeNombre(rolNombre);
                AlumnoController.CargarPermisosRolActual(idRol);

                if (AlumnoController.PermisosRolActual.Contains(1)) // Permiso docente
                {
                    nombreSeccion = fila.Cells["colSeccion"].Value?.ToString();
                    nuevo.id_seccion = controlador.ObtenerIdSeccionDesdeNombre(nombreSeccion);

                    using (var contexto = new RegistroDocenteEntities())
                    {
                        var seccionExistente = contexto.Usuarios
                            .Any(u => u.id_seccion == nuevo.id_seccion && u.cedula_usuario != cedula);

                        if (seccionExistente)
                        {
                            MessageBox.Show($"La sección {nombreSeccion} ya está asignada a otro usuario.",
                                          "Sección duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }
                    }
                }
                else if (AlumnoController.PermisosRolActual.Contains(2)) // Administrador
                {
                    nuevo.id_seccion = null;
                }
                else
                {
                    nuevo.id_seccion = null;
                }

                usuariosConSeccion.Add((nuevo, nombreSeccion));

                if (existente == null)
                {
                    controlador.RegistrarMovimiento(Sesion.IdUsuario, "Registro de nuevo usuario",
                                                       $"Se registró el usuario con cédula: {cedula}", "Usuarios");
                }
                else
                {
                    bool haCambiado =
                        existente.nombre_usuario != nuevo.nombre_usuario ||
                        existente.apellido_usuario != nuevo.apellido_usuario ||
                        existente.correo != nuevo.correo ||
                        existente.contraseña != nuevo.contraseña ||
                        existente.estado_usuario != nuevo.estado_usuario ||
                        existente.id_rol != nuevo.id_rol ||
                        existente.id_seccion != nuevo.id_seccion;

                    if (haCambiado)
                    {
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, "Actualización de usuario",
                                                        $"Se actualizó el usuario con cédula: {cedula}", "Usuarios");
                    }
                }
            }

            // Guardar solo si todo está correcto
            try
            {
                controlador.GuardarUsuarios(usuariosConSeccion);
                MessageBox.Show("Cambios guardados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void CargarUsuarios(string filtro = "")
        {
            datagridRoles.Rows.Clear();

            var usuarios = new AlumnoController().ObtenerUsuarios();

            // Filtrar si hay texto
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.ToLower();
                usuarios = usuarios
                    .Where(u => u.nombre_usuario.ToLower().Contains(filtro)
                             || u.apellido_usuario.ToLower().Contains(filtro))
                    .ToList();
            }

            foreach (var u in usuarios)
            {
                datagridRoles.Rows.Add(
                    u.nombre_usuario,
                    u.apellido_usuario,
                    u.correo,
                    u.contraseña,
                    u.cedula_usuario,
                    u.Secciones?.nombre_seccion ?? "",
                    u.estado_usuario,
                    u.Roles?.nombre_rol ?? "");
            }
        }

        private void ConfigurarTabla()
        {
            datagridRoles.Columns.Clear();

            datagridRoles.Columns.Add("colNombre", "Nombre");
            datagridRoles.Columns["colNombre"].DataPropertyName = "nombre_usuario";

            datagridRoles.Columns.Add("colApellido", "Apellido");
            datagridRoles.Columns["colApellido"].DataPropertyName = "apellido_usuario";

            datagridRoles.Columns.Add("colCorreo", "Correo");
            datagridRoles.Columns["colCorreo"].DataPropertyName = "correo";

            datagridRoles.Columns.Add("colContra", "Contraseña");
            datagridRoles.Columns["colContra"].DataPropertyName = "contraseña";

            datagridRoles.Columns.Add("colCedula", "Cédula");
            datagridRoles.Columns["colCedula"].DataPropertyName = "cedula_usuario";

            var secciones = new AlumnoController().ObtenerSecciones();
            var colSeccion = new DataGridViewComboBoxColumn
            {
                Name = "colSeccion",
                HeaderText = "Sección",
                DataSource = secciones,
                DisplayMember = "nombre_seccion",
                ValueMember = "nombre_seccion",
                DataPropertyName = "nombre_seccion"
            };
            datagridRoles.Columns.Add(colSeccion);

            var colEstado = new DataGridViewComboBoxColumn
            {
                Name = "colEstado",
                HeaderText = "Estado",
                DataSource = new List<string> { "A", "I" },
                DataPropertyName = "estado_usuario"
            };
            datagridRoles.Columns.Add(colEstado);

            var roles = new AlumnoController().ObtenerRoles();
            var colRol = new DataGridViewComboBoxColumn
            {
                Name = "colRol",
                HeaderText = "Rol",
                DataSource = roles,
                DisplayMember = "nombre_rol",
                ValueMember = "nombre_rol",
                DataPropertyName = "nombre_rol"
            };
            datagridRoles.Columns.Add(colRol);

            datagridRoles.AllowUserToAddRows = false;
            datagridRoles.ReadOnly = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscarUsuario.Text.Trim();
            CargarUsuarios(texto);
        }



        private void datagridRoles_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Verificamos si la edición es en la columna de Sección
            if (datagridRoles.Columns[e.ColumnIndex].Name == "colSeccion")
            {
                var fila = datagridRoles.Rows[e.RowIndex];
                var rolNombre = fila.Cells["colRol"].Value?.ToString();  // Obtenemos el nombre del rol

                if (string.IsNullOrEmpty(rolNombre)) return;

                // Obtener el ID del rol a partir del nombre
                int idRol = 0;
                using (var contexto = new RegistroDocenteEntities())
                {
                    var rol = contexto.Roles.FirstOrDefault(r => r.nombre_rol == rolNombre);
                    if (rol != null)
                    {
                        idRol = rol.id_rol;
                    }
                }

                // Si no encontramos el rol, cancelamos la edición
                if (idRol == 0)
                {
                    MessageBox.Show($"Rol '{rolNombre}' no encontrado.", "Error de rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                // Cargar los permisos del rol actual usando el ID del rol
                AlumnoController.CargarPermisosRolActual(idRol);

                // Verificar si el usuario es administrador (permiso 2)
                if (AlumnoController.PermisosRolActual.Contains(2))  // Si es administrador
                {
                    // Administradores no pueden asignar sección a otros administradores
                    MessageBox.Show("Los administradores no pueden asignar sección a otros administradores.",
                                    "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;  // Cancelamos la edición si el rol es administrador
                }
                else if (!AlumnoController.PermisosRolActual.Contains(1))  // Si no tiene el permiso docente (id_permiso != 1)
                {
                    // No permitir asignar una sección a alguien sin el permiso docente
                    MessageBox.Show($"Solo los usuarios con el permiso docente (ID: 1) pueden ser asignados a una sección.",
                                    "Permiso requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;  // Cancelamos la edición si el rol no tiene permiso docente
                }
            }
        }

        private void datagridRoles_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && datagridRoles.Columns[e.ColumnIndex].Name == "colRol")
            {
                var fila = datagridRoles.Rows[e.RowIndex];
                var rolNombre = fila.Cells["colRol"].Value?.ToString();
                int idRol = new AlumnoController().ObtenerIdRolDesdeNombre(rolNombre);

                if (idRol > 0)
                {
                    AlumnoController.CargarPermisosRolActual(idRol);

                    if (AlumnoController.PermisosRolActual.Contains(2)) // Si es admin
                    {
                        fila.Cells["colSeccion"].Value = null; // Limpiar sin ToolTip
                    }
                }
            }
        }




    }
}