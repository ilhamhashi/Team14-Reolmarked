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
                            (int)reader["ColumnIndex"],
                            (int)reader["RowIndex"],
                            (ShelfArrangement)Enum.Parse(typeof(ShelfArrangement),(string)reader["ShelfArrangement"]),
                            (ShelfStatus)Enum.Parse(typeof(ShelfStatus), (string)reader["ShelfStatus"]),
                            (double)reader["ShelfPrice"]
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
                            (int)reader["ColumnIndex"],
                            (int)reader["RowIndex"],
                            (ShelfArrangement)Enum.Parse(typeof(ShelfArrangement), (string)reader["ShelfArrangement"]),
                            (ShelfStatus)Enum.Parse(typeof(ShelfStatus), (string)reader["ShelfStatus"]),
                            (double)reader["ShelfPrice"]
                        );
                    }
                }
            }
            return shelf;
        }

        public void Add(Shelf entity)
        {
            string query = "INSERT INTO Shelf (ShelfId, ColumnIndex, RowIndex, ShelfArrangement, ShelfStatus, ShelfPrice) VALUES (@ShelfId, @ColumnIndex, @RowIndex, @Arrangement, @Status, @Price)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@ColumnIndex", entity.ColumnIndex);
                command.Parameters.AddWithValue("@RowIndex", entity.RowIndex);
                command.Parameters.AddWithValue("@Arrangement", entity.Arrangement);
                command.Parameters.AddWithValue("@Status", entity.Status);
                command.Parameters.AddWithValue("@Price", entity.Price);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Shelf entity)
        {
            string query = "UPDATE Shelf SET ColumnIndex = @ColumnIndex, RowIndex = @RowIndex, ShelfArrangement = @Arrangement, ShelfStatus = @Status, ShelfPrice = @Price WHERE ShelfId = @ShelfId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@ColumnIndex", entity.ColumnIndex);
                command.Parameters.AddWithValue("@RowIndex", entity.RowIndex);
                command.Parameters.AddWithValue("@Arrangement", entity.Arrangement);
                command.Parameters.AddWithValue("@Status", entity.Status);
                command.Parameters.AddWithValue("@Price", entity.Price);
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
