﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Entrega_proyecto_final
{
    public class ProductoVendidoHandler
    {
        // public List<ProductoVendido> productosVendidos = new List<ProductoVendido>();

        static public string connectionString = "Data Source=DESKTOP-G0AE57K;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection conexion = new SqlConnection(connectionString);

        public List<ProductoVendido> ObtenerProductosVendidos(long IdUsuario)
        {
            ProductoVendido productoPedido = new ProductoVendido();
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
            using (conexion)
            {
                //------------------------------ Creo el comando ------------------------------------------

                SqlCommand comandoProducto = new SqlCommand("SELECT * FROM ProductoVendido INNER JOIN Venta ON (Venta.Id=ProductoVendido.IdVenta ) " +
                    "WHERE Venta.IdUsuario= @IdUsuario", conexion);

                //---------------------------Creo el parametro -------------------------------------------

                SqlParameter productoVendidoParametro = new SqlParameter();
                productoVendidoParametro.Value = IdUsuario;
                productoVendidoParametro.SqlDbType = SqlDbType.BigInt;
                productoVendidoParametro.ParameterName = "IdUsuario";

                // ---------------- Al comando le paso el parámetro --------------------------------

                comandoProducto.Parameters.Add(productoVendidoParametro);
                conexion.Open();
                SqlDataReader reader = comandoProducto.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productoPedido.Id = reader.GetInt64(0);
                        productoPedido.Stock = reader.GetInt32(1);
                        productoPedido.IdProducto = reader.GetInt64(2);
                        productoPedido.IdVenta = reader.GetInt64(3);
                    }
                    productosVendidos.Add(productoPedido);
                }
            }
            return productosVendidos;
        }
    }
}
