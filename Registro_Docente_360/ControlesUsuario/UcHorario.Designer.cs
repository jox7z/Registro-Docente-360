using System.Windows.Forms;

namespace Registro_Docente_360.ControlesUsuario
{
    partial class UcHorario
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnEditarHorario = new System.Windows.Forms.Panel();
            this.btnEditarHorario = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSeccion = new System.Windows.Forms.TextBox();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNomDocente = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHorario = new System.Windows.Forms.Label();
            this.dataGridPerso1 = new Registro_Docente_360.ControlesUsuario.dataGridPerso();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnEditarHorario.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnEditarHorario, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHorario, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridPerso1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1145, 616);
            this.tableLayoutPanel1.TabIndex = 4;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // pnEditarHorario
            // 
            this.pnEditarHorario.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnEditarHorario.BackColor = System.Drawing.Color.Transparent;
            this.pnEditarHorario.Controls.Add(this.btnEditarHorario);
            this.pnEditarHorario.ForeColor = System.Drawing.Color.Transparent;
            this.pnEditarHorario.Location = new System.Drawing.Point(465, 555);
            this.pnEditarHorario.Name = "pnEditarHorario";
            this.pnEditarHorario.Size = new System.Drawing.Size(214, 58);
            this.pnEditarHorario.TabIndex = 19;
            // 
            // btnEditarHorario
            // 
            this.btnEditarHorario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEditarHorario.BackColor = System.Drawing.Color.Teal;
            this.btnEditarHorario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditarHorario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarHorario.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarHorario.ForeColor = System.Drawing.Color.White;
            this.btnEditarHorario.Location = new System.Drawing.Point(-12, -15);
            this.btnEditarHorario.Name = "btnEditarHorario";
            this.btnEditarHorario.Size = new System.Drawing.Size(239, 84);
            this.btnEditarHorario.TabIndex = 2;
            this.btnEditarHorario.Text = "EDITAR HORARIO";
            this.btnEditarHorario.UseVisualStyleBackColor = false;
            this.btnEditarHorario.Click += new System.EventHandler(this.btnEditarHorario_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.txtSeccion, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSeccion, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 103);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1139, 44);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // txtSeccion
            // 
            this.txtSeccion.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtSeccion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSeccion.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtSeccion.ForeColor = System.Drawing.Color.Gray;
            this.txtSeccion.Location = new System.Drawing.Point(111, 3);
            this.txtSeccion.Name = "txtSeccion";
            this.txtSeccion.Size = new System.Drawing.Size(265, 28);
            this.txtSeccion.TabIndex = 12;
            this.txtSeccion.Text = "Inserte número de sección";
            // 
            // lblSeccion
            // 
            this.lblSeccion.AutoSize = true;
            this.lblSeccion.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblSeccion.ForeColor = System.Drawing.Color.Black;
            this.lblSeccion.Location = new System.Drawing.Point(11, 0);
            this.lblSeccion.Name = "lblSeccion";
            this.lblSeccion.Size = new System.Drawing.Size(94, 30);
            this.lblSeccion.TabIndex = 11;
            this.lblSeccion.Text = "Sección:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblNomDocente, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1139, 44);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // lblNomDocente
            // 
            this.lblNomDocente.AutoSize = true;
            this.lblNomDocente.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblNomDocente.ForeColor = System.Drawing.Color.Teal;
            this.lblNomDocente.Location = new System.Drawing.Point(117, 0);
            this.lblNomDocente.Name = "lblNomDocente";
            this.lblNomDocente.Size = new System.Drawing.Size(432, 30);
            this.lblNomDocente.TabIndex = 10;
            this.lblNomDocente.Text = "Nombre del Docente (tomarlo del usuario)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(108, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "Docente:";
            // 
            // lblHorario
            // 
            this.lblHorario.AutoSize = true;
            this.lblHorario.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold);
            this.lblHorario.ForeColor = System.Drawing.Color.Teal;
            this.lblHorario.Location = new System.Drawing.Point(3, 0);
            this.lblHorario.Name = "lblHorario";
            this.lblHorario.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblHorario.Size = new System.Drawing.Size(303, 40);
            this.lblHorario.TabIndex = 7;
            this.lblHorario.Text = "Horario del Docente";
            // 
            // dataGridPerso1
            // 
            this.dataGridPerso1.BackColor = System.Drawing.Color.Transparent;
            this.dataGridPerso1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridPerso1.EncabezadoColumna0 = "Lección";
            this.dataGridPerso1.Location = new System.Drawing.Point(3, 153);
            this.dataGridPerso1.Name = "dataGridPerso1";
            this.dataGridPerso1.Padding = new System.Windows.Forms.Padding(20);
            this.dataGridPerso1.Size = new System.Drawing.Size(1139, 396);
            this.dataGridPerso1.TabIndex = 4;
            this.dataGridPerso1.Load += new System.EventHandler(this.dataGridPerso1_Load);
            // 
            // UcHorario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UcHorario";
            this.Size = new System.Drawing.Size(1145, 616);
            this.Load += new System.EventHandler(this.UcHorario_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnEditarHorario.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private dataGridPerso dataGridPerso1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblHorario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNomDocente;
        private System.Windows.Forms.TextBox txtSeccion;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.Panel pnEditarHorario;
        private System.Windows.Forms.Button btnEditarHorario;
    }
}
 