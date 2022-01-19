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

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para MantenedorInventario.xaml
    /// </summary>
    public partial class MantenedorInventario : Page
    {
        private MetodoNegocio MN = new();

        public MantenedorInventario()
        {
            InitializeComponent();

            cbFilCategoria.ItemsSource = MN.GetCategoriaStrings();
            cbFilCategoria.SelectedIndex = 0;

        }

        private void lvInventario_Loaded(object sender, RoutedEventArgs e)
        {
             lvInventario.ItemsSource =  MN.GetInsumoList();
        }

        private void cbFilCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (cbFilCategoria.SelectedIndex != 0)
            {

                lvInventario.ItemsSource =  null;
                lvInventario.ItemsSource = MN.GetInsumoList().Where(s => s.IdCategoria == cbFilCategoria.SelectedIndex).ToList();

                
            }

        }

        private void cbFilCategoria_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh()
        {

        }
       
    }
}
