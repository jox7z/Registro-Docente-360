namespace Registro_Docente_360
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.panel = new System.Windows.Forms.Panel();
            this.lblMenuPrincipal = new System.Windows.Forms.Label();
            this.btnHam = new System.Windows.Forms.PictureBox();
            this.windowBarControl1 = new Registro_Docente_360.ControlesUsuario.WindowBarControl();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.pnAsistencia = new System.Windows.Forms.Panel();
            this.btnAsistencia = new System.Windows.Forms.Button();
            this.pnHorario = new System.Windows.Forms.Panel();
            this.btnHorario = new System.Windows.Forms.Button();
            this.pnAlumnos = new System.Windows.Forms.Panel();
            this.btnAlumnos = new System.Windows.Forms.Button();
            this.pnReportes = new System.Windows.Forms.Panel();
            this.btnReportes = new System.Windows.Forms.Button();
            this.pnConfiguracion = new System.Windows.Forms.Panel();
            this.btnConfiguracion = new System.Windows.Forms.Button();
            this.pnSobreNosotros = new System.Windows.Forms.Panel();
            this.btnAboutUs = new System.Windows.Forms.Button();
            this.sidebarTransition = new System.Windows.Forms.Timer(this.components);
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).BeginInit();
            this.sidebar.SuspendLayout();
            this.pnAsistencia.SuspendLayout();
            this.pnHorario.SuspendLayout();
            this.pnAlumnos.SuspendLayout();
            this.pnReportes.SuspendLayout();
            this.pnConfiguracion.SuspendLayout();
            this.pnSobreNosotros.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel.Controls.Add(this.lblMenuPrincipal);
            this.panel.Controls.Add(this.btnHam);
            this.panel.Controls.Add(this.windowBarControl1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.ForeColor = System.Drawing.Color.Transparent;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(981, 32);
            this.panel.TabIndex = 0;
            // 
            // lblMenuPrincipal
            // 
            this.lblMenuPrincipal.AutoSize = true;
            this.lblMenuPrincipal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuPrincipal.ForeColor = System.Drawing.Color.Teal;
            this.lblMenuPrincipal.Location = new System.Drawing.Point(43, 6);
            this.lblMenuPrincipal.Name = "lblMenuPrincipal";
            this.lblMenuPrincipal.Size = new System.Drawing.Size(244, 17);
            this.lblMenuPrincipal.TabIndex = 1;
            this.lblMenuPrincipal.Text = "Menu Principal | Registro Docente 360";
            // 
            // btnHam
            // 
            this.btnHam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHam.Image = ((System.Drawing.Image)(resources.GetObject("btnHam.Image")));
            this.btnHam.Location = new System.Drawing.Point(2, 0);
            this.btnHam.Name = "btnHam";
            this.btnHam.Size = new System.Drawing.Size(34, 30);
            this.btnHam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnHam.TabIndex = 1;
            this.btnHam.TabStop = false;
            this.btnHam.Click += new System.EventHandler(this.btnHam_Click);
            // 
            // windowBarControl1
            // 
            this.windowBarControl1.BackColor = System.Drawing.Color.Transparent;
            this.windowBarControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowBarControl1.Location = new System.Drawing.Point(896, 0);
            this.windowBarControl1.Name = "windowBarControl1";
            this.windowBarControl1.Size = new System.Drawing.Size(85, 32);
            this.windowBarControl1.TabIndex = 2;
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.Teal;
            this.sidebar.Controls.Add(this.pnAsistencia);
            this.sidebar.Controls.Add(this.pnHorario);
            this.sidebar.Controls.Add(this.pnAlumnos);
            this.sidebar.Controls.Add(this.pnReportes);
            this.sidebar.Controls.Add(this.pnConfiguracion);
            this.sidebar.Controls.Add(this.pnSobreNosotros);
            this.sidebar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.ForeColor = System.Drawing.Color.Transparent;
            this.sidebar.Location = new System.Drawing.Point(0, 32);
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.sidebar.Size = new System.Drawing.Size(202, 523);
            this.sidebar.TabIndex = 1;
            this.sidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.sidebar_Paint);
            // 
            // pnAsistencia
            // 
            this.pnAsistencia.BackColor = System.Drawing.Color.Transparent;
            this.pnAsistencia.Controls.Add(this.btnAsistencia);
            this.pnAsistencia.Location = new System.Drawing.Point(3, 33);
            this.pnAsistencia.Name = "pnAsistencia";
            this.pnAsistencia.Size = new System.Drawing.Size(196, 46);
            this.pnAsistencia.TabIndex = 3;
            // 
            // btnAsistencia
            // 
            this.btnAsistencia.BackColor = System.Drawing.Color.Teal;
            this.btnAsistencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsistencia.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsistencia.ForeColor = System.Drawing.Color.White;
            this.btnAsistencia.Image = ((System.Drawing.Image)(resources.GetObject("btnAsistencia.Image")));
            this.btnAsistencia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsistencia.Location = new System.Drawing.Point(-20, -39);
            this.btnAsistencia.Name = "btnAsistencia";
            this.btnAsistencia.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnAsistencia.Size = new System.Drawing.Size(253, 124);
            this.btnAsistencia.TabIndex = 2;
            this.btnAsistencia.Text = "           Asistencia";
            this.btnAsistencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsistencia.UseVisualStyleBackColor = false;
            this.btnAsistencia.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnHorario
            // 
            this.pnHorario.BackColor = System.Drawing.Color.Transparent;
            this.pnHorario.Controls.Add(this.btnHorario);
            this.pnHorario.ForeColor = System.Drawing.Color.Transparent;
            this.pnHorario.Location = new System.Drawing.Point(3, 85);
            this.pnHorario.Name = "pnHorario";
            this.pnHorario.Size = new System.Drawing.Size(196, 46);
            this.pnHorario.TabIndex = 4;
            // 
            // btnHorario
            // 
            this.btnHorario.BackColor = System.Drawing.Color.Teal;
            this.btnHorario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHorario.Font = new System.Drawing.Font("Segoe UI Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHorario.ForeColor = System.Drawing.Color.White;
            this.btnHorario.Image = ((System.Drawing.Image)(resources.GetObject("btnHorario.Image")));
            this.btnHorario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHorario.Location = new System.Drawing.Point(-20, -38);
            this.btnHorario.Name = "btnHorario";
            this.btnHorario.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnHorario.Size = new System.Drawing.Size(253, 124);
            this.btnHorario.TabIndex = 2;
            this.btnHorario.Text = "           Horario";
            this.btnHorario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHorario.UseVisualStyleBackColor = false;
            this.btnHorario.Click += new System.EventHandler(this.btnHorario_Click);
            // 
            // pnAlumnos
            // 
            this.pnAlumnos.BackColor = System.Drawing.Color.Transparent;
            this.pnAlumnos.Controls.Add(this.btnAlumnos);
            this.pnAlumnos.ForeColor = System.Drawing.Color.Transparent;
            this.pnAlumnos.Location = new System.Drawing.Point(3, 137);
            this.pnAlumnos.Name = "pnAlumnos";
            this.pnAlumnos.Size = new System.Drawing.Size(196, 46);
            this.pnAlumnos.TabIndex = 5;
            // 
            // btnAlumnos
            // 
            this.btnAlumnos.BackColor = System.Drawing.Color.Teal;
            this.btnAlumnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlumnos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlumnos.ForeColor = System.Drawing.Color.White;
            this.btnAlumnos.Image = ((System.Drawing.Image)(resources.GetObject("btnAlumnos.Image")));
            this.btnAlumnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlumnos.Location = new System.Drawing.Point(-20, -38);
            this.btnAlumnos.Name = "btnAlumnos";
            this.btnAlumnos.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnAlumnos.Size = new System.Drawing.Size(253, 124);
            this.btnAlumnos.TabIndex = 2;
            this.btnAlumnos.Text = "           Alumnos";
            this.btnAlumnos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlumnos.UseVisualStyleBackColor = false;
            this.btnAlumnos.Click += new System.EventHandler(this.btnAlumnos_Click);
            // 
            // pnReportes
            // 
            this.pnReportes.BackColor = System.Drawing.Color.Transparent;
            this.pnReportes.Controls.Add(this.btnReportes);
            this.pnReportes.ForeColor = System.Drawing.Color.Transparent;
            this.pnReportes.Location = new System.Drawing.Point(3, 189);
            this.pnReportes.Name = "pnReportes";
            this.pnReportes.Size = new System.Drawing.Size(196, 46);
            this.pnReportes.TabIndex = 6;
            // 
            // btnReportes
            // 
            this.btnReportes.BackColor = System.Drawing.Color.Teal;
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Image = ((System.Drawing.Image)(resources.GetObject("btnReportes.Image")));
            this.btnReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.Location = new System.Drawing.Point(-20, -39);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnReportes.Size = new System.Drawing.Size(253, 124);
            this.btnReportes.TabIndex = 2;
            this.btnReportes.Text = "           Reportes";
            this.btnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.UseVisualStyleBackColor = false;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // pnConfiguracion
            // 
            this.pnConfiguracion.BackColor = System.Drawing.Color.Transparent;
            this.pnConfiguracion.Controls.Add(this.btnConfiguracion);
            this.pnConfiguracion.ForeColor = System.Drawing.Color.Transparent;
            this.pnConfiguracion.Location = new System.Drawing.Point(3, 241);
            this.pnConfiguracion.Name = "pnConfiguracion";
            this.pnConfiguracion.Size = new System.Drawing.Size(196, 46);
            this.pnConfiguracion.TabIndex = 7;
            // 
            // btnConfiguracion
            // 
            this.btnConfiguracion.BackColor = System.Drawing.Color.Teal;
            this.btnConfiguracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracion.ForeColor = System.Drawing.Color.White;
            this.btnConfiguracion.Image = ((System.Drawing.Image)(resources.GetObject("btnConfiguracion.Image")));
            this.btnConfiguracion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfiguracion.Location = new System.Drawing.Point(-20, -39);
            this.btnConfiguracion.Name = "btnConfiguracion";
            this.btnConfiguracion.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnConfiguracion.Size = new System.Drawing.Size(253, 124);
            this.btnConfiguracion.TabIndex = 2;
            this.btnConfiguracion.Text = "           Configuracion";
            this.btnConfiguracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfiguracion.UseVisualStyleBackColor = false;
            // 
            // pnSobreNosotros
            // 
            this.pnSobreNosotros.BackColor = System.Drawing.Color.Transparent;
            this.pnSobreNosotros.Controls.Add(this.btnAboutUs);
            this.pnSobreNosotros.ForeColor = System.Drawing.Color.Transparent;
            this.pnSobreNosotros.Location = new System.Drawing.Point(3, 293);
            this.pnSobreNosotros.Name = "pnSobreNosotros";
            this.pnSobreNosotros.Size = new System.Drawing.Size(196, 46);
            this.pnSobreNosotros.TabIndex = 8;
            // 
            // btnAboutUs
            // 
            this.btnAboutUs.BackColor = System.Drawing.Color.Teal;
            this.btnAboutUs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAboutUs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAboutUs.ForeColor = System.Drawing.Color.White;
            this.btnAboutUs.Image = ((System.Drawing.Image)(resources.GetObject("btnAboutUs.Image")));
            this.btnAboutUs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAboutUs.Location = new System.Drawing.Point(-20, -39);
            this.btnAboutUs.Name = "btnAboutUs";
            this.btnAboutUs.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnAboutUs.Size = new System.Drawing.Size(253, 124);
            this.btnAboutUs.TabIndex = 2;
            this.btnAboutUs.Text = "           Sobre Nosotros";
            this.btnAboutUs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAboutUs.UseVisualStyleBackColor = false;
            this.btnAboutUs.Click += new System.EventHandler(this.btnAboutUs_Click);
            // 
            // sidebarTransition
            // 
            this.sidebarTransition.Interval = 10;
            this.sidebarTransition.Tick += new System.EventHandler(this.sidebarTransition_Tick);
            // 
            // panelContenedor
            // 
            this.panelContenedor.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Padding = new System.Windows.Forms.Padding(200, 50, 200, 20);
            this.panelContenedor.Size = new System.Drawing.Size(981, 555);
            this.panelContenedor.TabIndex = 3;
            this.panelContenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenedor_Paint_1);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(981, 555);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.panelContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "MenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuPrincipal";
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).EndInit();
            this.sidebar.ResumeLayout(false);
            this.pnAsistencia.ResumeLayout(false);
            this.pnHorario.ResumeLayout(false);
            this.pnAlumnos.ResumeLayout(false);
            this.pnReportes.ResumeLayout(false);
            this.pnConfiguracion.ResumeLayout(false);
            this.pnSobreNosotros.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox btnHam;
        private System.Windows.Forms.Label lblMenuPrincipal;
        private ControlesUsuario.WindowBarControl windowBarControl1;
        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Button btnAsistencia;
        private System.Windows.Forms.Panel pnAsistencia;
        private System.Windows.Forms.Panel pnHorario;
        private System.Windows.Forms.Button btnHorario;
        private System.Windows.Forms.Panel pnReportes;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Panel pnAlumnos;
        private System.Windows.Forms.Button btnAlumnos;
        private System.Windows.Forms.Panel pnConfiguracion;
        private System.Windows.Forms.Button btnConfiguracion;
        private System.Windows.Forms.Panel pnSobreNosotros;
        private System.Windows.Forms.Button btnAboutUs;
        private System.Windows.Forms.Timer sidebarTransition;
        private System.Windows.Forms.Panel panelContenedor;
    }
}