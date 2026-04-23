using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistencia
{
    public class PedidoMaper : IPedidoMap
    {
       
        static string strcon = @"Server=.\SQLEXPRESS01;Initial Catalog=PracticosBD2;Integrated Security=true;TrustServerCertificate=true;Connect Timeout=60";


        public void Guardar(Pedido p)
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string sql = "INSERT INTO Pedido (cliente_id, fecha_hora, total, estado) VALUES (@idC, @fecha, @total, @estado)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@idC", p.IdCliente);
                cmd.Parameters.AddWithValue("@fecha", p.Fecha);
                cmd.Parameters.AddWithValue("@total", p.Total);
                cmd.Parameters.AddWithValue("@estado", p.Estado);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public List<Pedido> ObtenerTodos()
        {
            List<Pedido> lista = new List<Pedido>();
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string sql = "SELECT id_pedido, cliente_id, fecha_hora, total, estado FROM Pedido";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(MapearPedido(reader));
                    }
                }
            }
            return lista;
        }

        
        public Pedido ObtenerPorId(int id)
        {
            Pedido p = null;
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string sql = "SELECT id_pedido, cliente_id, fecha_hora, total, estado FROM Pedido WHERE id_pedido = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p = MapearPedido(reader);
                    }
                }
            }
            return p;
        }

        
        public void Eliminar(int id)
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string sql = "DELETE FROM Pedido WHERE id_pedido = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        private Pedido MapearPedido(SqlDataReader reader)
        {
            return new Pedido
            {
                Id = (int)reader["id_pedido"],
                IdCliente = (int)reader["cliente_id"],
                Fecha = Convert.ToDateTime(reader["fecha_hora"]),
                Total = Convert.ToDecimal(reader["total"]),
                Estado = reader["estado"].ToString()
            };
        }
    }
}