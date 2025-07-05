namespace Registro_Docente_360
{
    partial class UcDias
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
            this.lbldias = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbldias
            // 
            this.lbldias.AutoSize = true;
            this.lbldias.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldias.Location = new System.Drawing.Point(7, 8);
            this.lbldias.Name = "lbldias";
            this.lbldias.Size = new System.Drawing.Size(28, 21);
            this.lbldias.TabIndex = 0;
            this.lbldias.Text = "00";
            // 
            // UcDias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbldias);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "UcDias";
            this.Size = new System.Drawing.Size(133, 89);
            this.Load += new System.EventHandler(this.UcDias_Load);
            this.Click += new System.EventHandler(this.UcDias_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbldias;
    }
}
