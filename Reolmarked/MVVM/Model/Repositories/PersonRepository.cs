using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly string _connectionString;

        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetAll()
        {
            var persons = new List<Person>();
            string query = "SELECT * FROM PERSON";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        persons.Add(new Person
                        (
                            (int)reader["PersonId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"]
                         ));
                    }
                }
            }
            return persons;
        }

        public Person GetById(int id)
        {
            Person person = null;
            string query = "SELECT * FROM PERSON WHERE PersonId = @PersonId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        person = new Person
                        (
                            (int)reader["PersonId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"]
                        );
                    }
                }
            }
            return person;
        }

        public void Add(Person entity)
        {
            string query = "INSERT INTO PERSON (FirstName, LastName, CreationDate) VALUES (@FirstName, @LastName, @CreationDate)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('PERSON') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(Person entity)
        {
            string query = "UPDATE PERSON SET FirstName = @FirstName, LastName = @LastName, CreationDate = @CreationDate WHERE PersonId = @PersonId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonId", entity.PersonId);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM PERSON WHERE PersonId = @PersonId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
