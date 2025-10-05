namespace Reolmarked.MVVM.Model.Interfaces
{
    public interface IInvoiceable
    {
        DateTime Date { get; set; }
        bool IsPaid { get; set; }

        double RemainingBalance(double amountReceived);
    }
}
