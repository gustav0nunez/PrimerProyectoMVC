using Dominio;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Persistencia
{
    public class ClienteMaper : IClienteMap
    {
        static string strcon = @"Server=.\SQLEXPRESS01;Initial Catalog=PracticosBD2;Integrated Security=true;TrustServerCertificate=true;Connect Timeout=60";

        private Cliente MapearCliente(SqlDataReader reader)
        {
            return new Cliente
            {
                Id = (int)reader["id_cliente"],
                Nombre = reader["nombre_completo"].ToString(),
                Email = reader["email"].ToString(),
                Direccion = reader["dirección"].ToString(),
                FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"])
            };
        }
        public void Modificar(Cliente c)
        {

            //1- CONEXION: SQLCONNECTION
            SqlConnection conn = new SqlConnection(strcon);
            SqlTransaction tran = null;

            try
            {
                //2- ABRIR CONEXION
                conn.Open();
                //Iniciar transaccions
                tran = conn.BeginTransaction();

                //3- COMANDO: SQLCOMMAND


                string sql = "UPDATE Cliente SET nombre_completo = @nom, FECHA_NACIMIENTO = @fec, " +
                 "email = @email, dirección = @dir WHERE id_cliente = @id";

                var cmd = new SqlCommand(sql, conn, tran);

                cmd.Parameters.AddWithValue("@id", c.Id);
                cmd.Parameters.AddWithValue("@nom", c.Nombre);
                cmd.Parameters.AddWithValue("@fec", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@email", c.Email);
                cmd.Parameters.AddWithValue("@dir", c.Direccion);


                //4- EJECUTAR SENTENCIA
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception)
            {
                tran?.Rollback();
                throw;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    //CERRAR CONEXION
                    conn.Close();
                }
            }
        }
        public void Eliminar(int id)
        {
            //1- CONEXION: SQLCONNECTION
            SqlConnection conn = new SqlConnection(strcon);
            SqlTransaction tran = null;

            try
            {
                //2- ABRIR CONEXION
                conn.Open();
                tran = conn.BeginTransaction();

                //3- COMANDO: SQLCOMMAND
                string sql = "DELETE FROM Cliente WHERE id_cliente = @id";
                var cmd = new SqlCommand(sql, conn, tran);
                cmd.Parameters.AddWithValue("@id", id);

                //4- EJECUTAR SENTENCIA
                cmd.ExecuteNonQuery();

                
                tran.Commit();
            }
            catch (Exception ex)
            {
                
                if (tran != null) tran.Rollback();

                Console.WriteLine("Error en Mapper: " + ex.Message);

               
                throw new Exception("Error de persistencia al eliminar: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public Cliente ObtenerPorId(int id)
        {
           
            SqlConnection conn = new SqlConnection(strcon);
            Cliente encontrado = null;

            try
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Cliente WHERE id_cliente = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {


                    encontrado = MapearCliente(reader);

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de persistencia: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return encontrado;
        }
        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> lista = new List<Cliente>();

            // 1- CONEXION: El 'using' asegura que se cierre SI o SI al terminar
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                try
                {
                    // 2- ABRIR CONEXION
                    conn.Open();

                    // 3- COMANDO
                    string sql = "SELECT id_cliente, nombre_completo, email, dirección, fecha_nacimiento FROM Cliente";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // 4- EJECUTAR Y PROCESAR: El reader también necesita su 'using'
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Usamos el mapeador pro
                            Cliente c = MapearCliente(reader);
                            lista.Add(c);
                        }
                    } // Aquí se libera el Reader inmediatamente
                }
                catch (Exception ex)
                {
                    // Relanzamos para que la capa Web sepa qué pasó
                    throw new Exception("Error en la base de datos: " + ex.Message);
                }
            } // Aquí la conexión se cierra automáticamente. ¡Se acabaron los bloqueos!

            return lista;
        }
        public void Guardar(Cliente c)
        {
       

            //1- CONEXION: SQLCONNECTION
            SqlConnection conn = new SqlConnection(strcon);

            SqlTransaction tran = null;


            try
            {
                //2- ABRIR CONEXION
                conn.Open();
                //Iniciar transaccions
                tran = conn.BeginTransaction();

                //3- COMANDO: SQLCOMMAND

                string sql = "INSERT INTO Cliente (nombre_completo, FECHA_NACIMIENTO, email, dirección) " +
                     "VALUES (@nom, @fec, @email, @dir);SELECT SCOPE_IDENTITY();";




                var cmd = new SqlCommand(sql, conn, tran);

                cmd.Parameters.AddWithValue("@nom", c.Nombre);
                cmd.Parameters.AddWithValue("@fec", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@email", c.Email);
                cmd.Parameters.AddWithValue("@dir", c.Direccion);




                //4- EJECUTAR SENTENCIA
                int idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                c.Id = idGenerado;


                tran.Commit();
            }
            catch (Exception)
            {
                tran?.Rollback();
                throw;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    //CERRAR CONEXION
                    conn.Close();
                }
            }
        }
        public Cliente ObtenerClientePorMail(string mail)
        {
            SqlConnection conn = new SqlConnection(strcon);
            Cliente encontrado = null;

            try
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Cliente WHERE email = @mail", conn);
                cmd.Parameters.AddWithValue("@mail", mail);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    encontrado = MapearCliente(reader);

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de persistencia: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return encontrado;
        
    }
    }
}
    

