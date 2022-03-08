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
            if (lvTablero.SelectedItem != null)
            {
                var TableroCambio = (Pedido)lvTablero.SelectedItem;
                Pedido pe = new();
                pe.IdPedido = Convert.ToInt32(TableroCambio.IdPedido);


                if (MessageBox.Show("¿Está seguro que el pedido está listo, verifiqué si está todo entregado por favor ?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (metN.CambioEsstadoPedidoTablero(pe))
                    {
                        MessageBox.Show("Se ha cambiado el estado del pedido");
                        Refresh();  
                    }
                    else
                    {
                        MessageBox.Show("No ha pasado nada");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione alguna ordenes");
            }
        }


        private void Refresh()
        {
            lvTablero.ItemsSource = null;
            lvTablero.ItemsSource = metN.GetPedido();

        }
    }
}
