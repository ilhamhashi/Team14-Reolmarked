using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class PaymentRepository : IRepository<Payment>
    {
    private readonly string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Payment> GetAll()
        {
            var Payments = new List<Payment>();
            string query = "SELECT * FROM Payment";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Payments.Add(new Payment
                        (
                            (int)reader["PaymentId"],
                            (DateTime)reader["PaymentDate"],
                            (double)reader["Amount"],
                            (int)reader["PaymentMethodId"],
                            (int)reader["AgreementId"]
                        ));
                    }
                }
            }
            return Payments;
        }

        public Payment GetById(int id)
        {
            Payment Payment = null;
            string query = "SELECT * FROM Payment WHERE PaymentId = @PaymentId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Payment = new Payment
                        (
                            (int)reader["PaymentId"],
                            (DateTime)reader["PaymentDate"],
                            (double)reader["Amount"],
                            (int)reader["PaymentMethodId"]
                            //new RentalAgreement (int)reader["AgreementId"])
                        );
                    }
                }
            }
            return Payment;
        }

        public void Add(Payment entity)
        {
            string query = "INSERT INTO Payment (PaymentId, PaymentDate, Amount, PaymentMethodId, AgreementId) VALUES (@PaymentId, @PaymentDate, @Amount, @PaymentMethodId, @AgreementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentId", entity.PaymentId);
                command.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate);
                command.Parameters.AddWithValue("@Amount", entity.Amount);
                command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
                command.Parameters.AddWithValue("@AgreementId", entity.Rental.AgreementId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Payment entity)
        {
            string query = "UPDATE Payment SET PaymentId = @PaymentId, PaymentDate = @PaymentDate, Amount= @Amount, PaymentMethodId=@PaymentMethodId, AgreementId= @AgreementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DPaymentId", entity.PaymentId);
                command.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate);
                command.Parameters.AddWithValue("@Amount", entity.Amount);
                command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
                command.Parameters.AddWithValue("@AgreementId", entity.Rental.AgreementId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Payment WHERE PaymentId = @PaymentId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}