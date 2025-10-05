using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

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
            string query = "SELECT * FROM PERSON INNER JOIN EMPLOYEE ON PERSON.PersonId=EMPLOYEE.EmployeeId";

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
                            (Role)Enum.Parse(typeof(Role), (string)reader["Role"])
                         ));
                    }
                }
            }
            return employees;
        }

        public Employee GetById(int id)
        {
            Employee employee = null;
            string query = "SELECT * FROM EMPLOYEE WHERE EMPLOYEE = @EMPLOYEEId";

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
                            (Role)Enum.Parse(typeof(Role), (string)reader["Role"])
                        );
                    }
                }
            }
            return employee;
        }

        public void Add(Employee entity)
        {
            string query = "INSERT INTO EMPLOYEE (EmployeeId, Role) VALUES (@EmployeeId, @Role)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", entity.PersonId);
                command.Parameters.AddWithValue("@Role", entity.Role);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('EMPLOYEE') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(Employee entity)
        {
            string query = "UPDATE EMPLOYEE SET Role = @Role WHERE EmployeeId = @EmployeeId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", entity.PersonId);
                command.Parameters.AddWithValue("@Role", entity.Role);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM EMPLOYEE WHERE EmployeeId = @EmployeeId";

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
