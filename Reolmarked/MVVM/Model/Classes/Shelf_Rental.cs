namespace Reolmarked.MVVM.Model.Classes
{
    public class Shelf_Rental
    {
        public int ShelfId { get; set; }
        public int AgreementId {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }

        public Shelf_Rental(int shelfId, int agreementId, DateTime startDate, DateTime? endDate, bool isActive)
        {
            ShelfId = shelfId;
            AgreementId = agreementId;
            StartDate = startDate;
            IsActive = isActive;
        }
        public Shelf_Rental(int shelfId, int agreementId, DateTime startDate, bool isActive)
        {
            ShelfId = shelfId;
            AgreementId = agreementId;
            StartDate = startDate;
            IsActive = isActive;
        }
    }
}
