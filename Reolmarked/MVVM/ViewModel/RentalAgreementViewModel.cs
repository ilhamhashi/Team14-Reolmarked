using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Reolmarked.MVVM.ViewModel
{
    public class RentalAgreementViewModel : ViewModelBase
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private readonly IRepository<Shelf> shelfRepository = new ShelfRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<RentalAgreement> rentalRepository = new RentalAgreementRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Shelf_Rental> shelfrentalRepository = new Shelf_RentalRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Payment> paymentRepository = new PaymentRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Renter> renterRepository = new RenterRepository(Config.GetConnectionString("DefaultConnection"));

        public ObservableCollection<RentalAgreement>? Rentals { get; set; }
        public ObservableCollection<Shelf>? Shelves { get; set; }
        public ObservableCollection<Shelf_Rental>? Shelf_Rentals { get; set; }
        public ObservableCollection<Payment>? Payments { get; set; }
        public ObservableCollection<Renter>? Renters { get; set; }

        public static ICollectionView? RentalsCollectionView { get; set; }
        public static ICollectionView? ShelvesCollectionView { get; set; }
        public static ICollectionView? Shelf_RentalsCollectionView { get; set; }
        public static ICollectionView? RentersCollectionView { get; set; }

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

        private PaymentMethod selectedPaymentMethod;
        public PaymentMethod SelectedPaymentMethod
        {
            get { return selectedPaymentMethod; }
            set { selectedPaymentMethod = value; OnPropertyChanged(); }
        }

        private double paymentAmount;
        public double PaymentAmount
        {
            get { return paymentAmount; }
            set { paymentAmount = value; OnPropertyChanged(); }
        }

        private int renterId;
        public int RenterId
        {
            get { return renterId; }
            set { renterId = value; OnPropertyChanged(); }
        }


        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(); }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string streetName;
        public string StreetName
        {
            get { return streetName; }
            set { streetName = value; OnPropertyChanged(); }
        }
        private string streetNumber;
        public string StreetNumber
        {
            get { return streetNumber; }
            set { streetNumber = value; OnPropertyChanged(); }
        }

        private string zipCode;     
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; OnPropertyChanged(); }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged(); }
        }

        public ICommand AddRentalCommand { get; }
<<<<<<< HEAD
        public ICommand TerminateRentalCommand { get; }
        private bool CanAddRental() => true;
        private bool CanTerminateRental() => AgreementId != null; 
=======
        public ICommand AddRenterCommand { get; }
        public ICommand UpdateRenterCommand { get; }

        private bool CanAddRental() => SelectedShelf != null;
        private bool CanAddRenter() => !string.IsNullOrWhiteSpace(FirstName)
                                    && !string.IsNullOrWhiteSpace(LastName)
                                    && !string.IsNullOrWhiteSpace(Phone)
                                    && !string.IsNullOrWhiteSpace(Email)
                                    && !string.IsNullOrWhiteSpace(StreetName)
                                    && !string.IsNullOrWhiteSpace(StreetNumber)
                                    && !string.IsNullOrWhiteSpace(ZipCode)
                                    && !string.IsNullOrWhiteSpace(City);
        private bool CanUpdateRenter() => SelectedRenter != null;
>>>>>>> a42525cbebdccbdf3217df2f05d0b047594cd7a0

        public RentalAgreementViewModel()
        {
            //Collection over reoler hentes fra database
            Shelves = new ObservableCollection<Shelf>(shelfRepository.GetAll());
            ShelvesCollectionView = CollectionViewSource.GetDefaultView(Shelves);

            //Collection over lejeaftaler hentes fra database
            Rentals = new ObservableCollection<RentalAgreement>(rentalRepository.GetAll());
            RentalsCollectionView = CollectionViewSource.GetDefaultView(Rentals);

            AddRentalCommand = new RelayCommand(_ => AddRental(), _ => CanAddRental());
<<<<<<< HEAD
            TerminateRentalCommand = new RelayCommand(_ => TerminateRental(), _ => CanTerminateRental());
=======


>>>>>>> a42525cbebdccbdf3217df2f05d0b047594cd7a0
        }

        private void AddRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft oprettelse af lejeaftale", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Opret rentalagreement-objekt
                RentalAgreement rental = new RentalAgreement(DateTime.Now, EndDate, CancelDate, Total, Status, selectedRenter.UserId, selectedDiscount.DiscountId, currentUser.UserId);
                // Tilføj til database via repository
                rentalRepository.Add(rental);
                // Tilføj til observablecollection til UI-view
                Rentals.Add(rental);
                // Tilføj reolid og agreementid til Shelf_Rental
                // While-loop så man kan tilføje flere reoler
                Shelf_Rental shelfRental = new Shelf_Rental(SelectedShelf, rental, true);
                shelfrentalRepository.Add(shelfRental);
                Shelf_Rentals.Add(shelfRental);
                // Opret payment-objekt til lejeaftalen
                Payment payment = new Payment(DateTime.Now, PaymentAmount, SelectedPaymentMethod.PaymentMethodId, rental);
                paymentRepository.Add(payment);
                Payments.Add(payment);

                //vis bekræftelse
                MessageBox.Show($"Betaling lykkedes! Lejeaftalenr.: {rental.AgreementId} er oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
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

<<<<<<< HEAD
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
=======
        private void AddRenter()
        {
            //opret objekt og tilføj til repository og observablecollection
            Renter renter = new Renter(RenterId, FirstName, LastName, DateTime.Now, Phone, Email, StreetName, StreetNumber, ZipCode, City);
            renterRepository.Add(renter);
            Renters.Add(renter);

            //vis bekræftelse
            MessageBox.Show($"{FirstName} {LastName} oprettet.", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

            //nulstil felter
            FirstName = string.Empty;
            LastName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            StreetName = string.Empty;
            ZipCode = string.Empty;
            City = string.Empty;
        }

        private void UpdateRenter()
        {
            //opdater renter i repository
            renterRepository.Update(SelectedRenter);
            //vis bekræftelse 
            MessageBox.Show($"Ændringerne er gemt", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            //nulstil felter
            SelectedRenter = null;
        }

>>>>>>> a42525cbebdccbdf3217df2f05d0b047594cd7a0
    }
}
