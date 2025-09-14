namespace Reolmarked.MVVM.Model.Classes
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public int AgreementId { get; set; }

        public Payment(int paymentId, DateTime paymentDate, double amount, int paymentMethodId, int agreementId)
        {
            PaymentId = paymentId;
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethodId = paymentMethodId;
            AgreementId = agreementId;
        }

        public Payment(DateTime paymentDate, double amount, int paymentMethodId, int agreementId)
        {
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethodId = paymentMethodId;
            AgreementId = agreementId;
        }
    }
}