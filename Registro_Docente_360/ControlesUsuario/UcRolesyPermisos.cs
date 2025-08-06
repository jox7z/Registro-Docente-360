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

namespace Registro_Docente_360.Forms
{
    public partial class UcRolesyPermisos : UserControl
    {
        private bool cambiosRealizados = false;


        public UcRolesyPermisos()
        {
            InitializeComponent();

            // Configuración del DataGridView para selección de filas completas
            datagridRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();

            CargarRoles();
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
                        EstadoRol = r.estado_rol
                    })
                    .ToList();

                if (roles.Count == 0)
                {
                    MessageBox.Show("No se encontraron roles.");
                }

                // Desactivar la creación automática de columnas
                datagridRoles.AutoGenerateColumns = false;
                datagridRoles.DataSource = roles;

                // Asegúrate de que las propiedades coincidan
                ID.DataPropertyName = "IdRol";
                NombreRol.DataPropertyName = "NombreRol";
                EstadoRol.DataPropertyName = "EstadoRol";
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
                chkAccederReportes.Checked = false;
                chkAccederConfiguracion.Checked = false;

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
                    if (permisoId == 4) chkAccederReportes.Checked = true;
                    if (permisoId == 5) chkAccederConfiguracion.Checked = true;
                }
            }
        }




        // En el evento de cargar el rol seleccionado
        private void datagridRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener el ID del rol seleccionado
                var rolId = (int)datagridRoles.Rows[e.RowIndex].Cells["ID"].Value;

                // Llamar a CargarPermisosPorRol para cargar los permisos
                CargarPermisosPorRol(rolId);

                // Ahora marcar el estado del rol (activo o inactivo)
                using (var contexto = new RegistroDocenteEntities())
                {
                    // Obtener el estado del rol
                    var rol = contexto.Roles.FirstOrDefault(r => r.id_rol == rolId);
                    if (rol != null)
                    {
                        // Verificar el estado del rol y actualizar los RadioButton
                        if (rol.estado_rol == "A")
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

                        // Asignar el nombre del rol al TextBox
                        txtNombreRol.Text = rol.nombre_rol;
                    }
                }

                // Hacer que los checkboxes sean solo lectura
                chkAccesoModuloDocente.Enabled = false;
                chkAccesoModuloAdministrador.Enabled = false;
                chkModificarUsuarios.Enabled = false;
                chkAccederReportes.Enabled = false;
                chkAccederConfiguracion.Enabled = false;

                // Hacer el TextBox de solo lectura
                txtNombreRol.ReadOnly = true;
            }
        }





        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            // Mostrar la alerta de confirmación
            var result = MessageBox.Show("¿Quieres modificar el rol?", "Confirmar Modificación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Permitir que el TextBox sea editable
                txtNombreRol.ReadOnly = false;

                // Cambiar el color de los RadioButton para permitir la selección
                rbActivo.Enabled = true;
                rbInactivo.Enabled = true;

                // Hacer que los checkboxes sean editables nuevamente
                chkAccesoModuloDocente.Enabled = true;
                chkAccesoModuloAdministrador.Enabled = true;
                chkModificarUsuarios.Enabled = true;
                chkAccederReportes.Enabled = true;
                chkAccederConfiguracion.Enabled = true;

                // Cambiar el estado de cambios realizados a true
                cambiosRealizados = true;
            }
            else
            {
                // Si el usuario no desea modificar, no hacer nada
                MessageBox.Show("No se ha realizado ninguna modificación.");
            }
        }





        private void pnInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelAcciones_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            var rolId = (int)datagridRoles.SelectedRows[0].Cells["ID"].Value;
            var nuevoNombreRol = txtNombreRol.Text;

            using (var contexto = new RegistroDocenteEntities())
            {
                // Actualizar el nombre del rol
                var rol = contexto.Roles.FirstOrDefault(r => r.id_rol == rolId);
                if (rol != null)
                {
                    rol.nombre_rol = nuevoNombreRol;
                    contexto.SaveChanges(); // Guardar el cambio en la base de datos
                }

                // Actualizar los permisos seleccionados
                // Primero eliminamos los permisos actuales del rol
                contexto.Database.ExecuteSqlCommand("EXEC dbo.EliminarPermisosDeRol @RolId", new SqlParameter("@RolId", rolId));


                // Insertamos los nuevos permisos
                if (chkAccesoModuloDocente.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 1));
                if (chkAccesoModuloAdministrador.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 2));
                if (chkModificarUsuarios.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 3));
                if (chkAccederReportes.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 4));
                if (chkAccederConfiguracion.Checked)
                    contexto.Database.ExecuteSqlCommand("EXEC dbo.AgregarPermisoARol @RolId, @PermisoId", new SqlParameter("@RolId", rolId), new SqlParameter("@PermisoId", 5));

                // Guardar todos los cambios realizados
                contexto.SaveChanges();
            }

            // Restablecer a readonly los campos y deshabilitar la edición
            txtNombreRol.ReadOnly = true;

            // Deshabilitar los checkboxes
            chkAccesoModuloDocente.Enabled = false;
            chkAccesoModuloAdministrador.Enabled = false;
            chkModificarUsuarios.Enabled = false;
            chkAccederReportes.Enabled = false;
            chkAccederConfiguracion.Enabled = false;

            // Actualizar el DataGridView con la nueva información
            CargarRoles();
            MessageBox.Show("Rol modificado exitosamente.");
        }
    }
}
