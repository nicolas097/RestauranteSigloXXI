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
using Restaurant.Negocio;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para PedidoActual.xaml
    /// </summary>
    public partial class PedidoActual : Page
    {
        List<PlatoCarrito> platoCarritos = new List<PlatoCarrito>();    

        Carrito actualCarrito;


        MetodoNegocio metNeg = new();
        

        public PedidoActual(Carrito car)
        {
            InitializeComponent();
            actualCarrito = car;
            platoCarritos = actualCarrito.GetCarritos();
            lvResumenCarrito.ItemsSource = platoCarritos;
            lbSubtotal.Content = actualCarrito.GetCarritoNeto();
            
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnConfirmarPedido_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("¿Está seguro de realizar el pedido?", "Información", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                //Realiza el pedido y mete datos en la base de datos
                NegocioEspecifico ne = new(actualCarrito);
                if (ne.IngresarCarritoDB())
                {
                    MessageBox.Show("Se ha ingresado el pedido exitosamente, se volverá a la selección de platos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new Cliente(new Mesa() { IdMesa = actualCarrito.MesaID}));
                }
                else
                {
                    MessageBox.Show("No se ha podido ingresar el pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void BtnPago_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
