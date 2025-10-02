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
        private readonly IMonthlyStatementOverviewRepository monthlyStatementOverviewRepository = new MonthlyStatementOverviewRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        public ObservableCollection<Invoice>? Invoices { get; set; }
        public ObservableCollection<MonthlyStatementItem> MonthlyStatementItems { get; set; }
        public static ICollectionView? InvoicesCollectionView { get; set; }
        public static ICollectionView? MonthlyStatementItemsCollectionView { get; set; }
        public ICollectionView? RentalAgreementsCollectionView { get; set; }
        public ICollectionView? RentersCollectionView { get; set; }
        public ICollectionView? ShelfRentalsCollectionView { get; set; }
        public ICollectionView? SalesCollectionView { get; set; }

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

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                MonthlyStatementItemsCollectionView.Refresh();
            }
        }

        private MonthlyStatementItem selectedRenter;
        public MonthlyStatementItem SelectedRenter
        {
            get { return selectedRenter; }
            set { selectedRenter = value; OnPropertyChanged(); }
        }

        public ICommand AddInvoiceCommand { get; }
        public ICommand UpdateInvoiceCommand { get; }
        public ICommand RemoveInvoiceCommand { get; }
        public ICommand ShowMonthlyStatementCommand { get; }

        private bool CanAddInvoice() => true;
        private bool CanUpdateInvoice() => true;
        private bool CanRemoveInvoice() => true;
        private bool CanShowMonthlyStatement() => SelectedRenter != null;

        public MonthlyStatementViewModel()
        {
            Invoices = new ObservableCollection<Invoice>(invoiceRepository.GetAll());
            InvoicesCollectionView = CollectionViewSource.GetDefaultView(Invoices);
            MonthlyStatementItems = new ObservableCollection<MonthlyStatementItem>();
            MonthlyStatementItemsCollectionView = CollectionViewSource.GetDefaultView(MonthlyStatementItems);
            MonthlyStatementItemsCollectionView.Filter = FilterRenter;

            LoadOverview();

            AddInvoiceCommand = new RelayCommand(_ => AddInvoice(), _ => CanAddInvoice());
            UpdateInvoiceCommand = new RelayCommand(_ => UpdateInvoice(), _ => CanUpdateInvoice());
            RemoveInvoiceCommand = new RelayCommand(_ => RemoveInvoice(), _ => CanRemoveInvoice());
            ShowMonthlyStatementCommand = new RelayCommand(_ => ShowStatement(), _ => CanShowMonthlyStatement());
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

        // Metode til at loade statement items i oversigten
        private void LoadOverview()
        {
            try
            {
                // Henter oversigtsdata fra repository (fra database)
                var overviewData = monthlyStatementOverviewRepository.GetOverview();

                // Tømmer den eksisterende ObservableCollection, så vi starter med en frisk liste
                MonthlyStatementItems.Clear();

                // Går igennem hvert element i den hentede liste
                foreach (var item in overviewData)
                {
                    // Tilføjer elementet til ObservableCollection, som automatisk opdaterer UI'et
                    MonthlyStatementItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                // Viser en fejlmeddelelse hvis noget går galt under datahentning
                MessageBox.Show("Fejl ved indlæsning af månedsoversigt: " + ex.Message);
            }
        }

        private bool FilterRenter(object obj)
        {
            if (obj is MonthlyStatementItem item)
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                    return true;

                var lower = SearchText.ToLower();
                return item.RenterName?.ToLower().Contains(lower) == true ||
                       item.ShelfNumbers?.ToLower().Contains(lower) == true ||
                       item.AgreementId.ToString().Contains(lower) == true;
            }
            return false;
        }

        private void ShowStatement()
        {
            // ...
        }
    }
}
