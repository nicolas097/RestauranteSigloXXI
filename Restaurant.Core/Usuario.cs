using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante.DB;

namespace Restaurant.Core
{
    public class Usuario
    {
        public Conexion con = new ();
        public int IdUsuario { get; set; }  

        public string IdTipoUsuario { get; set; }

        
        
        public string Correo { get; set; }

        public string Contrasena { get; set; }


        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Direccion { get; set; }

        public string Run { get; set; }
    }
}
