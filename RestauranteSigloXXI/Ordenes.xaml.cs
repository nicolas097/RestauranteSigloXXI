using Restaurant.Negocio;
using System.Windows.Controls;

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

       
        
    }
}
