using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante.DB;

namespace Restaurant.Core
{
    public  class DetalleCompra
    {
        private Conexion con = new Conexion();
        public int cantidad { get; set; }

        public int IdCategoria { get; set; }

        public int IdProveedor { get; set; }


        public int idProveedor => Convert.ToInt32(con.RunOracleExecuteScalar($"SELECT NOMBREPROVEEDOR FROM PROVEEDOR WHERE IDPROVEEDOR = {IdProveedor}"));

        //public string RutProveedor => con.RunOracleExecuteScalar($"SELECT NOMBRE FROM PROVEEDOR WHERE RUTPROVEEDOR = {rutProveedor}").ToString();

        public int IdInsumo { get; set; }

        //public int idInsumo => Convert.ToInt32(con.RunOracleExecuteScalar($"SELECT NOMBREINSUMO FROM INSUMO WHERE IDINSUMO = {IdInsumo}"));

        public int IdCompra { get; set; }




    }
}
