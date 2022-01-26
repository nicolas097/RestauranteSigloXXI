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
    /// Lógica de interacción para ReponerInsumos.xaml
    /// </summary>
    public partial class ReponerInsumos : Page
    {
        private Usuario user;
        public ReponerInsumos(Usuario usuario)
        {
            InitializeComponent();
            user = usuario;
            MostrarIdUsuarioYNombreusuario();
          
          
        }

        private void MostrarIdUsuarioYNombreusuario()
        {
            lbNombreUsuario.Content = user.nombre;
            lbIdUsuario.Content = user.idUsuario;

        }
         
    }
}
