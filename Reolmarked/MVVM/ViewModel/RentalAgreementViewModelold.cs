using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;
using Reolmarked.MVVM.Model.Repositories;
using Reolmarked.MVVM.ViewModel.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Reolmarked.MVVM.ViewModel
{
    public class RentalAgreementViewModel : ViewModelBase
    {
/*        private readonly IRepository<Shelf> shelfRepository = new ShelfRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Renter> renterRepository = new RenterRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<PaymentMethod> paymentMethodRepository = new PaymentMethodRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<RentalAgreement> rentalRepository = new RentalAgreementRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Shelf_Rental> shelfrentalRepository = new Shelf_RentalRepository(Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Payment> paymentRepository = new PaymentRepository(Config.GetConnectionString("DefaultConnection"));

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

        private DateTime agreementStartDate;
        public DateTime AgreementStartDate
        {
            get { return agreementStartDate; }
            set { agreementStartDate = value; OnPropertyChanged(); }
        }

        private DateTime agreementEndDate;
        public DateTime AgreementEndDate
        {
            get { return agreementEndDate; }
            set { agreementEndDate = value; OnPropertyChanged(); }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set { total = value; OnPropertyChanged(); }
        }

        private RentalAgreementStatus agreementStatus;
        public RentalAgreementStatus AgreementStatus
        {
            get { return agreementStatus; }
            set { agreementStatus = value; OnPropertyChanged(); }
        }

        private Renter selectedRenter;
        public Renter SelectedRenter
        {
            get { return selectedRenter; }
            set { selectedRenter = value; OnPropertyChanged(); }
        }

        private Shelf selectedShelf;
        public Shelf SelectedShelf
        {
            get { return selectedShelf; }
            set { selectedShelf = value; OnPropertyChanged(); }
        }

        private ShelfArrangement selectedArrangement;
        public ShelfArrangement SelectedArrangement 
        {
            get { return selectedArrangement; }
            set { selectedArrangement = value; OnPropertyChanged(); }
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
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; OnPropertyChanged(); }
        }

        private string layout;
        public string Layout
        {
            get { return layout; }
            set { layout = value; OnPropertyChanged(); }
        }

        public ICommand AddRentalCommand { get; }
        public ICommand TerminateRentalCommand { get; }
        public ICommand AddRenterCommand { get; }
        public ICommand UpdateRenterCommand { get; }
        public ICommand LoadShelvesCommand { get; }
        public ICommand AddShelvesCommand { get; }
        public ICommand RemoveShelvesCommand { get; }
        public ICommand ConfirmCommand { get; }

        //private bool CanAddRental() => true;
        private bool CanTerminateRental() => AgreementId != 0 && AgreementEndDate != default; // Lejeaftale skal være valgt og dato skal være sat
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
        private bool CanLoadShelves() => true;
        private bool CanAddShelves() => true;
        private bool CanRemoveShelves() => true;
        private bool CanConfirmUpdate() => true;

        public RentalAgreementViewModel()
        {
            //Collection over reoler hentes fra database
            Shelves = new ObservableCollection<Shelf>(shelfRepository.GetAll());
            ShelvesCollectionView = CollectionViewSource.GetDefaultView(Shelves);

            //Collection over lejeaftaler hentes fra database
            Rentals = new ObservableCollection<RentalAgreement>(rentalRepository.GetAll());
            RentalsCollectionView = CollectionViewSource.GetDefaultView(Rentals);

            AddRentalCommand = new RelayCommand(_ => AddRental(), _ => CanAddRental());
            TerminateRentalCommand = new RelayCommand(_ => TerminateRental(), _ => CanTerminateRental());
            LoadShelvesCommand = new RelayCommand(_ => LoadShelves(), _ => CanLoadShelves());
            AddShelvesCommand = new RelayCommand(_ => AddShelves(), _ => CanAddShelves());
            RemoveShelvesCommand = new RelayCommand(_ => RemoveShelves(), _ => CanRemoveShelves());
            // ConfirmCommand = new RelayCommand(_ => ConfirmUpdate(), _ => CanConfirmUpdate());

            //shelfRepository.Add(new Shelf(10, 4, 8, ShelfArrangement.ShelvesOnly, ShelfStatus.Unavailable, 850));
            //renterRepository.Add(new Renter("Ida", "Andersen", DateTime.Now, "45784589", "ida@andersen", "Barsebæk", "34", "4300", "Holbæk"));
            //employeeRepository.Add(new Employee("Malene", "Bentsen", DateTime.Now, Role.Employee));
            //discountRepository.Add(new Discount("Mængderabat 2-3 reoler", 25));
            //paymentMethodRepository.Add(new PaymentMethod("Dankort", 0));
            //shelfRepository.Delete(1);
            //employeeRepository.Delete(2);
            //renterRepository.Delete(2);
            //discountRepository.Delete(2);
            //paymentMethodRepository.Delete(2);
            //rentalRepository.Add(new RentalAgreement(DateTime.Now, 850, RentalAgreementStatus.Active, 1, 1, 1));
            //rentalRepository.Delete(1003);
            //rentalRepository.Add(new RentalAgreement(DateTime.Now, 1650, RentalAgreementStatus.Active, 1, 1, 1));
        }

        private void LoadShelves()
        {
            // Tømmer de to ObservableCollection-lister, så de ikke indeholder data fra tidligere kald. 
            Shelves.Clear();
            Shelf_Rentals.Clear();

            var all = shelfrentalRepository.GetAll();
            var inAgreement = all.Where(s => s.AgreementId == AgreementId).ToList();
            var notInAgreement = all.Where(s => s.AgreementId != AgreementId).ToList();

            foreach (var shelfRental in notInAgreement)
                //Shelves.Add(shelfRental.); // Ilham Hjælp!!!!!!!

            foreach (var shelf in inAgreement)
                Shelf_Rentals.Add(shelf);
        }

        private void AddShelves()
        {
            foreach (var shelf in Shelf_Rentals)
            {
                shelf.AgreementId = AgreementId;
                //shelf.Arrangement = SelectedArrangement; // Ilham HJÆLPPPP!!!!
                shelfrentalRepository.Update(shelf); // Bruger Update til at tilknytte reolen
            }

            MessageBox.Show($"Tilføjet {Shelf_Rentals.Count} reoler med layout '{SelectedArrangement}'.");
            LoadShelves();
        }

        private void RemoveShelves()
        {
            foreach (var shelf in Shelf_Rentals)
            {
                shelf.EndDate = AgreementEndDate;
                shelfrentalRepository.Update(shelf); // Opdaterer opsigelsesdato
            }

            MessageBox.Show($"Opsagt {Shelf_Rentals.Count} reoler pr. {AgreementEndDate:dd-MM-yyyy}.");
            LoadShelves();
        }

        /*private void ConfirmUpdate()
        {
            var totalPrice = Shelf_Rentals.Sum(s => s.Shelf.ShelfPrice);
            var discount = CalculateDiscount(totalPrice);

            MessageBox.Show($"\n\nNy pris: {totalPrice - discount:C}\nRabat: {discount:C}\nVilkår opdateret.");
        }

        private double CalculateDiscount(double total)
        {
            if (total > 5000) return total * 0.1d; // Eksempel: 10% rabat over 5000 kr. Skal rettes!!!!!!!
            return 0;
        }

        private void AddRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft oprettelse af lejeaftale", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Opret rentalagreement-objekt
                RentalAgreement rental = new RentalAgreement(DateTime.Now, Total, RentalAgreementStatus.CreatedAwaitingPayment, selectedRenter.UserId, selectedDiscount.DiscountId, currentUser.UserId);
                // Tilføj til database via repository
                rentalRepository.Add(rental);
                // Tilføj til observablecollection til UI-view
                Rentals.Add(rental);
                // Tilføj reolid og agreementid til Shelf_Rental
                // While-loop så man kan tilføje flere reoler
                Shelf_Rental shelfRental = new Shelf_Rental(SelectedShelf.ShelfId, rental.AgreementId, DateTime.Now, true);
                shelfrentalRepository.Add(shelfRental);
                Shelf_Rentals.Add(shelfRental);
                // Opret payment-objekt til lejeaftalen
                Payment payment = new Payment(DateTime.Now, PaymentAmount, SelectedPaymentMethod.PaymentMethodId, rental.AgreementId);
                paymentRepository.Add(payment);
                Payments.Add(payment);
                //vis bekræftelse
                MessageBox.Show($"Betaling lykkedes! Lejeaftalenr.: {rental.AgreementId} er oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

                // if payment is succesfull
                // rental.Status = RentalAgreementStatus.Active;
                // rentalRepository.Update(rental);
            }
            else
            {
                MessageBox.Show($"Oprettelse af lejeaftale annulleret!", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //nulstil felter
            SelectedShelf = null;
            SelectedDiscount = null;
            SelectedRenter = null;
            selectedPaymentMethod = null;
            Total = 0;            
        }

        private void TerminateRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft opsigelse af lejeaftale {AgreementId}", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //find lejeaftale i database
                var rental = rentalRepository.GetById(AgreementId); // SelectedAgreement
                if (rental != null)
                {
                    //opdater status og opsigelsesdato
                    rental.Status = RentalAgreementStatus.Cancelled;
                    rental.EndDate = AgreementEndDate;
                    //opdater i database via repository
                    rentalRepository.Update(rental);
                    //opdater i observablecollection til UI-view
                    var rentalInList = Rentals.FirstOrDefault(r => r.AgreementId == AgreementId);
                    if (rentalInList != null)
                    {
                        // - Finder indekset (positionen) af det fundne objekt i listen
                        // - Opdaterer objektet på den position med det nye objekt
                        int index = Rentals.IndexOf(rentalInList);
                        Rentals[index] = rental;
                    }

                    // Deaktiver tilknyttede reoler hvis opsigelsen er i dag
                    if (AgreementEndDate == DateTime.Today)
                    {
                        var shelfrentals = shelfrentalRepository.GetAll()
                            .Where(sr => sr.AgreementId == AgreementId && sr.IsActive)
                            .ToList();

                        foreach (var sr in shelfrentals)
                        {
                            sr.IsActive = false;
                            // sr.IsRented = "Ledig"; // Hvis relevant
                            shelfrentalRepository.Update(sr);
                        }
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
                return;
            }

            //nulstil felter
            AgreementId = 0;
        }

        private bool IsValidCancelDate()
        {
            DateTime today = DateTime.Today;
            DateTime earliestAllowed = new DateTime(today.Year, today.Month, 1).AddMonths(1);

            // Beregn opsigelsesdato hvis ikke allerede valgt
            if (today.Day < 20)
            {
                // Før d. 20: Datoen må være fra og med i dag
                if (SelectedDate < today)
                {
                    MessageBox.Show($"Opsigelsesdatoen skal være fra og med i dag ({today:dd-MM-yyyy}).", "Ugyldig dato", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                AgreementEndDate = SelectedDate;
                return true;
            }
            else
            {
                if (SelectedDate < earliestAllowed)
                {
                    MessageBox.Show($"Opsigelsesdatoen kan tidligst sættes til {earliestAllowed:dd-MM-yyyy}.",
                    "Ugyldig dato!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }

                MessageBoxResult result = MessageBox.Show($"Opsigelsesdato er valgt til {SelectedDate:dd-MM-yyyy}. \n\nVil du gå med denne dato?",
                     "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    MessageBox.Show("Vælg ny dato.", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"Opsigelsesdatoen er valgt til {SelectedDate:dd-MM-yyyy}.", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                    AgreementEndDate = SelectedDate;
                    return true;
                }
            }

            // Overflødig, men er der for at tilfredsstille kompilatoren.
            return false;
        }

        private void AddRenter()
        {
            //opret objekt og tilføj til repository og observablecollection
            Renter renter = new Renter(FirstName, LastName, DateTime.Now, Phone, Email, StreetName, StreetNumber, ZipCode, City);
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

        */
    }
}
