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
    public class PersonsViewModel : ViewModelBase
    {
        private readonly IRepository<Renter> renterRepository = new RenterRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Person> personRepository = new PersonRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        private readonly IRepository<Employee> employeeRepository = new EmployeeRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        public ObservableCollection<Renter>? Renters { get; set; }
        public ObservableCollection<Employee>? Employees { get; set; }

        public static ICollectionView? RentersCollectionView { get; set; }
        public static ICollectionView? EmployeesCollectionView { get; set; }

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

        private string searchTerm = string.Empty;
        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                searchTerm = value;
                OnPropertyChanged(nameof(RentersFilter));
                RentersCollectionView.Refresh();
            }
        }

        public ICommand AddRenterCommand { get; }
        public ICommand UpdateRenterCommand { get; }
        public ICommand RemoveRenterCommand { get; }

        public PersonsViewModel()
        {
            Renters = new ObservableCollection<Renter>(renterRepository.GetAll());
            RentersCollectionView =  CollectionViewSource.GetDefaultView(Renters);
            Employees = new ObservableCollection<Employee>(employeeRepository.GetAll());
            EmployeesCollectionView = CollectionViewSource.GetDefaultView(Employees);

            RentersCollectionView.Filter = RentersFilter;
            LoadDemoRenters();

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
            personRepository.Add(renter);
            renter.PersonId = personRepository.GetLastInsertedId();
            renterRepository.Add(renter);
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
            personRepository.Update(selectedRenter);
            //vis bekræftelse 
            MessageBox.Show($"Ændringerne er gemt", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            //nulstil felter
            SelectedRenter = null;
        }

        private void RemoveRenter()
        {
            MessageBoxResult result = MessageBox.Show($"Er du sikker på, at du vil slette {SelectedRenter.GetFullName()}?",
                "Er du enig?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show($"{SelectedRenter.GetFullName()} er fjernet fra listen.",
                                "Udført", MessageBoxButton.OK, MessageBoxImage.Information);

                renterRepository.Delete(SelectedRenter.PersonId);
                personRepository.Delete(SelectedRenter.PersonId);
                Renters?.Remove(SelectedRenter);
            }
            else
            {
                MessageBox.Show($"Sletning er ikke gennemført.",
                                "Annulleret", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            selectedRenter = null;
        }

        private bool RentersFilter(object obj)
        {
            if (obj is Renter renter)
            {
                return renter.FirstName.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                       renter.LastName.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                       renter.Phone.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                       renter.Email.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                       renter.Address.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private void LoadDemoRenters()
        {
            if (renterRepository.GetAll().Count() == 0)
            {
                Renter renter1 = new Renter("Peter", "Holm", "47589685", "ph@gmail.com", "Silen 4, 7100 Vejle", DateTime.Now);
                personRepository.Add(renter1);
                renter1.PersonId = personRepository.GetLastInsertedId();
                renterRepository.Add(renter1);
                Renters.Add(renter1);

                Renter renter2 = new Renter("Louise", "Ebersbach", "71124578", "l.e@gmail.com", "Viben 8, 7100 Vejle", DateTime.Now);
                personRepository.Add(renter2);
                renter2.PersonId = personRepository.GetLastInsertedId();
                renterRepository.Add(renter2);
                Renters.Add(renter2);

                Employee employee = new Employee("Mette", "Larsen", DateTime.Now, Role.Employee);
                personRepository.Add(employee);
                employee.PersonId = personRepository.GetLastInsertedId();
                employeeRepository.Add(employee);
                Employees.Add(employee);
            }
        }
    }
}
