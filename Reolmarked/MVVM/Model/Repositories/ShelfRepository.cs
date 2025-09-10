using Reolmarked.MVVM.Model.Classes;
using Microsoft.Data.SqlClient;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class ShelfRepository : IRepository<Shelf>
    {
        private readonly string _connectionString;

        public ShelfRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Shelf> GetAll()
        {
            var shelves = new List<Shelf>();
            string query = "SELECT * FROM Shelf";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shelves.Add(new Shelf
                        (
                            (int)reader["ShelfId"],
                            (string)reader["ShelfPlacement"],
                            (string)reader["ShelfArrangement"],
                            (double)reader["ShelfPrice"],
                            (bool)reader["IsRented"]
                        ));
                    }
                }
            }
            return shelves;
        }

        public Shelf GetById(int id)
        {
            Shelf shelf = null;
            string query = "SELECT * FROM Shelf WHERE ShelfId = @ShelfId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        shelf = new Shelf
                        (
                            (int)reader["ShelfId"],
                            (string)reader["ShelfPlacement"],
                            (string)reader["ShelfArrangement"],
                            (double)reader["ShelfPrice"],
                            (bool)reader["IsRented"]
                        );
                    }
                }
            }
            return shelf;
        }

        public void Add(Shelf entity)
        {
            string query = "INSERT INTO Shelf (ShelfPlacement, ShelfArrangement, ShelfPrice, IsRented) VALUES (@ShelfPlacement, @ShelfArrangement, @ShelfPrice, @IsRented)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfPlacement", entity.ShelfPlacement);
                command.Parameters.AddWithValue("@ShelfArrangement", entity.ShelfArrangement);
                command.Parameters.AddWithValue("@ShelfPrice", entity.ShelfPrice);
                command.Parameters.AddWithValue("@IsRented", entity.IsRented);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Shelf entity)
        {
            string query = "UPDATE Shelf SET ShelfPlacement = @ShelfPlacement, ShelfArrangement = @ShelfArrangement, ShelfPrice = @ShelfPrice, IsRented = @IsRented WHERE ShelfId = @ShelfId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfPlacement", entity.ShelfPlacement);
                command.Parameters.AddWithValue("@ShelfArrangement", entity.ShelfArrangement);
                command.Parameters.AddWithValue("@ShelfPrice", entity.ShelfPrice);
                command.Parameters.AddWithValue("@IsRented", entity.IsRented);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Shelf WHERE ShelfId = @ShelfId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
