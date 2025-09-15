using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;

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
                //command.Parameters.AddWithValue("@AgreementId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        shelfrental = new Shelf_Rental
                        (
                            (int)reader["ShelfId"],
                            (int)reader["AgreementId"],
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
                //command.Parameters.AddWithValue("@AgreementId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        shelfrental = new Shelf_Rental
                        (
                            (int)reader["ShelfId"],
                            (int)reader["AgreementId"],
                            (bool)reader["IsActive"]
                        );
                    }
                }
            }
            return shelfrental;
        }
        public void Add(Shelf_Rental entity)
        {
            string query = "INSERT INTO Shelf_Rental (ShelfId, AgreementId, IsActive) VALUES (@ShelfId, @AgreementId, @IsActive)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.Shelf.ShelfId);
                command.Parameters.AddWithValue("@AgreementId", entity.Rental.AgreementId);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Shelf_Rental entity)
        {
            string query = "UPDATE Shelf_Rental SET ShelfId = @ShelfId, AgreementId = @AgreementId, IsActive = @IsActive";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ShelfId", entity.Shelf.ShelfId);
                command.Parameters.AddWithValue("@AgreementId", entity.Rental.AgreementId);
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
