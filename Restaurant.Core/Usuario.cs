using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Usuario
    {
        public int IdUsuario { get; set; }  

        public string IdTipoUsuario { get; set; }
        
        public string Correo { get; set; }

        public string contrasena { get; set; }

        public string? nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Direccion { get; set; }

        public string Run { get; set; }
    }
}
