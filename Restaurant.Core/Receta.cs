using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Receta
    {
        
        public int IdReceta { get; set; }

        public string NombreReceta { get; set; }

        public byte[]? Foto { get; set; }   

        public int Porciones { get; set; }

        public string Descripcion { get; set; } 

        public List<string> Pasos { get; set; } 

        public List<string> Ingredientes { get; set; }

    }
}
