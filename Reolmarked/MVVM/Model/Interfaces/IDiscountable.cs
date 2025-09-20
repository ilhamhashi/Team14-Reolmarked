namespace Reolmarked.MVVM.Model.Interfaces
{
    public interface IDiscountable : ISellable
    {
        double Discount { get; }
        double DiscountPctg { get; }

        void ReduceSellingPrice();
    }
}
