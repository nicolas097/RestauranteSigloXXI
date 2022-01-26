using Restaurant.Core;
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
    /// Lógica de interacción para Bodega.xaml
    /// </summary>
    public partial class Bodega : Page
    {
        public Usuario user;
        public Bodega(Usuario usuario)
        {
            InitializeComponent();
            user = usuario; 
        }



     

        private void TabItemBodega_Loaded(object sender, RoutedEventArgs e)
        {
            ReponerInsumoFrame.NavigationService.Navigate(new ReponerInsumos(new Usuario() { NombreUsuario = user.NombreUsuario })); 
        }

        private void TabItemBodega_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

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
