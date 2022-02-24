﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Restaurant.Core;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestauranteInterfaz
{
    /// <summary>
    /// Lógica de interacción para Carrito.xaml
    /// </summary>
    public partial class PageCarrito : Page
    {
     

        public PageCarrito(Carrito ca)
        {

            InitializeComponent();

            




            lvCarrito.ItemsSource = ca.GetCarritos();

        }

       

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
