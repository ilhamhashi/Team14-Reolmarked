using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    internal class EmployeeRepository : IRepository<SalesPerson>
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<SalesPerson> GetAll()
        {
            var employees = new List<SalesPerson>();
            string query = "SELECT * FROM Employee";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new SalesPerson
                        (
                            (int)reader["EmployeeId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"],
                            (Role)reader["Role"]
                         ));
                    }
                }
            }
            return employees;
        }

        public SalesPerson GetById(int id)
        {
            SalesPerson employee = null;
            string query = "SELECT * FROM Employee WHERE EmployeeId = @EmployeeId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new SalesPerson
                        (
                            (int)reader["EmployeeId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"],
                            (Role)reader["Role"]
                        );
                    }
                }
            }
            return employee;
        }

        public void Add(SalesPerson entity)
        {
            string query = "INSERT INTO Employee (FirstName, LastName, CreationDate, Role) VALUES (@FirstName, @LastName, @CreationDate, @Role)";

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
            string query = "UPDATE Employee SET EmployeeId = @EmployeeId, FirstName = @FirstName, LastName = @LastName, CreationDate = @CreationDate, Role = @Role WHERE EmployeeId = @EmployeeId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", entity.PersonId);
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
            string query = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
