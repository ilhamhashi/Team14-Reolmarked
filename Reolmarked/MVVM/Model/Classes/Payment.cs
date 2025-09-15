namespace Reolmarked.MVVM.Model.Classes
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public RentalAgreement Rental { get; set; }

        public Payment(int paymentId, DateTime paymentDate, double amount, int paymentMethodId, RentalAgreement rental)
        {
            PaymentId = paymentId;
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethodId = paymentMethodId;
            Rental = rental;
        }

        public Payment(DateTime paymentDate, double amount, int paymentMethodId, RentalAgreement rental)
        {
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethodId = paymentMethodId;
            Rental = rental;
        }
    }
}