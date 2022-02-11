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
        private int Contador = 0;

        public Restaurant.Core.Carrito car = new();
        Plato p = new();

        public MenuCliente()
        {
            InitializeComponent();
            ListPlato.ItemsSource = metN.ListarPlatos();

        }

        private void btnAddPlato_Click(object sender, RoutedEventArgs e)
        {
            var hola = Window.GetWindow(VisualParent);
            var frame = hola.Content as Grid;
            var hola2 = frame.Children[0] as Frame;
            var hola3 = hola2.Content as Cliente;
            Contador++;
            hola3.BadgeCarrito.Badge = Contador;
            Plato sexo = (Plato)(sender as Button).DataContext;
            car.AddPlato(p.ToPlatoCarrito(sexo));
            //Plato p = (sender as Card) as Plato;

            //car.AddPlato(p.ToPlatoCarrito(p));
        }
    }
}
