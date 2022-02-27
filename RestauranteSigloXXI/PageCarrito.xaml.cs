using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Restaurant.Core;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para Carrito.xaml
    /// </summary>
    public partial class PageCarrito : Page
    {

        public Carrito CurrentCarrito;
        public ResumenCarrito CurrentResumen = new();




        public PageCarrito(Carrito ca)
        {
            InitializeComponent();
            CurrentCarrito = ca;
            CurrentResumen._carrito = CurrentCarrito;
            lvCarrrito.ItemsSource = CurrentCarrito.GetCarritos();


            UpdateRC();
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
           
        }

        private void CantidadSpinner_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlatoCarrito PlatoCambiado = (sender as IntegerUpDown).DataContext as PlatoCarrito;

            if ((sender as IntegerUpDown).Value != 0)
            {
                bool IsThere = CurrentCarrito.platos.Exists(x => x == PlatoCambiado);

                if (IsThere)
                {
                    CurrentCarrito.platos = CurrentCarrito.platos.Select(x =>
                    {
                        if (x.IdPlatoCarrito == PlatoCambiado.IdPlatoCarrito)
                        {
                            x.Cantidad = PlatoCambiado.Cantidad;
                        }
                        return x;
                    }
                    ).ToList();
                    UpdateRC();
                }
            }
            else
            {
                EliminarItemCarrito(PlatoCambiado);
            }
        }


        //método que actualiza lo items en el carrito

        private void UpdateRC()
        {
            TotalPrecio.Content = CurrentResumen.Total;
            CantidadItems.Content = CurrentCarrito.GetPCCount();
            valorSubtotal.Content = CurrentResumen.Subtotal;
            valorIVA.Content = CurrentResumen.SubtotalIVA;
        }

        private void BtnBorrarItem_Click(object sender, RoutedEventArgs e)
        {
            PlatoCarrito papu = (sender as Button).DataContext as PlatoCarrito;
            EliminarItemCarrito(papu);



        }


        public void EliminarItemCarrito(PlatoCarrito pa)
        {

            CurrentCarrito.platos.Remove(pa);

            refresh();

            UpdateRC();


        }

        public void refresh()
        {
            lvCarrrito.ItemsSource = null;
            lvCarrrito.ItemsSource = CurrentCarrito.GetCarritos();

        }

    }
}
