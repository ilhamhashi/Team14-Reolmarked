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
            string query = "SELECT * FROM SALE";

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
                            (DateTime)reader["Date"],
                            (double)reader["Total"],
                            (bool)reader["IsPaid"],
                            (int)reader["EmployeeId"]
                        ));
                    }
                }
            }
            return sales;
        }

        public Sale GetById(int id)
        {
            Sale sale = null;
            string query = "SELECT * FROM SALE WHERE SaleId = @SaleId";

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
                            (DateTime)reader["Date"],
                            (double)reader["Total"],
                            (bool)reader["IsPaid"],
                            (int)reader["EmployeeId"]
                        );
                    }
                }
            }
            return sale;
        }

        public void Add(Sale entity)
        {
            string query = "INSERT INTO SALE (SaleId, Date, Total, IsPaid, EmployeeId) VALUES (@SaleId, @Date, @Total, @IsPaid, @EmployeeId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SaleId", entity.SaleId);
                command.Parameters.AddWithValue("@Date", entity.Date);
                command.Parameters.AddWithValue("@Total", entity.Total);
                command.Parameters.AddWithValue("@IsPaid", entity.IsPaid);
                command.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('SALE') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }
        public void Update(Sale entity)
        {
            string query = "UPDATE SALE SET SaleId = @SaleId, Date = @Date, Total = @Total, IsPaid = @IsPaid, EmployeeId = @EmployeeId WHERE SaleId = @SaleId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SaleId", entity.SaleId);
                command.Parameters.AddWithValue("@Date", entity.Date);
                command.Parameters.AddWithValue("@Total", entity.Total);
                command.Parameters.AddWithValue("@IsPaid", entity.IsPaid);
                command.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM SALE WHERE SaleId = @SaleId";

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
