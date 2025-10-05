using System.ComponentModel;

namespace Reolmarked.MVVM.Model.Classes
{
    public enum AgreementStatus
    {
        [Description("Oprettet - afventer betaling")]
        CreatedAwaitingPayment,
        [Description("Aktiv")]
        Active, 
        [Description("Opsagt")]
        Cancelled,
        [Description("Inaktiv")]
        InActive
    }

    public class RentalAgreement
    {
        public int AgreementId { get; set; }
        public AgreementStatus Status { get; set; }
        public int? RenterId { get; set; }
        public int? EmployeeId { get; set; }

        public RentalAgreement(int agreementId, AgreementStatus status, int? renterId, int? employeeId)
        {
            AgreementId = agreementId;
            Status = status;
            RenterId = renterId;
            EmployeeId = employeeId;
        }

        public RentalAgreement(AgreementStatus status, int? renterId, int? employeeId)
        {
            Status = status;
            RenterId = renterId;
            EmployeeId = employeeId;
        }
    }
}
