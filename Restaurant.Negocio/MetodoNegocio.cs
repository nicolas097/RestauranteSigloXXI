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
using System.Reflection;

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


        public List<Reserva> GetReserva()
        {
            List<Reserva> listaReserva = new List<Reserva>();

            string sqlCommand = "select R.idreserva AS NUMERORESERVA, R.cantidadpersona as CANTIDADPERSONA, R.FECHA AS FECHA, R.IDMESA AS NUMEROMESA, R.IDUSUARIO AS NUMEROUSUARIO, u.correo AS CORREOUSUARIO from reserva R INNER JOIN USUARIO U ON u.idusuario = r.idusuario";

            foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
            {
                Reserva r = new();
                r.IdReserva = Convert.ToInt32(dr["NUMERORESERVA"]);
                r.CantidadPersona = Convert.ToInt32(dr["CANTIDADPERSONA"]);
                r.FechaReserva = Convert.ToDateTime(dr["FECHA"].ToString());
                r.IdMesa = Convert.ToInt32(dr["NUMEROMESA"]);
                r.IdUsuario = Convert.ToInt32(dr["NUMEROUSUARIO"]);
                listaReserva.Add(r);

            }

            return listaReserva;


        }


        //public List<Mesa> GetMesaList()
        //{
        //    List<Mesa> listaMesa = new();
        //    string sqlCommand = "select m.idMesa as NúmeroMesa , m.cantidadSilla as CantidadSilla, m.idEstado as idEstadoMesa ,estMesa.estado as EstadoMesa FROM MESA m inner join estadoMesa estMesa on estmesa.idEstado = m.idestado ORDER BY 1 ASC";
        //    foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
        //    {
        //        Mesa mesa = new Mesa();
        //        mesa.IdMesa = Convert.ToInt32(dr["NúmeroMesa"]);
        //        mesa.CantSilla = Convert.ToInt32(dr["CantidadSilla"]);
        //        mesa.idEstado = Convert.ToChar(dr["idEstadoMesa"]);
        //        listaMesa.Add(mesa);

        //    }
        //    return listaMesa;
        //}


        public List<string> GetCategoriaStrings()
        {
            string sqlCommand = "select descripcion from categoriaInsumo Order by idCategoria asc";
            List<string> listaCategoria = con.OracleToDataTable(sqlCommand).AsEnumerable().Select(x => x.Field<string>(0)).ToList();
            return listaCategoria;
        }

        //public List<string> GetTipoEstado()
        //{
        //    string sqlCommand = "SELECT estado FROM estadoMesa";
        //    List<string> listaEstado = con.OracleToDataTable(sqlCommand).AsEnumerable().Select(x => x.Field<string>(0)).ToList();
        //    return listaEstado;
        //}

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


        public bool CambioEsstadoPedidoTablero(Pedido ped)
        {
            OracleCommand cmd = new("sp_estadoPedidoTablero", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_IDPEDIDO", ped.IdPedido);

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

            compraInsumo.IdCompra = GenerateId("idCompra", "CompraInsumo");
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


        public int ValorTotalFromCompraInsumo(int idCompra)
        {
            string sqlCommand = $"SELECT CANTIDAD FROM DETALLECOMPRA WHERE IDCOMPRA = {idCompra}";
            return Convert.ToInt32(con.RunOracleExecuteScalar(sqlCommand));
        }

        public bool CrearDetalleCompra(DetalleCompra detCompra)
        {
            detCompra.IdCompra = GenerateId("idCompra", "DETALLECOMPRA");
            OracleCommand cmd = new("SP_CREARDETALLECOMPRA", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_CANTIDAD", detCompra.cantidad);
            cmd.Parameters.Add("@P_IDCATEGORIA", detCompra.IdCategoria);
            cmd.Parameters.Add("@P_IDPROVEEDOR", detCompra.IdProveedor);
            cmd.Parameters.Add("@P_IDINSUMO", detCompra.IdInsumo);
            cmd.Parameters.Add("@P_IDCOMPRA", detCompra.IdCompra);

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



        public int traerStockInsumo(int idInsumo)
        {
            string sqlCommand = $"SELECT existencia FROM inusumo WHERE idInsumo = {idInsumo}";
            return Convert.ToInt32(con.RunOracleExecuteScalar(sqlCommand));
        }


        public void ActualizarExistencia(int idInsumo, int cantidadEntrada)
        {
            string sqlCommnad = $"UPDATE insumo SET existencia = existencia + {cantidadEntrada} WHERE idInsumo = {idInsumo}";
            con.RunOracleNonQuery(sqlCommnad);
        }

        public int traerPrecioUnitario(string nombreInsumo)
        {

            string sqlCommand = $"SELECT PRECIOUNITARIO FROM insumo WHERE  NOMBREINSUMO = '{nombreInsumo}'";
            return Convert.ToInt32(con.RunOracleExecuteScalar(sqlCommand));

        }


        public int EstadoPedido (int idOrden)
        {
            string sqlCommand = $"SELECT IDESTADOPEDIDO FROM PEDIDO WHERE = {idOrden}";
            return Convert.ToInt32(con.RunOracleExecuteScalar(sqlCommand));
        }



        public List<Usuario> GetUsuariosList()
        {
            List<Usuario> listaUsuario = new();
            string sqlCommand = "SELECT USUARIO.IDUSUARIO AS IDUSUARIO,TU.DESCRIPCION AS TIPOUSUARIO, USUARIO.CORREO AS CORREO, USUARIO.CONTRASENA AS CONTRASENA, USUARIO.NOMBRES AS NOMBRES, USUARIO.APELLIDOS AS APELLIDOS, USUARIO.DIRECCION AS DIRECCION, USUARIO.RUN AS RUN ,USUARIO.NOMBREUSUARIO AS NOMBREUSUARIO FROM USUARIO usuario INNER JOIN TIPOUSUARIO TU ON TU.IDTIPOUSUARIO = usuario.IDTIPOUSUARIO ORDER BY 1 ASC";
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



        public int GetInsumoFromNombreInsumo(string nombreInsumo)
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


        public bool InsertarPedido( Pedido ped)
        {

            string sqlcommandpedido = $"INSERT INTO PEDIDO VALUES ({ped.IdPedido}, {ped.IdMesa}, TO_DATE('{ped.fecha}', 'DD-MM-YYYY HH24:MI:SS'), {ped.TotalBruto}, {ped.TotalNeto}, 'PEN' , {ped.TotalIVA})";


            try
            {
                con.RunOracleNonQuery(sqlcommandpedido);

                return true;
            }
            catch 
            {

                return false;
            }

        }


        public bool InsertarDetPedido(DetallePedido detp)
        {

            string sqlCommand = $"INSERT INTO DETALLEPEDIDO VALUES ({detp.IdPedido},{detp.IdProducto},{detp.Cantidad},0 )";
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


        public List<Plato> ListarPlatos()
        {
            List<Plato> pla = new List<Plato>();

            string sqlCommnad = "SELECT * FROM PRODUCTO";
            foreach (DataRow dr in con.OracleToDataTable(sqlCommnad).Rows)
            {
                Plato plato = new Plato();
                plato.IdPlato = Convert.ToInt32(dr["IDPLATO"]);
                plato.Descripcion = dr["DESCRIPCION"].ToString();
                plato.Precio = Convert.ToInt32(dr["PRECIO"]);
                try
                {
                    plato.ImagenPlato = (byte[])dr["IMAGEN"];

                }
                catch
                {
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Recursos\SinImagen.png");
                    plato.ImagenPlato = File.ReadAllBytes(path);
                }
                pla.Add(plato);
            }
            return pla;
        }


        public List<Pedido> GetPedidoCuenta()
        {
            List<Pedido> ListaCuenta = new List<Pedido>();

            string sqlCommand = "SELECT * FROM PEDIDO WHERE IDESTADOPEDIDO = 'DEU'";
            foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
            {
                Pedido ped = new();
                ped.IdPedido = Convert.ToInt32(dr["IDPEDIDO"]);
                ped.IdMesa = Convert.ToInt32(dr["IDMESA"]);
                ped.TotalBruto = Convert.ToInt32(dr["SUBTOTAL"]);
                ped.TotalIVA = Convert.ToInt32(dr["IVA"]);
                ped.TotalNeto = Convert.ToInt32(dr["TOTAL"]);
                ped.ListaDetallePedido = GetDetallePedido(ped.IdPedido);
                ListaCuenta.Add(ped);

            }

            return ListaCuenta; 
        }


        

        public List<Pedido> GetPedido()
        {
            List<Pedido> pedidos = new List<Pedido>();

            string sqlCommand = "SELECT * FROM PEDIDO WHERE IDESTADOPEDIDO = 'PEN'";
            foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
            {
                Pedido ped = new();
                ped.IdPedido = Convert.ToInt32(dr["IDPEDIDO"]);
                ped.IdMesa = Convert.ToInt32(dr["IDMESA"]);
                ped.ListaDetallePedido = GetDetallePedido(ped.IdPedido);
                pedidos.Add(ped);   
                
            }

            return pedidos;
        }



        private List<DetallePedido> GetDetallePedido(int id_)
        {
            List<DetallePedido> detPed = new List<DetallePedido>();

            string sqlCommand = $"SELECT * FROM DETALLEPEDIDO WHERE IDPEDIDO = {id_}";

            foreach (DataRow dr in con.OracleToDataTable(sqlCommand).Rows)
            {
                DetallePedido det = new()
                {
                    IdPedido = Convert.ToInt32(dr["IDPEDIDO"]),
                    Cantidad = Convert.ToInt32(dr["CANTIDAD"]),
                    plato = GetProductoFromID(Convert.ToInt32(dr["IDPRODUCTO"])),

                };
                detPed.Add(det);    
            }

            return detPed;
        }


        public Plato GetProductoFromID(int id)
        {
            Plato pla = new Plato();

            string sqlCommnad = $"SELECT P.IDPLATO,P.DESCRIPCION,P.PRECIO, P.PRECIO * DETPED.CANTIDAD AS SUBTOTAL FROM detallepedido DETPED INNER JOIN producto P ON p.idplato = detped.idproducto WHERE detped.idproducto = {id}";

            foreach (DataRow dr in con.OracleToDataTable(sqlCommnad).Rows)
            {
                Plato plato1 = new()
                {
                    IdPlato = Convert.ToInt32(dr["IDPLATO"]),
                    Descripcion = dr["DESCRIPCION"].ToString(),
                    Precio = Convert.ToInt32(dr["PRECIO"]),
                    Subtotal = Convert.ToInt32(dr["SUBTOTAL"])


                };

                pla = plato1;

            }

            return pla;
        }


        //public List<DetallePedido> ListarPedido()
        //{
        //    List<DetallePedido> detallePedidos = new List<DetallePedido>();

        //    string sqlCommnad = "select  ped.idMesa as numeroMesa, detp.cantidad as Cantidad, p.descripcion as Nombre from detallepedido detP inner join pedido ped on ped.idpedido = detP.idpedido inner join producto p on p.idplato = detp.idproducto where ped.idestadoPedido = 'PEN'";

        //    foreach (DataRow dr in con.OracleToDataTable(sqlCommnad).Rows)
        //    {
        //        DetallePedido det = new DetallePedido();
        //        det.IdPedido = Convert.ToInt32(dr["numeroMesa"]);
        //        det.Cantidad = Convert.ToInt32(dr["Cantidad"]);
               
        //        detallePedidos.Add(det);
        //    }

            
        //    return detallePedidos;  

        //}


        


    }


    
   
}
