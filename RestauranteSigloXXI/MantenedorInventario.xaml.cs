using Restaurant.Core;
using Restaurant.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using SelectionMode = System.Windows.Controls.SelectionMode;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para MantenedorInventario.xaml
    /// </summary>
    public partial class MantenedorInventario : Page
    {
        private MetodoNegocio MN = new();

        List<string> hola = new();

        public MantenedorInventario()
        {
            InitializeComponent();
   
            cbFilCategoria.ItemsSource = MN.GetCategoriaStrings();
            cbCategoria.ItemsSource = MN.GetCategoriaStrings();
        }




        private void lvInventario_Loaded(object sender, RoutedEventArgs e)
        {
            lvInventario.ItemsSource = MN.GetInsumoList();
        }

        private void cbFilCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (cbFilCategoria.SelectedIndex != 0)
            {

                lvInventario.ItemsSource = null;
                lvInventario.ItemsSource = MN.GetInsumoList().Where(s => s.IdCategoria == cbFilCategoria.SelectedIndex).ToList();
            }
            else
            {
                Refresh();
            }
            //} else if (cbCategoria.SelectedIndex.ToString() == "Todos"){
            //    Refresh();
            //}

        }

        private void cbFilCategoria_Loaded(object sender, RoutedEventArgs e)
        {
            
           
        }



    

        private void Refresh()
        {
            lvInventario.ItemsSource = null;
            lvInventario.ItemsSource = MN.GetInsumoList();
            cbFilCategoria.SelectedIndex = 0;
            
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            popBoxCrear.IsPopupOpen = false;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();  
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (SePuedeAgregarInsumo())
            {
                Insumo insu = new ();
                insu.nombreInsumo = txtNombreInsumo.Text;
                insu.IdCategoria = Convert.ToInt32(cbCategoria.SelectedIndex);
                insu.Precio = Convert.ToInt32(txtPrecio.Text);
                insu.Existencia = Convert.ToInt32(txtStock.Text);
                if (MN.CrearInsumo(insu))
                {
                    MessageBox.Show("Se agregó el insumo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Refresh();
                }
                else
                {
                    MessageBox.Show("no sé agregó el insumo", "Información", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
              
            }
        }


        private bool SePuedeAgregarInsumo()
        {
            if (txtNombreInsumo.Text == string.Empty)
            {
                MessageBox.Show("No se ha escrito el nombre del nuevo insumo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbCategoria.Text == string.Empty)
            {
                MessageBox.Show("No se ha selecionado ninguna categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtPrecio.Text == string.Empty)
            {
                MessageBox.Show("No se ha escrito ningún precio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (txtStock.Text == string.Empty)
            {
                MessageBox.Show("No se ha escrito el Stock", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                return true;
            }

            return false;
        }

        private void cbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
        }

        private void cbCategoria_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
