using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Reserva
    {
        public int IdReserva { get; set; }  

        public int CantidadPersona { get; set; }

        public DateTime FechaReserva { get; set; }  

        public int IdMesa { get; set; }

        public int IdUsuario { get; set; }

        public string IdTipoUsuario { get; set; }

        public char estadoReserva { get; set; } 
    }
}
