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
        public int RenterId { get; set; }
        public int DiscountId { get; set; }
        public int EmployeeId { get; set; }

        public RentalAgreement(int agreementId, DateTime startDate, DateTime endDate, DateTime cancelDate, double total, string status, int renterId, int discountId, int employeeId)
        {
            RentalAgreementId = rentalAgreementId;
            StartDate = startDate;
            EndDate = endDate;
            CancelDate = cancelDate;
            Total = total;
            Status = status;
            RenterId = renterId;
            DiscountId = discountId;
            EmployeeId = employeeId;
        }

        public RentalAgreement(DateTime startDate, DateTime endDate, DateTime cancelDate, double total, string status, int renterId, int discountId, int employeeId)
        {
            StartDate = startDate;
            EndDate = endDate;
            CancelDate = cancelDate;
            Total = total;
            Status = status;
            RenterId = renterId;
            DiscountId = discountId;
            EmployeeId = employeeId;
        }

    }
}
