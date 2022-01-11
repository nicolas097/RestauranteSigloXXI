using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public  class DetalleCompra
    {
        public int cantidad { get; set; }

        public int IdCategoria { get; set; }

        public string RutProveedor { get; set; }

        public int IdInsumo { get; set; }

        public int IdCompra { get; set; }

        public int IdEmpleado { get; set; }


    }
}
