using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Sale : IInvoiceable
    {
        public int SaleId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public bool IsPaid { get; set; }
        public int EmployeeId { get; set; }

        public Sale(int saleId, DateTime date, double total, bool isPaid, int employeeId)
        {
            SaleId = saleId;
            Date = date;
            Total = total;
            IsPaid = isPaid;
            EmployeeId = employeeId;
        }

        public Sale(DateTime date, double total, bool isPaid, int employeeId)
        {
            Date = date;
            Total = total;
            IsPaid = isPaid;
            EmployeeId = employeeId;
        }

        public double RemainingBalance(double amountReceived)
        {
            return Total - amountReceived;
        }

    }
}
