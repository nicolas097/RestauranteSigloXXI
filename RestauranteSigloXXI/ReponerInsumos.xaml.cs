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

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para ReponerInsumos.xaml
    /// </summary>
    public partial class ReponerInsumos : Page
    {
        private MetodoNegocio mb = new(); 
        public ReponerInsumos()
        {
            InitializeComponent();
            MostrarNombreTipoUsuario();
        }



        public void MostrarNombreTipoUsuario()
        {

            LoginGeneral lg = new LoginGeneral();
            lg.txtUsuaio.Text = lbNombreUsuario.Visibility.ToString();
        }
    }
}
