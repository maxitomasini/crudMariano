using crudMariano.Database.Entities;
using System.Data.SqlClient;


namespace crudMariano.Database
{
    public class DataSale
    {
        private readonly SqlConnection conn;
        public DataSale()
        {
            DataAccess db = new DataAccess();
            conn = db.Getconnection();
        }
    public void InsertSale(Sale sale)
    {

        try
        {
            conn.Open();
            string query = @"
                                     insert into Sales (id , userId, saleDate, total)
                                     values (@id,@userId, @saleDate, @total )";

            SqlParameter userId = new SqlParameter();
            userId.ParameterName = "@userId";
            userId.Value = sale.UserId;
            userId.DbType = System.Data.DbType.String;

            SqlParameter saleDate = new SqlParameter("@saleDate", sale.Date);
            SqlParameter total = new SqlParameter("@total", sale.Total);
            SqlParameter Id = new SqlParameter("@id", sale.Id);

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.Add(userId);
            command.Parameters.Add(saleDate);
            command.Parameters.Add(total);
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

    public Sale? GetSale(int id)
    {
        Sale sale = null;

        string query = "SELECT TOP 1 * FROM Sales WHERE id = @id";

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
                        sale = new Sale
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            UserId = Convert.ToInt32(reader["userId"].ToString()),
                            Date = Convert.ToDateTime(reader["saleDate"]),
                            Total = Convert.ToDouble(reader["total"]),
                        };
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Error al consultar el venta", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        return sale;
    }

    public List<Sale> GetAllSale(string searchTxt = null)
    {
        List<Sale> saleList = new List<Sale>();

        try
        {
            conn.Open();

            string query = "select * from Sales";

            SqlCommand command = new SqlCommand();

            command.CommandText = query;
            command.Connection = conn;

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Sale sale = new Sale();

                sale.Id = int.Parse(reader["id"].ToString());
                sale.UserId = int.Parse(reader["userId"].ToString());
                sale.Date = DateTime.Parse(reader["saleDate"].ToString());
                sale.Total = double.Parse(reader["total"].ToString());
                saleList.Add(sale);
            }

        }
        catch (Exception)
        {

            throw;
        }
        finally { conn.Close(); }
        return saleList;
    }

    public void UpdateSale(Sale sale)
    {
        try
        {
            conn.Open();

            string query = @"UPDATE Sales 
                                                     SET       userId = @Name, 
                                                              saleDate = @Price,
                                                              total = @Stock
                                                           WHERE id = @Id";

            SqlParameter Id = new SqlParameter("@Id", sale.Id);
            SqlParameter Name = new SqlParameter("@Name", sale.UserId);
            SqlParameter Price = new SqlParameter("@Price", sale.Date);
            SqlParameter Stock = new SqlParameter("@Stock", sale.Total);

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

    public void DeleteSale(int id)
    {

        try
        {
            //conectar
            conn.Open();

            //def query sql
            string query = "DELETE FROM Sales WHERE id = @Id";

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

