using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante.DB;

namespace Restaurant.Core
{
    public class DetallePedido
    {
        public Conexion con = new Conexion();
        public int IdPedido { get; set; }   

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public int EstadoPedido { get; set; }


        public int IdMesa { get; set; }

        public int idMesa => Convert.ToInt32(con.RunOracleExecuteScalar($"SELECT IDMESA FROM PEDIDO WHERE IDPEDIDO = {IdPedido}"));

       

        public int idProducto => Convert.ToInt32(con.RunOracleExecuteScalar($"SELECT DESCRIPCION FROM PRODUCTO WHERE IDPLATO = {IdProducto}"));


        public Plato plato { get; set; }   
    }
}
