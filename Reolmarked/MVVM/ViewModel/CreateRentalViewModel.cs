using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;
using Reolmarked.MVVM.Model.Repositories;
using Reolmarked.MVVM.ViewModel.Core;
using System;
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
        public ICollectionView? EmployeesCollectionView { get; set; }

        public static ICollectionView? RentalsCollectionView { get; set; }
        public static ICollectionView? ShelfRentalsCollectionView { get; set; }
        public static ICollectionView? PaymentsCollectionView { get; set; }

        public RenterSelectionViewModel RenterSelectionVM { get; set; }
        public ShelfSelectionViewModel ShelfSelectionVM { get; set; }
        public NewRentalSummaryViewModel SummaryVM { get; set; }
        public PaymentViewModel PaymentVM { get; set; }

        private Renter? selectedRenter = null;
        public Renter? SelectedRenter
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

        private ShelfRental selectedShelfRental;
        public ShelfRental SelectedShelfRental
        {
            get { return selectedShelfRental; }
            set { selectedShelfRental = value; OnPropertyChanged(); }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return EndDate; }
            set { EndDate = value; OnPropertyChanged(); }
        }

        private double discount;
        public double Discount
        {
            get { return discount; }
            set { discount = value; }
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

        public ICommand AddRentalCommand { get; }
        public ICommand AddShelfToRentalCommand { get; }
        public ICommand PayforRentalCommand { get; }

        private bool CanAddRental() => SelectedRenter != null; 
        private bool CanAddShelfToRental() => SelectedRental != null & selectedShelf != null;

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

            RentersCollectionView = CollectionViewSource.GetDefaultView(PersonsViewModel.RentersCollectionView);
            EmployeesCollectionView = CollectionViewSource.GetDefaultView(PersonsViewModel.EmployeesCollectionView);

            AddRentalCommand = new RelayCommand(_ => AddRental(), _ => CanAddRental());
            ShelvesCollectionView = CollectionViewSource.GetDefaultView(ManageShelvesViewModel.ShelvesCollectionView);
            ShelfRentalsCollectionView.Filter = RenterShelvesFilter;

            AddShelfToRentalCommand = new RelayCommand(_ => AddShelfToRental(), _ => CanAddShelfToRental());
        }

        private void AddRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft oprettelse af ny lejekontrakt for {SelectedRenter.GetFullName()}", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Opret rentalagreement-objekt
                RentalAgreement rental = new RentalAgreement(AgreementStatus.CreatedAwaitingPayment, selectedRenter.PersonId, 9);
                // Tilføj til database via repository
                rentalRepository.Add(rental);
                rental.AgreementId = rentalRepository.GetLastInsertedId();
                // Tilføj til observablecollection til UI-view
                Rentals.Add(rental);
                //vis bekræftelse
                MessageBox.Show($"Lejekontrakt oprettet! Kontraktnr.: {rental.AgreementId} er oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedRenter = null;
            }
            else
            {
                MessageBox.Show($"Oprettelse af lejekontrakt annulleret!", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddShelfToRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft tilføjelse af reol {SelectedShelf.ShelfId} til kontraktnr. {SelectedRental.AgreementId}", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ShelfRental shelfRental = new ShelfRental(SelectedShelf.ShelfId, SelectedRental.AgreementId, DateTime.Now, true, SelectedShelf.Price, Discount);
                shelfRentalRepository.Add(shelfRental);
                ShelfRentals.Add(shelfRental);
                UpdateShelfRentalPrice(shelfRental.AgreementId);
            }
            else
            {
                MessageBox.Show($"Oprettelse af reolleje annulleret!", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            SelectedShelf = null;
            SelectedRental = null;
        }

        private void CancelShelfRental()
        {
            MessageBoxResult result = MessageBox.Show($"Er du sikker på, at reol {SelectedShelfRental.ShelfId} skal opsiges?",
                "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                EndRental();
            }
            else
            {
                MessageBox.Show($"Opsigelse af reolleje annulleret!", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            SelectedShelfRental = null;
        }

        private void EndRental()
        {
            DateTime today = DateTime.Today;
            DateTime earliestAllowed = new DateTime(today.Year, today.Month, 1).AddMonths(1);

            // Beregn opsigelsesdato hvis ikke allerede valgt
            if (today.Day < 20)
            {
                // Før d. 20: Datoen må være fra og med i dag
                if (EndDate < today)
                {
                    MessageBox.Show($"Opsigelsesdatoen skal være fra og med i dag ({today:dd-MM-yyyy}).", "Ugyldig dato", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                SelectedShelfRental.EndDate = EndDate;
            }
            else
            {
                if (EndDate < earliestAllowed)
                {
                    MessageBox.Show($"Opsigelsesdatoen kan tidligst sættes til {earliestAllowed:dd-MM-yyyy}.",
                    "Ugyldig dato!", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                MessageBoxResult result = MessageBox.Show($"Opsigelsesdato er valgt til {EndDate:dd-MM-yyyy}. \n\nVil du gå med denne dato?",
                     "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    MessageBox.Show("Vælg ny dato.", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"Opsigelsesdatoen er valgt til {EndDate:dd-MM-yyyy}.", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
                    SelectedShelfRental.EndDate = EndDate;
                }
            }
        }
        private void PayForRental()
        {
            if (SelectedRental.Status == AgreementStatus.Cancelled || SelectedRental.Status == AgreementStatus.InActive)
            {
                MessageBox.Show($"Lejeaftalen er opsagt eller ikke aktiv. Opret en ny for betaling!", "Mislykkedes", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                PaymentAmount = CalculateTotalRent(SelectedRental.AgreementId);

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
                MessageBox.Show($"Betaling for lejeaftalenr. {SelectedRental.AgreementId} er lykkedes!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

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


        private bool RentalsFilter(object obj)
        {
            if (obj is RentalAgreement rental && SelectedRenter != null)
            {
                return rental.RenterId.Equals(SelectedRenter.PersonId);
            }
            return false;
        }

        private bool RenterShelvesFilter(object obj)
        {
            if (obj is ShelfRental shelfrentals)
            {
                return shelfrentals.AgreementId.Equals(SelectedRental.AgreementId);
            }
            return false;
        }

    }
}
