namespace Registro_Docente_360.ControlesUsuario
{
    partial class UcNotas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcNotas));
            this.PanelAcciones = new System.Windows.Forms.TableLayoutPanel();
            this.pnExportar = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.pnGuardar = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.pnCancelar = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblNotas = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNomDocente = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDocentes = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSecc = new System.Windows.Forms.Label();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.cmbMateria = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.pnAgregarNotas = new System.Windows.Forms.Panel();
            this.btnGestionarNotas = new System.Windows.Forms.Button();
            this.tableLayoutContenedor = new System.Windows.Forms.TableLayoutPanel();
            this.tablaNotas = new Registro_Docente_360.ControlesUsuario.dataGridPersoNotas();
            this.PanelAcciones.SuspendLayout();
            this.pnExportar.SuspendLayout();
            this.pnGuardar.SuspendLayout();
            this.pnCancelar.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.pnAgregarNotas.SuspendLayout();
            this.tableLayoutContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelAcciones
            // 
            this.PanelAcciones.BackColor = System.Drawing.Color.Gainsboro;
            this.PanelAcciones.ColumnCount = 3;
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PanelAcciones.Controls.Add(this.pnExportar, 2, 0);
            this.PanelAcciones.Controls.Add(this.pnGuardar, 0, 0);
            this.PanelAcciones.Controls.Add(this.pnCancelar, 1, 0);
            this.PanelAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelAcciones.Location = new System.Drawing.Point(3, 512);
            this.PanelAcciones.Name = "PanelAcciones";
            this.PanelAcciones.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.PanelAcciones.RowCount = 1;
            this.PanelAcciones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelAcciones.Size = new System.Drawing.Size(973, 64);
            this.PanelAcciones.TabIndex = 21;
            // 
            // pnExportar
            // 
            this.pnExportar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnExportar.BackColor = System.Drawing.Color.Transparent;
            this.pnExportar.Controls.Add(this.btnExportar);
            this.pnExportar.ForeColor = System.Drawing.Color.Transparent;
            this.pnExportar.Location = new System.Drawing.Point(705, 9);
            this.pnExportar.Name = "pnExportar";
            this.pnExportar.Size = new System.Drawing.Size(145, 46);
            this.pnExportar.TabIndex = 13;
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportar.BackColor = System.Drawing.Color.Teal;
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.Location = new System.Drawing.Point(-15, -39);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnExportar.Size = new System.Drawing.Size(166, 124);
            this.btnExportar.TabIndex = 2;
            this.btnExportar.Text = "          Exportar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // pnGuardar
            // 
            this.pnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnGuardar.BackColor = System.Drawing.Color.Transparent;
            this.pnGuardar.Controls.Add(this.btnGuardar);
            this.pnGuardar.ForeColor = System.Drawing.Color.Transparent;
            this.pnGuardar.Location = new System.Drawing.Point(119, 9);
            this.pnGuardar.Name = "pnGuardar";
            this.pnGuardar.Size = new System.Drawing.Size(152, 46);
            this.pnGuardar.TabIndex = 11;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardar.BackColor = System.Drawing.Color.Teal;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(-13, -39);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnGuardar.Size = new System.Drawing.Size(171, 124);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "            Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // pnCancelar
            // 
            this.pnCancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnCancelar.BackColor = System.Drawing.Color.Transparent;
            this.pnCancelar.Controls.Add(this.btnCancelar);
            this.pnCancelar.ForeColor = System.Drawing.Color.Transparent;
            this.pnCancelar.Location = new System.Drawing.Point(414, 9);
            this.pnCancelar.Name = "pnCancelar";
            this.pnCancelar.Size = new System.Drawing.Size(145, 46);
            this.pnCancelar.TabIndex = 12;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelar.BackColor = System.Drawing.Color.Teal;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(-15, -39);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnCancelar.Size = new System.Drawing.Size(166, 124);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "          Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblNotas
            // 
            this.lblNotas.AutoSize = true;
            this.lblNotas.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotas.ForeColor = System.Drawing.Color.Teal;
            this.lblNotas.Location = new System.Drawing.Point(3, 0);
            this.lblNotas.Name = "lblNotas";
            this.lblNotas.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblNotas.Size = new System.Drawing.Size(186, 30);
            this.lblNotas.TabIndex = 7;
            this.lblNotas.Text = "Listado de Notas";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 772F));
            this.tableLayoutPanel3.Controls.Add(this.lblNomDocente, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbDocentes, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(973, 34);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // lblNomDocente
            // 
            this.lblNomDocente.AutoSize = true;
            this.lblNomDocente.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomDocente.ForeColor = System.Drawing.Color.Teal;
            this.lblNomDocente.Location = new System.Drawing.Point(108, 0);
            this.lblNomDocente.Name = "lblNomDocente";
            this.lblNomDocente.Size = new System.Drawing.Size(86, 25);
            this.lblNomDocente.TabIndex = 10;
            this.lblNomDocente.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(99, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Docente:";
            // 
            // cmbDocentes
            // 
            this.cmbDocentes.FormattingEnabled = true;
            this.cmbDocentes.Location = new System.Drawing.Point(204, 3);
            this.cmbDocentes.Name = "cmbDocentes";
            this.cmbDocentes.Size = new System.Drawing.Size(121, 21);
            this.cmbDocentes.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 788F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.lblSecc, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSeccion, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbMateria, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 83);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(973, 34);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // lblSecc
            // 
            this.lblSecc.AutoSize = true;
            this.lblSecc.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecc.ForeColor = System.Drawing.Color.Teal;
            this.lblSecc.Location = new System.Drawing.Point(102, 0);
            this.lblSecc.Name = "lblSecc";
            this.lblSecc.Size = new System.Drawing.Size(80, 25);
            this.lblSecc.TabIndex = 12;
            this.lblSecc.Text = "Seccion";
            // 
            // lblSeccion
            // 
            this.lblSeccion.AutoSize = true;
            this.lblSeccion.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccion.ForeColor = System.Drawing.Color.Black;
            this.lblSeccion.Location = new System.Drawing.Point(11, 0);
            this.lblSeccion.Name = "lblSeccion";
            this.lblSeccion.Size = new System.Drawing.Size(85, 25);
            this.lblSeccion.TabIndex = 11;
            this.lblSeccion.Text = "Sección:";
            // 
            // cmbMateria
            // 
            this.cmbMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMateria.FormattingEnabled = true;
            this.cmbMateria.Location = new System.Drawing.Point(188, 3);
            this.cmbMateria.Name = "cmbMateria";
            this.cmbMateria.Size = new System.Drawing.Size(121, 21);
            this.cmbMateria.TabIndex = 13;
            this.cmbMateria.SelectedIndexChanged += new System.EventHandler(this.cmbMateria_SelectedIndexChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.pnAgregarNotas, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 582);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(973, 94);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // pnAgregarNotas
            // 
            this.pnAgregarNotas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnAgregarNotas.BackColor = System.Drawing.Color.Transparent;
            this.pnAgregarNotas.Controls.Add(this.btnGestionarNotas);
            this.pnAgregarNotas.ForeColor = System.Drawing.Color.Transparent;
            this.pnAgregarNotas.Location = new System.Drawing.Point(377, 28);
            this.pnAgregarNotas.Name = "pnAgregarNotas";
            this.pnAgregarNotas.Size = new System.Drawing.Size(219, 57);
            this.pnAgregarNotas.TabIndex = 9;
            // 
            // btnGestionarNotas
            // 
            this.btnGestionarNotas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGestionarNotas.BackColor = System.Drawing.Color.Teal;
            this.btnGestionarNotas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionarNotas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarNotas.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarNotas.ForeColor = System.Drawing.Color.White;
            this.btnGestionarNotas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionarNotas.Location = new System.Drawing.Point(-24, -10);
            this.btnGestionarNotas.Name = "btnGestionarNotas";
            this.btnGestionarNotas.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnGestionarNotas.Size = new System.Drawing.Size(247, 77);
            this.btnGestionarNotas.TabIndex = 2;
            this.btnGestionarNotas.Text = "Gestionar Notas";
            this.btnGestionarNotas.UseVisualStyleBackColor = false;
            this.btnGestionarNotas.Click += new System.EventHandler(this.btnGestionarNotas_Click);
            // 
            // tableLayoutContenedor
            // 
            this.tableLayoutContenedor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutContenedor.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutContenedor.ColumnCount = 1;
            this.tableLayoutContenedor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutContenedor.Controls.Add(this.tableLayoutPanel5, 0, 5);
            this.tableLayoutContenedor.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutContenedor.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutContenedor.Controls.Add(this.lblNotas, 0, 0);
            this.tableLayoutContenedor.Controls.Add(this.PanelAcciones, 0, 4);
            this.tableLayoutContenedor.Controls.Add(this.tablaNotas, 0, 3);
            this.tableLayoutContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutContenedor.Location = new System.Drawing.Point(20, 0);
            this.tableLayoutContenedor.Name = "tableLayoutContenedor";
            this.tableLayoutContenedor.RowCount = 6;
            this.tableLayoutContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutContenedor.Size = new System.Drawing.Size(979, 679);
            this.tableLayoutContenedor.TabIndex = 6;
            // 
            // tablaNotas
            // 
            this.tablaNotas.BackColor = System.Drawing.Color.Gainsboro;
            this.tablaNotas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaNotas.Location = new System.Drawing.Point(3, 123);
            this.tablaNotas.Name = "tablaNotas";
            this.tablaNotas.Size = new System.Drawing.Size(973, 383);
            this.tablaNotas.TabIndex = 22;
            // 
            // UcNotas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.tableLayoutContenedor);
            this.Name = "UcNotas";
            this.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.Size = new System.Drawing.Size(1019, 679);
            this.Load += new System.EventHandler(this.UcNotas_Load);
            this.PanelAcciones.ResumeLayout(false);
            this.pnExportar.ResumeLayout(false);
            this.pnGuardar.ResumeLayout(false);
            this.pnCancelar.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.pnAgregarNotas.ResumeLayout(false);
            this.tableLayoutContenedor.ResumeLayout(false);
            this.tableLayoutContenedor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PanelAcciones;
        private System.Windows.Forms.Panel pnCancelar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel pnGuardar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblNotas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblNomDocente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblSecc;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel pnAgregarNotas;
        private System.Windows.Forms.Button btnGestionarNotas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutContenedor;
        private dataGridPersoNotas tablaNotas;
        private System.Windows.Forms.Panel pnExportar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.ComboBox cmbMateria;
        private System.Windows.Forms.ComboBox cmbDocentes;
    }
}
