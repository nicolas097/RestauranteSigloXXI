using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class PlatoCarrito
    {
        public int IdPlatoCarrito { get; set; } 
        public byte[]? Imagen { get; set; }

        public int Cantidad { get; set; }   

        public string Nombre { get; set; }
        
        public int Precio { get; set; } 
    }
}
