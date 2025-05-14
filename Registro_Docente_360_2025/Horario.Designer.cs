namespace Registro_Docente_360_2025
{
    partial class Horario
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Horario));
            btnVolver = new Button();
            lblHorario = new Label();
            dataGridHorario = new DataGridView();
            ColumLeccion = new DataGridViewTextBoxColumn();
            ColumnHora = new DataGridViewTextBoxColumn();
            ColumnLunes = new DataGridViewTextBoxColumn();
            ColumMartes = new DataGridViewTextBoxColumn();
            ColumnMiercoles = new DataGridViewTextBoxColumn();
            ColumnJueves = new DataGridViewTextBoxColumn();
            ColumnViernes = new DataGridViewTextBoxColumn();
            label1 = new Label();
            lblnumseccion = new Label();
            label2 = new Label();
            lblNombreDocente = new Label();
            btnEditarhorario = new Button();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dataGridHorario).BeginInit();
            SuspendLayout();
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImage = (Image)resources.GetObject("btnVolver.BackgroundImage");
            btnVolver.BackgroundImageLayout = ImageLayout.Zoom;
            btnVolver.Location = new Point(12, 12);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(31, 31);
            btnVolver.TabIndex = 25;
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHorario.ForeColor = Color.FromArgb(0, 192, 192);
            lblHorario.Location = new Point(49, 5);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(295, 40);
            lblHorario.TabIndex = 26;
            lblHorario.Text = "Horario del Docente";
            lblHorario.TextAlign = ContentAlignment.TopCenter;
            // 
            // dataGridHorario
            // 
            dataGridHorario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridHorario.Columns.AddRange(new DataGridViewColumn[] { ColumLeccion, ColumnHora, ColumnLunes, ColumMartes, ColumnMiercoles, ColumnJueves, ColumnViernes });
            dataGridHorario.Location = new Point(23, 156);
            dataGridHorario.Name = "dataGridHorario";
            dataGridHorario.Size = new Size(741, 389);
            dataGridHorario.TabIndex = 27;
            // 
            // ColumLeccion
            // 
            ColumLeccion.HeaderText = "Lección";
            ColumLeccion.Name = "ColumLeccion";
            ColumLeccion.ReadOnly = true;
            // 
            // ColumnHora
            // 
            ColumnHora.HeaderText = "Hora";
            ColumnHora.Name = "ColumnHora";
            ColumnHora.ReadOnly = true;
            // 
            // ColumnLunes
            // 
            ColumnLunes.HeaderText = "Lunes";
            ColumnLunes.Name = "ColumnLunes";
            // 
            // ColumMartes
            // 
            ColumMartes.HeaderText = "Martes";
            ColumMartes.Name = "ColumMartes";
            // 
            // ColumnMiercoles
            // 
            ColumnMiercoles.HeaderText = "Miércoles";
            ColumnMiercoles.Name = "ColumnMiercoles";
            // 
            // ColumnJueves
            // 
            ColumnJueves.HeaderText = "Jueves";
            ColumnJueves.Name = "ColumnJueves";
            // 
            // ColumnViernes
            // 
            ColumnViernes.HeaderText = "Viernes";
            ColumnViernes.Name = "ColumnViernes";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 124);
            label1.Name = "label1";
            label1.Size = new Size(85, 25);
            label1.TabIndex = 29;
            label1.Text = "Sección:";
            // 
            // lblnumseccion
            // 
            lblnumseccion.AutoSize = true;
            lblnumseccion.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblnumseccion.ForeColor = Color.FromArgb(0, 192, 192);
            lblnumseccion.Location = new Point(104, 125);
            lblnumseccion.Name = "lblnumseccion";
            lblnumseccion.Size = new Size(91, 25);
            lblnumseccion.TabIndex = 30;
            lblnumseccion.Text = "Numero ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(23, 100);
            label2.Name = "label2";
            label2.Size = new Size(96, 25);
            label2.TabIndex = 31;
            label2.Text = "Docente: ";
            // 
            // lblNombreDocente
            // 
            lblNombreDocente.AutoSize = true;
            lblNombreDocente.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombreDocente.ForeColor = Color.FromArgb(0, 192, 192);
            lblNombreDocente.Location = new Point(108, 100);
            lblNombreDocente.Name = "lblNombreDocente";
            lblNombreDocente.Size = new Size(391, 25);
            lblNombreDocente.TabIndex = 32;
            lblNombreDocente.Text = "Nombre del Docente (tomarlo del usuario)";
            // 
            // btnEditarhorario
            // 
            btnEditarhorario.BackColor = Color.White;
            btnEditarhorario.Cursor = Cursors.Hand;
            btnEditarhorario.FlatStyle = FlatStyle.Flat;
            btnEditarhorario.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditarhorario.ForeColor = Color.FromArgb(0, 192, 192);
            btnEditarhorario.Location = new Point(562, 556);
            btnEditarhorario.Name = "btnEditarhorario";
            btnEditarhorario.Size = new Size(203, 41);
            btnEditarhorario.TabIndex = 33;
            btnEditarhorario.Text = "EDITAR HORARIO";
            toolTip1.SetToolTip(btnEditarhorario, "Haz clic para editar el horario");
            btnEditarhorario.UseVisualStyleBackColor = false;
            btnEditarhorario.Click += btnEditarhorario_Click;
            // 
            // Horario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(787, 622);
            Controls.Add(btnEditarhorario);
            Controls.Add(lblNombreDocente);
            Controls.Add(label2);
            Controls.Add(lblnumseccion);
            Controls.Add(label1);
            Controls.Add(dataGridHorario);
            Controls.Add(lblHorario);
            Controls.Add(btnVolver);
            Name = "Horario";
            Text = "Horario";
            Load += Horario_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridHorario).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVolver;
        private Label lblHorario;
        private DataGridView dataGridHorario;
        private DataGridViewTextBoxColumn ColumLeccion;
        private DataGridViewTextBoxColumn ColumnHora;
        private DataGridViewTextBoxColumn ColumnLunes;
        private DataGridViewTextBoxColumn ColumMartes;
        private DataGridViewTextBoxColumn ColumnMiercoles;
        private DataGridViewTextBoxColumn ColumnJueves;
        private DataGridViewTextBoxColumn ColumnViernes;
        private Label label1;
        private Label lblnumseccion;
        private Label label2;
        private Label lblNombreDocente;
        private Button btnEditarhorario;
        private ToolTip toolTip1;
    }
}