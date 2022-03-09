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
using RestauranteInterfaz;
using Restaurant.Negocio;
using Restaurant.Core;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para LoginGeneral.xaml
    /// </summary>
    /// 

    public partial class LoginGeneral : Page
    {
        private readonly Conexion con = new();

        private MetodoNegocio mn = new MetodoNegocio(); 
        public LoginGeneral()
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

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
         
            ingresar(txtUsuaio.Text, txtPassword.Password);
            
        }



        public void ingresar(string user, string pass)
        {
            if (!(string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pass)))
            {
                switch(mn.Login(user, pass))
                {
                    case 0:
                        MessageBox.Show("La cuenta no existe, Ingrse datos diferentes..", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case 2:
                        MessageBox.Show("La contraseña es incorrecta..", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case 6:
                        Administrador mainAdminPage = new();
                        NavigationService.Navigate(mainAdminPage);                     
                        break;
                    case 7:
                        Finanzas mainFinanPage = new();
                        NavigationService.Navigate(mainFinanPage);
                        break;
                    case 8:
                        Bodega mainPageBodega = new(new Usuario() {  NombreUsuario = txtUsuaio.Text});
                        NavigationService.Navigate(mainPageBodega);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ingrese Usuario y contraseña en los campos de textos", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtUsuaio_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            RestaLauncher rl = new();
            rl.Show();

            var VentanaActual = Window.GetWindow(this);
            VentanaActual.Close();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lbBienvenida.Content = $"De vuelta, {Environment.UserName}!!";
        }
    }
}
