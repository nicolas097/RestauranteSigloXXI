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
using Restaurante.DB;
using Restaurant.Negocio;
using Restaurant.Core;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para TotemReserva.xaml
    /// </summary>
    public partial class TotemReserva : Page
    {
        MetodoNegocio metNeg = new MetodoNegocio(); 
        public TotemReserva()
        {
            InitializeComponent();
            lvReserva.ItemsSource = metNeg.GetReserva();
        }

        private void btnVolverTotem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnMesas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisponibilidadMesas());
        }
    }
}
