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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcDias));
            this.lbldias = new System.Windows.Forms.Label();
            this.lblevento = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
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
            // lblevento
            // 
            this.lblevento.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblevento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblevento.Location = new System.Drawing.Point(3, 50);
            this.lblevento.Name = "lblevento";
            this.lblevento.Size = new System.Drawing.Size(127, 35);
            this.lblevento.TabIndex = 1;
            this.lblevento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblevento.Click += new System.EventHandler(this.lblevento_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Transparent;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.Location = new System.Drawing.Point(108, 10);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(22, 21);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // UcDias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblevento);
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
        private System.Windows.Forms.Label lblevento;
        private System.Windows.Forms.Button btnEliminar;
    }
}
