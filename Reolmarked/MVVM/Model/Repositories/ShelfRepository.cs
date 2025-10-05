using Reolmarked.MVVM.Model.Classes;
using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Interfaces;

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
            string query = "SELECT * FROM SHELF";

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
                            (ShelfArrangement)Enum.Parse(typeof(ShelfArrangement),(string)reader["Arrangement"]),
                            (ShelfStatus)Enum.Parse(typeof(ShelfStatus), (string)reader["Status"]),
                            (double)reader["Price"]
                        ));
                    }
                }
            }
            return shelves;
        }

        public Shelf GetById(int id)
        {
            Shelf shelf = null;
            string query = "SELECT * FROM SHELF WHERE ShelfId = @ShelfId";

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
                            (ShelfArrangement)Enum.Parse(typeof(ShelfArrangement), (string)reader["Arrangement"]),
                            (ShelfStatus)Enum.Parse(typeof(ShelfStatus), (string)reader["Status"]),
                            (double)reader["Price"]
                        );
                    }
                }
            }
            return shelf;
        }

        public void Add(Shelf entity)
        {
            string query = "INSERT INTO SHELF (ShelfId, ColumnIndex, RowIndex, Arrangement, Status, Price) VALUES (@ShelfId, @ColumnIndex, @RowIndex, @Arrangement, @Status, @Price)";

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

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('SHELF') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(Shelf entity)
        {
            string query = "UPDATE SHELF SET ColumnIndex = @ColumnIndex, RowIndex = @RowIndex, Arrangement = @Arrangement, Status = @Status, Price = @Price WHERE ShelfId = @ShelfId";

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
            string query = "DELETE FROM SHELF WHERE ShelfId = @ShelfId";

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
