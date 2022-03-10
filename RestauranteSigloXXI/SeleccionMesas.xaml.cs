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
using System.Windows.Threading;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para SeleccionMesas.xaml
    /// </summary>
    public partial class SeleccionMesas : Page
    {
        private MetodoMesa metodoMesa = new();

        public SeleccionMesas()
        {
            InitializeComponent();
            lbMesasCirculo.ItemsSource = metodoMesa.GetMesaList();
            InitTimer();    


        }

        private void gMesa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Mesa MesaSeleccionada = (Mesa)(sender as Grid).DataContext;
                if (MesaSeleccionada.idEstado == 'D')
                {
                    NavigationService.Navigate(new Cliente(MesaSeleccionada));
                    CambioEstadoMesa(MesaSeleccionada);
                   
                }
                else
                {
                    MessageBox.Show("La mesa ya está ocupada, seleccione otra.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        public void InitTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {

            Refresh();
        }

        private void Refresh()
        {
            lbMesasCirculo.ItemsSource = null;
            lbMesasCirculo.ItemsSource = metodoMesa.GetMesaList(); 
        }


        private void CambioEstadoMesa(Mesa me)
        {
            var seleccionMessa = me;
            Mesa mes = new Mesa();
            mes.IdMesa = Convert.ToInt32(seleccionMessa.IdMesa);

            if (metodoMesa.CambioEstadoMesa(mes))
            {
                
            }
            else
            {
                MessageBox.Show("No se ha cambiado ningun estado");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var asdjkh = Window.GetWindow(this);
            RestaLauncher rl = new();
            rl.Show();
            asdjkh.Close();
        }

    
    }
}
