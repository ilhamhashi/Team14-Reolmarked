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
        private readonly IRepository<ShelfRental> shelfRentalRepository = new ShelfRentalRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<PaymentMethod> paymentMethodRepository = new PaymentMethodRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Payment> paymentRepository = new PaymentRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));

        public ObservableCollection<RentalAgreement>? Rentals { get; set; }
        public ObservableCollection<ShelfRental>? ShelfRentals { get; set; }  
        public ObservableCollection<PaymentMethod>? PaymentMethods { get; set; }
        public ObservableCollection<Payment>? Payments { get; set; }
        
        public ICollectionView? RentersCollectionView { get; set; }
        public ICollectionView? ShelvesCollectionView { get; set; }
        public ICollectionView? PaymentMethodsCollectionView { get; set; }

        public static ICollectionView? RentalsCollectionView { get; set; }
        public static ICollectionView? ShelfRentalsCollectionView { get; set; }
        public static ICollectionView? PaymentsCollectionView { get; set; }

        public RenterSelectionViewModel RenterSelectionVM { get; set; }
        public ShelfSelectionViewModel ShelfSelectionVM { get; set; }
        public NewRentalSummaryViewModel SummaryVM { get; set; }
        public PaymentViewModel PaymentVM { get; set; }

        private Renter? selectedRenter;
        public Renter? SelectedRenter
        {
            get { return selectedRenter; }
            set { 
                selectedRenter = value; 
                OnPropertyChanged(nameof(RenterShelvesFilter));
                ShelfRentalsCollectionView.Refresh();
            }
        }

        private Shelf selectedShelf;
        public Shelf SelectedShelf
        {
            get { return selectedShelf; }
            set { selectedShelf = value; OnPropertyChanged(); }
        }

        private ShelfRental selectedShelfRental;
        public ShelfRental SelectedShelfRental
        {
            get { return selectedShelfRental; }
            set { selectedShelfRental = value; OnPropertyChanged(); }
        }


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

        private AgreementStatus agreementStatus;
        public AgreementStatus AgreementStatus
        {
            get { return agreementStatus; }
            set { agreementStatus = value; OnPropertyChanged(); }
        }

        private Employee currentUser;
        public Employee CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged(); }
        }

        private double discount;
        public double Discount
        {
            get { return discount; }
            set { discount = value; }
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

        private object _currentCreateRentalView;
        public object CurrentCreateRentalView
        {
            get { return _currentCreateRentalView; }
            set { _currentCreateRentalView = value; OnPropertyChanged(); }
        }

        public ICommand ContinueCreatingRentalCommand { get; }        
        public ICommand AddRentalCommand { get; }        
        public ICommand ResetFieldsCommand { get; }
        public ICommand PayforRentalCommand { get; }

        private bool CanAddRental() => true; 
        private bool canContinueFromRenterSelectionVM => true;
        private bool canContinueFromShelfSelectionVM => SelectedShelf != null;
        //private bool canContinueFromSummaryVM => HasConfirmedTC == true;

        public CreateRentalViewModel()
        {            
            Rentals = new ObservableCollection<RentalAgreement>(rentalRepository.GetAll());
            RentalsCollectionView = CollectionViewSource.GetDefaultView(Rentals);
            ShelfRentals = new ObservableCollection<ShelfRental>(shelfRentalRepository.GetAll());
            ShelfRentalsCollectionView = CollectionViewSource.GetDefaultView(ShelfRentals);
            Payments = new ObservableCollection<Payment>(paymentRepository.GetAll());
            PaymentsCollectionView = CollectionViewSource.GetDefaultView(Payments);
            PaymentMethods = new ObservableCollection<PaymentMethod>(paymentMethodRepository.GetAll());
            PaymentMethodsCollectionView = CollectionViewSource.GetDefaultView(PaymentMethods);
            RentersCollectionView = CollectionViewSource.GetDefaultView(RentersViewModel.RentersCollectionView);
            AddRentalCommand = new RelayCommand(_ => AddRental(), _ => CanAddRental());
            ShelvesCollectionView = CollectionViewSource.GetDefaultView(ManageShelvesViewModel.ShelvesCollectionView);
            ShelfRentalsCollectionView.Filter = RenterShelvesFilter;

            ContinueCreatingRentalCommand = new RelayCommand(_ => Continue(), _ => true);
            RenterSelectionVM = new RenterSelectionViewModel();
            ShelfSelectionVM = new ShelfSelectionViewModel();
            SummaryVM = new NewRentalSummaryViewModel();
            PaymentVM = new PaymentViewModel();
            CurrentCreateRentalView = RenterSelectionVM;

            //RentersViewCommand = new RelayCommand(o =>
            //{
            //    CurrentCreateRentalView = RentersVM;
            //});

            //ShelfSelectionViewCommand = new RelayCommand(o =>
            //{
            //    CurrentCreateRentalView = ShelfSelectionVM;
            //});

            //ConfirmCustomerAndShelfViewCommand = new RelayCommand(o =>
            //{
            //    CurrentCreateRentalView = ConfirmCustomerAndShelfVM;
            //});
        }
        
        private void AddRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft oprettelse af lejeaftale", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Opret rentalagreement-objekt
                RentalAgreement rental = new RentalAgreement(AgreementStatus.CreatedAwaitingPayment, selectedRenter.PersonId, CurrentUser.PersonId);
                // Tilføj til database via repository
                rentalRepository.Add(rental);
                rental.AgreementId = rentalRepository.GetLastInsertedId();
                SelectedRental = rental;
                // Tilføj til observablecollection til UI-view
                Rentals.Add(SelectedRental);
                //vis bekræftelse
                MessageBox.Show($"Lejeaftale oprettet! Lejeaftalenr.: {SelectedRental.AgreementId} er oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                ShelfRental shelfRental = new ShelfRental(SelectedShelf.ShelfId, SelectedRental.AgreementId, DateTime.Now, true, selectedShelf.Price, Discount);
                shelfRentalRepository.Add(shelfRental);
                ShelfRentals.Add(shelfRental);

                while (isAddingMoreShelves)
                {
                    SelectedShelf = null;
                    ShelfRental newshelfRental = new ShelfRental(SelectedShelf.ShelfId, SelectedRental.AgreementId, DateTime.Now, true, selectedShelf.Price, Discount);
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
            if (SelectedRental.Status == AgreementStatus.Cancelled || selectedRental.Status == AgreementStatus.InActive)
            {
                MessageBox.Show($"Lejeaftalen er opsagt eller ikke aktiv. Opret en ny for betaling!", "Mislykkedes", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                PaymentAmount = CalculateTotalRent(agreementId);

                // Beregner byttepenge ved kontantbetaling
                if (SelectedPaymentMethod.Name == "Kontant")
                {
                    CashChange = CalculateCashChange(CashReceived, PaymentAmount);
                }

                // Opretter payment-objekt
                Payment payment = new Payment(DateTime.Now, PaymentAmount, SelectedPaymentMethod.PaymentMethodId, selectedRental.AgreementId, null);
                paymentRepository.Add(payment);
                Payments.Add(payment);

                // Vis bekræftelse
                MessageBox.Show($"Betaling for lejeaftalenr. {selectedRental.AgreementId} er lykkedes!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

                // Opdaterer lejeaftalestatus efter betaling
                selectedRental.Status = AgreementStatus.Active;
                rentalRepository.Update(selectedRental);
            }

            // Nulstiller felter
            PaymentAmount = 0;
            CashChange = 0;
            CashReceived = 0;
        }

        private void UpdateShelfRentalPrice(int agreementId)
        {
            List<ShelfRental> shelfRentals = shelfRentalRepository.GetAll().Where(r => r.AgreementId == agreementId).ToList();

            foreach (var shelfrental in shelfRentals)
            {
                shelfrental.GetPrice(shelfRentals.Count());
                shelfRentalRepository.Update(shelfrental);
            }
        }

        private double CalculateTotalRent(int agreementId)
        {
            List<ShelfRental> shelfRentals = shelfRentalRepository.GetAll().Where(r => r.AgreementId == agreementId).ToList();

            double totalRent = 0;
            foreach (var shelfrental in shelfRentals)
            {
                totalRent += shelfrental.Price;
            }
            return totalRent;
        }

        private double CalculateCashChange(double cashReceived, double paymentAmount)
        {
            return cashReceived - paymentAmount;
        }

        private void Continue()
        {
            if (CurrentCreateRentalView == RenterSelectionVM && canContinueFromRenterSelectionVM)
            {
                CurrentCreateRentalView = ShelfSelectionVM;
            }
            else if (CurrentCreateRentalView == ShelfSelectionVM && canContinueFromShelfSelectionVM)
            {
                CurrentCreateRentalView = SummaryVM;
            }
            else if (CurrentCreateRentalView == SummaryVM )
            {
                CurrentCreateRentalView = PaymentVM;
            }
            else
            {
                CurrentCreateRentalView = RenterSelectionVM;
            }
        }

        private bool RenterShelvesFilter(object obj)
        {
            if (obj is ShelfRental shelfrentals)
            {
                return shelfrentals.AgreementId.Equals(selectedRental.AgreementId);
            }
            return false;
        }

    }
}
