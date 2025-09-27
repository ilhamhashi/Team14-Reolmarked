namespace Reolmarked.MVVM.Model.Classes
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }
        public double? Fee { get; set; }

        public PaymentMethod(int paymentMethodId, string name, double? fee)
        {
            PaymentMethodId = paymentMethodId;
            Name = name;
            Fee = fee;
        }

        public PaymentMethod(string name, double? fee)
        {
            Name = name;
            Fee = fee;
        }
    }
}