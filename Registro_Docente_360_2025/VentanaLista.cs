using System;
using System.Windows.Forms;

namespace Registro_Docente_360_2025
{
    public partial class Ventana_de_Lista : Form
    {
        public Ventana_de_Lista()
        {
            InitializeComponent();
        }

        
        private void Ventana_de_Lista_Load(object sender, EventArgs e)
        {
            
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            
            datagridLista.Columns.Clear();
            datagridLista.Rows.Clear();



            // Crear las columnas ComboBox para los días de la semana
            DataGridViewComboBoxColumn lunesColumn = new DataGridViewComboBoxColumn();
            lunesColumn.Name = "ColumLunes"; 
            lunesColumn.HeaderText = "Lunes\nFebrero 3";
            lunesColumn.Items.AddRange("Presente", "Ausente", "Justificado");

            DataGridViewComboBoxColumn martesColumn = new DataGridViewComboBoxColumn();
            martesColumn.Name = "ColumMartes"; 
            martesColumn.HeaderText = "Martes\nFebrero 4";
            martesColumn.Items.AddRange("Presente", "Ausente", "Justificado");

            DataGridViewComboBoxColumn miercolesColumn = new DataGridViewComboBoxColumn();
            miercolesColumn.Name = "ColumMiercoles"; 
            miercolesColumn.HeaderText = "Miércoles\nFebrero 5";
            miercolesColumn.Items.AddRange("Presente", "Ausente", "Justificado");

            DataGridViewComboBoxColumn juevesColumn = new DataGridViewComboBoxColumn();
            juevesColumn.Name = "ColumJueves"; 
            juevesColumn.HeaderText = "Jueves\nFebrero 6";
            juevesColumn.Items.AddRange("Presente", "Ausente", "Justificado");

            DataGridViewComboBoxColumn viernesColumn = new DataGridViewComboBoxColumn();
            viernesColumn.Name = "ColumViernes"; 
            viernesColumn.HeaderText = "Viernes\nFebrero 7";
            viernesColumn.Items.AddRange("Presente", "Ausente", "Justificado");

            // Agregar las columnas al DataGridView 
            datagridLista.Columns.Add("ColumEstudiante", "Nombre Estudiante"); 
            datagridLista.Columns.Add(lunesColumn);
            datagridLista.Columns.Add(martesColumn);
            datagridLista.Columns.Add(miercolesColumn);
            datagridLista.Columns.Add(juevesColumn);
            datagridLista.Columns.Add(viernesColumn);

            // Agregar filas dinámicamente (ejemplo)
            datagridLista.Rows.Add("Estudiante 1", "Presente", "Presente", "Ausente", "Justificado", "Presente");
            datagridLista.Rows.Add("Estudiante 2", "Justificado", "Presente", "Presente", "Ausente", "Ausente");




        }


        //EN PROCESOOOOOOOOOOOOOOOOOO (no funciona)
        // Evento de clic del botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Lógica para guardar los datos aquí..........
            MessageBox.Show("¡Asistencia guardada!");
        }


        //EN PROCESOOOOOOOOOOOOOOOOOO (no funciona)
        // Cerrar el formulario y verificar si el usuario desea guardar
        private void Ventana_de_Lista_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("¿Deseas salir sin guardar?", "Cambios no guardados", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;  
            }
        }


        //EN PROCESOOOOOOOOOOOOOOOOOO (no funciona)
        // Alerta de cambios no guardados al intentar cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Deseas salir sin guardar?", "Cambios no guardados", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close(); 
            }
        }
    }
}
