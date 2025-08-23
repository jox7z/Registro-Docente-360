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
            ConfigurarPermisos();

        }
        private void ConfigurarPermisos()
        {
            // Verificar SOLO el permiso 3 (Modificar Usuarios)
            AlumnoController.CargarPermisosRolActual(Sesion.IdRol);
            bool tienePermisoModificar = AlumnoController.PermisosRolActual.Contains(3);

            // Si NO tiene permiso 3, desactivar TODOS los botones
            btnAgregar.Enabled = tienePermisoModificar;
            btnModificar.Enabled = tienePermisoModificar;
            btnEliminar.Enabled = tienePermisoModificar;

        }


        private void btnAgregar_Click(object sender, System.EventArgs e)
        {
            using (var frm = new FormAgregarUsuario())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    using (var contexto = new RegistroDocenteEntities())
                    {
                        contexto.Usuarios.Add(frm._usuario);
                        contexto.SaveChanges();
                        CargarUsuarios(); // Recargar datos
                        MessageBox.Show("Usuario creado correctamente");
                    }
                }
            }
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

                MessageBox.Show("Usuario marcado como inactivo.", "Inactivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios(); // Refrescar la tabla
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Verificar permiso 3 nuevamente por seguridad
            if (!AlumnoController.PermisosRolActual.Contains(3))
            {
                MessageBox.Show("No tiene permisos para gestionar usuarios", "Permiso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (datagridRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para modificar");
                return;
            }

            try
            {
                var fila = datagridRoles.SelectedRows[0];

                // Obtener ID del usuario
                int idUsuario = Convert.ToInt32(fila.Cells["colID"].Value);

                using (var contexto = new RegistroDocenteEntities())
                {
                    var usuario = contexto.Usuarios.Find(idUsuario);

                    if (usuario == null)
                    {
                        MessageBox.Show("Usuario no encontrado");
                        return;
                    }

                    // Obtener el nombre del rol desde el DataGridView
                    string nombreRol = fila.Cells["colRol"].Value?.ToString();

                    // Convertir nombre de rol a ID usando tu método existente
                    if (!string.IsNullOrEmpty(nombreRol))
                    {
                        usuario.id_rol = new AlumnoController().ObtenerIdRolDesdeNombre(nombreRol);
                    }

                    using (var frm = new FormAgregarUsuario(usuario))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            contexto.SaveChanges();
                            CargarUsuarios();
                            MessageBox.Show("Usuario actualizado correctamente");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar usuario: {ex.Message}");
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
                    u.id_usuario,
                    u.nombre_usuario,
                    u.apellido_usuario,
                    u.correo,
                    "*******",
                    u.cedula_usuario,
                    u.Secciones?.nombre_seccion ?? "",
                    u.estado_usuario,
                    u.Roles?.nombre_rol ?? "");
            }
        }

        private void ConfigurarTabla()
        {
            datagridRoles.Columns.Clear();

            // Hacer que todo el DataGridView sea de solo lectura
            datagridRoles.ReadOnly = true;

            // Opcional: Deshabilitar la edición de celdas
            datagridRoles.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Deshabilitar la adición de nuevas filas
            datagridRoles.AllowUserToAddRows = false;

            // Deshabilitar la eliminación de filas
            datagridRoles.AllowUserToDeleteRows = false;

            // Agregar columnas de texto normal (ya no con ComboBox)
            datagridRoles.Columns.Add("colID", "ID");
            datagridRoles.Columns["colID"].DataPropertyName = "id_usuario";

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

           
            datagridRoles.Columns.Add("colSeccion", "Sección");
            datagridRoles.Columns["colSeccion"].DataPropertyName = "nombre_seccion";  // Mostrar el nombre de la sección

            datagridRoles.Columns.Add("colEstado", "Estado");
            datagridRoles.Columns["colEstado"].DataPropertyName = "estado_usuario";  // Mostrar el estado del usuario (A/Inactivo)

            datagridRoles.Columns.Add("colRol", "Rol");
            datagridRoles.Columns["colRol"].DataPropertyName = "nombre_rol";  // Mostrar el nombre del rol

            // Configurar selección de filas
            datagridRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridRoles.MultiSelect = false;

            // Deshabilitar la adición de nuevas filas
            datagridRoles.AllowUserToAddRows = false;
            datagridRoles.ReadOnly = true;  
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscarUsuario.Text.Trim();
            CargarUsuarios(texto);
        }

        private void datagridRoles_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay una fila válida seleccionada
            bool filaValidaSeleccionada = datagridRoles.CurrentRow != null &&
                                         !datagridRoles.CurrentRow.IsNewRow &&
                                         datagridRoles.CurrentRow.Cells["colID"].Value != null;

            btnModificar.Enabled = filaValidaSeleccionada;
        }

        private void datagridRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Alternativa: Habilitar solo si se hace clic en una celda válida (no en encabezados)
            btnModificar.Enabled = e.RowIndex >= 0;
        }

    }
}