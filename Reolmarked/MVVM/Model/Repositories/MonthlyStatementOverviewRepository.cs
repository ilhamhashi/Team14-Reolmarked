using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class MonthlyStatementOverviewRepository : IMonthlyStatementOverviewRepository
    {
        private readonly string _connectionString;

        public MonthlyStatementOverviewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<MonthlyStatementItem> GetOverview()
        {
            var overviewList = new List<MonthlyStatementItem>();

            string query = @"
            SELECT 
                ra.AgreementId,
                r.FirstName + ' ' + r.LastName AS RenterName,
                r.Phone,
                r.Email,
                ra.Status,
                sr.Price,
                STRING_AGG(CONVERT(varchar, sr.ShelfId), ', ') WITHIN GROUP (ORDER BY sr.ShelfId) AS ShelfNumbers
            FROM RentalAgreement ra
            INNER JOIN Renter r ON ra.RenterId = r.RenterId
            LEFT JOIN ShelfRental sr ON ra.AgreementId = sr.AgreementId
            GROUP BY 
                ra.AgreementId, r.FirstName, r.LastName, r.Phone, r.Email, ra.Status, sr.Price
        ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        overviewList.Add(new MonthlyStatementItem
                        {
                            AgreementId = (int)reader["AgreementId"],
                            RenterName = (string)reader["RenterName"],
                            Phone = (string)reader["Phone"],
                            Email = (string)reader["Email"],
                            Status = (string)reader["Status"],
                            RentalPrice = (decimal)reader["Price"],
                            ShelfNumbers = reader["ShelfNumbers"] as string ?? ""
                        });
                    }
                }
            }

            return overviewList;
        }
    }
}
