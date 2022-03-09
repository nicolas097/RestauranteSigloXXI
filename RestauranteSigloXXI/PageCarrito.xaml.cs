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
using MessageBox = System.Windows.MessageBox;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para Carrito.xaml
    /// </summary>
    public partial class PageCarrito : Page
    {

        public Carrito CurrentCarrito;
        public Mesa CurrentMesa;
        public ResumenCarrito CurrentResumen = new();




        public PageCarrito(Carrito ca, Mesa mes)
        {
            InitializeComponent();
            CurrentCarrito = ca;
            CurrentMesa = mes;
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
            CantidadItems.Content = CurrentCarrito.GetCarritoCount();
            valorSubtotal.Content = CurrentResumen.Subtotal;
            valorIVA.Content = CurrentResumen.SubtotalIVA;

            if (Convert.ToInt32(CantidadItems.Content) < 1)
            {
                MessageBox.Show("El carrito está vacío, se volverá a la pantalla de selección de platos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                NavigationService.Navigate(new Cliente(CurrentMesa));
            }
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



        public void IngresarDetPedido()
        {
            
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
           
            UpdateRC();
            refresh();
            NavigationService.Navigate(new PedidoActual(CurrentCarrito));
        }
    }
}
