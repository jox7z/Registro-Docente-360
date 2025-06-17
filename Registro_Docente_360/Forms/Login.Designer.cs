using System.Drawing;
using System.Windows.Forms;
using System;

namespace Registro_Docente_360
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.btnIniciar = new System.Windows.Forms.Button();
            this.lblOlvidoContra = new System.Windows.Forms.Label();
            this.cbMostrarContra = new System.Windows.Forms.CheckBox();
            this.lbllogin1 = new System.Windows.Forms.Label();
            this.textClave = new Registro_Docente_360.Forms.LoginTextBox();
            this.textUsuario = new Registro_Docente_360.Forms.LoginTextBox();
            this.gradientPanel1 = new Registro_Docente_360.GradientPanel();
            this.lbllogin5 = new System.Windows.Forms.Label();
            this.lbllogin3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbllogin2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbllogin4 = new System.Windows.Forms.Label();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.Teal;
            this.btnIniciar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciar.ForeColor = System.Drawing.Color.White;
            this.btnIniciar.Location = new System.Drawing.Point(412, 273);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(200, 41);
            this.btnIniciar.TabIndex = 2;
            this.btnIniciar.Text = "Iniciar sesión";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // lblOlvidoContra
            // 
            this.lblOlvidoContra.AutoSize = true;
            this.lblOlvidoContra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOlvidoContra.ForeColor = System.Drawing.Color.Teal;
            this.lblOlvidoContra.Location = new System.Drawing.Point(450, 330);
            this.lblOlvidoContra.Name = "lblOlvidoContra";
            this.lblOlvidoContra.Size = new System.Drawing.Size(131, 13);
            this.lblOlvidoContra.TabIndex = 3;
            this.lblOlvidoContra.Text = "¿Olvidaste tu contraseña?";
            // 
            // cbMostrarContra
            // 
            this.cbMostrarContra.AutoSize = true;
            this.cbMostrarContra.BackColor = System.Drawing.Color.Transparent;
            this.cbMostrarContra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMostrarContra.ForeColor = System.Drawing.Color.Teal;
            this.cbMostrarContra.Location = new System.Drawing.Point(412, 210);
            this.cbMostrarContra.Name = "cbMostrarContra";
            this.cbMostrarContra.Size = new System.Drawing.Size(118, 17);
            this.cbMostrarContra.TabIndex = 4;
            this.cbMostrarContra.Text = "Mostrar Contraseña";
            this.cbMostrarContra.UseVisualStyleBackColor = false;
            this.cbMostrarContra.CheckedChanged += new System.EventHandler(this.cbMostrarContra_CheckedChanged);
            // 
            // lbllogin1
            // 
            this.lbllogin1.AutoSize = true;
            this.lbllogin1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllogin1.Location = new System.Drawing.Point(407, 22);
            this.lbllogin1.Name = "lbllogin1";
            this.lbllogin1.Size = new System.Drawing.Size(201, 25);
            this.lbllogin1.TabIndex = 5;
            this.lbllogin1.Text = "Accede para empezar";
            // 
            // textClave
            // 
            this.textClave.BackColor = System.Drawing.Color.Teal;
            this.textClave.isPassword = true;
            this.textClave.label = "Contraseña";
            this.textClave.Location = new System.Drawing.Point(412, 153);
            this.textClave.Name = "textClave";
            this.textClave.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.textClave.Size = new System.Drawing.Size(191, 41);
            this.textClave.TabIndex = 7;
            // 
            // textUsuario
            // 
            this.textUsuario.BackColor = System.Drawing.Color.Teal;
            this.textUsuario.isPassword = false;
            this.textUsuario.label = "Usuario";
            this.textUsuario.Location = new System.Drawing.Point(412, 95);
            this.textUsuario.Name = "textUsuario";
            this.textUsuario.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.textUsuario.Size = new System.Drawing.Size(191, 41);
            this.textUsuario.TabIndex = 6;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.lbllogin5);
            this.gradientPanel1.Controls.Add(this.lbllogin3);
            this.gradientPanel1.Controls.Add(this.pictureBox2);
            this.gradientPanel1.Controls.Add(this.lbllogin2);
            this.gradientPanel1.Controls.Add(this.pictureBox1);
            this.gradientPanel1.Controls.Add(this.lbllogin4);
            this.gradientPanel1.gradientBottom = System.Drawing.Color.LightSeaGreen;
            this.gradientPanel1.gradientTop = System.Drawing.Color.Teal;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(353, 397);
            this.gradientPanel1.TabIndex = 0;
            this.gradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.gradientPanel1_Paint_1);
            // 
            // lbllogin5
            // 
            this.lbllogin5.BackColor = System.Drawing.Color.Transparent;
            this.lbllogin5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lbllogin5.ForeColor = System.Drawing.Color.White;
            this.lbllogin5.Location = new System.Drawing.Point(50, 303);
            this.lbllogin5.Name = "lbllogin5";
            this.lbllogin5.Size = new System.Drawing.Size(218, 104);
            this.lbllogin5.TabIndex = 11;
            this.lbllogin5.Text = "Plataforma pensada para hacer tu labor docente más ágil, clara y eficiente.";
            this.lbllogin5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbllogin3
            // 
            this.lbllogin3.AutoSize = true;
            this.lbllogin3.BackColor = System.Drawing.Color.Transparent;
            this.lbllogin3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lbllogin3.ForeColor = System.Drawing.Color.White;
            this.lbllogin3.Location = new System.Drawing.Point(66, 210);
            this.lbllogin3.Name = "lbllogin3";
            this.lbllogin3.Size = new System.Drawing.Size(193, 25);
            this.lbllogin3.TabIndex = 8;
            this.lbllogin3.Text = "REGISTRO DOCENTE";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(75, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(149, 127);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // lbllogin2
            // 
            this.lbllogin2.AutoSize = true;
            this.lbllogin2.BackColor = System.Drawing.Color.Transparent;
            this.lbllogin2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllogin2.ForeColor = System.Drawing.Color.White;
            this.lbllogin2.Location = new System.Drawing.Point(12, 22);
            this.lbllogin2.Name = "lbllogin2";
            this.lbllogin2.Size = new System.Drawing.Size(273, 25);
            this.lbllogin2.TabIndex = 6;
            this.lbllogin2.Text = "TE DAMOS LA BIENVENIDA A";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(200, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(162, 407);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // lbllogin4
            // 
            this.lbllogin4.AutoSize = true;
            this.lbllogin4.BackColor = System.Drawing.Color.Transparent;
            this.lbllogin4.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lbllogin4.ForeColor = System.Drawing.Color.White;
            this.lbllogin4.Location = new System.Drawing.Point(131, 235);
            this.lbllogin4.Name = "lbllogin4";
            this.lbllogin4.Size = new System.Drawing.Size(45, 25);
            this.lbllogin4.TabIndex = 7;
            this.lbllogin4.Text = "360";
            // 
            // Login
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(650, 396);
            this.Controls.Add(this.textClave);
            this.Controls.Add(this.textUsuario);
            this.Controls.Add(this.lbllogin1);
            this.Controls.Add(this.cbMostrarContra);
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.lblOlvidoContra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "Inicio de sesión";
            this.Load += new System.EventHandler(this.Login_Load);
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private GradientPanel gradientPanel1;
        private Forms.LoginTextBox txtUsuario;
        private Forms.LoginTextBox txtClave;
        private System.Windows.Forms.Button btnIniciar;
        
        private System.Windows.Forms.Label lblOlvidoContra;
        private CheckBox cbMostrarContra;
        private Label lbllogin3;
        private Label lbllogin4;
        private Label lbllogin2;
        private PictureBox pictureBox2;
        private Label lbllogin1;
        private PictureBox pictureBox1;
        private Label lbllogin5;
        private Forms.LoginTextBox textUsuario;
        private Forms.LoginTextBox textClave;
    }
}