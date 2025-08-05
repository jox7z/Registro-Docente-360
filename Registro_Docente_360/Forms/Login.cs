using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
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

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string usuario = textUsuario.Texto;
            AlumnoController controlador = new AlumnoController();
            string clave = controlador.EncriptarContrasena(textClave.Texto);
            //string clave = textClave.Texto;

            using (var contexto = new RegistroDocenteEntities())
            {
                var user = contexto.Usuarios.FirstOrDefault(u => u.nombre_usuario == usuario);

                if (user != null)
                {
                    if(user.estado_usuario == "I")
                    {
                        RegistrarAcceso(user.id_usuario, "FALLIDO");
                        MessageBox.Show("El usuario se encuentra inactivo. Contacte al administrador.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (user.contraseña == clave)
                    {
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
                        MessageBox.Show("Contraseña incorrecta");
                    }

                }
                else
                {
                    MessageBox.Show("Usuario incorrecto");
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


        private void cbMostrarContra_CheckedChanged(object sender, EventArgs e)
        {
            textClave.MostrarContraseña(cbMostrarContra.Checked);
        }

        private void lblOlvidoContra_Click(object sender, EventArgs e)
        {

        }
    }
}
