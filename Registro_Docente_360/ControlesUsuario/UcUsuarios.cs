using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


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
            datagridRoles.Rows.Add();
            int nuevaFila = datagridRoles.Rows.Count - 1;
            datagridRoles.CurrentCell = datagridRoles.Rows[nuevaFila].Cells["colNombre"];
            datagridRoles.BeginEdit(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (datagridRoles.CurrentRow != null)
            {
                var fila = datagridRoles.CurrentRow;
                string cedula = fila.Cells["colCedula"].Value?.ToString();

                if (!string.IsNullOrEmpty(cedula))
                {
                    DialogResult confirmacion = MessageBox.Show(
                        $"¿Estás seguro de que deseas inactivar al usuario con cédula {cedula}?",
                        "Confirmar inactivación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirmacion == DialogResult.Yes)
                    {
                        AlumnoController controlador = new AlumnoController();
                        controlador.MarcarUsuarioComoInactivo(cedula);

                        // Registrar en la bitácora de movimientos
                        string accion = "Inactivación de usuario";
                        string descripcion = $"Se marcó como inactivo al usuario con cédula: {cedula}";
                        string modulo = "Usuarios";
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);

                        MessageBox.Show("Usuario marcado como inactivo.", "Inactivado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuarios(); // refresca la tabla
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para inactivar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AlumnoController controlador = new AlumnoController();
            List<(Usuarios usuario, string nombreSeccion)> usuariosConSeccion = new List<(Usuarios, string)>();


            foreach (DataGridViewRow fila in datagridRoles.Rows)
            {
                if (fila.IsNewRow) continue;

                if (fila.Cells["colNombre"].Value != null &&
                    fila.Cells["colCorreo"].Value != null &&
                    fila.Cells["colContra"].Value != null &&
                    fila.Cells["colEstado"].Value != null &&
                    fila.Cells["colCedula"].Value != null)
                {
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
                        apellido_usuario = fila.Cells["colApellido"].Value?.ToString(),
                        correo = fila.Cells["colCorreo"].Value.ToString(),
                        contraseña = contraseñaFinal,
                        estado_usuario = fila.Cells["colEstado"].Value.ToString(),
                        cedula_usuario = cedula,
                        id_rol = controlador.ObtenerIdRolDesdeNombre(fila.Cells["colRol"].Value?.ToString()),
                        fecha_registro = existente?.fecha_registro ?? DateTime.Now
                    };

                    string rolNombre = fila.Cells["colRol"].Value?.ToString();
                    string nombreSeccion = "";

                    if (rolNombre == "Docente")
                    {
                        nombreSeccion = fila.Cells["colSeccion"].Value?.ToString();
                    }
                    else
                    {
                        nombreSeccion = ""; // Asegúrate de limpiar secciones para no-docentes
                    }
                    usuariosConSeccion.Add((nuevo, nombreSeccion));


                    
                    // Verificar si el usuario ya existe
                    if (existente == null)
                    {
                        // NUEVO USUARIO
                        usuariosConSeccion.Add((nuevo, nombreSeccion));
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, "Registro de nuevo usuario", $"Se registró el usuario con cédula: {cedula}", "Usuarios");
                    }
                    else
                    {
                        // MODIFICADO? Comparar campos
                        bool haCambiado =
                            existente.nombre_usuario != nuevo.nombre_usuario ||
                            existente.apellido_usuario != nuevo.apellido_usuario ||
                            existente.correo != nuevo.correo ||
                            existente.contraseña != nuevo.contraseña ||
                            existente.estado_usuario != nuevo.estado_usuario ||
                            existente.id_rol != nuevo.id_rol||
                            existente.id_seccion != controlador.ObtenerIdSeccionDesdeNombre(nombreSeccion);

                        if (haCambiado)
                        {
                            usuariosConSeccion.Add((nuevo, nombreSeccion));
                            controlador.RegistrarMovimiento(Sesion.IdUsuario, "Actualización de usuario", $"Se actualizó el usuario con cédula: {cedula}", "Usuarios");
                        }
                    }
                }
            }

            // Solo guarda los usuarios modificados o nuevos
            controlador.GuardarUsuarios(usuariosConSeccion);

            MessageBox.Show("Cambios guardados correctamente");
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

            var secciones = new AlumnoController().ObtenerSecciones(); // crea este método si aún no existe

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
            // Verificamos si es la columna de Sección
            if (datagridRoles.Columns[e.ColumnIndex].Name == "colSeccion")
            {
                var fila = datagridRoles.Rows[e.RowIndex];
                var rol = fila.Cells["colRol"].Value?.ToString();

                if (rol != "Docente")
                {
                    MessageBox.Show("Solo los usuarios con rol 'Docente' pueden tener una sección asignada.", "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Cancela la edición
                }
            }
        }
    }
}