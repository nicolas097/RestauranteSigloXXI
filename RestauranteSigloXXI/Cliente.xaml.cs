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
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : Page
    {

        private Carrito car = new ();

        public Cliente()
        {
            InitializeComponent();
            FrameMenu.Navigate(new MenuCliente());
            
        }

        private void btnCarrito_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new PageCarrito(new Carrito()));
        }

       
       
    }
}
