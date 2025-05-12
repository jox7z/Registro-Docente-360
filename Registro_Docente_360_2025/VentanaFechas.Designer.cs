namespace Registro_Docente_360_2025
{
    partial class VentanaFechas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaFechas));
            comboMeses = new ComboBox();
            comboSemanas = new ComboBox();
            btnSeleccionarSemana = new Button();
            label12 = new Label();
            label1 = new Label();
            label2 = new Label();
            btnVolver = new Button();
            label3 = new Label();
            comboAnhos = new ComboBox();
            SuspendLayout();
            // 
            // comboMeses
            // 
            comboMeses.BackColor = Color.White;
            comboMeses.FlatStyle = FlatStyle.Flat;
            comboMeses.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboMeses.ForeColor = Color.Black;
            comboMeses.FormattingEnabled = true;
            comboMeses.Location = new Point(73, 216);
            comboMeses.Name = "comboMeses";
            comboMeses.Size = new Size(153, 25);
            comboMeses.TabIndex = 0;
            comboMeses.Text = "Meses";
            // 
            // comboSemanas
            // 
            comboSemanas.FlatStyle = FlatStyle.Flat;
            comboSemanas.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboSemanas.ForeColor = Color.Black;
            comboSemanas.FormattingEnabled = true;
            comboSemanas.Location = new Point(73, 314);
            comboSemanas.Name = "comboSemanas";
            comboSemanas.Size = new Size(155, 25);
            comboSemanas.TabIndex = 1;
            comboSemanas.Text = "Semanas";
            // 
            // btnSeleccionarSemana
            // 
            btnSeleccionarSemana.BackColor = Color.White;
            btnSeleccionarSemana.FlatStyle = FlatStyle.Flat;
            btnSeleccionarSemana.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSeleccionarSemana.ForeColor = Color.FromArgb(0, 192, 192);
            btnSeleccionarSemana.Location = new Point(64, 376);
            btnSeleccionarSemana.Name = "btnSeleccionarSemana";
            btnSeleccionarSemana.Size = new Size(176, 38);
            btnSeleccionarSemana.TabIndex = 2;
            btnSeleccionarSemana.Text = "Seleccionar Semana";
            btnSeleccionarSemana.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.FromArgb(0, 192, 192);
            label12.Location = new Point(63, 13);
            label12.Name = "label12";
            label12.Size = new Size(179, 30);
            label12.TabIndex = 21;
            label12.Text = "Meses - Semanas";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(0, 192, 192);
            label1.Location = new Point(51, 167);
            label1.Name = "label1";
            label1.Size = new Size(199, 30);
            label1.TabIndex = 22;
            label1.Text = "Seleccione un mes:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 192, 192);
            label2.Location = new Point(28, 265);
            label2.Name = "label2";
            label2.Size = new Size(245, 30);
            label2.TabIndex = 23;
            label2.Text = "Seleccione una semana:";
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImage = (Image)resources.GetObject("btnVolver.BackgroundImage");
            btnVolver.BackgroundImageLayout = ImageLayout.Zoom;
            btnVolver.Location = new Point(11, 13);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(29, 27);
            btnVolver.TabIndex = 24;
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(0, 192, 192);
            label3.Location = new Point(51, 72);
            label3.Name = "label3";
            label3.Size = new Size(201, 30);
            label3.TabIndex = 25;
            label3.Text = "Seleccione un Año:";
            // 
            // comboAnhos
            // 
            comboAnhos.BackColor = Color.White;
            comboAnhos.FlatStyle = FlatStyle.Flat;
            comboAnhos.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboAnhos.ForeColor = Color.Black;
            comboAnhos.FormattingEnabled = true;
            comboAnhos.Location = new Point(73, 120);
            comboAnhos.Name = "comboAnhos";
            comboAnhos.Size = new Size(153, 25);
            comboAnhos.TabIndex = 26;
            comboAnhos.Text = "Años";
            // 
            // VentanaFechas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(296, 450);
            Controls.Add(comboAnhos);
            Controls.Add(label3);
            Controls.Add(btnVolver);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label12);
            Controls.Add(btnSeleccionarSemana);
            Controls.Add(comboSemanas);
            Controls.Add(comboMeses);
            Name = "VentanaFechas";
            Text = "Seleccion de Fechas";
            Load += VentanaFechas_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboMeses;
        private ComboBox comboSemanas;
        private Button btnSeleccionarSemana;
        private Label label12;
        private Label label1;
        private Label label2;
        private Button btnVolver;
        private Label label3;
        private ComboBox comboAnhos;
    }
}