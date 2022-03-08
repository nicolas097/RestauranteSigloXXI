using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Plato
    {
        public int IdPlato { get; set; }

        public string Descripcion { get; set; } 
        
        public int Precio { get; set; }


        public byte[]? ImagenPlato { get; set; }


        //public Image? imagenPlato => (Image)new ImageConverter().ConvertFrom(ImagenPlato);


        public PlatoCarrito ToPlatoCarrito(Plato plat)
        {
            PlatoCarrito pc = new()
            {
                IdPlatoCarrito = plat.IdPlato,
                Cantidad = 1,
                Imagen = plat.ImagenPlato,
                Precio = plat.Precio,
                Nombre = plat.Descripcion
            };

            return pc;
        }
        

        public int Subtotal { get; set; }

    }
}
