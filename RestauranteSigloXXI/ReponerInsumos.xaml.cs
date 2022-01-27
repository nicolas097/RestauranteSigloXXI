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
            //cbCategInumoEntraActualizar.ItemsSource = metNeg.GetCategoriaStrings();
            
          

            

            
          
          
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
    }
}
