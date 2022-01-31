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
using MessageBox = System.Windows.MessageBox;
using SelectionMode = System.Windows.Controls.SelectionMode;


namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para ReponerInsumos.xaml
    /// </summary>
    public partial class ReponerInsumos : Page
    {
        public Insumo insumo = new();

        private Usuario user;

        private MetodoNegocio metNeg = new ();

        List<string> ListaCategoria = new List<string>();

        List<string> ListaInsumo = new();

        public ReponerInsumos(Usuario usuario)
        {
            InitializeComponent();
            user = usuario;
            MostrarIdUsuarioYNombreusuario();
            ListaCategoria = metNeg.GetCategoriaStrings();
            ListaCategoria.Insert(0, "Todos");
            cbFilCategoriaInsumo.ItemsSource = ListaCategoria;   
            cbCategInsumoEntra.ItemsSource = metNeg.GetCategoriaStrings();  
            cbProveedorEntra.ItemsSource = metNeg.GetProveedor();
            //cbInsumo.ItemsSource = metNeg.GetInsumoList();
                    
          
        }

        private void MostrarIdUsuarioYNombreusuario()
        {
            lbNombreUsuario.Content = user.NombreUsuario;
            lbIdUsuario.Content = user.IdUsuario;

        }

        private void cbInsumoActualizar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cbCategInsumoEntra.SelectedIndex != -1)
            //{
            //    cbInsumo.ItemsSource = metNeg.GetInsumoList().Where( s => s.IdCategoria == cbCategInsumoEntra.SelectedIndex).ToList() ;
            //}
        }



        private void cbCategInsumoEntra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            cbInsumo.ItemsSource = metNeg.GetInsumoList().Where(s => s.IdCategoria == cbCategInsumoEntra.SelectedIndex + 1).Select(s => s.NombreInsumo).ToList();

        }


       

        private void ingresarDetalleCompra()
        {
            DetalleCompra detCompra = new DetalleCompra();
            detCompra.cantidad = Convert.ToInt32(txtCantidadReponer.Text);
            detCompra.IdCategoria = Convert.ToInt32(cbCategInsumoEntra.SelectedIndex + 1);
            detCompra.IdProveedor = Convert.ToInt32(cbProveedorEntra.SelectedIndex + 1);
            detCompra.IdInsumo = Convert.ToInt32(metNeg.GetInsumoFromNombreInsumo(cbInsumo.Text));
            if (metNeg.CrearDetalleCompra(detCompra))
            {
                metNeg.ActualizarExistencia(detCompra.IdInsumo, detCompra.cantidad);
                MessageBox.Show("Se ingresó a detalle Compra");
                
            }
            else
            {
                MessageBox.Show("No se ingresó a detalle compra");
            }

            
        }


        private void ingresarCompra()
        {
            CompraInsumo compraInsumo = new();
            compraInsumo.ValorBruto = metNeg.traerPrecioUnitario(cbInsumo.Text);
            compraInsumo.ValorIva = (int)Math.Round(compraInsumo.ValorBruto * 0.19);
           
            compraInsumo.IdUsuario = user.idUsuario;
            if (metNeg.CrearCompraInsumo(compraInsumo))
            {

                MessageBox.Show("Se Ingreso algo");
            }
            else
            {
                MessageBox.Show("No se ingreso nada");
            }
        }

        private void btnIngresarInsumo_Click(object sender, RoutedEventArgs e)
        {
            //if (cbInsumo.SelectedIndex !=-1  && cbCategInsumoEntra.SelectedIndex !=-1)
            //{
            //    insumo = metNeg.GetInsumoList().Where(s => s.IdCategoria == cbCategInsumoEntra.SelectedIndex + 1 && s.NombreInsumo == cbInsumo.Text).FirstOrDefault();

                ingresarCompra();
                ingresarDetalleCompra();
                Refresh();

            //}
            
        }

        private void cbInsumo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Refresh()
        {
            lvInventarioReponer.ItemsSource = null;
            lvInventarioReponer.ItemsSource = metNeg.GetInsumoList();
            cbFilCategoriaInsumo.SelectedIndex = 0;
        }

        private void lvInventarioReponer_Loaded(object sender, RoutedEventArgs e)
        {
            lvInventarioReponer.ItemsSource = metNeg.GetInsumoList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void lvInventarioReponer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void cbFilCategoriaInsumo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFilCategoriaInsumo.SelectedIndex != 0)
            {
                lvInventarioReponer.ItemsSource = null;
                lvInventarioReponer.ItemsSource = metNeg.GetInsumoList().Where(s => s.IdCategoria == cbFilCategoriaInsumo.SelectedIndex).ToList();
            }
            else
            {
                Refresh();
            }
        }
    }
}
