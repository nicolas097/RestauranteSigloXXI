using Restaurant.Negocio;
using System.Windows;
using System.Windows.Controls;
using Restaurant.Core;
using MaterialDesignThemes.Wpf;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para MenuCliente.xaml
    /// </summary>
    public partial class MenuCliente : Page
    {
        private MetodoNegocio metN = new();


        public Carrito car = new();
        Plato p = new();

        public MenuCliente()
        {
            InitializeComponent();
            ListPlato.ItemsSource = metN.ListarPlatos();

        }

        private void btnAddPlato_Click(object sender, RoutedEventArgs e)
        {

            var CurrentWindow = Window.GetWindow(VisualParent);
            var CurrentPageGrid = CurrentWindow.Content as Grid;
            var FirstGridElement = CurrentPageGrid.Children[0] as Frame;
            var ClientPage = FirstGridElement.Content as Cliente;

            Plato listaCarrito = (Plato)(sender as Button).DataContext;

            car.AddPlato(p.ToPlatoCarrito(listaCarrito));
            ClientPage.BadgeCarrito.Badge = car.GetPCCount();
            //Plato p = (sender as Card) as Plato;
           
            //car.AddPlato(p.ToPlatoCarrito(p));
        }
    }
}
