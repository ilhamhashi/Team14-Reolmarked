using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;

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
            string query = "SELECT * FROM PaymentMethod";

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
                            (string)reader["Name"],
                            (double)reader["Fee"]
                        ));
                    }
                }
            }
            return PaymentMethods;
        }

        public PaymentMethod GetById(int id)
        {
            PaymentMethod PaymentMethod = null;
            string query = "SELECT * FROM PaymentMethod WHERE PaymentMethodId = @PaymentMethodId";

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
                            (string)reader["Name"],
                            (double)reader["Fee"]
                        );
                    }
                }
            }
            return PaymentMethod;
        }

        public void Add(PaymentMethod entity)
        {
            string query = "INSERT INTO PaymentMethod (PaymentMethodId, Name, Fee) VALUES (@PaymentMethodId, @Name, @Fee";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Fee", entity.Fee);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(PaymentMethod entity)
        {
            string query = "UPDATE PaymentMethod SET PaymentMethodId = @PaymentMethodId, Name = @Name, Fee= @Fee";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Fee", entity.Fee);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM PaymentMethod WHERE PaymentMethodId = @PaymentMethodId";

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

