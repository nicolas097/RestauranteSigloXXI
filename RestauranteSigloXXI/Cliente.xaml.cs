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

            var CurrentWindow = Window.GetWindow(VisualParent);
            var CurrentPageGrid = CurrentWindow.Content as Grid;
            var FirstGridElement = CurrentPageGrid.Children[0] as Frame;

            var NoSeSabe = FirstGridElement.Content;

            //Grid -> Segundo StackPanel -> FrameMenu

            var PageCliente = NoSeSabe as Cliente;

            var ContenidoFrameMenu = PageCliente.FrameMenu.Content as MenuCliente;

            if (ContenidoFrameMenu.car.GetPCCount() != 0)
            {
                NavigationService.Navigate(new PageCarrito(ContenidoFrameMenu.car));

            }
            else
            {
                NavigationService.Navigate(new PageCarrito(new Carrito()));

            }



        }



    }
}
