using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Item : IDiscountable
    {
        public int ItemId { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double DiscountPctg { get; set; }


        public void ReduceSellingPrice()
        {
            Price -= Price * DiscountPctg;
            Price -= Discount;
        }
    }
}
