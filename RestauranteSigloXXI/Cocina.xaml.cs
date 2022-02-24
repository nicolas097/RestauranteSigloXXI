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
    /// Lógica de interacción para Cocina.xaml
    /// </summary>
    public partial class Cocina : Page
    {
        public Cocina()
        {
            InitializeComponent();
        }

        private void tabReceta_Loaded(object sender, RoutedEventArgs e)
        {
            RecetaFrame.NavigationService.Navigate(new Receta());
        }

        private void tabOrdenes_Loaded(object sender, RoutedEventArgs e)
        {
            OrdenesFrame.NavigationService.Navigate(new Oredenes());
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LoginGeneral lg = new LoginGeneral();
                NavigationService.Navigate(lg);
            }
        }
    }
}
