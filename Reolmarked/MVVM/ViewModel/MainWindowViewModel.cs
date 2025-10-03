using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.ViewModel.Core;

namespace Reolmarked.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public RelayCommand CreateRentalViewCommand { get; set; }
        public RelayCommand MonthlyStatementViewCommand { get; set; }
        public RelayCommand RentalsViewCommand { get; set; }
        public RelayCommand RentersViewCommand { get; set; }
        public RelayCommand SalesViewCommand { get; set; }
        public RelayCommand PriceLabelsViewCommand { get; set; }
        public CreateRentalViewModel CreateRentalVM { get; set; }
        public MonthlyStatementViewModel MonthlyStatementVM { get; set; }
        public ManageRentalsViewModel ManageRentalsVM { get; set; }
        public RentersViewModel RentersVM { get; set; }
        public SalesViewModel SalesVM { get; set; }
        public PriceLabelsViewModel PriceLabelsVM { get; set; }
        public ManageShelvesViewModel ManageShelvesVM { get; set; }

        private object _currentMainWindowView;
        public object CurrentMainWindowView
        {
            get { return _currentMainWindowView; }
            set { _currentMainWindowView = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            RentersVM = new RentersViewModel();
            CreateRentalVM = new CreateRentalViewModel();
            MonthlyStatementVM = new MonthlyStatementViewModel();
            ManageShelvesVM = new ManageShelvesViewModel();
            ManageRentalsVM = new ManageRentalsViewModel();
            SalesVM = new SalesViewModel();
            PriceLabelsVM = new PriceLabelsViewModel();

            CurrentMainWindowView = SalesVM;

            CreateRentalViewCommand = new RelayCommand(o =>
            {
                CurrentMainWindowView = CreateRentalVM;
            });

            MonthlyStatementViewCommand = new RelayCommand(o =>
            {
                CurrentMainWindowView = MonthlyStatementVM;
            });

            RentalsViewCommand = new RelayCommand(o =>
            {
                CurrentMainWindowView = ManageRentalsVM;
            });

            RentersViewCommand = new RelayCommand(o =>
            {
                CurrentMainWindowView = RentersVM;
            });

            SalesViewCommand = new RelayCommand(o =>
            {
                CurrentMainWindowView = SalesVM;
            });

            PriceLabelsViewCommand = new RelayCommand(o =>
            {
                CurrentMainWindowView = PriceLabelsVM;
            });
        }
    }
}
