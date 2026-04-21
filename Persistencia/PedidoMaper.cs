using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class PedidoMaper : IPedidoMap
    {
        static string strcon = @"Server=.\SQLEXPRESS01;Initial Catalog=PracticosBD2;Integrated Security=true;TrustServerCertificate=true;Connect Timeout=60";

        public void Guardar(Pedido p)
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
               
                string sql = "INSERT INTO Pedido (id_cliente, fecha_pedido, total, estado) VALUES (@idC, @fecha, @total, @estado)";
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
            // Implementación necesaria para cumplir con la interfaz
            throw new NotImplementedException();
        }
    }
}
