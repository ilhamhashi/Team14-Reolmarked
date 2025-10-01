using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class ItemLine : IDiscountable
    {
        public int ItemId { get; set; }
        public int SaleId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Discount {  get; set; } = 0;
        public double DiscountPctg { get; set; } = 0;

        public ItemLine(int itemId, int saleId, double price, int quantity, double discount, double discountPctg)
        {
            ItemId = itemId;
            SaleId = saleId;
            Price = price;
            Quantity = quantity;
            Discount = discount;
            DiscountPctg = discountPctg;
        }

        public void ReduceSellingPrice()
        {
            if (Discount == 0 && DiscountPctg != 0)
            {
                Price -= Price * DiscountPctg;
            }
            else
            {
                Price -= Discount;
            }
        }
    }
}
