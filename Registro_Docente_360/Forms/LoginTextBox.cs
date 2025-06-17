using System;
using System.Windows.Forms;

namespace Registro_Docente_360.Forms
{
    public partial class LoginTextBox : UserControl
    {
        public LoginTextBox()
        {
            InitializeComponent();
        }

        // Campo privado para el texto del label
        private string _label = "default value";

        // Campo privado para indicar si es campo de contraseña
        private bool _isPassword = false;

        // Propiedad pública para el texto del label (se actualiza automáticamente)
        public string label
        {
            get { return _label; }
            set
            {
                _label = value;
                lbltextlogin.Text = value; // Actualiza el label visual
            }
        }

        // Propiedad pública para definir si debe comportarse como campo de contraseña
        public bool isPassword
        {
            get { return _isPassword; }
            set
            {
                _isPassword = value;

                if (_isPassword)
                {
                    textBox1.UseSystemPasswordChar = false; // desactiva símbolo del sistema
                    textBox1.PasswordChar = '*';            // usa asterisco
                }
                else
                {
                    textBox1.UseSystemPasswordChar = false;
                    textBox1.PasswordChar = '\0';           // texto visible
                }
            }
        }

        private void LoginTextBox_Paint(object sender, PaintEventArgs e)
        {
            lbltextlogin.Text = label;
        }

        public void MostrarContraseña(bool mostrar)
        {
            if (_isPassword)
            {
                if (mostrar)
                    textBox1.PasswordChar = '\0';  // muestra texto
                else
                    textBox1.PasswordChar = '*';   // oculta con asterisco
            }
        }
    }
}
