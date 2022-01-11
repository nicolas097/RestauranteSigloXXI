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

        public int IdPlato { get; set; }

        public int IdCategoria { get; set; }

        public int CantidadPlato { get; set; }

        public int PrecioCantPedido { get; set; }
    }
}
