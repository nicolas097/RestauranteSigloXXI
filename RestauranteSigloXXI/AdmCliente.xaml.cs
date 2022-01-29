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
using MessageBox = System.Windows.MessageBox;
using SelectionMode = System.Windows.Controls.SelectionMode;

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
            cbIdTipoUsuarioActualizar.ItemsSource = metNegocio.GetTipoUsuario();

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
                    Refresh();
                    limpiarPopBoxCrearUsuario();
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


        public void limpiarPopBoxCrearUsuario()
        {
            cbIdTipoUsuarioIngreso.SelectedItem = null;
            txtCorreoIngreso.Text = string.Empty;
            txtContrasenaIngreso.Text = string.Empty;
            txtNombresIngreso.Text = string.Empty;
            txtApellidoIngreso.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtRunIngreso.Text = string.Empty;
            txtNombreUsuarioIngreso.Text = string.Empty;
        }

        private void Refresh()
        {
            lvUsuarios.ItemsSource = null;
            lvUsuarios.ItemsSource = metNegocio.GetUsuariosList();
            cbFilCliente.SelectedIndex = 0;
        }

        private void lvUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvUsuarios.SelectedItem != null)
            {
                popBoxActualizarUsuario.IsEnabled = true;
                var usuarioEditar = (Usuario)lvUsuarios.SelectedItem;
                cbIdTipoUsuarioActualizar.Text = usuarioEditar.DescripcionTipoUsuario.ToString();
                txtCorreoActualizar.Text = usuarioEditar.Correo;
                txtContrasenaActualizar.Text = usuarioEditar.Contrasena;
                txtNombresActualizar.Text = usuarioEditar.Nombre;
                txtApellidoActualizar.Text = usuarioEditar.Apellido;
                txtDireccionActualizar.Text = usuarioEditar.Direccion;
                txtRunIngresoActualizar.Text = usuarioEditar.Run;
                txtNombreUsuarioActualizar.Text = usuarioEditar.NombreUsuario;

            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            int idUser = ((Usuario)lvUsuarios.SelectedItem).IdUsuario;
            Usuario usuario = new Usuario();
            usuario.IdUsuario = idUser;
            usuario.IdTipoUsuario = metNegocio.GetTipoUsuarioFromDescripcion(cbIdTipoUsuarioActualizar.Text);
            usuario.Correo = txtCorreoActualizar.Text;
            usuario.Contrasena = txtContrasenaActualizar.Text;  
            usuario.Nombre = txtNombresActualizar.Text;
            usuario.Apellido = txtApellidoActualizar.Text;
            usuario.Direccion = txtDireccionActualizar.Text;
            usuario.Run = txtRunIngresoActualizar.Text;
            usuario.NombreUsuario = txtNombreUsuarioActualizar.Text;
            if (metNegocio.ActulizarUsuario(usuario))
            {
                MessageBox.Show("Se ha actualizado el usuario");
                Refresh();
            }
            else
            {
                MessageBox.Show("No se ha actualizado nada");
            }
        }

        private void btnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (lvUsuarios.SelectedItem != null)
            {
                var usuarioEliminar = (Usuario)lvUsuarios.SelectedItem;
                Usuario usuario = new Usuario();
                usuario.IdUsuario = Convert.ToInt32(usuarioEliminar.IdUsuario);

                if (MessageBox.Show("Estas seguro que quieres eliminar al usuario:" + usuarioEliminar.NombreUsuario + "?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (metNegocio.EliminarUsuario(usuario))
                    {
                        MessageBox.Show("El usuario ha sido eliminado");
                        Refresh();

                    }
                    else
                    {
                        MessageBox.Show("No se ha logrado eliminar el usuario");
                    }
                }

            }
            else
            {
                MessageBox.Show("Seleccione algun Item");
            }
        }

        private void btnRefreshUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnCancelarActualizar_Click(object sender, RoutedEventArgs e)
        {
            popBoxActualizarUsuario.IsPopupOpen = false; 
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            popBoxCrearUsuario.IsPopupOpen = false;   
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
