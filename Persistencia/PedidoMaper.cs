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
                conn.Open();

                
               using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        
                        string sqlPedido = @"INSERT INTO Pedido (cliente_id, fecha_hora, total, estado) 
                                     VALUES (@idC, @fecha, @total, @estado);
                                     SELECT SCOPE_IDENTITY();";

                        int idGenerado;

                        
                        using (SqlCommand cmdP = new SqlCommand(sqlPedido, conn, trans))
                        {
                            cmdP.Parameters.AddWithValue("@idC", p.IdCliente);
                            cmdP.Parameters.AddWithValue("@fecha", p.Fecha);
                            cmdP.Parameters.AddWithValue("@total", p.Total);
                            cmdP.Parameters.AddWithValue("@estado", "Pendiente");

                            idGenerado = Convert.ToInt32(cmdP.ExecuteScalar());
                        } 

                        
                        foreach (var det in p.DetallesPedido)
                        {
                            string sqlDet = "INSERT INTO Detalle_Pedido (pedido_id, producto_id, cantidad) VALUES (@idP, @prodId, @cant)";

                            
                            using (SqlCommand cmdD = new SqlCommand(sqlDet, conn, trans))
                            {
                                cmdD.Parameters.AddWithValue("@idP", idGenerado);
                                cmdD.Parameters.AddWithValue("@prodId", det.ProductoId);
                                cmdD.Parameters.AddWithValue("@cant", det.Cantidad);

                                cmdD.ExecuteNonQuery();
                            } 
                        }

                        
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback(); 
                        throw new Exception("Error al guardar el pedido y sus detalles: " + ex.Message);
                    }
                } 
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
                string sql = "DELETE FROM Detalle_pedido WHERE pedido_id = @id; DELETE FROM Pedido WHERE id_pedido = @id";
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