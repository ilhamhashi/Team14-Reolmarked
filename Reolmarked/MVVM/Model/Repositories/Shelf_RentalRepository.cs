using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class Shelf_RentalRepository : IRepository<Shelf_Rental>
    {
        private readonly string _connectionString;

        public Shelf_RentalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Shelf_Rental> GetAll()
        {
            var shelfrentals = new List<Shelf_Rental>();

            string query = "SELECT * FROM ShelfRental";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shelfrentals.Add(new Shelf_Rental
                        (
                            (int)reader["ShelfId"],
                            (int)reader["AgreementId"],
                            (DateTime)reader["StartDate"],
                            Convert.IsDBNull(reader["EndDate"]) ? null : (DateTime?)reader["EndDate"],
                            (bool)reader["IsActive"],
                            (double)reader["Price"],
                            (double)reader["Discount"],
                            (double)reader["DiscountPctg"]
                        ));
                    }
                }
            }
            return shelfrentals;
        }

        public Shelf_Rental GetById(int shelfid)
        {
            Shelf_Rental shelfrental = null;
            
            string query = "SELECT * FROM ShelfRental WHERE ShelfId = @ShelfId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", shelfid);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        shelfrental = new Shelf_Rental
                        (
                            (int)reader["ShelfId"],
                            (int)reader["AgreementId"],
                            (DateTime)reader["StartDate"],
                            Convert.IsDBNull(reader["EndDate"]) ? null : (DateTime?)reader["EndDate"],
                            (bool)reader["IsActive"],
                            (double)reader["Price"],
                            (double)reader["Discount"],
                            (double)reader["DiscountPctg"]
                        );
                    }
                }
            }
            return shelfrental;
        }
        public Shelf_Rental GetByAgremmentId(int agreementid)
        {
            Shelf_Rental shelfrental = null;
            
            string query = "SELECT * FROM ShelfRental WHERE AgreementId = @AgreementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AgreementId", agreementid);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        shelfrental = new Shelf_Rental
                        (
                            (int)reader["ShelfId"],
                            (int)reader["AgreementId"],
                            (DateTime)reader["StartDate"],
                            Convert.IsDBNull(reader["EndDate"]) ? null : (DateTime?)reader["EndDate"],
                            (bool)reader["IsActive"],
                            (double)reader["Price"],
                            (double)reader["Discount"],
                            (double)reader["DiscountPctg"]
                        );
                    }
                }
            }
            return shelfrental;
        }
        public void Add(Shelf_Rental entity)
        {
            string query = "INSERT INTO ShelfRental (ShelfId, AgreementId, StartDate, IsActive, Price, Discount, DiscountPctg) VALUES (@ShelfId, @AgreementId, @StartDate, @IsActive, @Price, @Discount, @DiscountPctg)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                command.Parameters.AddWithValue("@DiscountPctg", entity.DiscountPctg);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('ShelfRental') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(Shelf_Rental entity)
        {
            string query = "UPDATE ShelfRental SET ShelfId = @ShelfId, AgreementId = @AgreementId, StartDate = @StartDate, EndDate = @EndDate, IsActive = @IsActive, Price = @Price, Discount = @Discount, DiscountPctg = @DiscountPctg WHERE ShelfID = @ShelfId, AgreementId = @AgreementId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                command.Parameters.AddWithValue("@DiscountPctg", entity.DiscountPctg);
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
