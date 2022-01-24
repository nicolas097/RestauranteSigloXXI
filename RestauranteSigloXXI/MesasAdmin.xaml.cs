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
            popBoxActualizarMesa.IsEnabled = false;
            cbEstadoActualizar.ItemsSource = metNegocio.GetTipoEstado();
            
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
   

  

        private bool sePuedeAgregar()
        {
            if (txtCantSilla.Text == string.Empty)
            {
                MessageBox.Show("Ingrese la cantidad de silla a la nueva mesa");
            }
            else if (cbEstado.Text == string.Empty)
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

        //private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lvMesas.SelectedItem != null)
        //    {
        //        var MesasEliminar = (Mesa)lvMesas.SelectedItem;
        //        if (MessageBox.Show("¿Está seguro que que quieres Eliminar " + MesasEliminar.IdMesa + "?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            if (metNegocio.EliminarInsumo(MesasEliminar.IdMesa))
        //            {
        //                MessageBox.Show("Se ha eliminado la mesa");
        //                Refresh();
        //            }
        //            else
        //            {
        //                MessageBox.Show("No se ha eliminado nada");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Seleccione alguna fila");
        //    }
        //}

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void cbFilEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFilEstado.SelectedIndex != 0)
            {
                lvMesas.ItemsSource = null;
                lvMesas.ItemsSource = metNegocio.GetMesaList().Where(s => s.idEstado == comoBoxChar(cbFilEstado.SelectedIndex)).ToList();
            }
            else
            {
                Refresh();
            }
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


        public int ComboBoxActualizarMesa(char idEstado)
        {
            switch (idEstado)
            {
                case 'D':
                    return 1;
                case 'O':
                    return 2;
                default:
                    break;
            }
            return 0;
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


        private void btnCancelarPopBoxCrear_Click(object sender, RoutedEventArgs e)
        {
            popBoxCrearMesa.IsPopupOpen = false;
        }

        private void lvMesas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMesas.SelectedItem != null)
            {
                popBoxActualizarMesa.IsEnabled = true;
                var MesaEditar = (Mesa)lvMesas.SelectedItem;
                txtCantSillaUpdate.Text = MesaEditar.CantSilla.ToString();
                cbEstadoActualizar.SelectedIndex = ComboBoxActualizarMesa(MesaEditar.idEstado)-1;

            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            int IdMesa = ((Mesa)lvMesas.SelectedItem).IdMesa;
            Mesa mesa = new();
            mesa.IdMesa = IdMesa;
            mesa.CantSilla = Convert.ToInt32(txtCantSillaUpdate.Text);
            mesa.idEstado = comoBoxChar(cbEstadoActualizar.SelectedIndex + 1);
            if (metNegocio.ActualizarMesa(mesa))
            {
                MessageBox.Show("Se ha actualizado la mesa");
                Refresh();
            }
            else
            {
                MessageBox.Show("No se ha logrado actualizar la mesa");
            }
        }

        private void btnCancelarPopBoxActualizar_Click(object sender, RoutedEventArgs e)
        {
            popBoxActualizarMesa.IsPopupOpen = false;
        }

        private void btnEliminarMesa_Click(object sender, RoutedEventArgs e)
        {
            if (lvMesas.SelectedItem != null)
            {
                var MesasEliminar = (Mesa)lvMesas.SelectedItem;
                if (MessageBox.Show("¿Está seguro que que quieres las mesa número " + MesasEliminar.IdMesa + "?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (metNegocio.EliminarMesa(MesasEliminar.IdMesa))
                    {
                        MessageBox.Show("Se ha eliminado la mesa");
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No se ha eliminado nada");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione alguna fila");
            }
        }
    }
}
