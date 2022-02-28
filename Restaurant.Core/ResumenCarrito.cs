using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{

    public class ResumenCarrito
    {
        public Carrito? _carrito;


        public double Total
        {
            get
            {
                double SubTotal = 0;

                foreach (var item in _carrito.platos)
                {
                    SubTotal += (item.PrecioTotal);
                }

                return SubTotal;
            }
        }

        public double SubtotalIVA => Total * 0.19;

        public double Subtotal => Total * 0.81;

     




        

    }
}
