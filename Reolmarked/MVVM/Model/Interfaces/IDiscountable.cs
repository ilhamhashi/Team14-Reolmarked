namespace Reolmarked.MVVM.Model.Interfaces
{
    public interface IDiscountable : ISellable
    {
        double Discount { get; }
        void ReducePrice();
    }
}
