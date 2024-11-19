
using crudMariano.Database.Entities;
using System.Data.SqlClient;


namespace crudMariano.Database
{
    public class DataClient
    {

        private readonly SqlConnection conn;

        public DataClient()
        {
            DataAccess db = new DataAccess();
            conn = db.Getconnection();
        }

        public void InsertClient(Client client)
        {

            try
            {
                 conn.Open();
                 string query = @"
                                     insert into client (id , name, last_name, dni)
                                     values (@id,@Name, @LastName, @dni )";

                 SqlParameter firstName = new SqlParameter();
                 firstName.ParameterName = "@Name";
                 firstName.Value = client.Name;
                 firstName.DbType = System.Data.DbType.String;

                 SqlParameter lastName = new SqlParameter("@LastName", client.LastName);
                 SqlParameter dni = new SqlParameter("@dni", client.Dni);
                 SqlParameter id= new SqlParameter("@id", client.Id);

                SqlCommand command = new SqlCommand(query, conn);

                 command.Parameters.Add(firstName);
                 command.Parameters.Add(lastName);
                 command.Parameters.Add(dni);
                 command.Parameters.Add(id);

                command.ExecuteNonQuery();


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public Client? GetClient(string dni = null)
        {
            Client client = null;

            string query = "SELECT TOP 1 * FROM client WHERE dni = @dni";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@dni", dni);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["user_name"].ToString(),
                                LastName = reader["password"].ToString()
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {

                    throw new Exception("Error al consultar el usuario", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

            return client;
        }

        public List<Client> GetAllClients(string searchTxt = null)
        {
            List<Client> clientList = new List<Client>();

            try
            {
                conn.Open();

                string query = "select * from client";

                SqlCommand command = new SqlCommand();

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Client client = new Client();

                    client.Id = int.Parse(reader["id"].ToString());
                    client.Name = reader["name"].ToString();
                    client.LastName = reader["last_name"].ToString();
                    client.Dni = int.Parse(reader["dni"].ToString());
                    clientList.Add(client);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return clientList;
        }

        public void UpdateClient(Client client)
        {
            try
            {
                 conn.Open();

                 string query = @"UPDATE client  
                                                     SET       Name = @FirstName, 
                                                          last_name = @LastName,
                                                                dni = @dni
                                                           WHERE id = @Id";

                 SqlParameter Id = new SqlParameter("@Id", client.Id);
                 SqlParameter Name = new SqlParameter("@FirstName", client.Name);
                 SqlParameter lastName = new SqlParameter("@LastName", client.LastName);
                 SqlParameter Dni = new SqlParameter("@dni", client.Dni);

                 SqlCommand command = new SqlCommand(query, conn);

                 command.Parameters.Add(Id);
                 command.Parameters.Add(Name);
                 command.Parameters.Add(lastName);
                 command.Parameters.Add(Dni);

                 command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteClient(int id)
        {

            try
            {
                //conectar
                conn.Open();

                //def query sql
                string query = "DELETE FROM client WHERE id = @Id";

                //tratar parametros
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));

                //ejecutar query
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }
    }
}
