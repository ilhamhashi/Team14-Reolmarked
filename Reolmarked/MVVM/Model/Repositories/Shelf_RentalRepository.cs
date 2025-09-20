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

            string query = "SELECT * FROM Shelf_Rental";

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
                            (bool)reader["IsActive"]
                        ));
                    }
                }
            }
            return shelfrentals;
        }

        public Shelf_Rental GetById(int shelfid)
        {
            Shelf_Rental shelfrental = null;
            
            string query = "SELECT * FROM Shelf_Rental WHERE ShelfId = @ShelfId";

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
                            (bool)reader["IsActive"]
                        );
                    }
                }
            }
            return shelfrental;
        }
        public Shelf_Rental GetByAgremmentId(int agreementid)
        {
            Shelf_Rental shelfrental = null;
            
            string query = "SELECT * FROM Shelf_Rental WHERE AgreementId = @AgreementId";

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
                            (bool)reader["IsActive"]
                        );
                    }
                }
            }
            return shelfrental;
        }
        public void Add(Shelf_Rental entity)
        {
            string query = "INSERT INTO Shelf_Rental (ShelfId, AgreementId, StartDate, EndDate, IsActive) VALUES (@ShelfId, @AgreementId, @StartDate, @EndDate, @IsActive)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                command.Parameters.AddWithValue("@EndDate", entity.EndDate);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Shelf_Rental entity)
        {
            string query = "UPDATE Shelf_Rental SET ShelfId = @ShelfId, AgreementId = @AgreementId, StartDate = @StartDate, EndDate = @EndDate, IsActive = @IsActive";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.ShelfId);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                command.Parameters.AddWithValue("@EndDate", entity.EndDate);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int shelfid)
        {
            string query = "DELETE FROM Shelf_Rental WHERE ShelfId = @ShelfId";

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
