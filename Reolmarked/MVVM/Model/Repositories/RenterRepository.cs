using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    internal class RenterRepository : IRepository<Renter>
    {
        private readonly string _connectionString;

        public RenterRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Renter> GetAll()
        {
            var renters = new List<Renter>();
            string query = "SELECT * FROM PERSON INNER JOIN RENTER ON PERSON.PersonId=Renter.RenterID;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        renters.Add(new Renter
                        (
                            (int)reader["RenterId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (string)reader["Phone"],
                            (string)reader["Email"],                            
                            (string)reader["Address"],
                            (DateTime)reader["CreationDate"]
                         ));
                    }
                }
            }
            return renters;
        }

        public Renter GetById(int id)
        {
            Renter renter = null;
            string query = "SELECT FROM PERSON INNER JOIN RENTER ON PERSON.PersonId=Renter.RenterID WHERE RenterId = @RenterId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RenterId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        renter = new Renter
                        (
                            (int)reader["RenterId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (string)reader["Phone"],
                            (string)reader["Email"],
                            (string)reader["Address"],
                            (DateTime)reader["CreationDate"]
                        );
                    }
                }
            }
            return renter;
        }

        public void Add(Renter entity)
        {
            string query = "INSERT INTO RENTER (RenterId, Address, Phone, Email) VALUES (@RenterId, @Address, @Phone, @Email)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RenterId", entity.PersonId);
                command.Parameters.AddWithValue("@Address", entity.Address);
                command.Parameters.AddWithValue("@Phone", entity.Phone);
                command.Parameters.AddWithValue("@Email", entity.Email);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('RENTER') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(Renter entity)
        {
            string query = "UPDATE RENTER SET Phone = @Phone, Email = @Email, Address = @Address WHERE RenterId = @RenterId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RenterId", entity.PersonId);
                command.Parameters.AddWithValue("@Address", entity.Address);
                command.Parameters.AddWithValue("@Phone", entity.Phone);
                command.Parameters.AddWithValue("@Email", entity.Email);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM RENTER WHERE RenterId = @RenterId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RenterId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
