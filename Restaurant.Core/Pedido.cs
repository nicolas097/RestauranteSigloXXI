using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public  class Pedido
    {
        public int IdPedido { get; set; }

        public int IdMesa { get; set; }

        public string IdEstadoPedido { get; set; }

        public DateTime fecha { get; set; } 

        public int TotalBruto { get; set; }


        public int TotalNeto { get; set; }


        public int TotalIVA { get; set; }    

    }
}
