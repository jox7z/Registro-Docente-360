namespace Registro_Docente_360.Forms
{
    partial class LoginTextBox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbltextlogin = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lbltextlogin);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 44);
            this.panel1.TabIndex = 0;
            // 
            // lbltextlogin
            // 
            this.lbltextlogin.AutoSize = true;
            this.lbltextlogin.Font = new System.Drawing.Font("Nirmala Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltextlogin.Location = new System.Drawing.Point(-3, 4);
            this.lbltextlogin.Name = "lbltextlogin";
            this.lbltextlogin.Size = new System.Drawing.Size(40, 15);
            this.lbltextlogin.TabIndex = 1;
            this.lbltextlogin.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Nirmala Text", 9F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(0, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(166, 23);
            this.textBox1.TabIndex = 0;
            // 
            // LoginTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.Controls.Add(this.panel1);
            this.Name = "LoginTextBox";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.Size = new System.Drawing.Size(166, 45);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LoginTextBox_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbltextlogin;
    }
}
