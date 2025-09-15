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
        public ICommand TerminateRentalCommand { get; }
        private bool CanAddRental() => true;
        private bool CanTerminateRental() => AgreementId != null; 

        public RentalAgreementViewModel()
        {
            AddRentalCommand = new RelayCommand(_ => AddRental(), _ => CanAddRental());
            TerminateRentalCommand = new RelayCommand(_ => TerminateRental(), _ => CanTerminateRental());
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

        private void TerminateRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft opsigelse af lejeaftale {AgreementId}", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //find lejeaftale i database
                var rental = rentalRepository.GetById(AgreementId);
                if (rental != null)
                {
                    //opdater status og opsigelsesdato
                    rental.Status = "Opsagt";
                    rental.CancelDate = DateTime.Now;
                    //opdater i database via repository
                    rentalRepository.Update(rental);
                    //opdater i observablecollection til UI-view
                    var rentalInList = Rentals.FirstOrDefault(r => r.AgreementId == AgreementId);
                    if (rentalInList != null)
                    {
                        int index = Rentals.IndexOf(rentalInList);
                        Rentals[index] = rental;
                    }
                    //find tilknyttede reoler i Shelf_Rental og sæt IsActive til false
                    var shelfrentals = shelfrentalRepository.GetAll().Where(sr => sr.AgreementId == AgreementId && sr.IsActive);
                    foreach (var sr in shelfrentals)
                    {
                        sr.IsActive = false;
                        shelfrentalRepository.Update(sr);
                    }
                    //vis bekræftelse
                    MessageBox.Show($"Lejeaftalen {AgreementId} er opsagt!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Lejeaftale {AgreementId} ikke fundet!", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show($"Opsigelse af lejeaftalen er annulleret!", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //nulstil felter
            AgreementId = 0;
        }
    }
}
