using RegistroDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RegistroDetalle.BLL;
using Microsoft.EntityFrameworkCore;

namespace RegistroDetalle.UI.Registro
{
    
    public partial class RPersonas : Window
    {
        public List<TelefonosDetalle> Detalle { get; set; }
        public RPersonas()
        {
            InitializeComponent();
            this.Detalle = new List<TelefonosDetalle>();
        }

        private void Limpiar()
        {
            IDTextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            FechaDataPicker.Text = Convert.ToString(DateTime.Now);

            this.Detalle = new List<TelefonosDetalle>();
            CargarGrid();
        }

        private Personas LlenaClases()
        {
            Personas persona = new Personas();
            if (string.IsNullOrWhiteSpace(IDTextBox.Text))
            {
                IDTextBox.Text = "0";
            }
            else persona.PersonaId = Convert.ToInt32(IDTextBox.Text);
            persona.Nombre = NombreTextBox.Text;
            persona.Cedula = CedulaTextBox.Text;
            persona.Direccion = DireccionTextBox.Text;
            persona.FechaNacimiento = Convert.ToDateTime(FechaDataPicker.SelectedDate);
            
            persona.Telefonos = this.Detalle;
            return persona;
        }

        private void LlenaCampos(Personas persona)
        {
            IDTextBox.Text = Convert.ToString(persona.PersonaId);
            NombreTextBox.Text = persona.Nombre;
            CedulaTextBox.Text = persona.Cedula;
            DireccionTextBox.Text = persona.Direccion;
            FechaDataPicker.SelectedDate = persona.FechaNacimiento;

            this.Detalle = persona.Telefonos;
            CargarGrid();
        }
        private void CargarGrid()
        {
            CargarDataGrid.ItemsSource = null;
            CargarDataGrid.ItemsSource = this.Detalle;
        }

        private bool Validar()
        {
            bool paso = true;


            if (NombreTextBox.Text == string.Empty)
            {
                MessageBox.Show(NombreTextBox.Text, "No puede estar vacio");
                NombreTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(DireccionTextBox.Text))
            {
                MessageBox.Show(DireccionTextBox.Text, "No puede estar vacio");
                DireccionTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text))
            {
                MessageBox.Show(CedulaTextBox.Text, "No puede estar vacio");
                CedulaTextBox.Focus();
                paso = false;
            }

            if (this.Detalle.Count == 0)
            {
                //MessageBox.Show(CargarDataGrid.ItemsSource,"Debe agregar algun telefono");
                TelefonoTextBox.Focus();
                paso = false;
            }

            return paso;
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas persona;
            bool paso = false;

            if (!Validar())
                return;

            persona = LlenaClases();

            if (string.IsNullOrWhiteSpace(IDTextBox.Text) || IDTextBox.Text == "0")
                paso = PersonasBll.Guardar(persona);
            else
            {
                if (!ExisteBD())
                {
                    MessageBox.Show("No Se puede Modificar porque no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonasBll.Modificar(persona);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
               MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            id = Convert.ToInt32(IDTextBox.Text);

            Limpiar();

            if (PersonasBll.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
               MessageBox.Show(IDTextBox.Text, "No se puede eliminar una persona que no existe");
        }



        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas persona = new Personas();
            int.TryParse(IDTextBox.Text, out id);

            Limpiar();

            persona = PersonasBll.Buscar(id);

            if (persona != null)
            {
                
                LlenaCampos(persona);
            }
            else
            {
                MessageBox.Show("Persona no Encontrada");
            }
        }

        private bool ExisteBD()
        {
            Personas persona = PersonasBll.Buscar(Convert.ToInt32(IDTextBox.Text));

            return (persona != null);
        }
        

       
            

        private void AgregarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (CargarDataGrid.ItemsSource != null)
                this.Detalle = (List<TelefonosDetalle>)CargarDataGrid.ItemsSource;

            this.Detalle.Add(new TelefonosDetalle {
                Telefono= TelefonoTextBox.Text,
                TipoTelefono= TipoTextBox.Text

                });

            CargarGrid();
            TelefonoTextBox.Focus();
            TelefonoTextBox.Clear();
            TipoTextBox.Clear();
        }

        private void RemoverButton_Click_1(object sender, RoutedEventArgs e)
        {
            Detalle.RemoveAt(CargarDataGrid.FrozenColumnCount);
            CargarGrid();
        }
    }
}
