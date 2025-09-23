using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    internal class SalesPersonRepository : IRepository<SalesPerson>
    {
        private readonly string _connectionString;

        public SalesPersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<SalesPerson> GetAll()
        {
            var salesPersons = new List<SalesPerson>();
            string query = "SELECT * FROM SalesPerson";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesPersons.Add(new SalesPerson
                        (
                            (int)reader["SalesPersonId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"],
                            (Role)reader["Role"]
                         ));
                    }
                }
            }
            return salesPersons;
        }

        public SalesPerson GetById(int id)
        {
            SalesPerson salesPerson = null;
            string query = "SELECT * FROM SalesPerson WHERE SalesPersonId = @SalesPersonId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SalesPersonId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        salesPerson = new SalesPerson
                        (
                            (int)reader["SalesPersonId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"],
                            (Role)reader["Role"]
                        );
                    }
                }
            }
            return salesPerson;
        }

        public void Add(SalesPerson entity)
        {
            string query = "INSERT INTO SalesPerson (FirstName, LastName, CreationDate, Role) VALUES (@FirstName, @LastName, @CreationDate, @Role)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                command.Parameters.AddWithValue("@Role", entity.Role);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(SalesPerson entity)
        {
            string query = "UPDATE SalesPerson SET SalesPersonId = @SalesPersonId, FirstName = @FirstName, LastName = @LastName, CreationDate = @CreationDate, Role = @Role WHERE SalesPersonId = @SalesPersonId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SalesPersonId", entity.PersonId);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                command.Parameters.AddWithValue("@Role", entity.Role);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM SalesPerson WHERE SalesPersonId = @SalesPersonId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SalesPersonId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
