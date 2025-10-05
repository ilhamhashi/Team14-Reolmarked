using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class ItemLineRepository : IRepository<ItemLine>
    {
        private readonly string _connectionString;
        public ItemLineRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ItemLine> GetAll()
        {
            var itemLines = new List<ItemLine>();
            string query = "SELECT * FROM ITEMLINE";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itemLines.Add(new ItemLine
                        (
                            (int)reader["ItemId"],
                            (int)reader["SaleId"],
                            (int)reader["Quantity"],
                            (double)reader["Price"],                            
                            (double)reader["Discount"]
                        ));
                    }
                }
            }
            return itemLines;
        }

        public ItemLine GetById(int id)
        {
            ItemLine itemLine = null;
            string query = "SELECT * FROM ITEMLINE WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        itemLine = new ItemLine
                        (
                            (int)reader["ItemId"],
                            (int)reader["SaleId"],
                            (int)reader["Quantity"],
                            (double)reader["Price"],
                            (double)reader["Discount"]
                        );
                    }
                }
            }
            return itemLine;
        }

        public void Add(ItemLine entity)
        {
            string query = "INSERT INTO ITEMLINE (ItemId, SaleId, Price, Quantity, Discount) VALUES (@ItemId, @SaleId, @Price, @Quantity, @Discount)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", entity.ItemId);
                command.Parameters.AddWithValue("@SaleId", entity.SaleId);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('ITEMLINE') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }
        public void Update(ItemLine entity)
        {
            string query = "UPDATE ITEMLINE SET ItemId = @ItemId, SaleId = @SaleId, Price = @Price, Quantity = @Quantity, Discount = @Discount WHERE ItemId = @ItemId, SaleId = @SaleId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", entity.ItemId);
                command.Parameters.AddWithValue("@SaleId", entity.SaleId);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM ITEMLINE WHERE ItemId = @ItemId, SaleId = @SaleId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", id);
                command.Parameters.AddWithValue("@SaleId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
