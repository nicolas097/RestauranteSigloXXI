using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Core;
using Restaurante.DB;
using System.IO;
using Oracle.ManagedDataAccess.Client;

namespace Restaurant.Negocio
{
    public class MetodoNegocio
    {
        public Conexion con = new Conexion();

        public int Login (string usuario, string contrasena)
        {
            string ExisteCuenta = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBRE = '{usuario}'";
            string ExisteCuentaAdministrador = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBRE = '{usuario}' AND idtipousuario = 'ADM'";
            string ExisteCuentaFinanza = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBRE = '{usuario}' AND idtipousuario = 'FIN'";
            string ExisteCuentaBod = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBRE = '{usuario}' AND idtipousuario = 'BOD'";
            string ExistePwdCuentaAdm = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBRE = '{usuario}' AND contrasena = '{contrasena}' AND idtipousuario = 'ADM'";
            string ExistePwdCuentaFin = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBRE = '{usuario}' AND contrasena = '{contrasena}' AND idtipousuario = 'FIN'";
            string ExistePwdCuentaBod = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBRE = '{usuario}' AND contrasena = '{contrasena}' AND idtipousuario = 'BOD'";

            int TipoLogin;

            if (con.RunOracleExecuteScalar(ExisteCuenta) == null)
            {
                TipoLogin = 0;
            }
            else
            {
                 TipoLogin = 1;
                 if(con.RunOracleExecuteScalar(ExisteCuentaAdministrador) != null)
                 {

                    
                    if (con.RunOracleExecuteScalar(ExistePwdCuentaAdm) != null)
                    {
                        TipoLogin = 6;
                    }
                    else
                    {
                        TipoLogin = 2;
                    }
                 }
                 if (con.RunOracleExecuteScalar(ExisteCuentaFinanza) != null)
                 {
                    if (con.RunOracleExecuteScalar(ExistePwdCuentaFin) != null)
                    {
                        TipoLogin = 7;
                    }
                    else
                    {
                        TipoLogin = 2;
                    }
                 }
                if (con.RunOracleExecuteScalar(ExisteCuentaBod) != null)
                {
                    if (con.RunOracleExecuteScalar(ExistePwdCuentaBod) != null)
                    {
                        TipoLogin = 8;
                    }
                    else
                    {
                        TipoLogin = 2;
                    }
                }   
            }
            return TipoLogin;
        }



        public List<Insumo> GetInsumoList()
        {
            List<Insumo> listaInsumo = new List<Insumo>();

            string sqlCommand = "SELECT I.IDINSUMO AS IDINSUMO, I.IDCATEGORIA AS IDCATEGORIA, CAT.DESCRIPCION AS NOMBRECATEGORIA, I.NOMBREINSUMO AS NOMBREINSUMO, I.PRECIOUNITARIO AS PRECIOUNITARIO, I.EXISTENCIA AS EXISTENCIA FROM INSUMO I INNER JOIN categoriainsumo CAT ON cat.idcategoria = i.idcategoria ORDER BY 1 ASC";

            foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
            {
                Insumo insumo = new Insumo();
                insumo.IdInsumo = Convert.ToInt32(dr["IDINSUMO"]);
                insumo.IdCategoria = Convert.ToInt32(dr["IDCATEGORIA"]);
                insumo.Precio = Convert.ToInt32(dr["PRECIOUNITARIO"]);
                insumo.Existencia = Convert.ToInt32(dr["EXISTENCIA"]);
                listaInsumo.Add(insumo);
            }

            return listaInsumo;
            
        }


       public List<string> GetCategoriaStrings()
       {
            string sqlCommand = "select descripcion from categoriaInsumo";
            List<string> listaCategoria = con.OracleToDataTable(sqlCommand).AsEnumerable().Select(x => x.Field<string>(0)).ToList();
            return listaCategoria;
        } 


        public int GenerateId(string IdColumn, string TableName)
        {
            List<int> idS = new();
            string sqlCommand = $"SELECT {IdColumn} FROM {TableName}";


            foreach (DataRow dataRow in con.OracleToDataTable(sqlCommand).Rows)
            {
                idS.Add(Convert.ToInt32(dataRow[IdColumn]));
            }


            idS.Sort();

            int  IdSequence = 1;

            foreach (var item in idS)
            {
                if (IdSequence == item)
                {
                    IdSequence++;
                }
                else
                {
                    return IdSequence;
                }
            }

            return IdSequence;
        }


        public bool CrearInsumo(Insumo insumo)
        {
           insumo.IdInsumo = GenerateId("IDINSUMO", "INSUMO");
           OracleCommand cmd = new("SP_CREARINSUMO", con.OracleConnection);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@P_IDINSUMO", insumo.IdInsumo);
           cmd.Parameters.Add("@P_IDCATEGORIA", insumo.IdCategoria);
           cmd.Parameters.Add("@P_NOMBREINSUMO", insumo.nombreInsumo);
           cmd.Parameters.Add("@P_PRECIOUNITARIO", insumo.Precio);
           cmd.Parameters.Add("@P_EXISTENCIA", insumo.Existencia);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        } 


        public bool EliminarInsumo(int idInsumo)
        {
            string sqlCommand = ($"DELETE FROM INSUMO WHERE IDINSUMO = {idInsumo}");
            try
            {
                con.RunOracleNonQuery(sqlCommand);
                return true;
            }
            catch 
            {

                return false;
            }
        }



        public bool ActualizarInsumo(Insumo ins)
        {
            OracleCommand cmd = new("SP_ACTUALIZARINSUMO", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_IDINSUMO", ins.IdInsumo);
            cmd.Parameters.Add("@P_IDCATEGORIA", ins.IdCategoria);
            cmd.Parameters.Add("@P_NOMBREINSUMO", ins.nombreInsumo);
            cmd.Parameters.Add("@P_PRECIOUNITARIO", ins.Precio);
            cmd.Parameters.Add("@P_EXISTENCIA", ins.Existencia);

            try
            {
                cmd.ExecuteNonQuery();
                return true;

            }
            catch 
            {

                return false;
            }
        }
        
    }
}
