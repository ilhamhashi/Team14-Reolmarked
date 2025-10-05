using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Item : ISellable
    {
        public int ItemId { get; set; }
        public string BarcodeImage { get; set; }
        public double Price { get; set; }
        public int ShelfId { get; set; }

        public Item(int itemId, string barcodeImage, double price, int shelfId)
        {
            ItemId = itemId;
            BarcodeImage = barcodeImage;
            Price = price;
            ShelfId = shelfId;
        }

        public Item(string barcodeImage, double price, int shelfId)
        {
            BarcodeImage = barcodeImage;
            Price = price;
            ShelfId = shelfId;
        }
    }
}
