using crudMariano.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudMariano.Database
{
    public class DataProducts
    {
        private readonly SqlConnection conn;

        public DataProducts()
        {
            DataAccess db = new DataAccess();
            conn = db.Getconnection();
        }

        public void InsertProduct(Product product)
        {

            try
            {
                conn.Open();
                string query = @"
                                     insert into products (id , name, price, stock)
                                     values (@id,@Name, @price, @stock )";

                SqlParameter Name = new SqlParameter();
                Name.ParameterName = "@Name";
                Name.Value = product.Name;
                Name.DbType = System.Data.DbType.String;

                SqlParameter Price = new SqlParameter("@price", product.Price);
                SqlParameter Stock = new SqlParameter("@stock", product.Stock);
                SqlParameter Id = new SqlParameter("@id", product.Id);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(Name);
                command.Parameters.Add(Price);
                command.Parameters.Add(Stock);
                command.Parameters.Add(Id);

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

        public Product? GetProduct(int id)
        {
            Product product = null;

            string query = "SELECT TOP 1 * FROM products WHERE id = @id";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString(),
                                Price = Convert.ToInt32(reader["price"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {

                    throw new Exception("Error al consultar el producto", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

            return product;
        }

        public List<Product> GetAllProducts(string searchTxt = null)
        {
            List<Product> productList = new List<Product>();

            try
            {
                conn.Open();

                string query = "select * from products";

                SqlCommand command = new SqlCommand();

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();

                    product.Id = int.Parse(reader["id"].ToString());
                    product.Name = reader["name"].ToString();
                    product.Price = double.Parse(reader["price"].ToString());
                    product.Stock = int.Parse(reader["stock"].ToString());
                    productList.Add(product);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return productList;
        }

        public void UpdateProduc(Product product)
        {
            try
            {
                conn.Open();

                string query = @"UPDATE products 
                                                     SET       name = @Name, 
                                                              price = @Price,
                                                              stock = @Stock
                                                           WHERE id = @Id";

                SqlParameter Id = new SqlParameter("@Id", product.Id);
                SqlParameter Name = new SqlParameter("@Name", product.Name);
                SqlParameter Price = new SqlParameter("@Price", product.Price);
                SqlParameter Stock = new SqlParameter("@Stock", product.Stock);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(Id);
                command.Parameters.Add(Name);
                command.Parameters.Add(Price);
                command.Parameters.Add(Stock);

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

        public void DeleteProduct(int id)
        {

            try
            {
                //conectar
                conn.Open();

                //def query sql
                string query = "DELETE FROM products WHERE id = @Id";

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

