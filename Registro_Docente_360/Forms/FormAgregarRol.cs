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

            this.StartPosition = FormStartPosition.Manual;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;

        }

        private void btnListo_Click(object sender, EventArgs e)
        {
            string nuevoNombreRol = txtNombreRol.Text;
            string nuevaDescripcion = txtDescripcion.Text;

            // Verificar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nuevoNombreRol) || string.IsNullOrWhiteSpace(nuevaDescripcion))
            {
                MessageBox.Show("El nombre del rol y la descripción son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var contexto = new RegistroDocenteEntities())
            {
                // Verificar si ya existe un rol con el mismo nombre y descripción
                var rolExistente = contexto.Roles
                    .FirstOrDefault(r => r.nombre_rol == nuevoNombreRol && r.descripcion_rol == nuevaDescripcion);

                if (rolExistente != null)
                {
                    MessageBox.Show("Ya existe un rol con el mismo nombre y descripción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el nuevo rol
                var nuevoRol = new Roles
                {
                    nombre_rol = nuevoNombreRol,
                    descripcion_rol = nuevaDescripcion,
                    estado_rol = "A"
                };

                contexto.Roles.Add(nuevoRol);
                contexto.SaveChanges();  // Guardar en la base de datos

                MessageBox.Show("Rol agregado exitosamente.");

                // Cerrar el formulario después de agregar el rol
                this.Close();
            }
        }



        private void FormAgregarRol_Load(object sender, EventArgs e)
        {

        }

       
    }
}
