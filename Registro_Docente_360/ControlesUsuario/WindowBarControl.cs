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
using Registro_Docente_360.Eventos;

namespace Registro_Docente_360.ControlesUsuario
{
    public partial class WindowBarControl : UserControl
    {
        public WindowBarControl()
        {
            InitializeComponent();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            if (parent != null)
            {
                if (parent.WindowState == FormWindowState.Maximized)
                {
                    parent.WindowState = FormWindowState.Normal;
                }
                else
                {
                    parent.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // Mostrar la alerta de confirmación antes de cerrar la aplicación
            DialogResult result = MessageBox.Show("¿Seguro que quieres salir?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Si el usuario selecciona Sí, cerrar la aplicación y registrar el cierre
                Application.Exit();
                RegistrarAcceso(Sesion.IdUsuario, "LOGOUT");
            }
            else
            {
                // Si el usuario selecciona No, no hacer nada
                return;
            }
        }


        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            if (parent != null)
                parent.WindowState = FormWindowState.Minimized;
        }

        private void RegistrarAcceso(int idUsuario, string tipoAcceso)
        {
            using (var contexto = new RegistroDocenteEntities())
            {
                var acceso = new Bitacora_Accesos
                {
                    id_usuario = idUsuario,
                    tipo_acceso = tipoAcceso,
                    fecha_acceso = DateTime.Now
                };

                contexto.Bitacora_Accesos.Add(acceso);
                contexto.SaveChanges();
            }
        }

    }
}
