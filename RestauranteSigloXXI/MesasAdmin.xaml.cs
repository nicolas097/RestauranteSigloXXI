using Restaurant.Negocio;
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
using MessageBox = System.Windows.MessageBox;
using SelectionMode = System.Windows.Controls.SelectionMode;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para MesasAdmin.xaml
    /// </summary>
    public partial class MesasAdmin : Page
    {
        private MetodoNegocio metNegocio = new();

        List<string> listaMesa = new();
        public MesasAdmin()
        {

            InitializeComponent();
            cbEstado.ItemsSource = metNegocio.GetTipoEstado();
            listaMesa = metNegocio.GetTipoEstado();
            listaMesa.Insert(0, "Todos");
            cbFilEstado.ItemsSource = listaMesa;
            popBoxActualizarMesa.IsEnabled = true;
            
        }

        private void lvMesas_Loaded(object sender, RoutedEventArgs e)
        {
            lvMesas.ItemsSource = metNegocio.GetMesaList();
        }

        private void btnGuardarUpdate_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Refresh()
        {
            lvMesas.ItemsSource = null;
            lvMesas.ItemsSource = metNegocio.GetMesaList();
            cbFilEstado.SelectedIndex = 0;
        }
   

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private bool sePuedeAgregar()
        {
            if (txtCantSilla.Text == string.Empty)
            {
                MessageBox.Show("Ingrese la cantidad de silla a la nueva mesa");
            }
            else if (cbEstadoMesa.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el estado de la mesa agregagada");
            }
            else
            {
                return true;
            }
            return false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbFilEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        public char comoBoxChar( int indice )
        {
            switch (indice)
            {
                case 1:
                    return 'D';
                case 2:
                    return 'O';
                default:
                    break;
            }
            return ' ';
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (sePuedeAgregar())
            {
                Mesa mesa = new Mesa();
                mesa.CantSilla = Convert.ToInt32(txtCantSilla.Text);
                mesa.idEstado = comoBoxChar(cbEstado.SelectedIndex + 1);
                if (metNegocio.CrearMesa(mesa))
                {
                    MessageBox.Show("Se agregó mesa");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("No se ha logrado agregar mesa");
                }
            }
        }

        private void btnCancelar_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
