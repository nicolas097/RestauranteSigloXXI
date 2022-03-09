using Restaurant.Negocio;
using System.Windows;
using System.Windows.Controls;
using Restaurante.DB;
using Restaurant.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para Oredenes.xaml
    /// </summary>
    public partial class Oredenes : Page
    {
        MetodoNegocio metN = new MetodoNegocio();
        public Oredenes()
        {
            InitializeComponent();
            lvTablero.ItemsSource = metN.GetPedido();



        }

        private void lvDetalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lvHola_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSalio_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }


        private void Refresh()
        {
            lvTablero.ItemsSource = null;
            lvTablero.ItemsSource = metN.GetPedido();

        }

        private void CambiarEstadoPedido(Pedido papapa)
        {
            var TableroCambio = papapa;
            Pedido pe = new();
            pe.IdPedido = Convert.ToInt32(TableroCambio.IdPedido);


            if (MessageBox.Show("¿Está seguro que el pedido está listo? Verifique si está todo preparado.", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (metN.CambioEsstadoPedidoTablero(pe))
                {
                    MessageBox.Show("Se ha cambiado el estado del pedido.");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("No se ha realizado ningún cambio.");
                }
            }
        }

        private void btnSalidaPedido_Click(object sender, RoutedEventArgs e)
        {

            Pedido pe = (Pedido)(sender as Button).DataContext; 
            CambiarEstadoPedido(pe);
        }
    }
}
