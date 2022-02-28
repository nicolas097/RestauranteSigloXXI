using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class DetallePedido
    {
        public int IdPedido { get; set; }   

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public int EstadoPedido { get; set; }
    }
}
