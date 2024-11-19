using crudMariano.Database.Entities;
using System.Data.SqlClient;


namespace crudMariano.Database
{
    public class DataSaleDetail
    {
        private readonly SqlConnection conn;
       
        public DataSaleDetail()
        {
         DataAccess db = new DataAccess();
         conn = db.Getconnection();
        }

        public void InsertSaleDetail(SaleDetails saleDetail)
        {

            try
            {
                conn.Open();
                string query = @"
                                     insert into saleDetails (id , SaleId, ProductId, quantity, price)
                                     values (@id,@saleId, @producId, @quantity, @price)";

                SqlParameter Name = new SqlParameter();
                Name.ParameterName = "@saleId";
                Name.Value = saleDetail.SaleId;
                Name.DbType = System.Data.DbType.Int32;

                SqlParameter productId = new SqlParameter("@producId", saleDetail.ProductId);
                SqlParameter quantity = new SqlParameter("@quantity", saleDetail.Quantity);
                SqlParameter price = new SqlParameter("@price", saleDetail.Price);
                SqlParameter Id = new SqlParameter("@id", saleDetail.Id);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(Name);
                command.Parameters.Add(productId);
                command.Parameters.Add(quantity);
                command.Parameters.Add(price);
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

        public SaleDetails? GetSaleDetail(int id)
        {
            SaleDetails saleDetails = null;

            string query = "SELECT TOP 1 * FROM saleDetails WHERE id = @id";

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
                            saleDetails = new SaleDetails
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                SaleId = Convert.ToInt32(reader["SaleId"]),
                                ProductId = Convert.ToInt32(reader["productId"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                Price = Convert.ToInt32(reader["price"]),
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {

                    throw new Exception("Error al consultar el venta detalle", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

            return saleDetails;
        }

        public List<SaleDetails> GetAllSaleDetail(string searchTxt = null)
        {
            List<SaleDetails> saleDetailtList = new List<SaleDetails>();

            try
            {
                conn.Open();

                string query = "select * from saleDetails";

                SqlCommand command = new SqlCommand();

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SaleDetails saleDetails = new SaleDetails();

                    saleDetails.Id = int.Parse(reader["id"].ToString());
                    saleDetails.SaleId = int.Parse(reader["SaleId"].ToString());
                    saleDetails.ProductId = int.Parse(reader["ProductId"].ToString());
                    saleDetails.Quantity = int.Parse(reader["quantity"].ToString());
                    saleDetails.Price = int.Parse(reader["price"].ToString());
                    saleDetailtList.Add(saleDetails);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return saleDetailtList;
        }

        public void UpdateSalesDetail(SaleDetails saleDetails)
        {
            try
            {
                conn.Open();

                string query = @"UPDATE saleDetails
                                                     SET        SaleId = @saleId, 
                                                             ProductId = @productId,
                                                              quantity = @quantity
                                                                 price = @price
                                                              WHERE id = @Id";

                SqlParameter Id = new SqlParameter("@Id", saleDetails.Id);
                SqlParameter SaleId = new SqlParameter("@saleId", saleDetails.SaleId);
                SqlParameter ProductId = new SqlParameter("@productId", saleDetails.ProductId);
                SqlParameter Quamtity = new SqlParameter("@quantity", saleDetails.Quantity);
                SqlParameter Price = new SqlParameter("@price", saleDetails.Price);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(Id);
                command.Parameters.Add(SaleId);
                command.Parameters.Add(ProductId);
                command.Parameters.Add(Quamtity);
                command.Parameters.Add(Price);

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

        public void DeleteSaleDetail(int id)
        {

            try
            {
                //conectar
                conn.Open();

                //def query sql
                string query = "DELETE FROM saleDetails WHERE id = @Id";

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
