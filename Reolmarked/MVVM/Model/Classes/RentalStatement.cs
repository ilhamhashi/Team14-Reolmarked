using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class RentalStatement : IInvoiceable
    {
        public int StatementId { get; set; }
        public DateTime Date { get; set; }
        public double TotalValueSold { get; set; }
        public double PrepaidRent {  get; set; }
        public double Total {  get; set; }
        public bool IsPaid { get; set; }
        public int AgreementId { get; set; }

        public RentalStatement(int statementId, DateTime date, double totalValueSold, double prepaidRent, double total, bool isPaid, int agreementId)
        {
            StatementId = statementId;
            Date = date;
            TotalValueSold = totalValueSold;
            PrepaidRent = prepaidRent;
            Total = total;
            IsPaid = isPaid;
            AgreementId = agreementId;
        }

        public RentalStatement(DateTime date, double totalValueSold, double prepaidRent, double total, bool isPaid, int agreementId)
        {
            Date = date;
            TotalValueSold = totalValueSold;
            PrepaidRent = prepaidRent;
            Total = total;
            IsPaid = isPaid;
            AgreementId = agreementId;
        }

        public double RemainingBalance(double amountReceived)
        {
            return GetTotal() - amountReceived;
        }

        public double GetTotal()
        {
             return TotalValueSold * 0.9 - PrepaidRent;
        }
        public double GetCommission()
        {
            return TotalValueSold * 0.10;
        }
    }
}
