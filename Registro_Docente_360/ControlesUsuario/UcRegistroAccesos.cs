using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelos.EntityFramework;

using Registro_Docente_360.Modelos;



namespace Registro_Docente_360.Forms
{
    public partial class UcRegistroAccesos : UserControl
    {
        public UcRegistroAccesos()
        {
            InitializeComponent();

            this.Load += UcRegistroAccesos_Load;
            this.Resize += (s, e) => CentrarMiniContenedor();
            dgAccesos.CellFormatting += dgAccesos_CellFormatting;

            CentrarMiniContenedor();
        }

        private void CentrarMiniContenedor()
        {
            int anchoContenedor = panelMiniContenedor.Width;
            int altoContenedor = panelMiniContenedor.Height;

            panelMiniContenedor.Left = (this.ClientSize.Width - anchoContenedor) / 2;
            panelMiniContenedor.Top = (this.ClientSize.Height - altoContenedor) / 2;

        }

        private void UcRegistroAccesos_Load(object sender, EventArgs e)
        {
            ID.DataPropertyName = "ID";
            Usuario.DataPropertyName = "Usuario";
            Ingreso.DataPropertyName = "Ingreso";
            Salida.DataPropertyName = "Salida";
            Resultado.DataPropertyName = "Resultado";

            CargarAccesos();
        }

        private void CargarAccesos()
        {
            string filtro = txtBuscarUsuario.Text.Trim().ToLower();

            using (var contexto = new RegistroDocenteEntities())
            {
                var accesos = contexto.Bitacora_Accesos
                    .Where(a => a.Usuarios != null)
                    .OrderBy(a => a.fecha_acceso)
                    .ToList();

                var sesiones = new List<SesionAcceso>();

                // 1. Agregar accesos exitosos (LOGIN + LOGOUT emparejados)
                foreach (var login in accesos.Where(a => a.tipo_acceso == "LOGIN"))
                {
                    var logout = accesos.FirstOrDefault(a =>
                        a.tipo_acceso == "LOGOUT" &&
                        a.id_usuario == login.id_usuario &&
                        a.fecha_acceso > login.fecha_acceso);

                    sesiones.Add(new SesionAcceso
                    {
                        ID = login.id_acceso,
                        Usuario = login.Usuarios.nombre_usuario,
                        Ingreso = login.fecha_acceso,
                        Salida = logout?.fecha_acceso,
                        Resultado = logout != null ? "Correcto" : "Sesión activa"
                    });
                }

                // 2. Agregar intentos fallidos como filas separadas
                foreach (var intentoFallido in accesos.Where(a => a.tipo_acceso == "FALLIDO"))
                {
                    sesiones.Add(new SesionAcceso
                    {
                        ID = intentoFallido.id_acceso,
                        Usuario = intentoFallido.Usuarios?.nombre_usuario ?? "Desconocido",
                        Ingreso = intentoFallido.fecha_acceso,
                        Salida = null,
                        Resultado = "Fallido"
                    });
                }

                // 3. Aplicar filtro si hay texto ingresado
                if (!string.IsNullOrEmpty(filtro))
                {
                    sesiones = sesiones
                        .Where(x => x.Usuario.ToLower().Contains(filtro))
                        .ToList();
                }

                // 4. Mostrar en el DataGridView
                dgAccesos.AutoGenerateColumns = false;
                dgAccesos.DataSource = sesiones.OrderByDescending(x => x.Ingreso).ToList();

                // 5. Mostrar total de sesiones
                lblTotal.Text = $"Total sesiones mostradas: {dgAccesos.RowCount}";
            }
        }

        private void dgAccesos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificamos si es la columna "Resultado" (nombre interno del DataGridViewColumn)
            if (dgAccesos.Columns[e.ColumnIndex].DataPropertyName == "Resultado")
            {
                string resultado = e.Value?.ToString();

                if (resultado == "Fallido")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (resultado == "Sesión activa")
                {
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (resultado == "Correcto")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Regular);
                }
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarAccesos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarUsuario.Text = "";
            CargarAccesos();
        }
    }
}
