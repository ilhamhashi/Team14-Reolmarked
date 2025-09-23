namespace Reolmarked.MVVM.Model.Interfaces
{
    public interface IRentable : IDiscountable
    {
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
        bool IsActive { get; set; }
        void BeginRental();
        void EndRental();
    }
}
