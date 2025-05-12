namespace Registro_Docente_360_2025
{
    partial class Login_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Form));
            pictureBox2 = new PictureBox();
            lblIniciarSesion = new Label();
            txtUsuario = new TextBox();
            txtClave = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnIngresar = new Button();
            cbMostrar = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.ErrorImage = null;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.InitialImage = null;
            pictureBox2.Location = new Point(122, 6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(125, 117);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // lblIniciarSesion
            // 
            lblIniciarSesion.AutoSize = true;
            lblIniciarSesion.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIniciarSesion.ForeColor = Color.FromArgb(0, 192, 192);
            lblIniciarSesion.Location = new Point(66, 138);
            lblIniciarSesion.Name = "lblIniciarSesion";
            lblIniciarSesion.Size = new Size(233, 40);
            lblIniciarSesion.TabIndex = 1;
            lblIniciarSesion.Text = "INICIAR SESION";
            lblIniciarSesion.TextAlign = ContentAlignment.TopCenter;
            lblIniciarSesion.Click += lblIniciarSesion_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsuario.ForeColor = Color.Black;
            txtUsuario.Location = new Point(66, 248);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(221, 35);
            txtUsuario.TabIndex = 2;
            // 
            // txtClave
            // 
            txtClave.BorderStyle = BorderStyle.FixedSingle;
            txtClave.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtClave.Location = new Point(66, 335);
            txtClave.Name = "txtClave";
            txtClave.Size = new Size(221, 35);
            txtClave.TabIndex = 3;
            txtClave.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(66, 215);
            label1.Name = "label1";
            label1.Size = new Size(94, 30);
            label1.TabIndex = 4;
            label1.Text = "Usuario:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(66, 302);
            label2.Name = "label2";
            label2.Size = new Size(129, 30);
            label2.TabIndex = 5;
            label2.Text = "Contraseña:";
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.White;
            btnIngresar.Cursor = Cursors.Hand;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresar.ForeColor = Color.FromArgb(0, 192, 192);
            btnIngresar.Location = new Point(66, 461);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(221, 49);
            btnIngresar.TabIndex = 6;
            btnIngresar.Text = "INGRESAR";
            btnIngresar.UseVisualStyleBackColor = false;
            // 
            // cbMostrar
            // 
            cbMostrar.AutoSize = true;
            cbMostrar.Cursor = Cursors.Hand;
            cbMostrar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbMostrar.Location = new Point(66, 376);
            cbMostrar.Name = "cbMostrar";
            cbMostrar.Size = new Size(88, 25);
            cbMostrar.TabIndex = 7;
            cbMostrar.Text = "Mostrar";
            cbMostrar.UseVisualStyleBackColor = true;
            cbMostrar.CheckedChanged += cbMostrar_CheckedChanged;
            // 
            // Login_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(359, 545);
            Controls.Add(cbMostrar);
            Controls.Add(btnIngresar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtClave);
            Controls.Add(txtUsuario);
            Controls.Add(lblIniciarSesion);
            Controls.Add(pictureBox2);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Login_Form";
            Text = "Registro Docente 360";
            Load += Login_Form_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private Label lblIniciarSesion;
        private PictureBox pictureBox1;
        private PictureBox pbUsuario;
        private TextBox txtUsuario;
        private TextBox txtClave;
        private Label label1;
        private Label label2;
        private Button btnIngresar;
        private CheckBox cbMostrar;
    }
}
