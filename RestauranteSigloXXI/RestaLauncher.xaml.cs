using MahApps.Metro.Controls;
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
    public partial class RestaLauncher : MetroWindow
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
    }
}
