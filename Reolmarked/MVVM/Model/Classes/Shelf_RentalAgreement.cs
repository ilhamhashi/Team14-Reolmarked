namespace Reolmarked.MVVM.Model.Classes
{
    public class Shelf_RentalAgreement
    {
        public int ShelfId { get; set; }
        public int RentalAgreementId { get; set; }
        public bool IsActive { get; set; }

        public Shelf_RentalAgreement(int shelfId, int rentalAgreementId, bool isActive)
        {
            ShelfId = shelfId;
            RentalAgreementId = rentalAgreementId;
            IsActive = isActive;
        }
    }
}
