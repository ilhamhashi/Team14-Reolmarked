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
    public class CreateRentalViewModel : ViewModelBase
    {
        private readonly IRepository<RentalAgreement> rentalRepository = new RentalAgreementRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Shelf> shelfRepository = new ShelfRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Shelf_Rental> shelfRentalRepository = new Shelf_RentalRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<PaymentMethod> paymentMethodRepository = new PaymentMethodRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Payment> paymentRepository = new PaymentRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));

        public ObservableCollection<RentalAgreement>? Rentals { get; set; }
        public ObservableCollection<Shelf>? Shelves { get; set; }
        public ObservableCollection<Shelf_Rental>? ShelfRentals { get; set; }  
        public ObservableCollection<PaymentMethod>? PaymentMethods { get; set; }
        public ObservableCollection<Payment>? Payments { get; set; }
        
        public static ICollectionView? RentalsCollectionView { get; set; }
        public static ICollectionView? ShelvesCollectionView { get; set; }
        public static ICollectionView? ShelfRentalsCollectionView { get; set; }
        public static ICollectionView? PaymentMethodsCollectionView { get; set; }
        public static ICollectionView? PaymentsCollectionView { get; set; }
        public ICollectionView? RentersCollectionView { get; set; }


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

        private SalesPerson currentUser;
        public SalesPerson CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged(); }
        }

        private bool isAddingMoreShelves;
        public bool IsAddingMoreShelves
        {
            get { return isAddingMoreShelves; }
            set { isAddingMoreShelves = value; OnPropertyChanged(); }
        }

        private RentalAgreement selectedRental;
        public RentalAgreement SelectedRental
        {
            get { return selectedRental; }
            set { selectedRental = value; OnPropertyChanged(); }
        }

        private double paymentAmount;
        public double PaymentAmount
        {
            get { return paymentAmount; }
            set { paymentAmount = value; OnPropertyChanged(); }
        }

        private PaymentMethod selectedPaymentMethod;
        public PaymentMethod SelectedPaymentMethod
        {
            get { return selectedPaymentMethod; }
            set { selectedPaymentMethod = value; OnPropertyChanged(); }
        }

        private double cashReceived;
        public double CashReceived
        {
            get { return cashReceived; }
            set { cashReceived = value; OnPropertyChanged(); }
        }

        private double cashChange;
        public double CashChange
        {
            get { return cashChange; }
            set { cashChange = value; OnPropertyChanged(); }
        }

        private object _currentMainWindowView;
        public object CurrentMainWindowView
        {
            get { return _currentMainWindowView; }
            set { _currentMainWindowView = value; OnPropertyChanged(); }
        }
        public ICommand AddRentalCommand { get; }
        public ICommand IsAddingMoreShelvesCommand { get; }
        public ICommand ResetFieldsCommand { get; }
        public ICommand PayforRentalCommand { get; }

        private bool CanAddRental() => true;
        public CreateRentalViewModel()
        {
            Rentals = new ObservableCollection<RentalAgreement>(rentalRepository.GetAll());
            RentalsCollectionView = CollectionViewSource.GetDefaultView(Rentals);
            RentersCollectionView = CollectionViewSource.GetDefaultView(RentersViewModel.RentersCollectionView);
            Shelves = new ObservableCollection<Shelf>(shelfRepository.GetAll());
            ShelvesCollectionView = CollectionViewSource.GetDefaultView(Shelves);
            ShelfRentals = new ObservableCollection<Shelf_Rental>(shelfRentalRepository.GetAll());
            ShelfRentalsCollectionView = CollectionViewSource.GetDefaultView(ShelfRentals);
            Payments = new ObservableCollection<Payment>(paymentRepository.GetAll());
            PaymentsCollectionView = CollectionViewSource.GetDefaultView(Payments);
            PaymentMethods = new ObservableCollection<PaymentMethod>(paymentMethodRepository.GetAll());
            PaymentMethodsCollectionView = CollectionViewSource.GetDefaultView(PaymentMethods);

            AddRentalCommand = new RelayCommand(_ => AddRental(), _ => CanAddRental());
            IsAddingMoreShelvesCommand = new RelayCommand(_ => ToggleAddMoreShelves(), _ => true);

            CurrentCreateRentalView = ConfirmCustomerAndShelfVM;

            ConfirmCustomerAndShelfViewCommand = new RelayCommand(o =>
            {
                CurrentCreateRentalView = ConfirmCustomerAndShelfVM;
            });
        }
        private void AddRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft oprettelse af lejeaftale", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Opret rentalagreement-objekt
                RentalAgreement rental = new RentalAgreement(DateTime.Now, RentalAgreementStatus.CreatedAwaitingPayment, selectedRenter.PersonId, 2);
                // Tilføj til database via repository
                rentalRepository.Add(rental);
                rental.AgreementId = rentalRepository.GetLastInsertedId();
                SelectedRental = rental;
                // Tilføj til observablecollection til UI-view
                Rentals.Add(SelectedRental);
                //vis bekræftelse
                MessageBox.Show($"Lejeaftale oprettet! Lejeaftalenr.: {SelectedRental.AgreementId} er oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                Shelf_Rental shelfRental = new Shelf_Rental(SelectedShelf.ShelfId, SelectedRental.AgreementId, DateTime.Now, true, selectedShelf.Price, 0, 0);
                shelfRentalRepository.Add(shelfRental);
                ShelfRentals.Add(shelfRental);

                while (isAddingMoreShelves)
                {
                    SelectedShelf = null;
                    Shelf_Rental newshelfRental = new Shelf_Rental(SelectedShelf.ShelfId, SelectedRental.AgreementId, DateTime.Now, true, selectedShelf.Price, 0, 0);
                    shelfRentalRepository.Add(newshelfRental);
                    ShelfRentals.Add(newshelfRental);
                }
                SelectedShelf = null;
                SelectedRenter = null;

            }
            else
            {
                MessageBox.Show($"Oprettelse af lejeaftale annulleret!", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void PayForRental()
        {
            if (SelectedRental.Status == RentalAgreementStatus.Cancelled || selectedRental.Status == RentalAgreementStatus.InActive)
            {
                MessageBox.Show($"Lejeaftalen er opsagt eller ikke aktiv. Opret en ny for betaling!", "Mislykkedes", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                List<Shelf_Rental> rentalShelves = shelfRentalRepository.GetAll().Where(s => s.AgreementId == selectedRental.AgreementId).ToList();
                PaymentAmount = CalculatePaymentAmount(rentalShelves);

                // Beregner byttepenge ved kontantbetaling
                if (SelectedPaymentMethod.Name == "Kontant")
                {
                    CashChange = CalculateCashChange(CashReceived, PaymentAmount);
                }

                // Opretter payment-objekt
                Payment payment = new Payment(DateTime.Now, PaymentAmount, SelectedPaymentMethod.PaymentMethodId, selectedRental.AgreementId);
                paymentRepository.Add(payment);
                Payments.Add(payment);

                // Vis bekræftelse
                MessageBox.Show($"Betaling for lejeaftalenr. {selectedRental.AgreementId} er lykkedes!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

                // Opdaterer lejeaftalestatus efter betaling
                selectedRental.Status = RentalAgreementStatus.Active;
                rentalRepository.Update(selectedRental);
            }

            // Nulstiller felter
            PaymentAmount = 0;
            CashChange = 0;
            CashReceived = 0;
        }

        private double CalculateDiscountPerShelf(int shelfCount)
        {
            if (shelfCount >= 4)
                return 50;

            else if (shelfCount > 2 && shelfCount < 4)
                return 25;
            
            else
                return 0;
        }

        private double CalculatePaymentAmount(List<Shelf_Rental> rentalShelves)
        {
            double paymentAmount = 0;
            int shelfCount = rentalShelves.Count();
            foreach (var shelfrental in rentalShelves)
            {
                paymentAmount += shelfrental.Price - CalculateDiscountPerShelf(shelfCount);
            }

            return paymentAmount;
        }

        private double CalculateCashChange(double cashReceived, double paymentAmount)
        {
            return cashReceived - paymentAmount;
        }

        private void ToggleAddMoreShelves()
        {
            IsAddingMoreShelves = !IsAddingMoreShelves;
        }
    }
}
