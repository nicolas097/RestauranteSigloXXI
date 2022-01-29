using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante.DB;

namespace Restaurant.Core
{
    public class Usuario
    {
        public Conexion con = new ();
        public int IdUsuario { get; set; }  

        public string IdTipoUsuario { get; set; }

        public int idUsuario => Convert.ToInt32(con.RunOracleExecuteScalar($"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{NombreUsuario}'"));

        public string idTipoUsuario => con.RunOracleExecuteScalar($"SELECT DESCRIPCION FROM TIPOUSUARIO WHERE IDTIPOUSUARIO = {idTipoUsuario}").ToString();
        
        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public string DescripcionTipoUsuario { get; set; } 

        public string Nombre { get; set; }

        public string NombreUsuario { get; set; }

        public string nombre => con.RunOracleExecuteScalar($"SELECT NOMBRE FROM USUARIO WHERE NOMBREUSUARIO = '{NombreUsuario}'").ToString();

        public string? Apellido { get; set; }

        public string? Direccion { get; set; }

        public string Run { get; set; }
    }
}
