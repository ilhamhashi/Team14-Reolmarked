using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Item : ISellable
    {
        public int ItemId { get; set; }
        public int ShelfId { get; set; }
        public double Price { get; set; }
        public string BarcodeImage { get; set; }

        public Item(int itemId, int shelfId, double price, string barcodeImage)
        {
            ItemId = itemId;
            ShelfId = shelfId;
            Price = price;
            BarcodeImage = barcodeImage;
        }

        public Item(int shelfId, double price, string barcodeImage)
        {
            ShelfId = shelfId;
            Price = price;
            BarcodeImage = barcodeImage;
        }
    }
}
