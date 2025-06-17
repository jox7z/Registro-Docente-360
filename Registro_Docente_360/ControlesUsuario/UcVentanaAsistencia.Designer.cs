namespace Registro_Docente_360.ControlesUsuario
{
    partial class UcVentanaAsistencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcVentanaAsistencia));
            this.pnGuardar = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tableLayoutPanelfondo = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridPerso1 = new Registro_Docente_360.ControlesUsuario.dataGridPerso();
            this.tableLayoutPaneltitulos = new System.Windows.Forms.TableLayoutPanel();
            this.lblSemana = new System.Windows.Forms.Label();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.PictureBox();
            this.pnGuardar.SuspendLayout();
            this.tableLayoutPanelfondo.SuspendLayout();
            this.tableLayoutPaneltitulos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnVolver)).BeginInit();
            this.SuspendLayout();
            // 
            // pnGuardar
            // 
            this.pnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnGuardar.BackColor = System.Drawing.Color.Transparent;
            this.pnGuardar.Controls.Add(this.btnGuardar);
            this.pnGuardar.ForeColor = System.Drawing.Color.Transparent;
            this.pnGuardar.Location = new System.Drawing.Point(449, 566);
            this.pnGuardar.Name = "pnGuardar";
            this.pnGuardar.Size = new System.Drawing.Size(196, 46);
            this.pnGuardar.TabIndex = 18;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Teal;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(-21, -20);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(239, 84);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanelfondo
            // 
            this.tableLayoutPanelfondo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanelfondo.ColumnCount = 1;
            this.tableLayoutPanelfondo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelfondo.Controls.Add(this.dataGridPerso1, 0, 1);
            this.tableLayoutPanelfondo.Controls.Add(this.pnGuardar, 0, 2);
            this.tableLayoutPanelfondo.Controls.Add(this.tableLayoutPaneltitulos, 0, 0);
            this.tableLayoutPanelfondo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelfondo.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelfondo.Name = "tableLayoutPanelfondo";
            this.tableLayoutPanelfondo.RowCount = 3;
            this.tableLayoutPanelfondo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfondo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelfondo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfondo.Size = new System.Drawing.Size(1094, 615);
            this.tableLayoutPanelfondo.TabIndex = 19;
            this.tableLayoutPanelfondo.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // dataGridPerso1
            // 
            this.dataGridPerso1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridPerso1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridPerso1.EncabezadoColumna0 = "Estudiante";
            this.dataGridPerso1.Location = new System.Drawing.Point(3, 109);
            this.dataGridPerso1.Name = "dataGridPerso1";
            this.dataGridPerso1.Padding = new System.Windows.Forms.Padding(20);
            this.dataGridPerso1.Size = new System.Drawing.Size(1088, 451);
            this.dataGridPerso1.TabIndex = 0;
            // 
            // tableLayoutPaneltitulos
            // 
            this.tableLayoutPaneltitulos.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPaneltitulos.ColumnCount = 1;
            this.tableLayoutPaneltitulos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPaneltitulos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPaneltitulos.Controls.Add(this.lblSemana, 0, 2);
            this.tableLayoutPaneltitulos.Controls.Add(this.lblGrupo, 0, 1);
            this.tableLayoutPaneltitulos.Controls.Add(this.btnVolver, 0, 0);
            this.tableLayoutPaneltitulos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPaneltitulos.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPaneltitulos.Name = "tableLayoutPaneltitulos";
            this.tableLayoutPaneltitulos.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.tableLayoutPaneltitulos.RowCount = 3;
            this.tableLayoutPaneltitulos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPaneltitulos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPaneltitulos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPaneltitulos.Size = new System.Drawing.Size(1088, 100);
            this.tableLayoutPaneltitulos.TabIndex = 20;
            // 
            // lblSemana
            // 
            this.lblSemana.AutoSize = true;
            this.lblSemana.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSemana.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSemana.ForeColor = System.Drawing.Color.Teal;
            this.lblSemana.Location = new System.Drawing.Point(18, 80);
            this.lblSemana.Name = "lblSemana";
            this.lblSemana.Size = new System.Drawing.Size(1067, 20);
            this.lblSemana.TabIndex = 1;
            this.lblSemana.Text = "label2";
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGrupo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrupo.ForeColor = System.Drawing.Color.Teal;
            this.lblGrupo.Location = new System.Drawing.Point(18, 40);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(1067, 40);
            this.lblGrupo.TabIndex = 0;
            this.lblGrupo.Text = "label1";
            // 
            // btnVolver
            // 
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.Image = ((System.Drawing.Image)(resources.GetObject("btnVolver.Image")));
            this.btnVolver.Location = new System.Drawing.Point(18, 3);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(37, 34);
            this.btnVolver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnVolver.TabIndex = 2;
            this.btnVolver.TabStop = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // UcVentanaAsistencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelfondo);
            this.Name = "UcVentanaAsistencia";
            this.Size = new System.Drawing.Size(1094, 615);
            this.pnGuardar.ResumeLayout(false);
            this.tableLayoutPanelfondo.ResumeLayout(false);
            this.tableLayoutPaneltitulos.ResumeLayout(false);
            this.tableLayoutPaneltitulos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnVolver)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnGuardar;
        private System.Windows.Forms.Button btnGuardar;
        private dataGridPerso dataGridPerso1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelfondo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPaneltitulos;
        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.Label lblSemana;
        private System.Windows.Forms.PictureBox btnVolver;
    }
}
