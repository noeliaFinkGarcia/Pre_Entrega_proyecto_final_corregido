using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Entrega_proyecto_final
{
    public class ProductoHandler
    {

        // ------------------ Creo la cadena de conexión a la base ------------------------------------------

       static public string connectionString = "Data Source=DESKTOP-G0AE57K;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection conexion = new SqlConnection(connectionString);
        public List<Producto> ObtenerProductos()
        {

            List<Producto> productos = new List<Producto>();
            using (conexion)
            {
                // ---------- Creo el comando -------------------------------------------

                SqlCommand comando = new SqlCommand("SELECT * FROM Producto", conexion);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto productoTemp = new Producto();
                        productoTemp.IdProducto = reader.GetInt64(0);
                        productoTemp.Descripciones = reader.GetString(1);
                        productoTemp.Costo = reader.GetDecimal(2);
                        productoTemp.PrecioVenta = reader.GetDecimal(3);
                        productoTemp.Stock = reader.GetInt32(4);
                        productoTemp.IdUsuario = reader.GetInt64(5);

                        productos.Add(productoTemp);

                    }
                }
                return productos;
            }
        }

        public Producto ObtenerProductoPorId (long id)
        {
            Producto productoPedido = new Producto();
            using (conexion)
            {
                //------------------------------ Creo el comando ------------------------------------------

                SqlCommand comandoProducto = new SqlCommand("SELECT * from Producto WHERE IdProducto=@id", conexion);

                //---------------------------Creo el parametro -------------------------------------------

                SqlParameter idParametro = new SqlParameter();
                idParametro.Value = id;
                idParametro.SqlDbType = SqlDbType.BigInt;
                idParametro.ParameterName = "IdProducto";

                // ---------------- Al comando le paso el parámetro --------------------------------

                comandoProducto.Parameters.Add(idParametro);
                conexion.Open();
                SqlDataReader reader = comandoProducto.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    productoPedido.IdProducto = reader.GetInt64(0);
                    productoPedido.Descripciones = reader.GetString(1);
                    productoPedido.Costo = reader.GetDecimal(2);
                    productoPedido.PrecioVenta = reader.GetDecimal(3);
                    productoPedido.Stock = reader.GetInt32(4);
                    productoPedido.IdUsuario = reader.GetInt64(5);

                }
            }
            return productoPedido;
        }


        public Producto ObtenerProductoPorDescripcion(string descripcion)
        {
            Producto productoPedidoDescrip = new Producto();
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand comandoProductoDescrip = new SqlCommand("SELECT * from Producto WHERE Descripciones= @descripcion", conexion);
                SqlParameter descripcionParametro = new SqlParameter();
                descripcionParametro.Value = descripcion;
                descripcionParametro.SqlDbType = SqlDbType.VarChar;
                descripcionParametro.ParameterName = "Descripciones";

                comandoProductoDescrip.Parameters.Add(descripcionParametro);
                conexion.Open();
                SqlDataReader reader = comandoProductoDescrip.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    productoPedidoDescrip.IdProducto = reader.GetInt64(0);
                    productoPedidoDescrip.Descripciones = reader.GetString(1);
                    productoPedidoDescrip.Costo = reader.GetDecimal(2);
                    productoPedidoDescrip.PrecioVenta = reader.GetDecimal(3);
                    productoPedidoDescrip.Stock = reader.GetInt32(4);
                    productoPedidoDescrip.IdUsuario = reader.GetInt64(5);

                }
            }
            return productoPedidoDescrip;
        }

        public int InsertarProducto (Producto productoParaInsertar)
        {
            using (conexion)
            {
                SqlCommand comandoInsertar = new SqlCommand("INSERT into Producto (Descripciones,costo,precioVenta,Stock, IdUsuario)" +
                    "VALUES (@descripciones,@costo,@precioVenta,@stock,@IdUsuario)", conexion);
                comandoInsertar.Parameters.AddWithValue("@descripciones", productoParaInsertar.Descripciones);
                comandoInsertar.Parameters.AddWithValue("@costo", productoParaInsertar.Costo);
                comandoInsertar.Parameters.AddWithValue("@precioVenta", productoParaInsertar.PrecioVenta);
                comandoInsertar.Parameters.AddWithValue("@stock", productoParaInsertar.Stock);
                comandoInsertar.Parameters.AddWithValue("@idUsuario", productoParaInsertar.IdUsuario);
                conexion.Open();
                return comandoInsertar.ExecuteNonQuery();
            }
        }
// Elimino de ProductoVendido el producto que corresponde al id de la tabla Producto. Luego elimino el producto con el id especificado
// en la firma del método

        public int EliminarProducto (int idProductoEliminar, string descripcionProducto)
        {
            using (conexion)
            {
                SqlCommand comandoEliminar = new SqlCommand("DELETE FROM ProductoVendido WHERE IdProducto =@idProductoEliminar ", conexion);
                SqlCommand comandoEliminarEnProductoVendido = new SqlCommand("DELETE FROM Producto WHERE Descripciones =" +
                    " '@descripcionProducto'", conexion);
                conexion.Open();
                return comandoEliminar.ExecuteNonQuery();
            }
        }

        public List<Producto> MostrarProductoPorIdUsuario(long idUsuario)
        {
            List<Producto> productosUsuario = new List<Producto>();
            using (conexion)
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE IdUsuario= @idUsuario", conexion);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto productoTemp = new Producto();
                        productoTemp.IdProducto = reader.GetInt64(0);
                        productoTemp.Descripciones = reader.GetString(1);
                        productoTemp.Costo = reader.GetDecimal(2);
                        productoTemp.PrecioVenta = reader.GetDecimal(3);
                        productoTemp.Stock = reader.GetInt32(4);
                        productoTemp.IdUsuario = reader.GetInt64(5);

                        productosUsuario.Add(productoTemp);

                    }
                }
                return productosUsuario;
            }
        }
       
    }
 }
    
    

