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
using Restaurant.Negocio;
using Restaurant.Core;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para AdmCliente.xaml
    /// </summary>
    public partial class AdmCliente : Page
    {
        private MetodoNegocio metNegocio = new();

        List<string> listaTipoCliente = new();

        public AdmCliente()
        {
            InitializeComponent();
            listaTipoCliente = metNegocio.GetTipoUsuario();
            listaTipoCliente.Insert(0, "Todos");
            cbFilCliente.ItemsSource = listaTipoCliente;
            cbIdTipoUsuarioIngreso.ItemsSource = metNegocio.GetTipoUsuario();
            popBoxActualizarUsuario.IsEnabled = false;  

        }

        private void lvUsuarios_Loaded(object sender, RoutedEventArgs e)
        {
            lvUsuarios.ItemsSource = metNegocio.GetUsuariosList();
        }

        private void btnIngresarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (cbIdTipoUsuarioIngreso.Text == string.Empty || txtCorreoIngreso.Text == string.Empty || txtContrasenaIngreso.Text == string.Empty || txtNombresIngreso.Text == string.Empty ||
                txtApellidoIngreso.Text == string.Empty || txtDireccion.Text == string.Empty || txtRunIngreso.Text == string.Empty ||txtNombreUsuarioIngreso.Text == string.Empty)
            {
                MessageBox.Show("Rellene todos los campos");
            }
            else
            {
                Usuario user = new Usuario();
                user.IdTipoUsuario = metNegocio.GetTipoUsuarioFromDescripcion(cbIdTipoUsuarioIngreso.Text);
                user.Correo = txtCorreoIngreso.Text;
                user.Contrasena = txtContrasenaIngreso.Text;
                user.Nombre = txtNombresIngreso.Text;
                user.Apellido = txtApellidoIngreso.Text;
                user.Direccion = txtDireccion.Text;
                user.Run = txtRunIngreso.Text;
                user.NombreUsuario = txtNombreUsuarioIngreso.Text;
                if (metNegocio.CrearUsuario(user))
                {
                    MessageBox.Show("Se ingreso el usuario correctamente");
                }
                else
                {
                    MessageBox.Show("No se logro ingresar el usuario");
                }
            }
        }
    }
}
