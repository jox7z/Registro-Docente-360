using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Docente_360_2025
{
    public partial class Horario : Form
    {
        public Horario()
        {
            InitializeComponent();

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuPrincipal menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
            this.Hide();
        }


        private void Horario_Load(object sender, EventArgs e)
        {
            string[] horas =
            {
                "7:00 A 7:40", "7:40 A 8:20", "8:35 A 9:15", "9:15 A 9:55",
                "10:05 A 10:45", "10:45 A 11:25", "11:30 A 12:10",
                "12:30 A 1:10", "1:10 A 1:50", "2:00 A 2:40",
                "2:40 A 3:20", "3:35 A 4:15", "4:15 A 4:55", "5:00 A 5:40"
            };

            //visual
            dataGridHorario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridHorario.AllowUserToAddRows = false;
            dataGridHorario.ScrollBars = ScrollBars.None;
            dataGridHorario.AllowUserToResizeRows = false;
            dataGridHorario.AllowUserToResizeColumns = false;
            dataGridHorario.RowHeadersVisible = false;

            //limpiar filas por si acaso se vuelven a cargar
            dataGridHorario.Rows.Clear();

            for (int i = 0; i < horas.Length; i++)
            {
                dataGridHorario.Rows.Add(i + 1, horas[i]);
            }

            //linea para diferenciar el horario de la tarde
            for (int i = 7; i < dataGridHorario.Rows.Count; i++)
            {
                dataGridHorario.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
            }

            //Altura del datagrid
            int altoFila = dataGridHorario.RowTemplate.Height;
            int cantidadFilas = dataGridHorario.Rows.Count;
            dataGridHorario.Height = (altoFila * cantidadFilas) + dataGridHorario.ColumnHeadersHeight + 2;




        }

        bool modoEdicion = false;
        private void btnEditarhorario_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                dataGridHorario.ReadOnly = false;

                lblHorario.Text = "MODO EDICION ACTIVADO";
                lblHorario.ForeColor = Color.Black;
                dataGridHorario.Columns[0].ReadOnly = true;
                dataGridHorario.Columns[1].ReadOnly = true;

                // Cambiar color del fondo del formulario
                this.BackColor = Color.FromArgb(220, 250, 253);

                btnEditarhorario.Text = "GUARDAR HORARIO";
                toolTip1.SetToolTip(btnEditarhorario, "Haz clic para guardar los cambios");
                modoEdicion = true;
            }
            else
            {
                //logica para guardar el horario en la base de datos aqui chicos

                //**************************************************************
                lblHorario.Text = "Horario del Docente";
                lblHorario.ForeColor = Color.FromArgb(0, 192, 192);
                lblHorario.Font = new Font("Segoe UI", 21, FontStyle.Bold);
                this.BackColor = SystemColors.Control;
                dataGridHorario.ReadOnly = true;
                btnEditarhorario.Text = "EDITAR HORARIO";
                toolTip1.SetToolTip(btnEditarhorario, "Haz clic para editar el horario");
                modoEdicion = false;
            }


        }

        
    }
}
