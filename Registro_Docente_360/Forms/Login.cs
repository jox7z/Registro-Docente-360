using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Registro_Docente_360
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }


        private void Login_Load(object sender, EventArgs e)
        {

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email; // Validación de correo electrónico
            }
            catch
            {
                return false; // Si la validación falla, el correo no es válido
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)

        {

            string email = textUsuario.Texto;
            AlumnoController controlador = new AlumnoController();
            string clave = controlador.EncriptarContrasena(textClave.Texto);
            //string clave = textClave.Texto;

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Por favor ingrese un correo electrónico válido.");
                return;  // Si no es válido, detener el proceso
            }

            using (var contexto = new RegistroDocenteEntities())
            {
                var user = contexto.Usuarios.FirstOrDefault(u => u.correo == email);

                if (user != null)
                {
                    if(user.estado_usuario == "I")
                    {
                        RegistrarAcceso(user.id_usuario, "FALLIDO");
                        MessageBox.Show("El usuario se encuentra inactivo. Contacte al administrador.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //validar contraseña
                    if (user.contraseña == clave)
                    {

                        //iniciar sesion
                        Sesion.IdUsuario = user.id_usuario;
                        Sesion.IdRol = user.id_rol.Value;
                        Sesion.Nombre = user.nombre_usuario;
                        Sesion.Correo = user.correo;
                        Sesion.Rol = user.Roles.nombre_rol;
                        Sesion.FechaRegistro = user.fecha_registro ?? DateTime.Now;

                        RegistrarAcceso(user.id_usuario, "LOGIN");

                        this.Hide();
                        
                        MenuPrincipal menu = new MenuPrincipal();
                        menu.Show();
                    }
                    else
                    {
                        RegistrarAcceso(user.id_usuario, "FALLIDO");
                        MessageBox.Show("Credenciales incorrectas. Por favor, intente nuevamente.");
                    }

                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas. Por favor, intente nuevamente.");
                }
            }
        }

        private void RegistrarAcceso(int idUsuario, string tipoAcceso)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var acceso = new Bitacora_Accesos
                {
                    id_usuario = idUsuario,
                    tipo_acceso = tipoAcceso,
                    fecha_acceso = DateTime.Now
                };

                contexto.Bitacora_Accesos.Add(acceso);
                contexto.SaveChanges();
            }
        }
        private void lblOlvidoContra_Click(object sender, EventArgs e)
        {
            // Pasar el ID del usuario al formulario de cambio de contraseña
            FormCambioContraseña formCambioContraseña = new FormCambioContraseña(Sesion.IdUsuario);


            // Ocultar el formulario de login
            this.Hide();

            // Mostrar el formulario de cambio de contraseña
            formCambioContraseña.ShowDialog();  // ShowDialog para que se espere hasta que el formulario se cierre

            // Volver a mostrar el formulario de login (opcional, dependiendo del flujo)
            this.Show();
        }


        private void cbMostrarContra_CheckedChanged(object sender, EventArgs e)
        {
            textClave.MostrarContraseña(cbMostrarContra.Checked);
        }

        
    }
}
