using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public int IdUsuario { get; set; }

        public string IdTipoUsuario {get; set;}

        public int Sueldo { get; set; }

        public int EmpleadoJefe { get; set; }

        public DateTime FechaContrato { get; set; }

        public string NumeroContacto { get; set; }  

    }
}
