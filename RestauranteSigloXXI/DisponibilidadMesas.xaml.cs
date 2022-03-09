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
using Restaurant.Core;
using Restaurant.Negocio;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para DisponibilidadMesas.xaml
    /// </summary>
    public partial class DisponibilidadMesas : Page
    {
        MetodoMesa metodoMesa = new();
        public DisponibilidadMesas()
        {
            InitializeComponent();
            ListMesa.ItemsSource = metodoMesa.GetMesaList();
            popBoxActualizarEst.IsEnabled = false;
            cbEstadoActualizar.ItemsSource = metodoMesa.GetTipoEstado();
            
            
           
            
        }

        private void TextBlock_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }



        public char comoBoxChar(int indice)
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

        private void cbCambioEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            int idMesa = ((Mesa)ListMesa.SelectedItem).IdMesa;
            Mesa mesa = new();
            mesa.IdMesa = idMesa;
            mesa.CantSilla = Convert.ToInt32(txtCantSillaUpdate.Text);
            mesa.idEstado = comoBoxChar(cbEstadoActualizar.SelectedIndex + 1);
            if (metodoMesa.ActualizarMesa(mesa))
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
            popBoxActualizarEst.IsPopupOpen = false;
        }


        public void Refresh()
        {
            ListMesa.ItemsSource = null;
            ListMesa.ItemsSource = metodoMesa.GetMesaList();    

        }

        private void ListMesa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListMesa.SelectedItem != null)
            {
                popBoxActualizarEst.IsEnabled = true;
                var EditarMesa = (Mesa)ListMesa.SelectedItem;
                txtCantSillaUpdate.Text = EditarMesa.CantSilla.ToString();
                cbEstadoActualizar.SelectedIndex = ComboBoxActualizarMesa(EditarMesa.idEstado)-1;
            }
        }

        private void btnVolverTotem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void popBoxActualizarEst_Closed(object sender, RoutedEventArgs e)
        {
            ListMesa.SelectedItem = null;
            popBoxActualizarEst.IsEnabled = false;

        }










        //private void cambioColor()
        //{
        //    var converterBrush = new System.Windows.Media.BrushConverter();
        //    var brush = (Brush)converterBrush.ConvertFromString("#FF0000");

        //    foreach (Mesa item in ListMesa.Items)
        //    {
        //        if (item.EstadoMesa == "Ocupado")
        //        {
        //            ListBoxItem row = (ListBoxItem)ListMesa.ItemContainerGenerator.ContainerFromItem(item);
        //            row.Background = brush;
        //        }
        //    }
        //}



    }
}
