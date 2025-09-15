using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class DiscountRepository : IRepository<Discount>
    {
        private readonly string _connectionString;

        public DiscountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Discount> GetAll()
        {
            var Discounts = new List<Discount>();
            string query = "SELECT * FROM Discount";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Discounts.Add(new Discount
                        (
                            (int)reader["DiscountId"],
                            (string)reader["Type"],
                            (double)reader["Rate"]
                        ));
                    }
                }
            }
            return Discounts;
        }

        public Discount GetById(int id)
        {
            Discount Discount = null;
            string query = "SELECT * FROM Discount WHERE DiscountId = @DiscountId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DiscountId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Discount = new Discount
                        (
                            (int)reader["DiscountId"],
                            (string)reader["Type"],
                            (double)reader["Rate"]
                        );
                    }
                }
            }
            return Discount;
        }

        public void Add(Discount entity)
        {
            string query = "INSERT INTO Discount (DiscountId, Type, Rate) VALUES (@DiscountId, @Type, @Rate";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DiscountId", entity.DiscountId);
                command.Parameters.AddWithValue("@Type", entity.Type);
                command.Parameters.AddWithValue("@Rate", entity.Rate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Discount entity)
        {
            string query = "UPDATE Discount SET DiscountId = @DiscountId, Type = @Type, Rate= @Rate";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DiscountId", entity.DiscountId);
                command.Parameters.AddWithValue("@Type", entity.Type);
                command.Parameters.AddWithValue("@Rate", entity.Rate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Discount WHERE DiscountId = @DiscountId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DiscountId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
