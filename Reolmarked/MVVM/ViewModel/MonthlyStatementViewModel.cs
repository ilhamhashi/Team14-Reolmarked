using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;
using Reolmarked.MVVM.Model.Repositories;
using Reolmarked.MVVM.ViewModel.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Reolmarked.MVVM.ViewModel
{
    public class MonthlyStatementViewModel : ViewModelBase
    {
        private readonly IRepository<Invoice> invoiceRepository = new InvoiceRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        public ObservableCollection<Invoice>? Invoices { get; set; }
        public static ICollectionView? InvoicesCollectionView { get; set; }

        private int invoiceId;
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; OnPropertyChanged(); }
        }

        private double totalValueSold;
        public double TotalValueSold
        {
            get { return totalValueSold; }
            set { totalValueSold = value; OnPropertyChanged(); }
        }

        private double deductibles;
        public double Deductibles
        {
            get { return deductibles; }
            set { deductibles = value; OnPropertyChanged(); }
        }

        private double grandTotal;
        public double GrandTotal
        {
            get { return grandTotal; }
            set { grandTotal = value; OnPropertyChanged(); }
        }

        private bool isInvoicePaid;
        public bool IsInvoicePaid
        {
            get { return isInvoicePaid; }
            set { isInvoicePaid = value; OnPropertyChanged(); }
        }

        private int salesPersonId;
        public int SalesPersonId
        {
            get { return salesPersonId; }
            set { salesPersonId = value; OnPropertyChanged(); }
        }

        private int agreementId;
        public int AgreementId
        {
            get { return agreementId; }
            set { agreementId = value; OnPropertyChanged(); }
        }

        private Invoice selectedInvoice;
        public Invoice SelectedInvoice
        {
            get { return selectedInvoice; }
            set { selectedInvoice = value; OnPropertyChanged(); }
        }

        private RentalAgreement selectedRental;
        public RentalAgreement SelectedRental
        {
            get { return selectedRental; }
            set { selectedRental = value; OnPropertyChanged(); }
        }

        public ICommand AddInvoiceCommand { get; }
        public ICommand UpdateInvoiceCommand { get; }
        public ICommand RemoveInvoiceCommand { get; }

        private bool CanAddInvoice() => true;
        private bool CanUpdateInvoice() => true;
        private bool CanRemoveInvoice() => true;

        public MonthlyStatementViewModel()
        {
            Invoices = new ObservableCollection<Invoice>(invoiceRepository.GetAll());
            InvoicesCollectionView = CollectionViewSource.GetDefaultView(Invoices);

            AddInvoiceCommand = new RelayCommand(_ => AddInvoice(), _ => CanAddInvoice());
            UpdateInvoiceCommand = new RelayCommand(_ => UpdateInvoice(), _ => CanUpdateInvoice());
            RemoveInvoiceCommand = new RelayCommand(_ => RemoveInvoice(), _ => CanRemoveInvoice());
        }

        private void AddInvoice()
        {
            // Opret invoice-objekt
            Invoice invoice = new Invoice(DateTime.Now, TotalValueSold, Deductibles, GrandTotal, IsInvoicePaid, SalesPersonId, SelectedRental.AgreementId);
            // Tilføj til database via repository
            invoiceRepository.Add(invoice);
            // Tilføj til observablecollection til UI-view
            Invoices?.Add(invoice);
            //vis bekræftelse
            MessageBox.Show($"Faktura oprettet", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateInvoice()
        {
            //opdater invoice i repository
            invoiceRepository.Update(SelectedInvoice);
            //vis bekræftelse 
            MessageBox.Show($"Ændringerne er gemt", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            //nulstil felter
            SelectedInvoice = null;
        }

        private void RemoveInvoice()
        {
            invoiceRepository.Delete(SelectedInvoice.InvoiceId);
            Invoices?.Remove(SelectedInvoice);
        }

    }
}
