namespace Registro_Docente_360_2025
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {

        }

        private void lblIniciarSesion_Click(object sender, EventArgs e)
        {

        }

        private void cbMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMostrar.Checked == true)
            {
                txtClave.UseSystemPasswordChar = false;
            }
            else
            {
                txtClave.UseSystemPasswordChar = true;
            }
        }

        
    }
}
