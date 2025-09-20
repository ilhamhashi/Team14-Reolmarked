namespace Reolmarked.MVVM.Model.Classes
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime SalesDate { get; set; }
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }

        public Sale(int saleId, DateTime salesDate, double totalPrice, bool isPaid)
        {
            SaleId = saleId;
            SalesDate = salesDate;
            TotalPrice = totalPrice;
            IsPaid = isPaid;
        }

        public Sale(DateTime salesDate, double totalPrice, bool isPaid)
        {
            SalesDate = salesDate;
            TotalPrice = totalPrice;
            IsPaid = isPaid;
        }

        public bool RegisterPaymentForSale()
        {
            throw new NotImplementedException();
        }
    }
}
