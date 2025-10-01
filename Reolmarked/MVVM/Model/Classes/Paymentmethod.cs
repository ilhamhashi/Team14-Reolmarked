namespace Reolmarked.MVVM.Model.Classes
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }

        public PaymentMethod(int paymentMethodId, string name)
        {
            PaymentMethodId = paymentMethodId;
            Name = name;
        }

        public PaymentMethod(string name)
        {
            Name = name;
        }
    }
}