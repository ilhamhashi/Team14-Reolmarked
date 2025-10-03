using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.View;
using Reolmarked.MVVM.ViewModel.Core;
using System.Windows;
using System.Windows.Input;

namespace Reolmarked.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public RelayCommand CreateRentalViewCommand { get; set; }
        public RelayCommand MonthlyStatementViewCommand { get; set; }
        public RelayCommand RentersViewCommand { get; set; }
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


        public ICommand CloseCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand MinimizeCommand { get; }

        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            // Window control commands -> Luk, maksimer, minimer
            CloseCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });

            MaximizeCommand = new RelayCommand(o =>
            {
                var window = Application.Current.MainWindow;
                window.WindowState = window.WindowState == WindowState.Normal
                    ? WindowState.Maximized
                    : WindowState.Normal;
            });

            MinimizeCommand = new RelayCommand(o =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });

            // Initial view
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
