using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class ItemRepository : IRepository<Item>
    {
        private readonly string _connectionString;
        public ItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Item> GetAll()
        {
            var items = new List<Item>();
            string query = "SELECT * FROM Item";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new Item
                        (
                            (int)reader["ItemId"],
                            (int)reader["ShelfId"],
                            (double)reader["Price"], 
                            (string)reader["BarcodeImagePath"]
                        ));
                    }
                }
            }
            return items;
        }

        public Item GetById(int id)
        {
            Item item = null;
            string query = "SELECT * FROM Item WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new Item
                        (
                            (int)reader["ItemId"],
                            (int)reader["ShelfId"],
                            (double)reader["Price"],
                            (string)reader["BarcodeImagePath"]
                        );
                    }
                }
            }
            return item;
        }

        public void Add(Item entity)
        {
            string query = "INSERT INTO Item (ShelfId, Price, BarcodeImagePath) VALUES (@ShelfId, @Price, @BarcodeImagePath)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@BarcodeImagePath", entity.BarcodeImage);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Item entity)
        {
            string query = "UPDATE Item SET ShelfId = @ShelfId, Price = @Price, BarcodeImagePath = @BarcodeImagePath WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@BarcodeImagePath", entity.BarcodeImage);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Item WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
