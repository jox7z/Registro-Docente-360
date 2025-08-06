namespace Registro_Docente_360.Forms
{
    partial class UcRolesyPermisos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcRolesyPermisos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMiniContenedor = new SiticoneNetFrameworkUI.SiticonePanel();
            this.panelContenidos = new System.Windows.Forms.FlowLayoutPanel();
            this.pnTitulo = new System.Windows.Forms.Panel();
            this.lblMantenimientoRoles = new System.Windows.Forms.Label();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.PanelAcciones = new System.Windows.Forms.TableLayoutPanel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.pnRolExistente = new System.Windows.Forms.Panel();
            this.lblLista = new System.Windows.Forms.Label();
            this.datagridRoles = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnPermisosyEstados = new System.Windows.Forms.Panel();
            this.lblEstadoRol = new System.Windows.Forms.Label();
            this.permisosCheckListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPermisosAsignados = new System.Windows.Forms.Label();
            this.chkAccesoModuloDocente = new System.Windows.Forms.CheckBox();
            this.chkAccesoModuloAdministrador = new System.Windows.Forms.CheckBox();
            this.chkModificarUsuarios = new System.Windows.Forms.CheckBox();
            this.chkAccederReportes = new System.Windows.Forms.CheckBox();
            this.chkAccederConfiguracion = new System.Windows.Forms.CheckBox();
            this.rbInactivo = new System.Windows.Forms.RadioButton();
            this.rbActivo = new System.Windows.Forms.RadioButton();
            this.txtNombreRol = new System.Windows.Forms.TextBox();
            this.lblEditarNombre = new System.Windows.Forms.Label();
            this.panelMiniContenedor.SuspendLayout();
            this.panelContenidos.SuspendLayout();
            this.pnTitulo.SuspendLayout();
            this.pnInfo.SuspendLayout();
            this.PanelAcciones.SuspendLayout();
            this.pnRolExistente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridRoles)).BeginInit();
            this.pnPermisosyEstados.SuspendLayout();
            this.permisosCheckListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMiniContenedor
            // 
            this.panelMiniContenedor.AcrylicTintColor = System.Drawing.Color.White;
            this.panelMiniContenedor.AutoSize = true;
            this.panelMiniContenedor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMiniContenedor.BackColor = System.Drawing.Color.Transparent;
            this.panelMiniContenedor.BorderAlignment = System.Drawing.Drawing2D.PenAlignment.Center;
            this.panelMiniContenedor.BorderDashPattern = null;
            this.panelMiniContenedor.BorderGradientEndColor = System.Drawing.Color.Purple;
            this.panelMiniContenedor.BorderGradientStartColor = System.Drawing.Color.Blue;
            this.panelMiniContenedor.BorderThickness = 2F;
            this.panelMiniContenedor.Controls.Add(this.panelContenidos);
            this.panelMiniContenedor.CornerRadiusBottomLeft = 9F;
            this.panelMiniContenedor.CornerRadiusBottomRight = 9F;
            this.panelMiniContenedor.CornerRadiusTopLeft = 9F;
            this.panelMiniContenedor.CornerRadiusTopRight = 9F;
            this.panelMiniContenedor.EnableAcrylicEffect = false;
            this.panelMiniContenedor.EnableMicaEffect = false;
            this.panelMiniContenedor.EnableRippleEffect = false;
            this.panelMiniContenedor.FillColor = System.Drawing.Color.White;
            this.panelMiniContenedor.GradientColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.LightGray,
        System.Drawing.Color.Gray};
            this.panelMiniContenedor.GradientPositions = new float[] {
        0F,
        0.5F,
        1F};
            this.panelMiniContenedor.Location = new System.Drawing.Point(6, 4);
            this.panelMiniContenedor.Name = "panelMiniContenedor";
            this.panelMiniContenedor.Padding = new System.Windows.Forms.Padding(7);
            this.panelMiniContenedor.PatternStyle = System.Drawing.Drawing2D.HatchStyle.LargeGrid;
            this.panelMiniContenedor.RippleAlpha = 50;
            this.panelMiniContenedor.RippleAlphaDecrement = 3;
            this.panelMiniContenedor.RippleColor = System.Drawing.Color.White;
            this.panelMiniContenedor.RippleMaxSize = 600F;
            this.panelMiniContenedor.RippleSpeed = 15F;
            this.panelMiniContenedor.ShowBorder = false;
            this.panelMiniContenedor.Size = new System.Drawing.Size(772, 673);
            this.panelMiniContenedor.TabIndex = 4;
            this.panelMiniContenedor.TabStop = true;
            this.panelMiniContenedor.TrackSystemTheme = false;
            this.panelMiniContenedor.UseBorderGradient = false;
            this.panelMiniContenedor.UseMultiGradient = false;
            this.panelMiniContenedor.UsePatternTexture = false;
            this.panelMiniContenedor.UseRadialGradient = false;
            // 
            // panelContenidos
            // 
            this.panelContenidos.AutoSize = true;
            this.panelContenidos.Controls.Add(this.pnTitulo);
            this.panelContenidos.Controls.Add(this.pnInfo);
            this.panelContenidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenidos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelContenidos.Location = new System.Drawing.Point(7, 7);
            this.panelContenidos.Name = "panelContenidos";
            this.panelContenidos.Size = new System.Drawing.Size(758, 659);
            this.panelContenidos.TabIndex = 1;
            this.panelContenidos.WrapContents = false;
            // 
            // pnTitulo
            // 
            this.pnTitulo.Controls.Add(this.lblMantenimientoRoles);
            this.pnTitulo.ForeColor = System.Drawing.Color.White;
            this.pnTitulo.Location = new System.Drawing.Point(3, 3);
            this.pnTitulo.Name = "pnTitulo";
            this.pnTitulo.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.pnTitulo.Size = new System.Drawing.Size(752, 44);
            this.pnTitulo.TabIndex = 0;
            // 
            // lblMantenimientoRoles
            // 
            this.lblMantenimientoRoles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMantenimientoRoles.AutoSize = true;
            this.lblMantenimientoRoles.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimientoRoles.ForeColor = System.Drawing.Color.Teal;
            this.lblMantenimientoRoles.Location = new System.Drawing.Point(220, 7);
            this.lblMantenimientoRoles.Name = "lblMantenimientoRoles";
            this.lblMantenimientoRoles.Size = new System.Drawing.Size(327, 25);
            this.lblMantenimientoRoles.TabIndex = 2;
            this.lblMantenimientoRoles.Text = "Mantenimiento de Roles y Permisos";
            // 
            // pnInfo
            // 
            this.pnInfo.Controls.Add(this.PanelAcciones);
            this.pnInfo.Controls.Add(this.pnRolExistente);
            this.pnInfo.Controls.Add(this.pnPermisosyEstados);
            this.pnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnInfo.Location = new System.Drawing.Point(3, 53);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(752, 603);
            this.pnInfo.TabIndex = 1;
            this.pnInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnInfo_Paint);
            // 
            // PanelAcciones
            // 
            this.PanelAcciones.BackColor = System.Drawing.Color.White;
            this.PanelAcciones.ColumnCount = 4;
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.PanelAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PanelAcciones.Controls.Add(this.btnAgregar, 0, 0);
            this.PanelAcciones.Controls.Add(this.btnModificar, 1, 0);
            this.PanelAcciones.Controls.Add(this.btnEliminar, 2, 0);
            this.PanelAcciones.Controls.Add(this.btnGuardar, 3, 0);
            this.PanelAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelAcciones.Location = new System.Drawing.Point(0, 506);
            this.PanelAcciones.Name = "PanelAcciones";
            this.PanelAcciones.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.PanelAcciones.RowCount = 1;
            this.PanelAcciones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelAcciones.Size = new System.Drawing.Size(752, 97);
            this.PanelAcciones.TabIndex = 30;
            this.PanelAcciones.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelAcciones_Paint);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAgregar.BackColor = System.Drawing.Color.Teal;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.Window;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(39, 28);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(124, 40);
            this.btnAgregar.TabIndex = 23;
            this.btnAgregar.Text = "        Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.UseVisualStyleBackColor = false;
            // 
            // btnModificar
            // 
            this.btnModificar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnModificar.BackColor = System.Drawing.Color.Teal;
            this.btnModificar.FlatAppearance.BorderSize = 0;
            this.btnModificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnModificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.ForeColor = System.Drawing.Color.White;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(222, 28);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(125, 40);
            this.btnModificar.TabIndex = 24;
            this.btnModificar.Text = "       Modificar";
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click_1);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEliminar.BackColor = System.Drawing.Color.Teal;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(408, 28);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(119, 40);
            this.btnEliminar.TabIndex = 25;
            this.btnEliminar.Text = "       Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardar.BackColor = System.Drawing.Color.Teal;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(586, 28);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(129, 40);
            this.btnGuardar.TabIndex = 27;
            this.btnGuardar.Text = "      Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click_1);
            // 
            // pnRolExistente
            // 
            this.pnRolExistente.Controls.Add(this.lblLista);
            this.pnRolExistente.Controls.Add(this.datagridRoles);
            this.pnRolExistente.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnRolExistente.Location = new System.Drawing.Point(0, 0);
            this.pnRolExistente.Name = "pnRolExistente";
            this.pnRolExistente.Size = new System.Drawing.Size(752, 225);
            this.pnRolExistente.TabIndex = 31;
            // 
            // lblLista
            // 
            this.lblLista.AutoSize = true;
            this.lblLista.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLista.ForeColor = System.Drawing.Color.Teal;
            this.lblLista.Location = new System.Drawing.Point(8, 0);
            this.lblLista.Name = "lblLista";
            this.lblLista.Size = new System.Drawing.Size(225, 25);
            this.lblLista.TabIndex = 3;
            this.lblLista.Text = "Lista de roles existentes:";
            // 
            // datagridRoles
            // 
            this.datagridRoles.AllowUserToAddRows = false;
            this.datagridRoles.AllowUserToDeleteRows = false;
            this.datagridRoles.AllowUserToResizeColumns = false;
            this.datagridRoles.AllowUserToResizeRows = false;
            this.datagridRoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridRoles.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.datagridRoles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridRoles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.datagridRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridRoles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.NombreRol,
            this.EstadoRol});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridRoles.DefaultCellStyle = dataGridViewCellStyle4;
            this.datagridRoles.EnableHeadersVisualStyles = false;
            this.datagridRoles.Location = new System.Drawing.Point(13, 28);
            this.datagridRoles.Name = "datagridRoles";
            this.datagridRoles.ReadOnly = true;
            this.datagridRoles.RowHeadersVisible = false;
            this.datagridRoles.Size = new System.Drawing.Size(725, 196);
            this.datagridRoles.TabIndex = 14;
            this.datagridRoles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridRoles_CellClick);
            this.datagridRoles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridRoles_CellContentClick);
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ID.FillWeight = 50F;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // NombreRol
            // 
            this.NombreRol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NombreRol.HeaderText = "Nombre del Rol";
            this.NombreRol.Name = "NombreRol";
            this.NombreRol.ReadOnly = true;
            // 
            // EstadoRol
            // 
            this.EstadoRol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EstadoRol.FillWeight = 50F;
            this.EstadoRol.HeaderText = "Estado";
            this.EstadoRol.Name = "EstadoRol";
            this.EstadoRol.ReadOnly = true;
            // 
            // pnPermisosyEstados
            // 
            this.pnPermisosyEstados.Controls.Add(this.lblEditarNombre);
            this.pnPermisosyEstados.Controls.Add(this.lblEstadoRol);
            this.pnPermisosyEstados.Controls.Add(this.permisosCheckListPanel);
            this.pnPermisosyEstados.Controls.Add(this.rbInactivo);
            this.pnPermisosyEstados.Controls.Add(this.rbActivo);
            this.pnPermisosyEstados.Controls.Add(this.txtNombreRol);
            this.pnPermisosyEstados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPermisosyEstados.Location = new System.Drawing.Point(0, 0);
            this.pnPermisosyEstados.Name = "pnPermisosyEstados";
            this.pnPermisosyEstados.Size = new System.Drawing.Size(752, 603);
            this.pnPermisosyEstados.TabIndex = 32;
            // 
            // lblEstadoRol
            // 
            this.lblEstadoRol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEstadoRol.AutoSize = true;
            this.lblEstadoRol.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoRol.ForeColor = System.Drawing.Color.Teal;
            this.lblEstadoRol.Location = new System.Drawing.Point(499, 235);
            this.lblEstadoRol.Name = "lblEstadoRol";
            this.lblEstadoRol.Size = new System.Drawing.Size(138, 25);
            this.lblEstadoRol.TabIndex = 29;
            this.lblEstadoRol.Text = "Estado del rol:";
            // 
            // permisosCheckListPanel
            // 
            this.permisosCheckListPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.permisosCheckListPanel.Controls.Add(this.lblPermisosAsignados);
            this.permisosCheckListPanel.Controls.Add(this.chkAccesoModuloDocente);
            this.permisosCheckListPanel.Controls.Add(this.chkAccesoModuloAdministrador);
            this.permisosCheckListPanel.Controls.Add(this.chkModificarUsuarios);
            this.permisosCheckListPanel.Controls.Add(this.chkAccederReportes);
            this.permisosCheckListPanel.Controls.Add(this.chkAccederConfiguracion);
            this.permisosCheckListPanel.Location = new System.Drawing.Point(5, 231);
            this.permisosCheckListPanel.Name = "permisosCheckListPanel";
            this.permisosCheckListPanel.Size = new System.Drawing.Size(488, 269);
            this.permisosCheckListPanel.TabIndex = 22;
            // 
            // lblPermisosAsignados
            // 
            this.lblPermisosAsignados.AutoSize = true;
            this.lblPermisosAsignados.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermisosAsignados.ForeColor = System.Drawing.Color.Teal;
            this.lblPermisosAsignados.Image = ((System.Drawing.Image)(resources.GetObject("lblPermisosAsignados.Image")));
            this.lblPermisosAsignados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPermisosAsignados.Location = new System.Drawing.Point(3, 0);
            this.lblPermisosAsignados.Name = "lblPermisosAsignados";
            this.lblPermisosAsignados.Size = new System.Drawing.Size(392, 25);
            this.lblPermisosAsignados.TabIndex = 15;
            this.lblPermisosAsignados.Text = "       Permisos asignados (al seleccionar rol):\r\n";
            // 
            // chkAccesoModuloDocente
            // 
            this.chkAccesoModuloDocente.AutoSize = true;
            this.chkAccesoModuloDocente.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.chkAccesoModuloDocente.ForeColor = System.Drawing.Color.Teal;
            this.chkAccesoModuloDocente.Location = new System.Drawing.Point(3, 28);
            this.chkAccesoModuloDocente.Name = "chkAccesoModuloDocente";
            this.chkAccesoModuloDocente.Size = new System.Drawing.Size(267, 29);
            this.chkAccesoModuloDocente.TabIndex = 17;
            this.chkAccesoModuloDocente.Text = " Acceso a Módulo Docente";
            this.chkAccesoModuloDocente.UseVisualStyleBackColor = true;
            // 
            // chkAccesoModuloAdministrador
            // 
            this.chkAccesoModuloAdministrador.AutoSize = true;
            this.chkAccesoModuloAdministrador.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.chkAccesoModuloAdministrador.ForeColor = System.Drawing.Color.Teal;
            this.chkAccesoModuloAdministrador.Location = new System.Drawing.Point(3, 63);
            this.chkAccesoModuloAdministrador.Name = "chkAccesoModuloAdministrador";
            this.chkAccesoModuloAdministrador.Size = new System.Drawing.Size(317, 29);
            this.chkAccesoModuloAdministrador.TabIndex = 18;
            this.chkAccesoModuloAdministrador.Text = "Acceso a Módulo Administrador";
            this.chkAccesoModuloAdministrador.UseVisualStyleBackColor = true;
            // 
            // chkModificarUsuarios
            // 
            this.chkModificarUsuarios.AutoSize = true;
            this.chkModificarUsuarios.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.chkModificarUsuarios.ForeColor = System.Drawing.Color.Teal;
            this.chkModificarUsuarios.Location = new System.Drawing.Point(3, 98);
            this.chkModificarUsuarios.Name = "chkModificarUsuarios";
            this.chkModificarUsuarios.Size = new System.Drawing.Size(259, 29);
            this.chkModificarUsuarios.TabIndex = 19;
            this.chkModificarUsuarios.Text = "Puede modificar Usuarios";
            this.chkModificarUsuarios.UseVisualStyleBackColor = true;
            // 
            // chkAccederReportes
            // 
            this.chkAccederReportes.AutoSize = true;
            this.chkAccederReportes.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.chkAccederReportes.ForeColor = System.Drawing.Color.Teal;
            this.chkAccederReportes.Location = new System.Drawing.Point(3, 133);
            this.chkAccederReportes.Name = "chkAccederReportes";
            this.chkAccederReportes.Size = new System.Drawing.Size(260, 29);
            this.chkAccederReportes.TabIndex = 20;
            this.chkAccederReportes.Text = "Puede acceder a Reportes";
            this.chkAccederReportes.UseVisualStyleBackColor = true;
            // 
            // chkAccederConfiguracion
            // 
            this.chkAccederConfiguracion.AutoSize = true;
            this.chkAccederConfiguracion.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.chkAccederConfiguracion.ForeColor = System.Drawing.Color.Teal;
            this.chkAccederConfiguracion.Location = new System.Drawing.Point(3, 168);
            this.chkAccederConfiguracion.Name = "chkAccederConfiguracion";
            this.chkAccederConfiguracion.Size = new System.Drawing.Size(308, 29);
            this.chkAccederConfiguracion.TabIndex = 21;
            this.chkAccederConfiguracion.Text = "Puede acceder a Configuración";
            this.chkAccederConfiguracion.UseVisualStyleBackColor = true;
            // 
            // rbInactivo
            // 
            this.rbInactivo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbInactivo.AutoSize = true;
            this.rbInactivo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.rbInactivo.ForeColor = System.Drawing.Color.Red;
            this.rbInactivo.Location = new System.Drawing.Point(643, 270);
            this.rbInactivo.Name = "rbInactivo";
            this.rbInactivo.Size = new System.Drawing.Size(101, 29);
            this.rbInactivo.TabIndex = 28;
            this.rbInactivo.TabStop = true;
            this.rbInactivo.Text = "Inactivo";
            this.rbInactivo.UseVisualStyleBackColor = true;
            // 
            // rbActivo
            // 
            this.rbActivo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbActivo.AutoSize = true;
            this.rbActivo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.rbActivo.ForeColor = System.Drawing.Color.DarkGreen;
            this.rbActivo.Location = new System.Drawing.Point(643, 235);
            this.rbActivo.Name = "rbActivo";
            this.rbActivo.Size = new System.Drawing.Size(86, 29);
            this.rbActivo.TabIndex = 27;
            this.rbActivo.TabStop = true;
            this.rbActivo.Text = "Activo";
            this.rbActivo.UseVisualStyleBackColor = true;
            // 
            // txtNombreRol
            // 
            this.txtNombreRol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombreRol.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreRol.Location = new System.Drawing.Point(504, 366);
            this.txtNombreRol.Name = "txtNombreRol";
            this.txtNombreRol.Size = new System.Drawing.Size(225, 25);
            this.txtNombreRol.TabIndex = 22;
            // 
            // lblEditarNombre
            // 
            this.lblEditarNombre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEditarNombre.AutoSize = true;
            this.lblEditarNombre.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditarNombre.ForeColor = System.Drawing.Color.Teal;
            this.lblEditarNombre.Location = new System.Drawing.Point(499, 329);
            this.lblEditarNombre.Name = "lblEditarNombre";
            this.lblEditarNombre.Size = new System.Drawing.Size(153, 25);
            this.lblEditarNombre.TabIndex = 30;
            this.lblEditarNombre.Text = "Nombre del rol:";
            // 
            // UcRolesyPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelMiniContenedor);
            this.Name = "UcRolesyPermisos";
            this.Size = new System.Drawing.Size(790, 689);
            this.panelMiniContenedor.ResumeLayout(false);
            this.panelMiniContenedor.PerformLayout();
            this.panelContenidos.ResumeLayout(false);
            this.pnTitulo.ResumeLayout(false);
            this.pnTitulo.PerformLayout();
            this.pnInfo.ResumeLayout(false);
            this.PanelAcciones.ResumeLayout(false);
            this.pnRolExistente.ResumeLayout(false);
            this.pnRolExistente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridRoles)).EndInit();
            this.pnPermisosyEstados.ResumeLayout(false);
            this.pnPermisosyEstados.PerformLayout();
            this.permisosCheckListPanel.ResumeLayout(false);
            this.permisosCheckListPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SiticoneNetFrameworkUI.SiticonePanel panelMiniContenedor;
        private System.Windows.Forms.FlowLayoutPanel panelContenidos;
        private System.Windows.Forms.Panel pnTitulo;
        private System.Windows.Forms.Label lblMantenimientoRoles;
        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.FlowLayoutPanel permisosCheckListPanel;
        private System.Windows.Forms.Label lblPermisosAsignados;
        private System.Windows.Forms.CheckBox chkAccesoModuloDocente;
        private System.Windows.Forms.CheckBox chkAccesoModuloAdministrador;
        private System.Windows.Forms.CheckBox chkModificarUsuarios;
        private System.Windows.Forms.CheckBox chkAccederReportes;
        private System.Windows.Forms.CheckBox chkAccederConfiguracion;
        private System.Windows.Forms.DataGridView datagridRoles;
        private System.Windows.Forms.Label lblLista;
        private System.Windows.Forms.RadioButton rbInactivo;
        private System.Windows.Forms.RadioButton rbActivo;
        private System.Windows.Forms.Label lblEstadoRol;
        private System.Windows.Forms.TableLayoutPanel PanelAcciones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel pnRolExistente;
        private System.Windows.Forms.Panel pnPermisosyEstados;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoRol;
        private System.Windows.Forms.Label lblEditarNombre;
        private System.Windows.Forms.TextBox txtNombreRol;
    }
}
