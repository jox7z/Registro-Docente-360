using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf.codec.wmf;
using Modelos.EntityFramework;
using Registro_Docente_360.Eventos;

namespace Registro_Docente_360.Forms
{
    public partial class UcRolesyPermisos : UserControl
    {
        private bool cambiosRealizados = false;

        // Variables para almacenar los valores originales
        private string originalNombreRol;
        private string originalDescripcion;
        private string originalEstadoRol;
        private bool originalAccesoModuloDocente;
        private bool originalAccesoModuloAdministrador;
        private bool originalModificarUsuarios;
        private bool originalAccederConfiguracion;
        private bool originalAccederBitacoras;

        public UcRolesyPermisos()
        {
            InitializeComponent();

            // Configuración del DataGridView para selección de filas completas
            datagridRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();

            CargarRoles();

            // Manejamos el evento de cambio de ventana (al intentar cambiar a otro UserControl o ventana)
            this.Leave += new EventHandler(UcRolesyPermisos_Leave);
        }

        // Método que maneja el evento Leave (al cambiar de ventana)
        private void UcRolesyPermisos_Leave(object sender, EventArgs e)
        {
            if (cambiosRealizados)
            {
                // Mostrar alerta de confirmación
                DialogResult result = MessageBox.Show("Tienes cambios no guardados. ¿Estás seguro deseas terminar la edición?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    // Cancela el cambio de ventana si el usuario selecciona No
                    this.Focus(); // Mantiene el foco en el control actual para evitar el cambio de ventana
                    return;
                }
                else
                {
                    // Restaurar los valores originales si el usuario decide salir sin guardar
                    RestablecerCampos();
                }
            }
            else
            {
                // Si no hay cambios realizados, se restablecen los campos y el estado de los botones a readonly
                RestablecerCampos();
            }
        }

        // Método para restablecer los valores originales de los campos
        private void RestablecerCampos()
        {
            // Restaurar los valores de los campos a su estado original antes de la modificación
            txtNombreRol.Text = originalNombreRol;
            txtDescripcion.Text = originalDescripcion;
            rbActivo.Checked = originalEstadoRol == "A";
            rbInactivo.Checked = originalEstadoRol == "I";

            // Restaurar los permisos a su estado original
            chkAccesoModuloDocente.Checked = originalAccesoModuloDocente;
            chkAccesoModuloAdministrador.Checked = originalAccesoModuloAdministrador;
            chkModificarUsuarios.Checked = originalModificarUsuarios;
            chkAccederConfiguracion.Checked = originalAccederConfiguracion;
            chkAccederBitacoras.Checked = originalAccederBitacoras;

            // Desmarcar cambios
            cambiosRealizados = false;

            // Poner todo en readonly y deshabilitar los botones
            txtNombreRol.ReadOnly = true;
            txtDescripcion.ReadOnly = true;

            // Deshabilitar los checkboxes
            chkAccesoModuloDocente.Enabled = false;
            chkAccesoModuloAdministrador.Enabled = false;
            chkModificarUsuarios.Enabled = false;
            chkAccederConfiguracion.Enabled = false;
            chkAccederBitacoras.Enabled = false;

            // Deshabilitar los RadioButtons
            rbActivo.Enabled = false;
            rbInactivo.Enabled = false;

            // Restablecer el botón de "Modificar"
            btnModificar.Enabled = true;
        }


        // Cuando el formulario carga, guarda los valores originales antes de realizar modificaciones
        private void CargarDatosOriginales()
        {
            originalNombreRol = txtNombreRol.Text;
            originalDescripcion = txtDescripcion.Text;
            originalEstadoRol = rbActivo.Checked ? "A" : "I";
            originalAccesoModuloDocente = chkAccesoModuloDocente.Checked;
            originalAccesoModuloAdministrador = chkAccesoModuloAdministrador.Checked;
            originalModificarUsuarios = chkModificarUsuarios.Checked;
            originalAccederConfiguracion = chkAccederConfiguracion.Checked;
            originalAccederBitacoras = chkAccederBitacoras.Checked;
        }

        private void CentrarMiniContenedor()
        {
            int anchoContenedor = panelMiniContenedor.Width;
            int altoContenedor = panelMiniContenedor.Height;

            panelMiniContenedor.Left = (this.ClientSize.Width - anchoContenedor) / 2;
            panelMiniContenedor.Top = (this.ClientSize.Height - altoContenedor) / 2;
        }

