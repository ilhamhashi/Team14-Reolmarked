using System.ComponentModel;

namespace Reolmarked.MVVM.Model.Classes
{
    public enum RentalAgreementStatus
    {
        [Description("Oprettet - afventer betaling")]
        CreatedAwaitingPayment,
        [Description("Aktiv")]
        Active, 
        [Description("Opsagt")]
        Cancelled,
        [Description("Afsluttet")]
        InActive
    }

    public class RentalAgreement
    {
        public int AgreementId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Total {  get; set; }
        public RentalAgreementStatus Status { get; set; }
        public int? RenterId { get; set; }
        public int? DiscountId { get; set; }
        public int? EmployeeId { get; set; }

        public RentalAgreement(int agreementId, DateTime startDate, DateTime? endDate, double total, RentalAgreementStatus status, int renterId, int discountId, int employeeId)
        {
            AgreementId = agreementId;
            StartDate = startDate;
            EndDate = endDate;
            Total = total;
            Status = status;
            RenterId = renterId;
            DiscountId = discountId;
            EmployeeId = employeeId;
        }

        public RentalAgreement(DateTime startDate, DateTime? endDate, double total, RentalAgreementStatus status, int renterId, int discountId, int employeeId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Total = total;
            Status = status;
            RenterId = renterId;
            DiscountId = discountId;
            EmployeeId = employeeId;
        }

        public RentalAgreement(DateTime startDate, double total, RentalAgreementStatus status, int renterId, int discountId, int employeeId)
        {
            StartDate = startDate;
            Total = total;
            Status = status;
            RenterId = renterId;
            DiscountId = discountId;
            EmployeeId = employeeId;
        }
    }
}
