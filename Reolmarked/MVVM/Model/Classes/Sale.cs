using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Sale : IInvoiceable
    {
        public int SaleId { get; set; }
        public DateTime DateTime { get; set; }
        public double GrandTotal { get; set; }
        public bool IsPaid { get; set; }
        public int SalesPersonId { get; set; }

        public Sale(int saleId, DateTime dateTime, double grandTotal, bool isPaid, int salesPersonId)
        {
            SaleId = saleId;
            DateTime = dateTime;
            GrandTotal = grandTotal;
            IsPaid = isPaid;
            SalesPersonId = salesPersonId;
        }

        public Sale(DateTime dateTime, double grandTotal, bool isPaid, int salesPersonId)
        {
            DateTime = dateTime;
            GrandTotal = grandTotal;
            IsPaid = isPaid;
            SalesPersonId = salesPersonId;
        }

        public bool RegisterPaymentForSale()
        {
            throw new NotImplementedException();
        }

        public void PrintInvoice()
        {
            throw new NotImplementedException();
        }
    }
}
