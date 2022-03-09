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

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para SeleccionMesas.xaml
    /// </summary>
    public partial class SeleccionMesas : Page
    {
        private MetodoMesa metodoMesa = new();

        public SeleccionMesas()
        {
            InitializeComponent();
            lbMesasCirculo.ItemsSource = metodoMesa.GetMesaList();

        }

        private void gMesa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Mesa MesaSeleccionada = (Mesa)(sender as Grid).DataContext;
                if (MesaSeleccionada.idEstado == 'D')
                {
                    NavigationService.Navigate(new Cliente(MesaSeleccionada));
                }
                else
                {
                    MessageBox.Show("La mesa ya está ocupada, seleccione otra.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var asdjkh = Window.GetWindow(this);
            RestaLauncher rl = new();
            rl.Show();
            asdjkh.Close();
        }
    }
}
