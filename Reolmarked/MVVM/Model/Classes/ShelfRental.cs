using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class ShelfRental : IRentable
    {
        public int ShelfId { get; set; }
        public int AgreementId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } = 0;

        public ShelfRental(int shelfId, int agreementId, DateTime startDate, DateTime? endDate, bool isActive, double price, double discount)
        {
            ShelfId = shelfId;
            AgreementId = agreementId;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
            Price = price;
            Discount = discount;
        }

        public ShelfRental(int shelfId, int agreementId, DateTime startDate, bool isActive, double price, double discount)
        {
            ShelfId = shelfId;
            AgreementId = agreementId;
            StartDate = startDate;
            IsActive = isActive;
            Price = price;
            Discount = discount;
        }

        public double GetPrice(int shelfCount)
        {
            if (shelfCount >= 4)
                return Price = 800;

            else if (shelfCount > 2 && shelfCount < 4)
                return Price = 825;

            else
                return Price = 850;
        }

        public void ReducePrice()
        {
            Price -= Discount;
        }

        public void EndRental()
        {
            throw new NotImplementedException();
        }
    }
}
