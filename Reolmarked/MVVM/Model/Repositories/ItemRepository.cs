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
            string query = "SELECT * FROM ITEM";

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
                            (string)reader["BarcodeImagePath"],
                            (double)reader["Price"],
                            (int)reader["ShelfId"]
                        ));
                    }
                }
            }
            return items;
        }

        public Item GetById(int id)
        {
            Item item = null;
            string query = "SELECT * FROM ITEM WHERE ItemId = @ItemId";

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
                            (string)reader["BarcodeImagePath"],
                            (double)reader["Price"],
                            (int)reader["ShelfId"]
                        );
                    }
                }
            }
            return item;
        }

        public void Add(Item entity)
        {
            string query = "INSERT INTO ITEM (BarcodeImagePath, Price, ShelfId) VALUES (@BarcodeImagePath, @Price, @ShelfId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BarcodeImagePath", entity.BarcodeImage);
                command.Parameters.AddWithValue("@Price", entity.Price); 
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);           
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('ITEM') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(Item entity)
        {
            string query = "UPDATE ITEM SET ShelfId = @ShelfId, Price = @Price, BarcodeImagePath = @BarcodeImagePath WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", entity.ItemId);
                command.Parameters.AddWithValue("@BarcodeImagePath", entity.BarcodeImage);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM ITEM WHERE ItemId = @ItemId";

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
