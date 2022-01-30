using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante.DB;
using System.Windows.Media;


namespace Restaurant.Core
{
    public class Insumo
    {
        public Conexion con = new Conexion();
        public int IdInsumo { get; set; }   

        public int IdCategoria { get; set; }

        public int Precio { get; set; }

        public string DescripCategoria => con.RunOracleExecuteScalar($"SELECT DESCRIPCION FROM CATEGORIAINSUMO WHERE idcategoria = {IdCategoria}").ToString();


        public string NombreInsumo => con.RunOracleExecuteScalar($"SELECT nombreinsumo FROM Insumo WHERE idinsumo = {IdInsumo}").ToString();

        public string nombreInsumo { get; set; }

        public int idInsumo => Convert.ToInt32(con.RunOracleExecuteScalar($"SELECT IDINSUMO FROM INSUMO WHERE IDCATEGORIA = {IdCategoria} and NOMBREINSUMO = {NombreInsumo}"));

        public int Existencia { get; set; }

        public int Estado  { get; set; }





      


    }
}
