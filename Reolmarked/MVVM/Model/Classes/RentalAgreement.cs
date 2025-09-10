namespace Reolmarked.MVVM.Model.Classes
{
    public class RentalAgreement
    {
        public int RentalAgreementId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CancelDate { get; set; }
        public double Total {  get; set; }
        public bool IsActive { get; set; }

        public RentalAgreement(int rentalAgreementId, DateTime startDate, DateTime endDate, DateTime cancelDate, double total, bool isActive)
        {
            RentalAgreementId = rentalAgreementId;
            StartDate = startDate;
            EndDate = endDate;
            CancelDate = cancelDate;
            Total = total;
            IsActive = isActive;
        }

        public RentalAgreement(DateTime startDate, DateTime endDate, DateTime cancelDate, double total, bool isActive)
        {
            StartDate = startDate;
            EndDate = endDate;
            CancelDate = cancelDate;
            Total = total;
            IsActive = isActive;
        }
    }
}
