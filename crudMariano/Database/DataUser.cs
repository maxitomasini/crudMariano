using crudMariano.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudMariano.Database
{
    public class DataUser
    {
        private readonly SqlConnection conn; 

        public DataUser() {
            DataAccess db  = new DataAccess();
            conn = db.Getconnection();
        }

        public void InsertContact(User user)
        {
            try
            {
                /* conn.Open();
                 string query = @"
                                     insert into Contacts (FirstName, LastName, Phone, Address)
                                     values (@FirstName, @LastName, @Phone, @Address)";

                 SqlParameter firstName = new SqlParameter();
                 firstName.ParameterName = "@FirstName";
                 firstName.Value = contact.FirstName;
                 firstName.DbType = System.Data.DbType.String;

                 SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                 SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                 SqlParameter address = new SqlParameter("@Address", contact.Address);

                 SqlCommand command = new SqlCommand(query, conn);

                 command.Parameters.Add(firstName);
                 command.Parameters.Add(lastName);
                 command.Parameters.Add(phone);
                 command.Parameters.Add(address);

                 command.ExecuteNonQuery();*/


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
        public User? GetUser(string userName = null)
        {
            User user = null;

            string query = "SELECT TOP 1 id, user_name, password FROM Users WHERE user_name = @UserName";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@UserName", userName);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                UserName = reader["user_name"].ToString(),
                                Password = reader["password"].ToString()
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

            return user;
        }

        public List<User> GetUsers(string searchTxt = null)
        {
            List<User> userList = new List<User>();

            try
            {
                  conn.Open();

                  string query = "select * from users";
                
                  SqlCommand command = new SqlCommand();

                 /* if (!string.IsNullOrEmpty(searchTxt))
                  {
                      query += @" WHERE FirstName LIKE @search 
                                      OR LastName LIKE @search 
                                      OR Phone    LIKE @search
                                      OR Address  LIKE @search";

                      command.Parameters.Add(new SqlParameter("@search", $"%{searchTxt}%"));
                  }*/

                  command.CommandText = query;
                  command.Connection = conn;

                  SqlDataReader reader = command.ExecuteReader();

                  while (reader.Read())
                  {
                  User user = new User();

                      user.Id = int.Parse(reader["id"].ToString());
                      user.UserName = reader["user_name"].ToString();
                      user.Role = reader["role"].ToString();
                      userList.Add(user);
                  }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return userList;
        }

        public void UpdateContact(User user)
        {
            try
            {
                /* conn.Open();

                 string query = @"UPDATE Contacts  
                                                     SET FirstName = @FirstName, 
                                                          LastName = @LastName,
                                                             Phone = @Phone,
                                                           Address = @Address
                                                          WHERE Id = @Id";

                 SqlParameter id = new SqlParameter("@Id", contact.Id);
                 SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                 SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                 SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                 SqlParameter address = new SqlParameter("@Address", contact.Address);

                 SqlCommand command = new SqlCommand(query, conn);

                 command.Parameters.Add(id);
                 command.Parameters.Add(firstName);
                 command.Parameters.Add(lastName);
                 command.Parameters.Add(phone);
                 command.Parameters.Add(address);

                 command.ExecuteNonQuery();*/

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

        public void DeleteContact(int id)
        {

            try
            {
                //conectar
                conn.Open();

                //def query sql
                string query = "DELETE FROM Contacts WHERE Id = @Id";

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
