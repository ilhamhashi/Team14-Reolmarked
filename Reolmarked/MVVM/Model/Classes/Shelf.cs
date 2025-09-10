namespace Reolmarked.MVVM.Model.Classes
{
    public class Shelf
    {
        public int ShelfId { get; set; }
        public string ShelfPlacement { get; set; }
        public string ShelfArrangement { get; set; }
        public double ShelfPrice { get; set; }
        public string Status { get; set; }

        public Shelf(int shelfId, string shelfPlacement, string shelfArrangement, double shelfPrice, string status)
        {
            ShelfId = shelfId;
            ShelfPlacement = shelfPlacement;
            ShelfArrangement = shelfArrangement;
            ShelfPrice = shelfPrice;
            Status = status;
        }

        public Shelf(string shelfPlacement, string shelfArrangement, double shelfPrice, string status)
        {
            ShelfPlacement = shelfPlacement;
            ShelfArrangement = shelfArrangement;
            ShelfPrice = shelfPrice;
            Status = status;
        }
    }
}
