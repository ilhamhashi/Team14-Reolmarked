using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class SaleRepository : IRepository<Sale>
    {
        private readonly string _connectionString;
        public SaleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Sale> GetAll()
        {
            var sales = new List<Sale>();
            string query = "SELECT * FROM Sale";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sales.Add(new Sale
                        (
                            (int)reader["SaleId"],
                            (DateTime)reader["SaleDateTime"],
                            (double)reader["SaleGrandTotal"],
                            (bool)reader["IsSalePaid"],
                            (int)reader["SalesPersonId"]
                        ));
                    }
                }
            }
            return sales;
        }

        public Sale GetById(int id)
        {
            Sale sale = null;
            string query = "SELECT * FROM Sale WHERE SaleId = @SaleId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SaleId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sale = new Sale
                        (
                            (int)reader["SaleId"],
                            (DateTime)reader["SaleDateTime"],
                            (double)reader["SaleGrandTotal"],
                            (bool)reader["IsSalePaid"],
                            (int)reader["SalesPersonId"]
                        );
                    }
                }
            }
            return sale;
        }

        public void Add(Sale entity)
        {
            string query = "INSERT INTO Sale (SaleId, SaleDateTime, SaleGrandTotal, IsSalePaid, SalesPersonId) VALUES (@SaleId, @SaleDateTime, @SaleGrandTotal, @IsSalePaid, @SalesPersonId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SaleId", entity.SaleId);
                command.Parameters.AddWithValue("@SaleDateTime", entity.DateTime);
                command.Parameters.AddWithValue("@SaleGrandTotal", entity.GrandTotal);
                command.Parameters.AddWithValue("@IsSalePaid", entity.IsPaid);
                command.Parameters.AddWithValue("@SalesPersonId", entity.SalesPersonId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(Sale entity)
        {
            string query = "UPDATE Sale SET SaleId = @SaleId, SaleDateTime = @SaleDateTime, SaleGrandTotal = @SaleGrandTotal, IsSalePaid = @IsSalePaid, SalesPersonId = @SalesPersonId WHERE SaleId = @SaleId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SaleId", entity.SaleId);
                command.Parameters.AddWithValue("@SaleDateTime", entity.DateTime);
                command.Parameters.AddWithValue("@SaleGrandTotal", entity.GrandTotal);
                command.Parameters.AddWithValue("@IsSalePaid", entity.IsPaid);
                command.Parameters.AddWithValue("@SalesPersonId", entity.SalesPersonId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Sale WHERE SaleId = @SaleId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SaleId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
