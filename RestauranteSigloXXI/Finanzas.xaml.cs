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
    /// Lógica de interacción para Finanzas.xaml
    /// </summary>
    public partial class Finanzas : Page
    {
        public Finanzas()
        {
            InitializeComponent();
        }

        private void TabItemInicio_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void tabCuentas_Loaded(object sender, RoutedEventArgs e)
        {
            CuentaFrame.NavigationService.Navigate(new PageCuentas());
        }

        private void tabContabilidad_Loaded(object sender, RoutedEventArgs e)
        {
            ContabilidadFrame.NavigationService.Navigate(new PageContabilidad());
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
