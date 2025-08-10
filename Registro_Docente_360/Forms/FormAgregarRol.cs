using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Modelos.EntityFramework;

namespace Registro_Docente_360.Forms
{
    public partial class FormAgregarRol : Form
    {
        public string NombreRol { get; private set; }
        public string DescripcionRol { get; private set; }

        public FormAgregarRol()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnListo_Click(object sender, EventArgs e)
        {
            string nuevoNombreRol = txtNombreRol.Text.Trim();
            string nuevaDescripcion = txtDescripcion.Text.Trim();

            // Validación básica de campos vacíos
            if (string.IsNullOrWhiteSpace(nuevoNombreRol))
            {
                MessageBox.Show("El nombre del rol es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreRol.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nuevaDescripcion))
            {
                MessageBox.Show("La descripción del rol es obligatoria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            using (var contexto = new RegistroDocenteEntities())
            {
                // Validar si ya existe un rol con el MISMO NOMBRE (sin importar la descripción)
                bool nombreExiste = contexto.Roles.Any(r => r.nombre_rol.ToLower() == nuevoNombreRol.ToLower());

                // Validar si ya existe un rol con la MISMA DESCRIPCIÓN (sin importar el nombre)
                bool descripcionExiste = contexto.Roles.Any(r => r.descripcion_rol.ToLower() == nuevaDescripcion.ToLower());

                if (nombreExiste)
                {
                    MessageBox.Show("Ya existe un rol con ese nombre. Por favor ingrese un nombre diferente.",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreRol.SelectAll();
                    txtNombreRol.Focus();
                    return;
                }

                if (descripcionExiste)
                {
                    MessageBox.Show("Ya existe un rol con esa descripción. Por favor ingrese una descripción diferente.",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescripcion.SelectAll();
                    txtDescripcion.Focus();
                    return;
                }

                // Crear el nuevo rol
                var nuevoRol = new Roles
                {
                    nombre_rol = nuevoNombreRol,
                    descripcion_rol = nuevaDescripcion,
                    estado_rol = "A" // Activo
                };

                contexto.Roles.Add(nuevoRol);
                contexto.SaveChanges();

                MessageBox.Show("Rol agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void FormAgregarRol_Load(object sender, EventArgs e)
        {
            // Enfocar el campo de nombre al cargar el formulario
            txtNombreRol.Focus();
        }
    }
}