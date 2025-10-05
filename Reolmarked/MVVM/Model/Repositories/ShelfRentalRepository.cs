using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class ShelfRentalRepository : IRepository<ShelfRental>
    {
        private readonly string _connectionString;

        public ShelfRentalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ShelfRental> GetAll()
        {
            var shelfrentals = new List<ShelfRental>();

            string query = "SELECT * FROM SHELFRENTAL";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shelfrentals.Add(new ShelfRental
                        (
                            (int)reader["ShelfId"],
                            (int)reader["AgreementId"],
                            (DateTime)reader["StartDate"],
                            Convert.IsDBNull(reader["EndDate"]) ? null : (DateTime?)reader["EndDate"],
                            (bool)reader["IsActive"],
                            (double)reader["Price"],
                            (double)reader["Discount"]
                        ));
                    }
                }
            }
            return shelfrentals;
        }
                
        public ShelfRental GetById(int agreementid)
        {
            ShelfRental shelfrental = null;
            
            string query = "SELECT * FROM SHELFRENTAL WHERE AgreementId = @AgreementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgreementId", agreementid);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        shelfrental = new ShelfRental
                        (
                            (int)reader["ShelfId"],
                            (int)reader["AgreementId"],
                            (DateTime)reader["StartDate"],
                            Convert.IsDBNull(reader["EndDate"]) ? null : (DateTime?)reader["EndDate"],
                            (bool)reader["IsActive"],
                            (double)reader["Price"],
                            (double)reader["Discount"]
                        );
                    }
                }
            }
            return shelfrental;
        }
        public void Add(ShelfRental entity)
        {
            string query = "INSERT INTO SHELFRENTAL (ShelfId, AgreementId, StartDate, IsActive, Price, Discount) VALUES (@ShelfId, @AgreementId, @StartDate, @IsActive, @Price, @Discount)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('SHELFRENTAL') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(ShelfRental entity)
        {
            string query = "UPDATE ShelfRental SET ShelfId = @ShelfId, AgreementId = @AgreementId, StartDate = @StartDate, EndDate = @EndDate, IsActive = @IsActive, Price = @Price, Discount = @Discount, WHERE ShelfID = @ShelfId, AgreementId = @AgreementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int shelfid)
        {
            string query = "DELETE FROM ShelfRental WHERE ShelfId = @ShelfId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", shelfid);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
