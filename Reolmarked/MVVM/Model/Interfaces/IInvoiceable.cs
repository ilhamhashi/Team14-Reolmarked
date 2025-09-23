namespace Reolmarked.MVVM.Model.Interfaces
{
    public interface IInvoiceable
    {
        DateTime DateTime { get; set; }
        double GrandTotal { get; set; }
        bool IsPaid { get; set; }
        int SalesPersonId { get; set; }

        void PrintInvoice();
    }
}
