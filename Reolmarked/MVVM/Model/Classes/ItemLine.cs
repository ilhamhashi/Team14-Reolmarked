namespace Reolmarked.MVVM.Model.Classes
{
    public class ItemLine
    {
        public int ItemId { get; set; }
        public int SaleId { get; set; }
        public int Quantity { get; set; }

        public ItemLine(int itemId, int saleId, int quantity)
        {
            ItemId = itemId;
            SaleId = saleId;
            Quantity = quantity;
        }
    }
}
