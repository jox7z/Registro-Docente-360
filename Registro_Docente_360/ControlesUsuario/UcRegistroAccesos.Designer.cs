namespace Registro_Docente_360.Forms
{
    partial class UcRegistroAccesos
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMiniContenedor = new SiticoneNetFrameworkUI.SiticonePanel();
            this.panelContenidos = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRegistrodeAccesos = new System.Windows.Forms.Label();
            this.panelCambiarContra = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscarUsuario = new System.Windows.Forms.TextBox();
            this.dgAccesos = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Salida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMiniContenedor.SuspendLayout();
            this.panelContenidos.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelCambiarContra.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAccesos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMiniContenedor
            // 
            this.panelMiniContenedor.AcrylicTintColor = System.Drawing.Color.White;
            this.panelMiniContenedor.AutoSize = true;
            this.panelMiniContenedor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMiniContenedor.BackColor = System.Drawing.Color.Transparent;
            this.panelMiniContenedor.BorderAlignment = System.Drawing.Drawing2D.PenAlignment.Center;
            this.panelMiniContenedor.BorderDashPattern = null;
            this.panelMiniContenedor.BorderGradientEndColor = System.Drawing.Color.Purple;
            this.panelMiniContenedor.BorderGradientStartColor = System.Drawing.Color.Blue;
            this.panelMiniContenedor.BorderThickness = 2F;
            this.panelMiniContenedor.Controls.Add(this.panelContenidos);
            this.panelMiniContenedor.CornerRadiusBottomLeft = 9F;
            this.panelMiniContenedor.CornerRadiusBottomRight = 9F;
            this.panelMiniContenedor.CornerRadiusTopLeft = 9F;
            this.panelMiniContenedor.CornerRadiusTopRight = 9F;
            this.panelMiniContenedor.EnableAcrylicEffect = false;
            this.panelMiniContenedor.EnableMicaEffect = false;
            this.panelMiniContenedor.EnableRippleEffect = false;
            this.panelMiniContenedor.FillColor = System.Drawing.Color.White;
            this.panelMiniContenedor.GradientColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.LightGray,
        System.Drawing.Color.Gray};
            this.panelMiniContenedor.GradientPositions = new float[] {
        0F,
        0.5F,
        1F};
            this.panelMiniContenedor.Location = new System.Drawing.Point(14, 14);
            this.panelMiniContenedor.Name = "panelMiniContenedor";
            this.panelMiniContenedor.Padding = new System.Windows.Forms.Padding(7);
            this.panelMiniContenedor.PatternStyle = System.Drawing.Drawing2D.HatchStyle.LargeGrid;
            this.panelMiniContenedor.RippleAlpha = 50;
            this.panelMiniContenedor.RippleAlphaDecrement = 3;
            this.panelMiniContenedor.RippleColor = System.Drawing.Color.White;
            this.panelMiniContenedor.RippleMaxSize = 600F;
            this.panelMiniContenedor.RippleSpeed = 15F;
            this.panelMiniContenedor.ShowBorder = false;
            this.panelMiniContenedor.Size = new System.Drawing.Size(706, 615);
            this.panelMiniContenedor.TabIndex = 3;
            this.panelMiniContenedor.TabStop = true;
            this.panelMiniContenedor.TrackSystemTheme = false;
            this.panelMiniContenedor.UseBorderGradient = false;
            this.panelMiniContenedor.UseMultiGradient = false;
            this.panelMiniContenedor.UsePatternTexture = false;
            this.panelMiniContenedor.UseRadialGradient = false;
            // 
            // panelContenidos
            // 
            this.panelContenidos.AutoSize = true;
            this.panelContenidos.Controls.Add(this.panel1);
            this.panelContenidos.Controls.Add(this.panelCambiarContra);
            this.panelContenidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenidos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelContenidos.Location = new System.Drawing.Point(7, 7);
            this.panelContenidos.Name = "panelContenidos";
            this.panelContenidos.Size = new System.Drawing.Size(692, 601);
            this.panelContenidos.TabIndex = 1;
            this.panelContenidos.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRegistrodeAccesos);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel1.Size = new System.Drawing.Size(686, 43);
            this.panel1.TabIndex = 0;
            // 
            // lblRegistrodeAccesos
            // 
            this.lblRegistrodeAccesos.AutoSize = true;
            this.lblRegistrodeAccesos.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistrodeAccesos.ForeColor = System.Drawing.Color.Teal;
            this.lblRegistrodeAccesos.Location = new System.Drawing.Point(247, 7);
            this.lblRegistrodeAccesos.Name = "lblRegistrodeAccesos";
            this.lblRegistrodeAccesos.Size = new System.Drawing.Size(187, 25);
            this.lblRegistrodeAccesos.TabIndex = 2;
            this.lblRegistrodeAccesos.Text = "Registro de Accesos";
            // 
            // panelCambiarContra
            // 
            this.panelCambiarContra.Controls.Add(this.lblTotal);
            this.panelCambiarContra.Controls.Add(this.btnLimpiar);
            this.panelCambiarContra.Controls.Add(this.panel2);
            this.panelCambiarContra.Controls.Add(this.dgAccesos);
            this.panelCambiarContra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCambiarContra.Location = new System.Drawing.Point(3, 52);
            this.panelCambiarContra.Name = "panelCambiarContra";
            this.panelCambiarContra.Size = new System.Drawing.Size(686, 546);
            this.panelCambiarContra.TabIndex = 1;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Teal;
            this.lblTotal.Location = new System.Drawing.Point(33, 499);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(171, 17);
            this.lblTotal.TabIndex = 20;
            this.lblTotal.Text = "Total sesiones mostradas: ";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Teal;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(512, 499);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(137, 29);
            this.btnLimpiar.TabIndex = 19;
            this.btnLimpiar.Text = "Limpiar filtro";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.txtBuscarUsuario);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(686, 74);
            this.panel2.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(31, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Buscar por usuario:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Teal;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(436, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(79, 29);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscarUsuario
            // 
            this.txtBuscarUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarUsuario.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarUsuario.Location = new System.Drawing.Point(221, 25);
            this.txtBuscarUsuario.Name = "txtBuscarUsuario";
            this.txtBuscarUsuario.Size = new System.Drawing.Size(210, 29);
            this.txtBuscarUsuario.TabIndex = 17;
            // 
            // dgAccesos
            // 
            this.dgAccesos.AllowUserToAddRows = false;
            this.dgAccesos.AllowUserToDeleteRows = false;
            this.dgAccesos.AllowUserToResizeColumns = false;
            this.dgAccesos.AllowUserToResizeRows = false;
            this.dgAccesos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgAccesos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgAccesos.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgAccesos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAccesos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgAccesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAccesos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Usuario,
            this.Ingreso,
            this.Salida,
            this.Resultado});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAccesos.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgAccesos.EnableHeadersVisualStyles = false;
            this.dgAccesos.Location = new System.Drawing.Point(36, 93);
            this.dgAccesos.Name = "dgAccesos";
            this.dgAccesos.ReadOnly = true;
            this.dgAccesos.RowHeadersVisible = false;
            this.dgAccesos.Size = new System.Drawing.Size(613, 391);
            this.dgAccesos.TabIndex = 15;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ID.FillWeight = 50F;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Usuario
            // 
            this.Usuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Usuario.HeaderText = "Usuario";
            this.Usuario.Name = "Usuario";
            this.Usuario.ReadOnly = true;
            // 
            // Ingreso
            // 
            this.Ingreso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Ingreso.FillWeight = 50F;
            this.Ingreso.HeaderText = "Ingreso";
            this.Ingreso.Name = "Ingreso";
            this.Ingreso.ReadOnly = true;
            // 
            // Salida
            // 
            this.Salida.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Salida.FillWeight = 50F;
            this.Salida.HeaderText = "Salida";
            this.Salida.Name = "Salida";
            this.Salida.ReadOnly = true;
            // 
            // Resultado
            // 
            this.Resultado.FillWeight = 80F;
            this.Resultado.HeaderText = "Tipo de acceso";
            this.Resultado.Name = "Resultado";
            this.Resultado.ReadOnly = true;
            // 
            // UcRegistroAccesos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelMiniContenedor);
            this.Name = "UcRegistroAccesos";
            this.Size = new System.Drawing.Size(735, 642);
            this.Load += new System.EventHandler(this.UcRegistroAccesos_Load);
            this.panelMiniContenedor.ResumeLayout(false);
            this.panelMiniContenedor.PerformLayout();
            this.panelContenidos.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCambiarContra.ResumeLayout(false);
            this.panelCambiarContra.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAccesos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SiticoneNetFrameworkUI.SiticonePanel panelMiniContenedor;
        private System.Windows.Forms.FlowLayoutPanel panelContenidos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRegistrodeAccesos;
        private System.Windows.Forms.Panel panelCambiarContra;
        private System.Windows.Forms.TextBox txtBuscarUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgAccesos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ingreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Salida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resultado;
    }
}
