using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante.DB;

namespace Restaurant.Core
{

    public class Mesa
    {
        public Conexion con = new();
        public int IdMesa { get; set; }

        public int CantSilla { get; set; }

        public char idEstado { get; set; }

        public string EstadoMesa => con.RunOracleExecuteScalar($"SELECT ESTADO FROM ESTADOMESA WHERE idestado = '{idEstado}'").ToString();

    }
}
