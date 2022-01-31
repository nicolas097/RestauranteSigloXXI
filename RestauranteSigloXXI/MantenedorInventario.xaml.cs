using Restaurant.Core;
using Restaurant.Negocio;
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
    /// Lógica de interacción para MantenedorInventario.xaml
    /// </summary>
    public partial class MantenedorInventario : Page
    {
        private MetodoNegocio MN = new();

        List<string> categoriaListado = new();

        

        public MantenedorInventario()
        {
            InitializeComponent();
            categoriaListado = MN.GetCategoriaStrings();
            categoriaListado.Insert(0, "Todos");
            cbFilCategoria.ItemsSource = categoriaListado;
            cbCategoria.ItemsSource = MN.GetCategoriaStrings();
            popBoxActualizar.IsEnabled = false;
            cbCategoriaBtnUpdate.ItemsSource= MN.GetCategoriaStrings(); 
        }





      



        private void lvInventario_Loaded(object sender, RoutedEventArgs e)
        {
            lvInventario.ItemsSource = MN.GetInsumoList();
        }

        private void cbFilCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (cbFilCategoria.SelectedIndex != 0)
            {

                lvInventario.ItemsSource = null;
                lvInventario.ItemsSource = MN.GetInsumoList().Where(s => s.IdCategoria == cbFilCategoria.SelectedIndex).ToList();
            }
            else
            {
                Refresh();
            }
            //} else if (cbCategoria.SelectedIndex.ToString() == "Todos"){
            //    Refresh();
            //}

        }

 



    

        private void Refresh()
        {
            lvInventario.ItemsSource = null;
            lvInventario.ItemsSource = MN.GetInsumoList();
            cbFilCategoria.SelectedIndex = 0;

        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            popBoxCrear.IsPopupOpen = false;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();  
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (SePuedeAgregarInsumo())
            {
                Insumo insu = new ();
                insu.nombreInsumo = txtNombreInsumo.Text;
                insu.IdCategoria = Convert.ToInt32(cbCategoria.SelectedIndex + 1);
                insu.Precio = Convert.ToInt32(txtPrecio.Text);
                insu.Existencia = Convert.ToInt32(txtStock.Text);
                insu.Estado = 1;
                if (MN.CrearInsumo(insu))
                {
                    MessageBox.Show("Se agregó el insumo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Refresh();
                    //LimpiarPopBoxCrear();
                    limpiarpopBoxCrearInsumo();
                }
                else
                {
                    MessageBox.Show("no sé agregó el insumo", "Información", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
              
            }
        }


   


        private bool SePuedeAgregarInsumo()
        {
            if (txtNombreInsumo.Text == string.Empty)
            {
                MessageBox.Show("No se ha escrito el nombre del nuevo insumo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cbCategoria.Text == string.Empty)
            {
                MessageBox.Show("No se ha selecionado ninguna categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtPrecio.Text == string.Empty)
            {
                MessageBox.Show("No se ha escrito ningún precio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (txtStock.Text == string.Empty)
            {
                MessageBox.Show("No se ha escrito el Stock", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                return true;
            }

            return false;
        }

        private void btnGuardarUpdate_Click(object sender, RoutedEventArgs e)
        {

                int IdInsumo = ((Insumo)lvInventario.SelectedItem).IdInsumo;

                Insumo insu = new Insumo();
                insu.IdInsumo = IdInsumo;
                insu.nombreInsumo = txtNombreInsumoBtnUpdate.Text;
                insu.IdCategoria = Convert.ToInt32(cbCategoriaBtnUpdate.SelectedIndex + 1);
                insu.Precio = Convert.ToInt32(txtPrecioBtnUpdate.Text);
                insu.Existencia = Convert.ToInt32(txtStockBtnUpdate.Text);
                if (MessageBox.Show("¿Está seguro que que quieres Actualizar " + insu.NombreInsumo + "?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (MN.ActualizarInsumo(insu))
                    {
                        MessageBox.Show($"Se ha modificado el insumo a {insu.NombreInsumo}", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No se ha modificado nada recuerde rellenar Todos los campos" , "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }           
        }

        private void lvInventario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvInventario.SelectedItem != null)
            {
                popBoxActualizar.IsEnabled = true;
                var InsumoEditar = (Insumo)lvInventario.SelectedItem;
                txtNombreInsumoBtnUpdate.Text = InsumoEditar.NombreInsumo;
                cbCategoriaBtnUpdate.SelectedIndex = InsumoEditar.IdCategoria - 1;
                txtPrecioBtnUpdate.Text = InsumoEditar.Precio.ToString();
                txtStockBtnUpdate.Text = InsumoEditar.Existencia.ToString();

            }
        }


        public void limpiarpopBoxCrearInsumo()
        {
            txtNombreInsumo.Text = string.Empty;
            cbCategoria.SelectedItem = null;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;   

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lvInventario.SelectedItem != null)
            {
                var InsumoEliminar = (Insumo)lvInventario.SelectedItem;
                Insumo insu = new ();
                insu.IdInsumo = Convert.ToInt32(InsumoEliminar.IdInsumo);
 

                if (MessageBox.Show("¿Está seguro que que quieres Eliminar " + InsumoEliminar.NombreInsumo + "?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (MN.EliminarInsumo(insu))
                    {
                        MessageBox.Show("Se ha eliminado el insumo");
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido eliminar");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione alguna fila");
            }
        }

        private void btnCancelarUpdate_Click(object sender, RoutedEventArgs e)
        {
            popBoxActualizar.IsPopupOpen = false;   
        }

      
    }
}
