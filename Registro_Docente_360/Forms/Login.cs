using Modelos.EntityFramework;
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
            string clave = textClave.Texto;

            using (var contexto = new RegistroDocenteEntities())
            {
                var user = contexto.Usuarios.FirstOrDefault(u => u.nombre_usuario == usuario);

                if (user != null)
                {
                    if (user.contraseña == clave)
                    {
                        Sesion.IdUsuario = user.id_usuario;
                        Sesion.IdRol = user.id_rol;
                        Sesion.NombreUsuario = user.nombre_usuario;


                        this.Hide();
                        MenuPrincipal menu = new MenuPrincipal();
                        menu.Show();
                    }
                    else
                    {
                        MessageBox.Show("Contra incorrecta");
                    }

                }
                else
                {
                    MessageBox.Show("Usuario incorrecto");
                }
            }
        }
        private void cbMostrarContra_CheckedChanged(object sender, EventArgs e)
        {
            textClave.MostrarContraseña(cbMostrarContra.Checked);
        }
    }
}
