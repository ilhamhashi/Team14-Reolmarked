namespace Reolmarked.MVVM.Model.Classes
{
    public class RentalAgreement
    {
        public int RentalAgreementId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CancelDate { get; set; }
        public double Total {  get; set; }
        public string Status { get; set; }

        public RentalAgreement(int rentalAgreementId, DateTime startDate, DateTime endDate, DateTime cancelDate, double total, string status)
        {
            RentalAgreementId = rentalAgreementId;
            StartDate = startDate;
            EndDate = endDate;
            CancelDate = cancelDate;
            Total = total;
            Status = status;
        }

        public RentalAgreement(DateTime startDate, DateTime endDate, DateTime cancelDate, double total, string status)
        {
            StartDate = startDate;
            EndDate = endDate;
            CancelDate = cancelDate;
            Total = total;
            Status = status;
        }
    }
}
