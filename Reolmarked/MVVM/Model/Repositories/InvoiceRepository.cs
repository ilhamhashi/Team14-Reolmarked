using Microsoft.Data.SqlClient;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private readonly string _connectionString;
        public InvoiceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Invoice> GetAll()
        {
            var invoices = new List<Invoice>();
            string query = "SELECT * FROM Invoice";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoices.Add(new Invoice
                        (
                            (int)reader["InvoiceId"],
                            (DateTime)reader["InvoiceDateTime"],
                            (double)reader["TotalValueSold"],
                            (double)reader["Deductibles"],
                            (double)reader["GrandTotal"],
                            (bool)reader["IsInvoicePaid"],
                            (int)reader["SalesPersonId"],
                            (int)reader["AgreementId"]
                        ));
                    }
                }
            }
            return invoices;
        }

        public Invoice GetById(int id)
        {
            Invoice invoice = null;
            string query = "SELECT * FROM Invoice WHERE InvoiceId = @InvoiceId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InvoiceId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        invoice = new Invoice
                        (
                            (int)reader["InvoiceId"],
                            (DateTime)reader["InvoiceDateTime"],
                            (double)reader["TotalValueSold"],
                            (double)reader["Deductibles"],
                            (double)reader["GrandTotal"],
                            (bool)reader["IsInvoicePaid"],
                            (int)reader["SalesPersonId"],
                            (int)reader["AgreementId"]
                        );
                    }
                }
            }
            return invoice;
        }

        public void Add(Invoice entity)
        {
            string query = "INSERT INTO Invoice (InvoiceId, InvoiceDateTime, TotalValueSold, Deductibles, InvoiceGrandTotal, IsInvoicePaid, SalesPersonId, AgreementId) VALUES (@InvoiceId, @InvoiceDateTime, @TotalValueSold, @Deductibles, @InvoiceGrandTotal, @IsInvoicePaid, @SalesPersonId, @AgreementId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InvoiceId", entity.InvoiceId);
                command.Parameters.AddWithValue("@InvoiceDateTime", entity.DateTime);
                command.Parameters.AddWithValue("@TotalValueSold", entity.TotalValueSold);
                command.Parameters.AddWithValue("@Deductibles", entity.DateTime);
                command.Parameters.AddWithValue("@InvoiceGrandTotal", entity.GrandTotal);
                command.Parameters.AddWithValue("@IsInvoicePaid", entity.IsPaid);
                command.Parameters.AddWithValue("@SalesPersonId", entity.SalesPersonId);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Invoice entity)
        {
            string query = "UPDATE Invoice SET InvoiceId = @InvoiceId, InvoiceDateTime = @InvoiceDateTime, TotalValueSold = @TotalValueSold, Deductibles = @Deductibles, InvoiceGrandTotal = @InvoiceGrandTotal, IsInvoicePaid = @IsInvoicePaid, SalesPersonId = @SalesPersonId, AgreementId = @AgreementId WHERE SaleId = @SaleId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InvoiceId", entity.InvoiceId);
                command.Parameters.AddWithValue("@InvoiceDateTime", entity.DateTime);
                command.Parameters.AddWithValue("@TotalValueSold", entity.TotalValueSold);
                command.Parameters.AddWithValue("@Deductibles", entity.DateTime);
                command.Parameters.AddWithValue("@InvoiceGrandTotal", entity.GrandTotal);
                command.Parameters.AddWithValue("@IsInvoicePaid", entity.IsPaid);
                command.Parameters.AddWithValue("@SalesPersonId", entity.SalesPersonId);
                command.Parameters.AddWithValue("@AgreementId", entity.AgreementId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Invoice WHERE InvoiceId = @InvoiceId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InvoiceId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        } 
    }
}
