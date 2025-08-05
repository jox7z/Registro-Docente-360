using Modelos.EntityFramework;
using Registro_Docente_360.Controladores;
using Registro_Docente_360.ControlesUsuario;
using Registro_Docente_360.Eventos;
using Registro_Docente_360.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Docente_360
{
    public partial class UcDias : UserControl
    {
        // Variable estática para compartir el día seleccionado entre componentes
        public static string static_day;

        public UcDias()
        {
            InitializeComponent();
        }

        // Se ejecuta al cargar el control
        private void UcDias_Load(object sender, EventArgs e)
        {
            // Evita múltiples asignaciones al evento Click
            btnEliminar.Click -= btnEliminar_Click;
            btnEliminar.Click += btnEliminar_Click;
        }

        // Asigna el número del día al label correspondiente
        public void days(int numday)
        {
            lbldias.Text = numday + "";
        }

        // Muestra la descripción del evento en el label del evento
        public void MostrarEvento(string descripcion)
        {
            lblevento.Text = descripcion;
        }

        // Evento al hacer clic sobre el día en el calendario
        private void UcDias_Click(object sender, EventArgs e)
        {
            static_day = lbldias.Text;

            // Obtener descripción actual (si existe)
            string descripcion = lblevento.Text;

            // Abrir formulario para crear o editar evento
            FormEvento formEvento = new FormEvento();
            formEvento.DescripcionExistente = descripcion; // Prellenar con evento actual

            formEvento.ShowDialog(); // Mostrar la ventana emergente de agregar evento

            // Actualizar visualmente el evento luego de cerrar el formulario
            displayEvent();
        }

        // Busca y muestra el evento del día actual (si existe), y ajusta estilo visual
        public void displayEvent()
        {
            // Construye la fecha a partir del año, mes y día
            string fecha = UcCalendario.static_year + "-" + UcCalendario.static_month + "-" + lbldias.Text;

            // Validar que la fecha esté en formato correcto el tryparse hace la conversion en todo caso
            if (!DateTime.TryParse(fecha, out DateTime fechaSeleccionada))
            {
                return;
            }

            using (var contexto = new RegistroDocenteEntities())
            {
                // Busca evento que coincida exactamente con la fecha (ignorando hora)
                var evento = contexto.Calendario
                    .FirstOrDefault(e => DbFunctions.TruncateTime(e.fecha_evento) == fechaSeleccionada.Date);

                if (evento != null)
                {
                    // Si se encuentra evento, mostrar su descripción y cambiar fondo
                    lblevento.Text = evento.descripcion_evento;
                    this.BackColor = Color.FromArgb(255, 230, 250, 245);
                    btnEliminar.Visible = true;
                }
                else
                {
                    // Si no hay evento, limpiar visuales
                    lblevento.Text = "";
                    this.BackColor = Color.White;
                    btnEliminar.Visible = false;
                }
            }
        }

       
        // Evento de clic en el botón de eliminar evento
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Confirmación visual al usuario
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este evento?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Construir fecha actual
                DateTime fecha = new DateTime(
                    UcCalendario.static_year,
                    UcCalendario.static_month,
                    int.Parse(lbldias.Text));

                using (var contexto = new RegistroDocenteEntities())
                {
                    // Buscar evento por fecha exacta (ignorando hora)
                    var evento = contexto.Calendario
                        .FirstOrDefault(ev => DbFunctions.TruncateTime(ev.fecha_evento) == fecha.Date);

                    if (evento != null)
                    {
                        // Eliminar evento de la base de datos
                        contexto.Calendario.Remove(evento);
                        contexto.SaveChanges();

                        // Limpiar interfaz luego de eliminar
                        lblevento.Text = "";
                        btnEliminar.Visible = false;
                        this.BackColor = Color.White;

                        // Notificar éxito
                        MessageBox.Show("Evento eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string descripcion = $"Eliminacion evento: {evento.descripcion_evento}";
                        string accion = "Eliminacion evento";
                        string modulo = "Calendario";

                        AlumnoController controlador = new AlumnoController();
                        controlador.RegistrarMovimiento(Sesion.IdUsuario, accion, descripcion, modulo);
                    }
                }
            }
        }

        private void lblevento_Click(object sender, EventArgs e) { }
    }
}
