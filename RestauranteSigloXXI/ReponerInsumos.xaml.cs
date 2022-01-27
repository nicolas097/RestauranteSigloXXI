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
            PopuBoxActuliazarEntrada.IsEnabled = false;
            cbProveedorEntra.ItemsSource = metNeg.GetProveedor();
                    
          
        }

        private void MostrarIdUsuarioYNombreusuario()
        {
            lbNombreUsuario.Content = user.nombre;
            lbIdUsuario.Content = user.idUsuario;

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
         
            cbInsumo.ItemsSource = metNeg.GetInsumoList().Where(s => s.IdCategoria == cbCategInsumoEntra.SelectedIndex + 1).Select( s => s.NombreInsumo).ToList();
            
        }

       

        private void ingresarDetalleCompra()
        {
            DetalleCompra detCompra = new DetalleCompra();
            detCompra.cantidad = Convert.ToInt32(txtCantidadReponer.Text);
            detCompra.IdCategoria = Convert.ToInt32(cbCategInsumoEntra.SelectedIndex + 1);
            detCompra.IdInsumo = Convert.ToInt32(cbCategInsumoEntra.SelectedIndex + 1);
            detCompra.IdProveedor = Convert.ToInt32(cbProveedorEntra.SelectedIndex + 1);
            if (metNeg.CrearDetalleCompra(detCompra))
            {
                //metNeg.ActualizarExistencia(detCompra.IdCompra, detCompra.cantidad);
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
            compraInsumo.ValorBruto = Convert.ToInt32(metNeg.traerPrecioUnitario(cbInsumo.SelectedIndex + 1));
            compraInsumo.ValorIva = Convert.ToInt32(metNeg.traerPrecioUnitario(cbInsumo.SelectedIndex) * 0.19);
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
            ingresarCompra();
            ingresarDetalleCompra();
        }
    }
}
