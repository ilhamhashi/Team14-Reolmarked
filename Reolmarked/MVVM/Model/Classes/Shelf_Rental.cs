using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Shelf_Rental : IDiscountable
    {
        public int ShelfId { get; set; }
        public int AgreementId {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } = 0;
        public double DiscountPctg { get; set; } = 0;

        public Shelf_Rental(int shelfId, int agreementId, DateTime startDate, DateTime? endDate, bool isActive, double price, double discount, double discountPctg)
        {
            ShelfId = shelfId;
            AgreementId = agreementId;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
            Price = price;
            Discount = discount;
            DiscountPctg = discountPctg;
        }

        public void ReduceSellingPrice()
        {
            throw new NotImplementedException();
        }
    }
}
