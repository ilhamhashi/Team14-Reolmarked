using Reolmarked.MVVM.Model.Interfaces;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Invoice : IInvoiceable
    {
        public int InvoiceId { get; set; }
        public DateTime DateTime { get; set; }
        public double TotalValueSold { get; set; }
        public double Deductibles {  get; set; }
        public double GrandTotal { get; set; }
        public bool IsPaid { get; set; }
        public int SalesPersonId { get; set; }
        public int AgreementId { get; set; }

        public Invoice(int invoiceId, DateTime dateTime, double totalValueSold, double deductibles, double grandTotal, bool isPaid, int salesPersonId, int agreementId)
        {
            InvoiceId = invoiceId;
            DateTime = dateTime;
            TotalValueSold = totalValueSold;
            Deductibles = deductibles;
            GrandTotal = grandTotal;
            IsPaid = isPaid;
            SalesPersonId = salesPersonId;
            AgreementId = agreementId;
        }

        public Invoice(DateTime dateTime, double totalValueSold, double deductibles, double grandTotal, bool isPaid, int salesPersonId, int agreementId)
        {
            DateTime = dateTime;
            TotalValueSold = totalValueSold;
            Deductibles = deductibles;
            GrandTotal = grandTotal;
            IsPaid = isPaid;
            SalesPersonId = salesPersonId;
            AgreementId = agreementId;
        }

        public void PrintInvoice()
        {
            throw new NotImplementedException();
        }
    }
}
