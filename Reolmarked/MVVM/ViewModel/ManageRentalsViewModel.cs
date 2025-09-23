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
    public class RentalAgreementViewModel : ViewModelBase
    {
        private readonly IRepository<Shelf> shelfRepository = new ShelfRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Renter> renterRepository = new RenterRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<SalesPerson> employeeRepository = new EmployeeRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Discount> discountRepository = new DiscountRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<PaymentMethod> paymentMethodRepository = new PaymentMethodRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));

        private readonly IRepository<RentalAgreement> rentalRepository = new RentalAgreementRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Shelf_Rental> shelfrentalRepository = new Shelf_RentalRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Payment> paymentRepository = new PaymentRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));

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

        private Discount selectedDiscount;
        public Discount SelectedDiscount
        {
            get { return selectedDiscount; }
            set { selectedDiscount = value; OnPropertyChanged(); }
        }

        private SalesPerson currentUser;
        public SalesPerson CurrentUser
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
        private bool CanTerminateRental() => AgreementId != 0 && CancelDate != default; // Lejeaftale skal være valgt og dato skal være sat
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
            TerminateRentalCommand = new RelayCommand(_ => TerminateRental(), _ => CanTerminateRental());
            LoadShelvesCommand = new RelayCommand(_ => LoadShelves(), _ => CanLoadShelves());
            AddShelvesCommand = new RelayCommand(_ => AddShelves(), _ => CanAddShelves());
            RemoveShelvesCommand = new RelayCommand(_ => RemoveShelves(), _ => CanRemoveShelves());
            ConfirmCommand = new RelayCommand(_ => ConfirmUpdate(), _ => CanConfirmUpdate());
        }

        private void LoadShelves()
        {
            // Tømmer de to ObservableCollection-lister, så de ikke indeholder data fra tidligere kald. 
            Shelves.Clear();
            Shelf_Rentals.Clear();

            var all = shelfrentalRepository.GetAll();
            var inAgreement = all.Where(s => s.Rental.AgreementId == AgreementId).ToList();
            var notInAgreement = all.Where(s => s.Rental.AgreementId != AgreementId).ToList();

            foreach (var shelfRental in notInAgreement)
                Shelves.Add(shelfRental.Shelf);

            foreach (var shelf in inAgreement)
                Shelf_Rentals.Add(shelf);
        }

        private void AddShelves()
        {
            foreach (var shelf in Shelf_Rentals)
            {
                shelf.Rental.AgreementId = AgreementId;
                shelf.Shelf.ShelfArrangement = Layout;
                shelfrentalRepository.Update(shelf); // Bruger Update til at tilknytte reolen
            }

            MessageBox.Show($"Tilføjet {Shelf_Rentals.Count} reoler med layout '{Layout}'.");
            LoadShelves();
        }

        private void RemoveShelves()
        {
            foreach (var shelf in Shelf_Rentals)
            {
                shelf.Rental.EndDate = EndDate;
                shelfrentalRepository.Update(shelf); // Opdaterer opsigelsesdato
            }

            MessageBox.Show($"Opsagt {Shelf_Rentals.Count} reoler pr. {CancelDate:dd-MM-yyyy}.");
            LoadShelves();
        }

        private void ConfirmUpdate()
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
                RentalAgreement rental = new RentalAgreement(DateTime.Now, Total, RentalAgreementStatus.CreatedAwaitingPayment, selectedRenter.PersonId, selectedDiscount.DiscountId, currentUser.PersonId);
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
                    rental.Status = "Opsagt";
                    rental.CancelDate = CancelDate;
                    //opdater i database via repository
                    rentalRepository.Update(rental);
                    //opdater i observablecollection til UI-view
                    var rentalInList = Rentals.FirstOrDefault(r => r.AgreementId == AgreementId);
                    if (rentalInList != null)
                    {
                        int index = Rentals.IndexOf(rentalInList);
                        Rentals[index] = rental;
                    }

                    // Deaktiver tilknyttede reoler hvis opsigelsen er i dag
                    if (EndDate == DateTime.Today)
                    {
                        var shelfrentals = shelfrentalRepository.GetAll()
                            .Where(sr => sr.Rental.AgreementId == AgreementId && sr.IsActive)
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

                EndDate = SelectedDate;
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
                    EndDate = SelectedDate;
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
    }
}
