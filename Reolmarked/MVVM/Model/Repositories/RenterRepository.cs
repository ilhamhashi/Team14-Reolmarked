using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Repositories
{
    internal class RenterRepository : IRepository<Renter>
    {
        private readonly string _connectionString;

        public RenterRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Renter> GetAll()
        {
            var renters = new List<Renter>();
            string query = "SELECT * FROM Renter";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        renters.Add(new Renter
                        (
                            (int)reader["RenterId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"],
                            (string)reader["StreetName"],
                            (string)reader["StreetNumber"],
                            (string)reader["ZipCode"],
                            (string)reader["City"],
                            (string)reader["Phone"],
                            (string)reader["Email"]
                         ));
                    }
                }
            }
            return renters;
        }

        public Renter GetById(int id)
        {
            Renter renter = null;
            string query = "SELECT * FROM Renter WHERE RenterId = @RenterId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RenterId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        renter = new Renter
                        (
                            (int)reader["RenterId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["CreationDate"],
                            (string)reader["StreetName"],
                            (string)reader["StreetNumber"],
                            (string)reader["ZipCode"],
                            (string)reader["City"],
                            (string)reader["PhoneNumber"],
                            (string)reader["Email"]
                        );
                    }
                }
            }
            return renter;
        }

        public void Add(Renter entity)
        {
            string query = "INSERT INTO Renter (FirstName, LastName, CreationDate, StreetName, StreetNumber, ZipCode, City, Phone, Email) " +
                           "VALUES (@FirstName, @LastName, @CreationDate, @StreetName, @StreetNumber, @ZipCode, @City, @Phone, @Email)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                command.Parameters.AddWithValue("@StreetName", entity.StreetName);
                command.Parameters.AddWithValue("@StreetNumber", entity.StreetNumber);
                command.Parameters.AddWithValue("@ZipCode", entity.ZipCode);
                command.Parameters.AddWithValue("@City", entity.City);
                command.Parameters.AddWithValue("@Phone", entity.Phone);
                command.Parameters.AddWithValue("@Email", entity.Email);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Renter entity)
        {
            string query = "UPDATE Renter SET FirstName = @FirstName, LastName = @LastName, CreationDate = @CreationDate, StreetName = @StreetName, StreetNumber = @StrretNumber," +
                "ZipCode = @Zipcode, City = @City, Phone = @Phone, Email = @Email WHERE RenterId = @RenterId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RenterId", entity.UserId);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                command.Parameters.AddWithValue("@StreetName", entity.StreetName);
                command.Parameters.AddWithValue("@StreetNumber", entity.StreetNumber);
                command.Parameters.AddWithValue("@ZipCode", entity.ZipCode);
                command.Parameters.AddWithValue("@City", entity.City);
                command.Parameters.AddWithValue("@Phone", entity.Phone);
                command.Parameters.AddWithValue("@Email", entity.Email);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Renter WHERE RenterId = @RenterId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RenterId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
