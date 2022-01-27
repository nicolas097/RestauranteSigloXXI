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

        public int Login(string usuario, string contrasena)
        {
            //Verifica si existe el usuario
            string ExisteCuenta = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}'";
            //verifica sí el tipo usuario es de tipo adsministrador
            string ExisteCuentaAdministrador = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND idtipousuario = 'ADM'";
            //verifica si el tipo de usuario es de finanza
            string ExisteCuentaFinanza = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND idtipousuario = 'FIN'";

            string ExisteCuentaBod = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND idtipousuario = 'BOD'";
            string ExistePwdCuentaAdm = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND contrasena = '{contrasena}' AND idtipousuario = 'ADM'";
            string ExistePwdCuentaFin = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND contrasena = '{contrasena}' AND idtipousuario = 'FIN'";
            string ExistePwdCuentaBod = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND contrasena = '{contrasena}' AND idtipousuario = 'BOD'";


            int TipoLogin;

            if (con.RunOracleExecuteScalar(ExisteCuenta) == null)
            {
                TipoLogin = 0;
            }
            else
            {
                TipoLogin = 1;
                if (con.RunOracleExecuteScalar(ExisteCuentaAdministrador) != null)
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
                       
                        TipoLogin = 8 ;
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

            string sqlCommand = "SELECT I.IDINSUMO AS IDINSUMO, I.IDCATEGORIA AS IDCATEGORIA, CAT.DESCRIPCION AS NOMBRECATEGORIA, I.NOMBREINSUMO AS NOMBREINSUMO, I.PRECIOUNITARIO AS PRECIOUNITARIO, I.EXISTENCIA AS EXISTENCIA, I.ESTADO AS ESTADO FROM INSUMO I INNER JOIN categoriainsumo CAT ON cat.idcategoria = i.idcategoria  WHERE ESTADO = 1 ORDER BY 1 ASC";

            foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
            {
                Insumo insumo = new Insumo();
                insumo.IdInsumo = Convert.ToInt32(dr["IDINSUMO"]);
                insumo.IdCategoria = Convert.ToInt32(dr["IDCATEGORIA"]);
                insumo.Precio = Convert.ToInt32(dr["PRECIOUNITARIO"]);
                insumo.Existencia = Convert.ToInt32(dr["EXISTENCIA"]);
                insumo.Estado = Convert.ToInt32(dr["ESTADO"]);
                listaInsumo.Add(insumo);
            }

            return listaInsumo;

        }


        public List<Mesa> GetMesaList()
        {
            List<Mesa> listaMesa = new();
            string sqlCommand = "select m.idMesa as NúmeroMesa , m.cantidadSilla as CantidadSilla, m.idEstado as idEstadoMesa ,estMesa.estado as EstadoMesa FROM MESA m inner join estadoMesa estMesa on estmesa.idEstado = m.idestado ORDER BY 1 ASC";
            foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
            {
                Mesa mesa = new Mesa();
                mesa.IdMesa = Convert.ToInt32(dr["NúmeroMesa"]);
                mesa.CantSilla = Convert.ToInt32(dr["CantidadSilla"]);
                mesa.idEstado = Convert.ToChar(dr["idEstadoMesa"]);
                listaMesa.Add(mesa);

            }
            return listaMesa;
        }


        public List<string> GetCategoriaStrings()
        {
            string sqlCommand = "select descripcion from categoriaInsumo Order by idCategoria asc";
            List<string> listaCategoria = con.OracleToDataTable(sqlCommand).AsEnumerable().Select(x => x.Field<string>(0)).ToList();
            return listaCategoria;
        }

        public List<string> GetTipoEstado()
        {
            string sqlCommand = "SELECT estado FROM estadoMesa";
            List<string> listaEstado = con.OracleToDataTable(sqlCommand).AsEnumerable().Select(x => x.Field<string>(0)).ToList();
            return listaEstado;
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

            int IdSequence = 1;

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
            cmd.Parameters.Add("@P_ESTADO", insumo.Estado);

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


        //public bool EliminarMesa(int IdMesa)
        //{
        //    OracleCommand cmd = new("SP_ELIMINARMESA", con.OracleConnection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("P_IDMESA", IdMesa);

        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch
        //    {

        //        return false;
        //    }
        //}


        //public bool EliminarInsumo(int idInsumo)
        //{
        //    string sqlCommand = ($"DELETE FROM INSUMO WHERE IDINSUMO = {idInsumo}");
        //    try
        //    {
        //        con.RunOracleNonQuery(sqlCommand);
        //        return true;
        //    }
        //    catch
        //    {

        //        return false;
        //    }
        //}



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


        public bool EliminarInsumo(Insumo insumo)
        {
            OracleCommand cmd = new("SP_ELIMINARINSUMO", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_IDINSUMO", insumo.IdInsumo);

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



        public bool CrearMesa(Mesa mes)
        {
            mes.IdMesa = GenerateId("IDMESA", "MESA");
            OracleCommand cmd = new("SP_CREARMESA", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_IDMESA", mes.IdMesa);
            cmd.Parameters.Add("@P_CANTIDADSILLA", mes.CantSilla);
            cmd.Parameters.Add("@P_IDESTADO", mes.idEstado);


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


        public bool EliminarMesa(int idMesa)
        {
            string sqlCommand = ($"DELETE FROM MESA WHERE IDMESA = {idMesa}");

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


        public bool ActualizarMesa(Mesa mesa)
        {
            OracleCommand cmd = new ("SP_ACTUALIZARMESA", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_IDMESA", mesa.IdMesa);
            cmd.Parameters.Add("@P_CANTIDADSILLA", mesa.CantSilla);
            cmd.Parameters.Add("@P_IDESTADO", mesa.idEstado);

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
