using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Entrega_proyecto_final
{
    public class UsuarioHandler
    {        
       static public string connectionString = "Data Source=DESKTOP-G0AE57K;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public  SqlConnection conexion = new SqlConnection(connectionString);


        public Usuario MostrarUsuario(long idUsuario)
        {
            Usuario usuarioSolicitado = new Usuario();
            using (conexion)
            {
                SqlCommand comandoUsuario = new SqlCommand("SELECT Nombre, Apellido, Mail FROM Usuario WHERE Id= @idUsuario;", conexion);
                SqlParameter parametroUsuario = new SqlParameter();
                parametroUsuario.Value = idUsuario;
                parametroUsuario.SqlDbType = SqlDbType.BigInt;
                parametroUsuario.ParameterName = "Id";

                comandoUsuario.Parameters.Add(parametroUsuario);
                conexion.Open();
                SqlDataReader reader = comandoUsuario.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuarioSolicitado.IdUsuario = reader.GetInt64(0);
                    usuarioSolicitado.Nombre = reader.GetString(1);
                    usuarioSolicitado.Apellido = reader.GetString(2);
                    usuarioSolicitado.Contrasena = reader.GetString(3);
                    usuarioSolicitado.Mail = reader.GetString(4);

                }
            }
            return usuarioSolicitado;
        }


        public Usuario InicioDeSesion(string nombreUsuario, string contrasena)
        {
            Usuario LogIn = new Usuario();

            using (conexion)
            {
                SqlCommand comandoSesion = new SqlCommand("SELECT Nombre, Apellido,Id FROM Usuario WHERE " +
                    "NombreUsuario = '@nombreUsuario' AND Contraseña = '@contrasena' ", conexion);
                SqlParameter parametroUsuario = new SqlParameter();
                parametroUsuario.SqlDbType = SqlDbType.VarChar;
                parametroUsuario.ParameterName = "NombreUsuario";
                parametroUsuario.Value = nombreUsuario;

                SqlParameter parametroContrasena = new SqlParameter();
                parametroContrasena.SqlDbType = SqlDbType.VarChar;
                parametroContrasena.ParameterName = "Contraseña";
                parametroContrasena.Value = contrasena;

                comandoSesion.Parameters.Add(parametroUsuario);
                comandoSesion.Parameters.Add(parametroContrasena);

                SqlDataReader reader = comandoSesion.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    LogIn.IdUsuario = reader.GetInt64(0);
                    LogIn.Nombre = reader.GetString(1);
                    LogIn.Apellido = reader.GetString(2);
                    LogIn.NombreUsuario = reader.GetString(3);
                    LogIn.Contrasena = reader.GetString(4);
                    LogIn.Mail = reader.GetString(5);
                }
            }
            return LogIn;
        }
            
    }
}
    


