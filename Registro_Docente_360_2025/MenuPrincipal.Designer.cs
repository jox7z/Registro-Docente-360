namespace Registro_Docente_360_2025
{
    partial class MenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            btnAsistencia = new Button();
            btnHorario = new Button();
            btnAlumnos = new Button();
            pictureBox1 = new PictureBox();
            btnReportes = new Button();
            lblNombreUsuario = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnAsistencia
            // 
            btnAsistencia.BackColor = Color.White;
            btnAsistencia.FlatStyle = FlatStyle.Flat;
            btnAsistencia.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAsistencia.ForeColor = Color.FromArgb(0, 192, 192);
            btnAsistencia.Location = new Point(65, 129);
            btnAsistencia.Name = "btnAsistencia";
            btnAsistencia.Size = new Size(374, 66);
            btnAsistencia.TabIndex = 0;
            btnAsistencia.Text = "Asistencia";
            btnAsistencia.UseVisualStyleBackColor = false;
            btnAsistencia.Click += btnAsistencia_Click;
            // 
            // btnHorario
            // 
            btnHorario.BackColor = Color.White;
            btnHorario.FlatStyle = FlatStyle.Flat;
            btnHorario.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHorario.ForeColor = Color.FromArgb(0, 192, 192);
            btnHorario.Location = new Point(65, 226);
            btnHorario.Name = "btnHorario";
            btnHorario.Size = new Size(374, 66);
            btnHorario.TabIndex = 1;
            btnHorario.Text = "Horario";
            btnHorario.UseVisualStyleBackColor = false;
            btnHorario.Click += btnHorario_Click;
            // 
            // btnAlumnos
            // 
            btnAlumnos.BackColor = Color.White;
            btnAlumnos.FlatStyle = FlatStyle.Flat;
            btnAlumnos.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAlumnos.ForeColor = Color.FromArgb(0, 192, 192);
            btnAlumnos.Location = new Point(65, 320);
            btnAlumnos.Name = "btnAlumnos";
            btnAlumnos.Size = new Size(374, 66);
            btnAlumnos.TabIndex = 2;
            btnAlumnos.Text = "Alumnos";
            btnAlumnos.UseVisualStyleBackColor = false;
            btnAlumnos.Click += btnAlumnos_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(201, 38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.White;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReportes.ForeColor = Color.FromArgb(0, 192, 192);
            btnReportes.Location = new Point(65, 413);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(374, 66);
            btnReportes.TabIndex = 4;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Location = new Point(196, 8);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(111, 15);
            lblNombreUsuario.TabIndex = 5;
            lblNombreUsuario.Text = "Nombre De Usuario";
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 526);
            Controls.Add(lblNombreUsuario);
            Controls.Add(btnReportes);
            Controls.Add(pictureBox1);
            Controls.Add(btnAlumnos);
            Controls.Add(btnHorario);
            Controls.Add(btnAsistencia);
            Name = "MenuPrincipal";
            Text = "Menu Principal";
            Load += MenuPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAsistencia;
        private Button btnHorario;
        private Button btnAlumnos;
        private PictureBox pictureBox1;
        private Button btnReportes;
        private Label lblNombreUsuario;
    }
}