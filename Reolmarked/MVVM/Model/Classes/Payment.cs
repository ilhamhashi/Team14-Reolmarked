namespace Reolmarked.MVVM.Model.Classes
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public int? AgreementId { get; set; }
        public int? SaleId { get; set; }

        public Payment(int paymentId, DateTime paymentDate, double amount, int paymentMethodId, int? agreementId, int? saleId)
        {
            PaymentId = paymentId;
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethodId = paymentMethodId;
            AgreementId = agreementId;
            SaleId = saleId;
        }
        public Payment(DateTime paymentDate, double amount, int paymentMethodId, int? agreementId, int? saleId)
        {
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethodId = paymentMethodId;
            AgreementId = agreementId;
            SaleId = saleId;
        }
    }
}