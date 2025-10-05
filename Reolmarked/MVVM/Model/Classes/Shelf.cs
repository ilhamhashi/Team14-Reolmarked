using Reolmarked.MVVM.Model.Interfaces;
using System.ComponentModel;

namespace Reolmarked.MVVM.Model.Classes
{
    public enum ShelfArrangement 
    {
        [Description("Med bøjlestang")]
        WithHangingRail,
        [Description("Uden bøjlestang")]
        ShelvesOnly
    }

    public enum ShelfStatus
    {
        [Description("Udlejet")]
        Unavailable,
        [Description("Ledig")]
        Available
    }

    public class Shelf : ISellable
    {
        public int ShelfId { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public ShelfArrangement Arrangement { get; set; }
        public ShelfStatus Status { get; set; }
        public double Price { get; set; }

        public Shelf(int shelfId, int columnIndex, int rowIndex, ShelfArrangement shelfArrangement, ShelfStatus status, double price)
        {
            ShelfId = shelfId;
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
            Arrangement = shelfArrangement;
            Status = status;
            Price = price;
        }
    }
}
