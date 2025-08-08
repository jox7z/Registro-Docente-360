using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;

namespace Registro_Docente_360.Forms
{
    public partial class FormCambioContraseña : Form
    {
        private string codigoGenerado;

        private int idUsuario; // Declarar una variable para guardar el id

     
        public FormCambioContraseña(int idUsuario)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.WindowState = FormWindowState.Normal;
            this.idUsuario = idUsuario;

        }
        private void FormCambioContraseña_Load_1(object sender, EventArgs e)
        {
            MostrarPanel(pnConfirmarCorreo);
        }
       

        private void MostrarPanel(Panel panelMostrar)
        {
            // Ocultar todos los paneles
            pnConfirmarCorreo.Visible = false;
            pnConfirmarCodigo.Visible = false;
            pnNuevaContra.Visible = false;

            // Mostrar el panel seleccionado
            panelMostrar.Visible = true;

            // Ajustar el tamaño del formulario según el panel mostrado
            if (panelMostrar == pnConfirmarCorreo)
            {
                this.Size = new System.Drawing.Size(490,300);
            }
            else if (panelMostrar == pnConfirmarCodigo)
            {
                this.Size = new System.Drawing.Size(490, 300);
            }
            else if (panelMostrar == pnConfirmarCodigo)
            {
                this.Size = new System.Drawing.Size(490, 400);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnConfirmarCorreo_Click_1(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;

            if (IsValidEmail(correo))
            {
                using (var contexto = new RegistroDocenteEntities())
                {
                    var usuario = contexto.Usuarios.FirstOrDefault(u => u.correo == correo);

                    if (usuario != null)
                    {
                        codigoGenerado = GenerarCodigoAleatorio();
                        EnviarCorreo(correo, codigoGenerado);
                        MostrarPanel(pnConfirmarCodigo);
                        MessageBox.Show("Se ha enviado un código de verificación a su correo electrónico. Por favor, ingréselo para continuar.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encuentra ninguna cuenta asociada a este correo electrónico. Por favor, verifique e intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un correo electrónico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnConfirmarCodigo_Click_1(object sender, EventArgs e)
        {
            string codigoIngresado = txtCodigo.Text;

            if (codigoIngresado == codigoGenerado)
            {
                MessageBox.Show("Código verificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarPanel(pnNuevaContra);
            }
            else
            {
                MessageBox.Show("Código incorrecto. Intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       

        private string GenerarCodigoAleatorio()
        {
            Random random = new Random();
            int codigo = random.Next(100000, 999999);
            return codigo.ToString();
        }

        private void EnviarCorreo(string correoDestino, string codigo)
        {
            string correoEmisor = "registrodocente360@gmail.com";
            string claveCorreo = "wjxt dwcb azhl gjwm";

            var clienteSmtp = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(correoEmisor, claveCorreo),
                EnableSsl = true
            };

            var correo = new System.Net.Mail.MailMessage(correoEmisor, correoDestino)
            {
                Subject = "Código de verificación para cambio de contraseña",
                Body = $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        color: #333;
                        background-color: #f4f4f4;
                        padding: 20px;
                    }}
                    .container {{
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                    }}
                    .code {{
                        font-size: 24px;
                        font-weight: bold;
                        color: #0066cc;
                        background-color: #f0f0f0;
                        padding: 10px;
                        border-radius: 4px;
                    }}
                    .footer {{
                        margin-top: 20px;
                        font-size: 12px;
                        color: #777;
                    }}
                    h2 {{
                        color: #0056b3;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>¡Hola!</h2>
                    <p>Gracias por usar nuestro sistema de cambio de contraseña.</p>
                    <p>Para proceder con el cambio, por favor ingresa el siguiente código de verificación:</p>
                    <div class='code'>{codigo}</div>
                    <p>Este código es valido temporalmente. Si no lo solicitaste, ignora este mensaje.</p>
                    <p>Si tienes alguna pregunta, no dudes en contactar a un administrador.</p>
                    <div class='footer'>
                        <p>Este es un correo automático, por favor no respondas a este mensaje.</p>
                    </div>
                </div>
            </body>
            </html>",
                IsBodyHtml = true  // Esto le dice al correo que el cuerpo está en formato HTML
            };

            clienteSmtp.Send(correo);
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = txtNuevaContra.Text;
            string confirmarContraseña = txtConfirmarContra.Text;

            if (nuevaContraseña == confirmarContraseña)
            {
                // Verificar si el correo existe y obtener el ID
                string correo = txtCorreo.Text; // Usamos el correo del formulario
                using (var contexto = new RegistroDocenteEntities())
                {
                    var usuario = contexto.Usuarios.FirstOrDefault(u => u.correo == correo);

                    if (usuario != null)
                    {
                        // Encriptar la nueva contraseña usando el controlador
                        AlumnoController controlador = new AlumnoController();
                        string contrasenaEncriptada = controlador.EncriptarContrasena(nuevaContraseña);

                        // Actualizar la contraseña
                        ActualizarContraseña(usuario.id_usuario, contrasenaEncriptada);
                        MessageBox.Show("Contraseña actualizada correctamente.","Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar el usuario asociado a este correo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void ActualizarContraseña(int idUsuario, string contrasenaEncriptada)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                // Buscar el usuario usando el ID
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.id_usuario == idUsuario);

                if (usuario != null)
                {
                    // Actualizar la contraseña encriptada
                    usuario.contraseña = contrasenaEncriptada;
                    contexto.SaveChanges();
                }
            }
        }


        private void cbMostrarContra_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbMostrarContra.Checked)
            {
                txtConfirmarContra.UseSystemPasswordChar = false;
                txtNuevaContra.UseSystemPasswordChar = false;
            }
            else
            {
                txtNuevaContra.UseSystemPasswordChar = true;
                txtConfirmarContra.UseSystemPasswordChar = true;
            }
        }

        private void panelMiniContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual (UcVentanaAsistencia o cualquier otro formulario).
            this.Close();

            // Muestra el formulario de login.
            Login loginForm = new Login();
            loginForm.Show();
        }
    }
}
