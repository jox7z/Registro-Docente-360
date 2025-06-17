using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registro_Docente_360.Forms;

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

        }

        private void gradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lbllogin5_Click(object sender, EventArgs e)
        {

        }
        private void cbMostrarContra_CheckedChanged(object sender, EventArgs e)
        {
            txtUsuario.MostrarContraseña(cbMostrarContra.Checked);
        }
    }
} 
