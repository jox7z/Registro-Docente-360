using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Text;

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
            dataGridHorario.CellValueChanged += dataGridHorario_CellValueChanged;
            dataGridHorario.CellEndEdit += dataGridHorario_CellEndEdit;
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

            //Bloquea la edicion, fuera del modo de edicion
            dataGridHorario.ReadOnly = true;
            txtSeccion.ReadOnly = true;

            //permite solo numeros
            txtSeccion.KeyPress += txtSeccion_KeyPress;


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

                txtSeccion.ReadOnly = false;
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

                txtSeccion.ReadOnly = true;
            }


        }

        //evento de click (lo abri sin querer)
        private void dataGridHorario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridHorario_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridHorario.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dataGridHorario.Invalidate(); //refresca visualmente
        }

        private void dataGridHorario_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 2) return;

            var celda = dataGridHorario.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string valor = celda.Value?.ToString().Trim().ToLower() ?? "";

            if (valor == "español")
                celda.Style.BackColor = Color.IndianRed;
            else if (valor == "matemáticas" || valor == "matematicas")
                celda.Style.BackColor = Color.Khaki;
            else if (valor == "ciencias")
                celda.Style.BackColor = Color.LightGreen;
            else if (valor == "inglés" || valor == "ingles")
                celda.Style.BackColor = Color.DodgerBlue;
            else if (valor == "est. sociales" || valor == "estudios sociales")
                celda.Style.BackColor = Color.DeepSkyBlue;
            else
                celda.Style.BackColor = Color.White;

        }

        private void txtSeccion_Enter(object sender, EventArgs e)
        {

            if (modoEdicion == false) return;

            if (txtSeccion.Text == "Inserte numero de seccion")
            {
                txtSeccion.Text = "";
                txtSeccion.ForeColor = Color.Black;
            }
        }
        private void txtSeccion_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSeccion.Text))
            {
                txtSeccion.Text = "Inserte numero de seccion";
            }
        }

        private void txtSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; //bloquea la tecla si no es numero 
            }

            if (e.KeyChar == '-' && (sender as TextBox).Text.Contains("-"))
            {
                e.Handled = true;  // Solo permite un guion (no más de uno)
            }

        }


    }
}
