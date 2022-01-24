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
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : Page
    {
        public Administrador()
        
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TabItemAdmin_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TabItemAdmin_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void tabMantInvetario_Loaded(object sender, RoutedEventArgs e)
        {
            InventarioFrame.NavigationService.Navigate(new MantenedorInventario());
        }

        private void tabMantMesas_Loaded(object sender, RoutedEventArgs e)
        {
            MesasFrame.NavigationService.Navigate(new MesasAdmin());
        }

        private void tabMantCliente_Loaded(object sender, RoutedEventArgs e)
        {
            ClienteFrame.NavigationService.Navigate(new AdmCliente());
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
