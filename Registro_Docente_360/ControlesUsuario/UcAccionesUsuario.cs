using Modelos.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Registro_Docente_360.Forms
{
    public partial class UcAccionesUsuario : UserControl
    {
        public UcAccionesUsuario()
        {
            InitializeComponent();
            this.Load += UcAccionesUsuario_Load;
            this.Resize += (s, e) => CentrarMiniContenedor();
            CentrarMiniContenedor();
        }

        private void CentrarMiniContenedor()
        {
            int anchoContenedor = panelMiniContenedor.Width;
            int altoContenedor = panelMiniContenedor.Height;

            panelMiniContenedor.Left = (this.ClientSize.Width - anchoContenedor) / 2;
            panelMiniContenedor.Top = (this.ClientSize.Height - altoContenedor) / 2;
        }

        private void UcAccionesUsuario_Load(object sender, EventArgs e)
        {
            dgMovimientos.Columns.Clear();

            dgMovimientos.Columns.Add("colUsuario", "Usuario");
            dgMovimientos.Columns["colUsuario"].DataPropertyName = "Usuario";

            dgMovimientos.Columns.Add("colAccion", "Acción");
            dgMovimientos.Columns["colAccion"].DataPropertyName = "Accion";

            dgMovimientos.Columns.Add("colDescripcion", "Descripción");
            dgMovimientos.Columns["colDescripcion"].DataPropertyName = "Descripcion";

            dgMovimientos.Columns.Add("colFecha", "Fecha/Hora");
            dgMovimientos.Columns["colFecha"].DataPropertyName = "FechaHora";

            dgMovimientos.Columns.Add("colModulo", "Módulo");
            dgMovimientos.Columns["colModulo"].DataPropertyName = "Modulo";

            dgMovimientos.ReadOnly = true;
            dgMovimientos.AllowUserToAddRows = false;
            dgMovimientos.AllowUserToDeleteRows = false;
            dgMovimientos.AutoGenerateColumns = false;

            CargarMovimientos();
        }

        

        private void CargarMovimientos()
        {
            string filtro = txtBuscarUsuario.Text.Trim().ToLower();

            using (var contexto = new RegistroDocenteEntities())
            {
                // Traemos los movimientos filtrados por usuario directamente desde la base de datos
                var movimientos = contexto.Bitacora_Movimientos
                    .Include(m => m.Usuarios) // Aseguramos que cargue los usuarios
                    .Where(m => string.IsNullOrEmpty(filtro) || m.Usuarios.nombre_usuario.ToLower().Contains(filtro)) // Filtramos por el nombre de usuario
                    .OrderByDescending(m => m.fecha_hora) // Ordenamos por fecha de la más reciente a la más antigua
                    .ToList(); // Cargamos los datos a memoria

                // Convertimos los datos a una lista de objetos MovimientoUsuario
                var lista = movimientos
                    .Select(m => new MovimientoUsuario
                    {
                        ID = m.id_movimiento,
                        Usuario = m.Usuarios?.nombre_usuario ?? "Desconocido", // Si el usuario es null, mostrar "Desconocido"
                        Accion = m.accion,
                        Descripcion = m.descripcion,
                        FechaHora = m.fecha_hora,
                        Modulo = m.modulo
                    })
                    .ToList();

                // Asignamos la lista al DataGridView
                dgMovimientos.DataSource = lista;

            }
        }





        // Clase auxiliar para mostrar los datos en el grid
        public class MovimientoUsuario
        {
            public int ID { get; set; }
            public string Usuario { get; set; }
            public string Accion { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaHora { get; set; }
            public string Modulo { get; set; }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarMovimientos();
        }

        private void UcAccionesUsuario_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                CargarMovimientos();
            }
        }

    }
}
