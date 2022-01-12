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
using Restaurante.DB;

namespace RestauranteSigloXXI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Conexion con = new();
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConexion_Click(object sender, RoutedEventArgs e)
        {
            if (con.CheckDatabase())
            {
                MessageBox.Show("Se ha podido conectar con la base de datos", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("No se ha podido conectar con la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
