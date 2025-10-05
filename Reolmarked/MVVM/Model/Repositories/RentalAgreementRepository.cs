using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

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
            string query = "SELECT * FROM RENTALAGREEMENT";

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
                            (AgreementStatus)Enum.Parse(typeof(AgreementStatus), (string)reader["Status"]),
                            (int)reader["RenterId"],
                            (int)reader["EmployeeId"]
                        ));
                    }
                }
            }
            return rentals;
        }

        public RentalAgreement GetById(int id)
        {
            RentalAgreement rental = null;
            string query = "SELECT * FROM RENTALAGREEMENT WHERE AgreementId = @AgreementId";

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
                            (AgreementStatus)Enum.Parse(typeof(AgreementStatus), (string)reader["Status"]),
                            (int)reader["RenterId"],
                            (int)reader["EmployeeId"]
                        );
                    }
                }
            }
            return rental;
        }

        public void Add(RentalAgreement entity)
        {
            string query = "INSERT INTO RENTALAGREEMENT (Status, RenterId, EmployeeId) VALUES (@Status, @RenterId, @EmployeeId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", entity.Status);
                command.Parameters.AddWithValue("@RenterId", entity.RenterId);
                command.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('RENTALAGREEMENT') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(RentalAgreement entity)
        {
            string query = "UPDATE RENTALAGREEMENT SET Status = @Status, RenterId = @RenterId, EmployeeId = @EmployeeId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@Status", entity.Status);
                command.Parameters.AddWithValue("@RenterId", entity.RenterId);
                command.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM RENTALAGREEMENT WHERE AgreementId = @AgreementId";

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
