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
        public ObservableCollection<RentalAgreement>? Rentals { get; set; }
        public static ICollectionView? RentalsCollectionView { get; set; }
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

        private SalesPerson currentUser;
        public SalesPerson CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged(); }
        }

        public ICommand AddRentalCommand { get; }
        private bool CanAddRental() => true;
        public CreateRentalViewModel()
        {
            Rentals = new ObservableCollection<RentalAgreement>(rentalRepository.GetAll());
            RentalsCollectionView = CollectionViewSource.GetDefaultView(Rentals);
            RentersCollectionView = CollectionViewSource.GetDefaultView(RentersViewModel.RentersCollectionView);

            AddRentalCommand = new RelayCommand(_ => AddRental(), _ => CanAddRental());
        }
        private void AddRental()
        {
            MessageBoxResult result = MessageBox.Show($"Bekræft oprettelse af lejeaftale", "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Opret rentalagreement-objekt
                RentalAgreement rental = new RentalAgreement(DateTime.Now, RentalAgreementStatus.CreatedAwaitingPayment, selectedRenter.PersonId, currentUser.PersonId);
                // Tilføj til database via repository
                rentalRepository.Add(rental);
                // Tilføj til observablecollection til UI-view
                Rentals.Add(rental);
                //vis bekræftelse
                MessageBox.Show($"Lejeaftale oprettet! Lejeaftalenr.: {rental.AgreementId} er oprettet!", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Oprettelse af lejeaftale annulleret!", "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //nulstil felter            
            SelectedRenter = null;
        }
    }
}
