using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Core;
using Restaurante.DB;

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

        
    }
}
