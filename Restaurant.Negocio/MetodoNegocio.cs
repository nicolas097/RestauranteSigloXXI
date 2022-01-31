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

        public List<string> GetProveedor()
        {
            string sqlCommnand = "SELECT nombreProveedor FROM proveedor";
            List<string> listadoProveedor = con.OracleToDataTable(sqlCommnand).AsEnumerable().Select(x => x.Field<string>(0)).ToList();
            return listadoProveedor;
        }

        public List<string> GetTipoUsuario()
        {
            string sqlCommand = "SELECT DESCRIPCION FROM TIPOUSUARIO";
            List<string> listaTipoUsuario = con.OracleToDataTable(sqlCommand).AsEnumerable().Select(x => x.Field<string>(0)).ToList();
            return listaTipoUsuario;
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


        public bool CrearCompraInsumo(CompraInsumo compraInsumo)
        {
            DateTime fecha = DateTime.Now;  
           
            compraInsumo.IdCompra = GenerateId("idCompra","CompraInsumo");
            OracleCommand cmd = new("SP_CREARCOMPRAINSUMO", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_IDCOMPRA", compraInsumo.IdCompra);
            cmd.Parameters.Add("@P_FECHA", fecha.ToString("dd/MM/yy"));
            cmd.Parameters.Add("@P_VALORBRUTO", compraInsumo.ValorBruto);
            cmd.Parameters.Add("@P_IVA", compraInsumo.ValorIva);
            cmd.Parameters.Add("@P_TOTAL", compraInsumo.ValorTotal);
            cmd.Parameters.Add("@P_IDUSUARIO", compraInsumo.IdUsuario);
            cmd.Parameters.Add("@P_IDTIPOUSUARIO", compraInsumo.IdTipoUsuario);

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


        public int ValorTotalFromCompraInsumo( int idCompra)
        {
            string sqlCommand = $"SELECT CANTIDAD FROM DETALLECOMPRA WHERE IDCOMPRA = {idCompra}";
            return Convert.ToInt32(con.RunOracleExecuteScalar(sqlCommand));
        }

        public bool CrearDetalleCompra(DetalleCompra detCompra)
        {
            detCompra.IdCompra = GenerateId("idCompra","DETALLECOMPRA");
            OracleCommand cmd = new("SP_CREARDETALLECOMPRA", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_CANTIDAD", detCompra.cantidad);
            cmd.Parameters.Add("@P_IDCATEGORIA", detCompra.IdCategoria);
            cmd.Parameters.Add("@P_IDPROVEEDOR", detCompra.IdProveedor);
            cmd.Parameters.Add("@P_IDINSUMO", detCompra.IdInsumo);
            cmd.Parameters.Add("@P_IDCOMPRA",detCompra.IdCompra);

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



        public int traerStockInsumo (int idInsumo)
        {
            string sqlCommand = $"SELECT existencia FROM inusumo WHERE idInsumo = {idInsumo}";
            return Convert.ToInt32(con.RunOracleExecuteScalar(sqlCommand));
        } 


        public void ActualizarExistencia(int idInsumo, int cantidadEntrada)
        {
            string sqlCommnad = $"UPDATE insumo SET existencia = existencia + {cantidadEntrada} WHERE idInsumo = {idInsumo}";
            con.RunOracleNonQuery(sqlCommnad);        
        }

        public int traerPrecioUnitario( string nombreInsumo)
        {
            
            string sqlCommand = $"SELECT PRECIOUNITARIO FROM insumo WHERE  NOMBREINSUMO = '{nombreInsumo}'";
            return Convert.ToInt32(con.RunOracleExecuteScalar(sqlCommand));
            
        }



        public List<Usuario> GetUsuariosList()
        {
            List<Usuario> listaUsuario = new();
            string sqlCommand = "SELECT USUARIO.IDUSUARIO AS IDUSUARIO,TU.DESCRIPCION AS TIPOUSUARIO, USUARIO.CORREO AS CORREO, USUARIO.CONTRASENA AS CONTRASENA, USUARIO.NOMBRE AS NOMBRES, USUARIO.APELLIDOS AS APELLIDOS, USUARIO.DIRECCION AS DIRECCION, USUARIO.RUN AS RUN ,USUARIO.NOMBREUSUARIO AS NOMBREUSUARIO FROM USUARIO usuario INNER JOIN TIPOUSUARIO TU ON TU.IDTIPOUSUARIO = usuario.IDTIPOUSUARIO ORDER BY 1 ASC";
            foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
            {
                Usuario usuario = new Usuario
                {
                   IdUsuario = Convert.ToInt32(dr["IDUSUARIO"]),
                   DescripcionTipoUsuario = dr["TIPOUSUARIO"].ToString(),
                   Correo = dr["CORREO"].ToString(),
                   Contrasena = dr["CONTRASENA"].ToString(),
                   Nombre = dr["NOMBRES"].ToString(),
                   Apellido = dr["APELLIDOS"].ToString(),
                   Direccion = dr["DIRECCION"].ToString(),
                   Run = dr["RUN"].ToString(),
                   NombreUsuario = dr["NOMBREUSUARIO"].ToString()
                };
                listaUsuario.Add(usuario);
            }
            return listaUsuario;
        }




        public bool CrearUsuario(Usuario user)
        {
            OracleCommand cmd = new("SP_CREARUSUARIO", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_IDTIPOUSUARIO", user.IdTipoUsuario);
            cmd.Parameters.Add("@P_CORREO", user.Correo);
            cmd.Parameters.Add("@P_CONTRASENA", user.Contrasena);
            cmd.Parameters.Add("@P_NOMBRE", user.Nombre);
            cmd.Parameters.Add("@P_APELLIDO", user.Apellido);
            cmd.Parameters.Add("@P_DIRECCION", user.Direccion);
            cmd.Parameters.Add("@P_RUN", user.Run);
            cmd.Parameters.Add("@P_NOMBREUSUARIO", user.NombreUsuario);


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


        public string GetTipoUsuarioFromDescripcion(string nombreTipoUsuario)
        {
            string sqlCommand = $"SELECT IDTIPOUSUARIO FROM TIPOUSUARIO WHERE DESCRIPCION = '{nombreTipoUsuario}'";
            return con.RunOracleExecuteScalar(sqlCommand).ToString();
        }



        public int GetInsumoFromNombreInsumo (string nombreInsumo)
        {
            string sqlCommand = $"SELECT IDINSUMO FROM INSUMO WHERE NOMBREINSUMO = '{nombreInsumo}'";
            return Convert.ToInt32(con.RunOracleExecuteScalar(sqlCommand));
        }


    



        public bool ActulizarUsuario(Usuario usuario)
        {
            OracleCommand cmd = new("SP_ACTUALIZARUSUARIO", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_IDUSUARIO", usuario.IdUsuario);
            cmd.Parameters.Add("P_IDTIPOUSUARIO", usuario.IdTipoUsuario);
            cmd.Parameters.Add("P_CORREO", usuario.Correo);
            cmd.Parameters.Add("P_CONTRASENA", usuario.Contrasena);
            cmd.Parameters.Add("P_NOMBRES", usuario.Nombre);
            cmd.Parameters.Add("P_APELLIDOS", usuario.Apellido);
            cmd.Parameters.Add("P_DIRECCION", usuario.Direccion);
            cmd.Parameters.Add("P_RUN", usuario.Run);
            cmd.Parameters.Add("P_NOMBREUSUARIO", usuario.NombreUsuario);

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


        public bool EliminarUsuario(Usuario usuario)
        {
            OracleCommand cmd = new("SP_ELIMINARUSUARIO", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_IDUSUARIO", usuario.IdUsuario);
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
