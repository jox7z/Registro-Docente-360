using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.Eventos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Docente_360.Forms
{
    public partial class FormEvento : Form
    {
        
        public FormEvento()
        {
            InitializeComponent();
            this.Load += FormEvento_Load;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 3 + 250
            );
            this.WindowState = FormWindowState.Normal;
            // Se ejecuta cuando el formulario termina de cargarse
        }

        // Carga inicial del formulario
        private void FormEvento_Load(object sender, EventArgs e)
        {
            // Se establece la fecha actual seleccionada en formato M/d/yyyy
            txtFecha.Text = $"{UcCalendario.static_month}/{UcDias.static_day}/{UcCalendario.static_year}";
        }

        // Evento que se dispara al presionar el botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación: no se permite guardar si el campo está vacío o solo tiene espacios
            if (string.IsNullOrWhiteSpace(txtEvento.Text))
            {
                MessageBox.Show("Por favor, escribe un evento.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Se convierte el texto de la fecha a un objeto DateTime
            DateTime fecha = DateTime.ParseExact(txtFecha.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            // Conexión a la base de datos
            using (var db = new RegistroDocenteEntities())
            {
                // Se busca si ya existe un evento para esa fecha y ese usuario
                var evento = db.Calendario.FirstOrDefault(ev =>
                    DbFunctions.TruncateTime(ev.fecha_evento) == fecha.Date &&
                    ev.id_usuario == Sesion.IdUsuario);

                // Si ya existe un evento, se solicita confirmación para sobrescribirlo
                if (evento != null)
                {
                    if (MessageBox.Show("Ya existe un evento para esta fecha. ¿Deseas sobrescribirlo?",
                                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        evento.descripcion_evento = txtEvento.Text; // Se actualiza el texto del evento
                    else return; // Si el usuario no desea sobrescribir, se cancela la operación
                }
                else
                {
                    // Si no existe evento previo, se crea uno nuevo
                    db.Calendario.Add(new Calendario
                    {
                        id_usuario = Sesion.IdUsuario,
                        descripcion_evento = txtEvento.Text,
                        fecha_evento = fecha
                    });
                }

                // Se guardan los cambios en la base de datos
                db.SaveChanges();

                string descripcion = $"Nuevo evento : {txtEvento.Text}";
                string accion = "Nuevo evento";
                string modulo = "Calendario";

                AlumnoController controlador = new AlumnoController();
                controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
            }

            // Se muestra mensaje de éxito y se cierra el formulario
            MessageBox.Show("Evento guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        // Propiedad pública para precargar la descripción del evento en el TextBox
        public string DescripcionExistente
        {
            get => txtEvento.Text; // Al obtener, devuelve el texto actual del TextBox
            set => txtEvento.Text = value; // Al asignar, establece el valor en el TextBox
        }

      
        private void FormEvento_Click(object sender, EventArgs e)
        {
        }
    }
}
