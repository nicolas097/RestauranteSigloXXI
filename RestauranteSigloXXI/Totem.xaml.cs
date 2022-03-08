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
    /// Lógica de interacción para Totem.xaml
    /// </summary>
    public partial class Totem : Page
    {
        public Totem()
        {
            InitializeComponent();
        }

      

        private void btnMesas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisponibilidadMesas());
        }

        private void btnReservas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TotemReserva());
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