        private void datagridRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Comprobar si se hizo clic en una celda de tipo CheckBox
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Seleccionar toda la fila al hacer clic en cualquier CheckBox
                datagridRoles.Rows[e.RowIndex].Selected = true;
            }
        }

        public class RolPermiso
        {
            public int IdRol { get; set; }
            public string NombreRol { get; set; }
            public string EstadoRol { get; set; }
            public string DescripcionRol { get; set; }
        }

        public class Permiso
        {
            public int IdPermiso { get; set; }
            public string NombrePermiso { get; set; }
        }

        private void CargarRoles()
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var roles = contexto.Roles
                    .Select(r => new RolPermiso
                    {
                        IdRol = r.id_rol,
                        NombreRol = r.nombre_rol,
                        EstadoRol = r.estado_rol,
                        DescripcionRol = r.descripcion_rol
                    })
                    .ToList();

                if (roles.Count == 0)
                {
                    MessageBox.Show("No se encontraron roles.");
                }

                // Desactivar la creación automática de columnas
                datagridRoles.AutoGenerateColumns = false;
                datagridRoles.DataSource = roles;

                ID.DataPropertyName = "IdRol";
                NombreRol.DataPropertyName = "NombreRol";
                EstadoRol.DataPropertyName = "EstadoRol";
                DescripcionRol.DataPropertyName = "DescripcionRol";

                // Seleccionar la primera fila y cargar el estado del rol
                if (datagridRoles.Rows.Count > 0)
                {
                    datagridRoles.Rows[0].Selected = true;
                    var rolId = (int)datagridRoles.Rows[0].Cells["ID"].Value;
                    datagridRoles_CellClick(this, new DataGridViewCellEventArgs(0, 0)); // Simulamos el click en la primera fila
                }
            }
        }

        private void CargarPermisosPorRol(int rolId)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                // Limpiar los CheckBox actuales
                chkAccesoModuloDocente.Checked = false;
                chkAccesoModuloAdministrador.Checked = false;
                chkModificarUsuarios.Checked = false;
                chkAccederConfiguracion.Checked = false;
                chkAccederBitacoras.Checked = false; // Añadido para el permiso "Acceder a Bitácoras"

                // Ejecutar el procedimiento almacenado para obtener los permisos del rol
                var permisosDelRol = contexto.Database.SqlQuery<int>(
                    "EXEC dbo.ObtenerPermisosPorRol @RolId",
                    new SqlParameter("@RolId", rolId)
                ).ToList();

                // Asignar los permisos al rol
                foreach (var permisoId in permisosDelRol)
                {
                    if (permisoId == 1) chkAccesoModuloDocente.Checked = true;
                    if (permisoId == 2) chkAccesoModuloAdministrador.Checked = true;
                    if (permisoId == 3) chkModificarUsuarios.Checked = true;
                    if (permisoId == 5) chkAccederConfiguracion.Checked = true;
                    if (permisoId == 6) chkAccederBitacoras.Checked = true;
                }
            }
        }




        private void datagridRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegurarse de que la fila seleccionada sea válida
            if (e.RowIndex >= 0 && datagridRoles.Rows[e.RowIndex].Cells["ID"].Value != null)
            {
                // Acceder al ID del rol de la fila seleccionada
                var rolId = (int)datagridRoles.Rows[e.RowIndex].Cells["ID"].Value;

                using (var contexto = new RegistroDocenteEntities())
                {
                    var rol = contexto.Roles.FirstOrDefault(r => r.id_rol == rolId);
                    if (rol != null)
                    {
                        txtNombreRol.Text = rol.nombre_rol;
                        txtDescripcion.Text = rol.descripcion_rol;

                        // Set estado de rol
                        if (rol.estado_rol == "A")
                        {
                            rbActivo.Checked = true;
                            rbInactivo.Checked = false;
                        }
                        else
                        {
                            rbActivo.Checked = false;
                            rbInactivo.Checked = true;
                        }

                        // Llamar al método para cargar los permisos asociados al rol
                        CargarPermisosPorRol(rolId);
                    }
                }

                // Deshabilitar edición por defecto
                txtNombreRol.ReadOnly = true;
                txtDescripcion.ReadOnly = true;

                // Deshabilitar los checkboxes y los RadioButtons por defecto
                chkAccesoModuloDocente.Enabled = false;
                chkAccesoModuloAdministrador.Enabled = false;
                chkModificarUsuarios.Enabled = false;
                chkAccederConfiguracion.Enabled = false;
                chkAccederBitacoras.Enabled = false;

                // Deshabilitar los RadioButtons
                rbActivo.Enabled = false;
                rbInactivo.Enabled = false;
            }
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            // Mostrar la alerta de confirmación
            var result = MessageBox.Show("¿Quieres modificar el rol?", "Confirmar Modificación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Guardamos los valores originales antes de la edición
                CargarDatosOriginales();

                // Permitir que el TextBox sea editable
                txtNombreRol.ReadOnly = false;
                txtDescripcion.ReadOnly = false;

                // Cambiar el color de los RadioButton para permitir la selección
                rbActivo.Enabled = true;
                rbInactivo.Enabled = true;

                // Hacer que los checkboxes sean editables nuevamente
                chkAccesoModuloDocente.Enabled = true;
                chkAccesoModuloAdministrador.Enabled = true;
                chkModificarUsuarios.Enabled = true;
                chkAccederConfiguracion.Enabled = true;
                chkAccederBitacoras.Enabled = true;

                cambiosRealizados = true;  // Cambiar el estado de cambios realizados a true
            }
            else
            {
                // Si el usuario no desea modificar, no hacer nada
                MessageBox.Show("No se ha realizado ninguna modificación.");
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            var rolId = (int)datagridRoles.SelectedRows[0].Cells["ID"].Value;
            var nuevoNombreRol = txtNombreRol.Text;
            var nuevaDescripcion = txtDescripcion.Text;

            string estadoRol = rbActivo.Checked ? "A" : "I"; // Asignar el estado basado en el radio button

            using (var contexto = new RegistroDocenteEntities())
            {
                // Obtener el rol actual para actualizar su nombre, descripción y estado
                var rol = contexto.Roles.FirstOrDefault(r => r.id_rol == rolId);
                if (rol != null)
                {
                    rol.nombre_rol = nuevoNombreRol;
                    rol.descripcion_rol = nuevaDescripcion;
                    rol.estado_rol = estadoRol;
                    contexto.SaveChanges(); // Guardar el cambio en la base de datos
                }

                // Guardar los permisos actuales (antes de eliminarlos)
                var permisosActivos = contexto.Database.SqlQuery<int>(
                    "EXEC dbo.ObtenerPermisosPorRol @RolId", new SqlParameter("@RolId", rolId)).ToList();

                // Eliminar los permisos actuales del rol
                contexto.Database.ExecuteSqlCommand("EXEC dbo.EliminarPermisosDeRol @RolId", new SqlParameter("@RolId", rolId));

                // Insertar los nuevos permisos seleccionados
                if (chkAccesoModuloDocente.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 1));
                if (chkAccesoModuloAdministrador.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 2));
                if (chkModificarUsuarios.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 3));

                if (chkAccederConfiguracion.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 5));
                if (chkAccederBitacoras.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 6));

                // Guardar todos los cambios realizados
                contexto.SaveChanges();
            }

            // Restablecer a readonly los campos y deshabilitar la edición
            txtNombreRol.ReadOnly = true;
            txtDescripcion.ReadOnly = true;

            // Deshabilitar los checkboxes
            chkAccesoModuloDocente.Enabled = false;
            chkAccesoModuloAdministrador.Enabled = false;
            chkModificarUsuarios.Enabled = false;

            chkAccederConfiguracion.Enabled = false;
            chkAccederBitacoras.Enabled = false;

            // Actualizar el color de los RadioButton basado en el estado
            if (estadoRol == "A")
            {
                rbActivo.Checked = true;
                rbInactivo.Checked = false;
                rbActivo.ForeColor = Color.Green;
                rbInactivo.ForeColor = Color.Gray;
            }
            else
            {
                rbActivo.Checked = false;
                rbInactivo.Checked = true;
                rbActivo.ForeColor = Color.Gray;
                rbInactivo.ForeColor = Color.Red;
            }

            // Actualizar el DataGridView con la nueva información
            CargarRoles();
            MessageBox.Show("Rol modificado exitosamente.");

            cambiosRealizados = false;  // Restablecer a falso cuando se guardan los cambios
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Abre el formulario para agregar un nuevo rol
            FormAgregarRol formAgregar = new FormAgregarRol();
            formAgregar.ShowDialog();  // Abrir el formulario de manera modal

            // Después de cerrar el formulario, recargar los roles en el DataGridView
            CargarRoles();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay alguna fila seleccionada
            if (datagridRoles.SelectedRows.Count > 0)
            {
                // Obtener el ID del rol seleccionado
                var rolId = (int)datagridRoles.SelectedRows[0].Cells["ID"].Value;

                // Mostrar un mensaje de confirmación
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este rol?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (var contexto = new RegistroDocenteEntities())
                        {
                            // Buscar el rol en la base de datos
                            var rol = contexto.Roles.FirstOrDefault(r => r.id_rol == rolId);
                            if (rol != null)
                            {
                                // Eliminar la relación entre el rol y los permisos
                                var relacionesPermisos = contexto.Roles_Permisos.Where(rp => rp.id_rol == rolId).ToList();
                                contexto.Roles_Permisos.RemoveRange(relacionesPermisos);  // Elimina las relaciones

                                // Eliminar el rol
                                contexto.Roles.Remove(rol);

                                // Guardar los cambios en la base de datos
                                contexto.SaveChanges();

                                // Mostrar un mensaje de éxito
                                MessageBox.Show("Rol eliminado correctamente.", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Actualizar el DataGridView
                                CargarRoles(); // Recargar los roles en el DataGridView
                            }
                            else
                            {
                                MessageBox.Show("El rol seleccionado no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el rol: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Si no se ha seleccionado ningún rol
                MessageBox.Show("Por favor, selecciona un rol para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
