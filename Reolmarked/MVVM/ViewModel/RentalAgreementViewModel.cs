using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Reolmarked.MVVM.ViewModel
{
    public class RentalAgreementViewModel : ViewModelBase
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private readonly IRepository<RentalAgreement> rentalRepository = new RentalAgreementRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Shelf_Rental> shelfrentalRepository = new Shelf_RentalRepository(Config.GetConnectionString("DefaultConnection"));

        public ObservableCollection<RentalAgreement>? Rentals { get; set; }
        public ObservableCollection<RentalAgreement>? Shelf_Rental { get; set; }

        public static ICollectionView? RentalsCollectionView { get; set; }
        public static ICollectionView? ShelvesCollectionView { get; set; }
        public static ICollectionView? Shelf_RentalsCollectionView { get; set; }

        private int agreementId;
        public int AgreementId
        {
            get { return agreementId; }
            set { agreementId = value; OnPropertyChanged(); }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; OnPropertyChanged(); }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; OnPropertyChanged(); }
        }

        private DateTime cancelDate;
        public DateTime CancelDate
        {
            get { return cancelDate; }
            set { cancelDate = value; OnPropertyChanged(); }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set { total = value; OnPropertyChanged(); }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        private Renter selectedRenter;
        public Renter SelectedRenter
        {
            get { return selectedRenter; }
            set { selectedRenter = value; OnPropertyChanged(); }
        }

        private Discount selectedDiscount;
        public Discount SelectedDiscount
        {
            get { return selectedDiscount; }
            set { selectedDiscount = value; OnPropertyChanged(); }
        }

        private Employee currentUser;
        public Employee CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged(); }
        }

        private Shelf selectedShelf;
        public Shelf SelectedShelf
        {
            get { return selectedShelf; }
            set { selectedShelf = value; OnPropertyChanged(); }
        }

        public ICommand AddRentalCommand { get; }
        private bool CanAddRental() => true;

        public RentalAgreementViewModel()
        {
            AddRentalCommand = new RelayCommand(_ => AddRental(), _ => CanAddRental());
        }

        private void AddRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft oprettelse af lejeaftale", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Opret lejeaftale-objekt
                RentalAgreement rental = new RentalAgreement(DateTime.Now, EndDate, CancelDate, Total, Status, selectedRenter.UserId, selectedDiscount.DiscountId, currentUser.UserId);
                // Tilføj til database via repository
                rentalRepository.Add(rental);
                // Tilføj til observablecollection til UI-view
                Rentals.Add(rental);
                // Tilføj reolid og lejeaftaleid til Shelf_Rental
                shelfrentalRepository.Add(new Shelf_Rental(SelectedShelf.ShelfId, rental.AgreementId, true));

                //vis bekræftelse
                MessageBox.Show($"Lejeaftalen {rental.AgreementId} oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Oprettelse af lejeaftale annulleret!", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //nulstil felter
            SelectedShelf = null;
            SelectedDiscount = null;
            SelectedRenter = null;
            EndDate = DateTime.Today;
            CancelDate = DateTime.Today;
            Total = 0;
            Status = string.Empty;
        }

    }
}
