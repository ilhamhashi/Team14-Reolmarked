using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class RentalAgreementRepository : IRepository<RentalAgreement>
    {
        private readonly string _connectionString;

        public RentalAgreementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<RentalAgreement> GetAll()
        {
            var rentals = new List<RentalAgreement>();
            string query = "SELECT * FROM RentalAgreement";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentals.Add(new RentalAgreement
                        (
                            (int)reader["AgreementId"],
                            (DateTime)reader["StartDate"],
                            (DateTime)reader["EndDate"],
                            (DateTime)reader["CancelDate"],
                            (double)reader["Total"],
                            (string)reader["Status"]
                        ));
                    }
                }
            }
            return rentals;
        }

        public RentalAgreement GetById(int id)
        {
            RentalAgreement rental = null;
            string query = "SELECT * FROM RentalAgreement WHERE AgreementId = @AgreementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgreementId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        rental = new RentalAgreement
                        (
                            (int)reader["AgreementId"],
                            (DateTime)reader["StartDate"],
                            (DateTime)reader["EndDate"],
                            (DateTime)reader["CancelDate"],
                            (double)reader["Total"],
                            (string)reader["Status"]
                        );
                    }
                }
            }
            return rental;
        }

        public void Add(RentalAgreement entity)
        {
            string query = "INSERT INTO RentalAgreement (AgreementId, StartDate, EndDate, CancelDate, Total, Status) VALUES (@AgreementId, @StartDate, @EndDate, @CancelDate, @Total, @Status)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                command.Parameters.AddWithValue("@EndDate", entity.EndDate);
                command.Parameters.AddWithValue("@CancelDate", entity.CancelDate);
                command.Parameters.AddWithValue("@Total", entity.Total);
                command.Parameters.AddWithValue("@Status", entity.Status);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(RentalAgreement entity)
        {
            string query = "UPDATE RentalAgreement SET AgreementId = @AgreementId, StartDate = @StartDate, EndDate = @EndDate, CancelDate = @CancelDate, Total = @Total, IsActive = @IsActive";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                command.Parameters.AddWithValue("@EndDate", entity.EndDate);
                command.Parameters.AddWithValue("@CancelDate", entity.CancelDate);
                command.Parameters.AddWithValue("@Total", entity.Total);
                command.Parameters.AddWithValue("@Status", entity.Status);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM RentalAgreement WHERE AgreementId = @AgreementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgreementId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
