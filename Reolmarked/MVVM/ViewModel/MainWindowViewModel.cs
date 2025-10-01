using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.ViewModel.Core;

namespace Reolmarked.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public RelayCommand CreateRentalViewCommand { get; set; }
        public RelayCommand MonthlyStatementViewCommand { get; set; }
        public RelayCommand RentersViewCommand { get; set; }
        public RelayCommand RentalsViewCommand { get; set; }
        public RelayCommand SalesViewCommand { get; set; }
        public RelayCommand PriceLabelsViewCommand { get; set; }
        public CreateRentalViewModel CreateRentalVM { get; set; }
        public MonthlyStatementViewModel MonthlyStatementVM { get; set; }
        public ManageRentalsViewModel ManageRentalsVM { get; set; }
        public RentersViewModel RentersVM { get; set; }
        public SalesViewModel SalesVM { get; set; }
        public PriceLabelsViewModel PriceLabelsVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            CreateRentalVM = new CreateRentalViewModel();
            MonthlyStatementVM = new MonthlyStatementViewModel();
            ManageRentalsVM = new ManageRentalsViewModel();
            RentersVM = new RentersViewModel();
            SalesVM = new SalesViewModel();
            PriceLabelsVM = new PriceLabelsViewModel();

            CurrentView = SalesVM;

            CreateRentalViewCommand = new RelayCommand(o =>
            {
                CurrentView = CreateRentalVM;
            });

            MonthlyStatementViewCommand = new RelayCommand(o =>
            {
                CurrentView = MonthlyStatementVM;
            });

            RentalsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ManageRentalsVM;
            });

            RentersViewCommand = new RelayCommand(o =>
            {
                CurrentView = RentersVM;
            });

            SalesViewCommand = new RelayCommand(o =>
            {
                CurrentView = SalesVM;
            });

            PriceLabelsViewCommand = new RelayCommand(o =>
            {
                CurrentView = PriceLabelsVM;
            });
        }
    }
}
