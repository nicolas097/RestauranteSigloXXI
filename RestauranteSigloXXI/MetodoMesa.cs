using Oracle.ManagedDataAccess.Client;
using Restaurant.Core;
using Restaurante.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteInterfaz
{
    public class MetodoMesa
    {
        public Conexion con = new();

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


        public bool ActualizarMesa(Mesa mesa)
        {
            OracleCommand cmd = new("SP_ACTUALIZARMESA", con.OracleConnection);
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


        public List<string> GetTipoEstado()
        {
            string sqlCommand = "SELECT estado FROM estadoMesa";
            List<string> listaEstado = con.OracleToDataTable(sqlCommand).AsEnumerable().Select(x => x.Field<string>(0)).ToList();
            return listaEstado;
        }



        public bool CambioEstadoMesa(Mesa mes)
        {
            OracleCommand cmd = new("sp_CambioEstadoMesa", con.OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_IDMESA", mes.IdMesa);

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
