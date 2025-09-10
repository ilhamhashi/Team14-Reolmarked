namespace Reolmarked.MVVM.Model.Classes
{
    public class Shelf
    {
        public int ShelfId { get; set; }
        public string ShelfPlacement { get; set; }
        public string ShelfArrangement { get; set; }
        public double ShelfPrice { get; set; }
        public bool IsRented { get; set; }

        public Shelf(int shelfId, string shelfPlacement, string shelfArrangement, double shelfPrice, bool isRented)
        {
            ShelfId = shelfId;
            ShelfPlacement = shelfPlacement;
            ShelfArrangement = shelfArrangement;
            ShelfPrice = shelfPrice;
            IsRented = isRented;
        }

        public Shelf(string shelfPlacement, string shelfArrangement, double shelfPrice, bool isRented)
        {
            ShelfPlacement = shelfPlacement;
            ShelfArrangement = shelfArrangement;
            ShelfPrice = shelfPrice;
            IsRented = isRented;
        }
    }
}
