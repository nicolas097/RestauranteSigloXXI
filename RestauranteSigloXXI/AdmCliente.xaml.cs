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

        private void cbFilCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFilCliente.SelectedIndex != 0)
            {
                lvUsuarios.ItemsSource = null;
                if (cbFilCliente.Text != " ")
                {
                    lvUsuarios.ItemsSource = metNegocio.GetUsuariosList().Where(s => s.DescripcionTipoUsuario == cbFilCliente.SelectedItem.ToString()).ToList();

                }
            }
            else
            {
                Refresh();
            }
        }


        private void Refresh()
        {
            lvUsuarios.ItemsSource = null;
            lvUsuarios.ItemsSource = metNegocio.GetUsuariosList();
            cbFilCliente.SelectedIndex = 0;
        }


        //public string comboBoxIdTipoUsuario(int indice)
        //{
        //    switch (indice)
        //    {
        //        case 1:
        //            return "ADM";
        //        case 2:
        //            return "CLI";
        //        case 3:
        //            return "BOD";
        //        case 4:
        //            return "REC";
        //        case 5:
        //            return "COC";
        //        case 6:
        //            return "SIS";
        //        case 7:
        //            return "FIN";
        //        default:
        //            break;
        //    }
        //    return " ";
        //}
    }
}
