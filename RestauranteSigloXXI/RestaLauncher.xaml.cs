
using RestauranteSigloXXI;
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
using System.Windows.Shapes;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para RestaLauncher.xaml
    /// </summary>
    public partial class RestaLauncher : Window
    {
        public RestaLauncher()
        {
            InitializeComponent();
        }

        private void btnModoOtros_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mv = new MainWindow(3);
            mv.Show();
            Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void spBarra_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void btnModoCocina_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mvc = new MainWindow(1);
            mvc.Show();
            Close();
            
            
        }

        private void btnModoCliente_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mv = new MainWindow(0);
            mv.Show();
            Close();
        }

        private void btnModoTotem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mv = new MainWindow(2);
            mv.Show();
            Close();
        }
    }
}
