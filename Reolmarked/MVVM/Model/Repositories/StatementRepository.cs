using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class StatementRepository : IRepository<RentalStatement>
    {
        private readonly string _connectionString;
        public StatementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<RentalStatement> GetAll()
        {
            var invoices = new List<RentalStatement>();
            string query = "SELECT * FROM RENTALSTATEMENT";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoices.Add(new RentalStatement
                        (
                            (int)reader["StatementId"],
                            (DateTime)reader["Date"],
                            (double)reader["TotalValueSold"],
                            (double)reader["PrepaidRent"],
                            (double)reader["Total"],
                            (bool)reader["IsPaid"],
                            (int)reader["AgreementId"]
                        ));
                    }
                }
            }
            return invoices;
        }

        public RentalStatement GetById(int id)
        {
            RentalStatement invoice = null;
            string query = "SELECT * FROM RENTALSTATEMENT WHERE StatementId = @StatementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatementId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        invoice = new RentalStatement
                        (
                            (int)reader["StatementId"],
                            (DateTime)reader["Date"],
                            (double)reader["TotalValueSold"],
                            (double)reader["PrepaidRent"],
                            (double)reader["Total"],
                            (bool)reader["IsPaid"],
                            (int)reader["AgreementId"]
                        );
                    }
                }
            }
            return invoice;
        }

        public void Add(RentalStatement entity)
        {
            string query = "INSERT INTO RENTALSTATEMENT (StatementId, Date, TotalValueSold, PrepaidRent, Total, IsPaid, AgreementId) VALUES (@StatementId, @Date, @TotalValueSold, @PrepaidRent, @Total, @IsPaid, @AgreementId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatementId", entity.StatementId);
                command.Parameters.AddWithValue("@Date", entity.Date);
                command.Parameters.AddWithValue("@TotalValueSold", entity.TotalValueSold);
                command.Parameters.AddWithValue("@PrepaidRent", entity.PrepaidRent);
                command.Parameters.AddWithValue("@Total", entity.Total);
                command.Parameters.AddWithValue("@IsPaid", entity.IsPaid);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('RENTALSTATEMENT') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(RentalStatement entity)
        {
            string query = "UPDATE RENTALSTATEMENT SET StatementId = @StatementId, Date = @Date, TotalValueSold = @TotalValueSold, PrepaidRent = @PrepaidRent, Total = @Total, IsPaid = @IsPaid, AgreementId = @AgreementId WHERE StatementId = @StatementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatementId", entity.StatementId);
                command.Parameters.AddWithValue("@Date", entity.Date);
                command.Parameters.AddWithValue("@TotalValueSold", entity.TotalValueSold);
                command.Parameters.AddWithValue("@PrepaidRent", entity.PrepaidRent);
                command.Parameters.AddWithValue("@Total", entity.Total);
                command.Parameters.AddWithValue("@IsPaid", entity.IsPaid);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM RENTALSTATEMENT WHERE StatementId = @StatementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatementId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        } 
    }
}
