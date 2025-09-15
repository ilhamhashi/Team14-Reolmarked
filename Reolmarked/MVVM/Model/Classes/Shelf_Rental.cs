namespace Reolmarked.MVVM.Model.Classes
{
    public class Shelf_Rental
    {
        public Shelf Shelf { get; set; }
        public RentalAgreement Rental {  get; set; }
        public bool IsActive { get; set; }

        public Shelf_Rental(Shelf shelf, RentalAgreement rental, bool isActive)
        {
            Shelf = shelf;
            Rental = rental;
            IsActive = isActive;
        }
    }
}
