using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Repositories
{
    internal class EmployeeRepository : IRepository<Employee>
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();
            string query = "SELECT * FROM Employee";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        (
                            (int)reader["EmployeeId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"],
                            (string)reader["Role"]
                         ));
                    }
                }
            }
            return employees;
        }

        public Employee GetById(int id)
        {
            Employee employee = null;
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
                        employee = new Employee
                        (
                            (int)reader["EmployeeId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"],
                            (string)reader["Role"]
                        );
                    }
                }
            }
            return employee;
        }

        public void Add(Employee entity)
        {
            string query = "INSERT INTO Employee (EmployeeId, FirstName, LastName, CreationDate, Role) VALUES (@EmployeeId, @FirstName, @LastName, @CreationDate, @Role)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", entity.UserId);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                command.Parameters.AddWithValue("@Role", entity.Role);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Employee entity)
        {
            string query = "UPDATE Employee SET EmployeeId = @EmployeeId, FirstName = @FirstName, LastName = @LastName, CreationDate = @CreationDate, Role = @Role WHERE EmployeeId = @EmployeeId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", entity.UserId);
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
