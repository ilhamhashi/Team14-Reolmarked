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
    public class RentersViewModel : ViewModelBase
    {
        private readonly IRepository<Renter> renterRepository = new RenterRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        public ObservableCollection<Renter>? Renters { get; set; }
        public static ICollectionView? RentersCollectionView { get; set; }

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

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        private Renter? selectedRenter;
        public Renter? SelectedRenter
        {
            get { return selectedRenter; }
            set { selectedRenter = value; OnPropertyChanged(); }
        }
        
        public ICommand AddRenterCommand { get; }
        public ICommand UpdateRenterCommand { get; }
        public ICommand RemoveRenterCommand { get; }

        public RentersViewModel()
        {
            Renters = new ObservableCollection<Renter>(renterRepository.GetAll());
            RentersCollectionView =  CollectionViewSource.GetDefaultView(Renters);

            AddRenterCommand = new RelayCommand(_ => AddRenter(), _ => CanAddRenter());
            UpdateRenterCommand = new RelayCommand(_ => UpdateRenter(), _ => CanUpdateRenter());
            RemoveRenterCommand = new RelayCommand(_ => RemoveRenter(), _ => CanRemoveRenter());
        }

        private bool CanAddRenter() => !string.IsNullOrWhiteSpace(FirstName)
                            && !string.IsNullOrWhiteSpace(LastName)
                            && !string.IsNullOrWhiteSpace(Phone)
                            && !string.IsNullOrWhiteSpace(Email)
                            && !string.IsNullOrWhiteSpace(Address);
        private bool CanUpdateRenter() => SelectedRenter != null;
        private bool CanRemoveRenter() => SelectedRenter != null;
        private void AddRenter()
        {
            //opret objekt og tilføj til repository og observablecollection
            Renter renter = new Renter(FirstName, LastName, Email, Phone, Address, DateTime.Now);
            renterRepository.Add(renter);
            renter.PersonId = renterRepository.GetLastInsertedId();
            Renters.Add(renter);

            //vis bekræftelse
            MessageBox.Show($"{renter.GetFullName()} er oprettet.", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

            //nulstil felter
            FirstName = string.Empty;
            LastName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
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

        private void RemoveRenter()
        {
            renterRepository.Delete(SelectedRenter.PersonId);
            Renters?.Remove(SelectedRenter);
        }


    }
}
