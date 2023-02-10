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
    public class VentaHandler
    {

        static public string connectionString = "Data Source=DESKTOP-G0AE57K;Initial Catalog=SistemaGestion;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
            "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
       public  SqlConnection conexion = new SqlConnection (connectionString);
       

        public List<Venta> ventasPorUsuario (long idUsuario)
        {
            Venta ventaPedida = new Venta();
            List<Venta> ventas = new List<Venta>();

            using (conexion)
            {
                SqlCommand comandoVentas = new SqlCommand("SELECT * FROM Venta WHERE IdUsuario=@idUsuario", conexion);

                SqlParameter ventasPorUsuarioParametro = new SqlParameter();
                ventasPorUsuarioParametro.Value = idUsuario;
                ventasPorUsuarioParametro.SqlDbType = SqlDbType.BigInt;
                ventasPorUsuarioParametro.ParameterName = "IdUsuario";

                comandoVentas.Parameters.Add(ventasPorUsuarioParametro);
                conexion.Open();
                SqlDataReader reader = comandoVentas.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        ventaPedida.Id = reader.GetInt64(0);
                        ventaPedida.Comentarios = reader.GetString(1);
                        ventaPedida.IdUsuario = reader.GetInt64(2);
                    }
                    ventas.Add(ventaPedida);
                }
            }
            return ventas;
        }

    }
    
}
