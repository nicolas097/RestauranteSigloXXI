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
    /// Lógica de interacción para PedidoActual.xaml
    /// </summary>
    public partial class PedidoActual : Page
    {
        List<PlatoCarrito> platoCarritos = new List<PlatoCarrito>();    
        public PedidoActual(Carrito car)
        {
            InitializeComponent();
            platoCarritos = car.GetCarritos();
            lvResumenCarrito.ItemsSource = platoCarritos;   
            
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
