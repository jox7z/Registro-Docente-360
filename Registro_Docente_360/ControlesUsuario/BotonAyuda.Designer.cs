namespace Registro_Docente_360.Forms
{
    partial class BotonAyuda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BotonAyuda));
            this.pictureAyuda = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAyuda)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureAyuda
            // 
            this.pictureAyuda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureAyuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureAyuda.Image = ((System.Drawing.Image)(resources.GetObject("pictureAyuda.Image")));
            this.pictureAyuda.Location = new System.Drawing.Point(6, 3);
            this.pictureAyuda.Name = "pictureAyuda";
            this.pictureAyuda.Size = new System.Drawing.Size(39, 39);
            this.pictureAyuda.TabIndex = 1;
            this.pictureAyuda.TabStop = false;
            this.pictureAyuda.Click += new System.EventHandler(this.pictureAyuda_Click);
            // 
            // BotonAyuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.pictureAyuda);
            this.Name = "BotonAyuda";
            this.Size = new System.Drawing.Size(52, 45);
            ((System.ComponentModel.ISupportInitialize)(this.pictureAyuda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureAyuda;
    }
}
