using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante.DB;

namespace Restaurant.Core
{
    public class Insumo
    {
        public Conexion con = new Conexion();
        public int IdInsumo { get; set; }   

        public int IdCategoria { get; set; }

        public int precio { get; set; }

        public string descripCategoria => con.RunOracleExecuteScalar($"SELECT DESCRIPCION FROM CATEGORIAINSUMO WHERE idcategoria = {IdCategoria}").ToString();


        public string NombreInsumo => con.RunOracleExecuteScalar($"SELECT nombreinsumo FROM Insumo WHERE idinsumo = {IdInsumo}").ToString();

        public int Existencia { get; set; }


    }
}
