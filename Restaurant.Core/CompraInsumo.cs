using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public  class CompraInsumo
    {
       public int IdCompra { get; set; }

       public DateTime FechaCompra { get; set; }

       public int ValorNeto { get; set; }

       public int iva { get; set; }

       public int IdEmpleado { get; set; }

       public int IdUsuario { get; set; }

       public string IdTipoUsuario { get; set; }
    }
}
