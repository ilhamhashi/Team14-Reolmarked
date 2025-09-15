namespace Reolmarked.MVVM.Model.Classes
{
    public class Shelf
    {
        public int ShelfId { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public string ShelfArrangement { get; set; }
        public double ShelfPrice { get; set; }
        public bool IsRented { get; set; }

        public Shelf(int shelfId, int columnIndex, int rowIndex, string shelfArrangement, double shelfPrice, bool isRented)
        {
            ShelfId = shelfId;
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
            ShelfArrangement = shelfArrangement;
            ShelfPrice = shelfPrice;
            IsRented = isRented;
        }

    }
}
