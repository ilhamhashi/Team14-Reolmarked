using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class ItemLine : IDiscountable
    {
        public int ItemId { get; set; }
        public int SaleId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount {  get; set; } = 0;

        public ItemLine(int itemId, int saleId, int quantity, double price, double discount)
        {
            ItemId = itemId;
            SaleId = saleId;
            Quantity = quantity;
            Price = price;
            Discount = discount;
        }

        public void ReducePrice()
        {
            Price -= Discount;
        }
    }
}
