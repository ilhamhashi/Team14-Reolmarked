namespace Reolmarked.MVVM.Model.Classes
{
    public class Shelf_Rental
    {
        public int ShelfId { get; set; }
        public int AgreementId { get; set; }
        public bool IsActive { get; set; }

        public Shelf_Rental(int shelfId, int agreementId, bool isActive)
        {
            ShelfId = shelfId;
            AgreementId = agreementId;
            IsActive = isActive;
        }
    }
}
