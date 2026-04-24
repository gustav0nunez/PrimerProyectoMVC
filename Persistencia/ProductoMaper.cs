using Dominio; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient; 

namespace Persistencia
{
    public class ProductoMaper : IProductoMap
    {
        
        static string strcon = @"Server=.\SQLEXPRESS01;Initial Catalog=PracticosBD2;Integrated Security=true;TrustServerCertificate=true;Connect Timeout=60";


        private Producto MapearProducto(SqlDataReader reader)
        {
            return new Producto
            {
                Id_producto = (int)reader["id_producto"],
                Codigo = reader["codigo"].ToString(),
                Nombre = reader["nombre"].ToString(),
                Precio = Convert.ToSingle(reader["precio"]),
            };
        }
        public List<Producto> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();

            
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string sql = "SELECT id_producto, codigo, nombre, precio FROM Producto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto p = MapearProducto(reader);
                            productos.Add(p);
                        }
                    }
                }
            }
            return productos;
        }
    }
}