namespace Registro_Docente_360_2025
{
    partial class Ventana_de_Lista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_de_Lista));
            datagridLista = new DataGridView();
            ColumEstudiante = new DataGridViewTextBoxColumn();
            ColumLunes = new DataGridViewTextBoxColumn();
            ColumMartes = new DataGridViewTextBoxColumn();
            ColumMiercoles = new DataGridViewTextBoxColumn();
            ColumJueves = new DataGridViewTextBoxColumn();
            ColumViernes = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            btnSeleccionarSemana = new Button();
            label12 = new Label();
            label1 = new Label();
            btnVolver = new Button();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)datagridLista).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // datagridLista
            // 
            datagridLista.BackgroundColor = Color.FromArgb(224, 224, 224);
            datagridLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridLista.Columns.AddRange(new DataGridViewColumn[] { ColumEstudiante, ColumLunes, ColumMartes, ColumMiercoles, ColumJueves, ColumViernes });
            datagridLista.Dock = DockStyle.Fill;
            datagridLista.GridColor = Color.Black;
            datagridLista.Location = new Point(0, 0);
            datagridLista.Name = "datagridLista";
            datagridLista.Size = new Size(644, 467);
            datagridLista.TabIndex = 0;
            // 
            // ColumEstudiante
            // 
            ColumEstudiante.HeaderText = "Estudiante";
            ColumEstudiante.Name = "ColumEstudiante";
            // 
            // ColumLunes
            // 
            ColumLunes.HeaderText = "Lunes";
            ColumLunes.Name = "ColumLunes";
            // 
            // ColumMartes
            // 
            ColumMartes.HeaderText = "Martes";
            ColumMartes.Name = "ColumMartes";
            // 
            // ColumMiercoles
            // 
            ColumMiercoles.HeaderText = "Miercoles";
            ColumMiercoles.Name = "ColumMiercoles";
            // 
            // ColumJueves
            // 
            ColumJueves.HeaderText = "Jueves";
            ColumJueves.Name = "ColumJueves";
            // 
            // ColumViernes
            // 
            ColumViernes.HeaderText = "Viernes";
            ColumViernes.Name = "ColumViernes";
            // 
            // panel1
            // 
            panel1.Controls.Add(datagridLista);
            panel1.Location = new Point(11, 58);
            panel1.Name = "panel1";
            panel1.Size = new Size(644, 467);
            panel1.TabIndex = 2;
            // 
            // btnSeleccionarSemana
            // 
            btnSeleccionarSemana.BackColor = Color.White;
            btnSeleccionarSemana.FlatStyle = FlatStyle.Flat;
            btnSeleccionarSemana.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSeleccionarSemana.ForeColor = Color.FromArgb(0, 192, 192);
            btnSeleccionarSemana.Location = new Point(671, 466);
            btnSeleccionarSemana.Name = "btnSeleccionarSemana";
            btnSeleccionarSemana.Size = new Size(176, 59);
            btnSeleccionarSemana.TabIndex = 4;
            btnSeleccionarSemana.Text = "Guardar";
            btnSeleccionarSemana.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.FromArgb(0, 192, 192);
            label12.Location = new Point(669, 98);
            label12.Name = "label12";
            label12.Size = new Size(59, 30);
            label12.TabIndex = 23;
            label12.Text = "Mes:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(0, 192, 192);
            label1.Location = new Point(669, 58);
            label1.Name = "label1";
            label1.Size = new Size(96, 30);
            label1.TabIndex = 23;
            label1.Text = "Semana:";
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImage = (Image)resources.GetObject("btnVolver.BackgroundImage");
            btnVolver.BackgroundImageLayout = ImageLayout.Zoom;
            btnVolver.Location = new Point(11, 15);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(29, 27);
            btnVolver.TabIndex = 25;
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(771, 58);
            label2.Name = "label2";
            label2.Size = new Size(45, 30);
            label2.TabIndex = 26;
            label2.Text = "3-7";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(734, 98);
            label3.Name = "label3";
            label3.Size = new Size(88, 30);
            label3.TabIndex = 27;
            label3.Text = "Febrero";
            // 
            // Ventana_de_Lista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 545);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnVolver);
            Controls.Add(label1);
            Controls.Add(label12);
            Controls.Add(btnSeleccionarSemana);
            Controls.Add(panel1);
            Name = "Ventana_de_Lista";
            Text = "Ventana_de_Lista";
            Load += Ventana_de_Lista_Load;
            ((System.ComponentModel.ISupportInitialize)datagridLista).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView datagridLista;
        private DataGridViewTextBoxColumn ColumEstudiante;
        private DataGridViewTextBoxColumn ColumLunes;
        private DataGridViewTextBoxColumn ColumMartes;
        private DataGridViewTextBoxColumn ColumMiercoles;
        private DataGridViewTextBoxColumn ColumJueves;
        private DataGridViewTextBoxColumn ColumViernes;
        private Panel panel1;
        private Button btnSeleccionarSemana;
        private Label label12;
        private Label label1;
        private Button btnVolver;
        private Label label2;
        private Label label3;
    }
}