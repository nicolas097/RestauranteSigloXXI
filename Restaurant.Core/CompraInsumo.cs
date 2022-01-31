using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante.DB;

namespace Restaurant.Core
{
    public  class CompraInsumo
    {

       private Conexion con = new();
       public int IdCompra { get; set; }

       public DateTime FechaCompra { get; set; }

       public int ValorBruto { get; set; }


       public int IdUsuario { get; set; }
       

        public int ValorIva { get; set; }

        public int ValorTotal => ValorIva + ValorBruto;


        public string IdTipoUsuario => con.RunOracleExecuteScalar($"SELECT IDTIPOUSUARIO FROM USUARIO WHERE IDUSUARIO = {IdUsuario}").ToString();



    }
}
