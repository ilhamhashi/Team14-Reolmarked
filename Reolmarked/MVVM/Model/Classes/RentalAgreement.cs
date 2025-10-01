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
        public RentalAgreementStatus Status { get; set; }
        public int? RenterId { get; set; }
        public int? SalesPersonId { get; set; }

        public RentalAgreement(int agreementId, DateTime startDate, DateTime? endDate, RentalAgreementStatus status, int renterId, int salesPersonId)
        {
            AgreementId = agreementId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            RenterId = renterId;
            SalesPersonId = salesPersonId;
        }

        public RentalAgreement(DateTime startDate, DateTime? endDate, RentalAgreementStatus status, int renterId, int salesPersonId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            RenterId = renterId;
            SalesPersonId = salesPersonId;
        }

        public RentalAgreement(DateTime startDate, RentalAgreementStatus status, int renterId, int salesPersonId)
        {
            StartDate = startDate;
            Status = status;
            RenterId = renterId;
            SalesPersonId = salesPersonId;
        }

        public RentalAgreement(DateTime startDate, RentalAgreementStatus status, int? renterId)
        {
            StartDate = startDate;
            Status = status;
            RenterId = renterId;
        }
    }
}
