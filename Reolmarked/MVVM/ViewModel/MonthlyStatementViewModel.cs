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
        private readonly IRepository<RentalStatement> statementRepository = new StatementRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IMonthlyStatementOverviewRepository monthlyStatementOverviewRepository = new MonthlyStatementOverviewRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        public ObservableCollection<RentalStatement>? Statements { get; set; }
        public ObservableCollection<MonthlyStatementItem> MonthlyStatementItems { get; set; }
        public static ICollectionView? StatementsCollectionView { get; set; }
        public static ICollectionView? MonthlyStatementItemsCollectionView { get; set; }
        public ICollectionView? RentalAgreementsCollectionView { get; set; }
        public ICollectionView? RentersCollectionView { get; set; }
        public ICollectionView? ShelfRentalsCollectionView { get; set; }
        public ICollectionView? SalesCollectionView { get; set; }

        private int statementId;
        public int StatementId
        {
            get { return statementId; }
            set { statementId = value; OnPropertyChanged(); }
        }

        private double totalValueSold;
        public double TotalValueSold
        {
            get { return totalValueSold; }
            set { totalValueSold = value; OnPropertyChanged(); }
        }

        private double prepaidRent;
        public double PrepaidRent
        {
            get { return prepaidRent; }
            set { prepaidRent = value; OnPropertyChanged(); }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set { total = value; OnPropertyChanged(); }
        }

        private bool isPaid;
        public bool IsPaid
        {
            get { return isPaid; }
            set { isPaid = value; OnPropertyChanged(); }
        }

        private int employeeId;
        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; OnPropertyChanged(); }
        }

        private int agreementId;
        public int AgreementId
        {
            get { return agreementId; }
            set { agreementId = value; OnPropertyChanged(); }
        }

        private RentalStatement selectedStatement;
        public RentalStatement SelectedStatement
        {
            get { return selectedStatement; }
            set { selectedStatement = value; OnPropertyChanged(); }
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

        public ICommand AddStatementCommand { get; }
        public ICommand UpdateStatementCommand { get; }
        public ICommand RemoveStatementCommand { get; }
        public ICommand ShowMonthlyStatementCommand { get; }

        private bool CanAddStatement() => true;
        private bool CanUpdateStatement() => true;
        private bool CanRemoveStatement() => true;
        private bool CanShowMonthlyStatement() => SelectedRenter != null;

        public MonthlyStatementViewModel()
        {
            Statements = new ObservableCollection<RentalStatement>(statementRepository.GetAll());
            StatementsCollectionView = CollectionViewSource.GetDefaultView(Statements);
            MonthlyStatementItems = new ObservableCollection<MonthlyStatementItem>();
            MonthlyStatementItemsCollectionView = CollectionViewSource.GetDefaultView(MonthlyStatementItems);
            MonthlyStatementItemsCollectionView.Filter = FilterRenter;

            LoadOverview();

            AddStatementCommand = new RelayCommand(_ => AddStatement(), _ => CanAddStatement());
            UpdateStatementCommand = new RelayCommand(_ => UpdateStatement(), _ => CanUpdateStatement());
            RemoveStatementCommand = new RelayCommand(_ => RemoveStatement(), _ => CanRemoveStatement());
            ShowMonthlyStatementCommand = new RelayCommand(_ => ShowStatement(), _ => CanShowMonthlyStatement());
        }

        private void AddStatement()
        {
            // Opret invoice-objekt
            RentalStatement statement = new RentalStatement(DateTime.Now, TotalValueSold, PrepaidRent, Total, IsPaid, SelectedRental.AgreementId);
            // Tilføj til database via repository
            statementRepository.Add(statement);
            // Tilføj til observablecollection til UI-view
            Statements?.Add(statement);
            //vis bekræftelse
            MessageBox.Show($"Faktura oprettet", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateStatement()
        {
            //opdater invoice i repository
            statementRepository.Update(SelectedStatement);
            //vis bekræftelse 
            MessageBox.Show($"Ændringerne er gemt", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            //nulstil felter
            SelectedStatement = null;
        }

        private void RemoveStatement()
        {
            statementRepository.Delete(SelectedStatement.StatementId);
            Statements?.Remove(SelectedStatement);
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
