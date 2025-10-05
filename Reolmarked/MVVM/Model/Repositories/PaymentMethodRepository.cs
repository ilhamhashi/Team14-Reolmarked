using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class PaymentMethodRepository : IRepository<PaymentMethod>
    {
        private readonly string _connectionString;
        public PaymentMethodRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<PaymentMethod> GetAll()
        {
            var PaymentMethods = new List<PaymentMethod>();
            string query = "SELECT * FROM PAYMENTMETHOD";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PaymentMethods.Add(new PaymentMethod
                        (
                            (int)reader["PaymentMethodId"],
                            (string)reader["Name"]
                        ));
                    }
                }
            }
            return PaymentMethods;
        }

        public PaymentMethod GetById(int id)
        {
            PaymentMethod PaymentMethod = null;
            string query = "SELECT * FROM PAYMENTMETHOD WHERE PaymentMethodId = @PaymentMethodId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentMethodId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        PaymentMethod = new PaymentMethod
                        (
                            (int)reader["PaymentMethodId"],
                            (string)reader["Name"]
                        );
                    }
                }
            }
            return PaymentMethod;
        }

        public void Add(PaymentMethod entity)
        {
            string query = "INSERT INTO PAYMENTMETHOD (Name) VALUES (@Name)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", entity.Name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('PAYMENTMETHOD') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }
        public void Update(PaymentMethod entity)
        {
            string query = "UPDATE PAYMENTMETHOD SET PaymentMethodId = @PaymentMethodId, Name = @Name";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
                command.Parameters.AddWithValue("@Name", entity.Name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM PAYMENTMETHOD WHERE PaymentMethodId = @PaymentMethodId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentMethodId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}

