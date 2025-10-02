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
        public CreateRentalViewModel CreateRentalVM { get; set; }
        public MonthlyStatementViewModel MonthlyStatementVM { get; set; }

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
            CreateRentalVM = new CreateRentalViewModel();

            CurrentView = CreateRentalVM;

            CreateRentalViewCommand = new RelayCommand(o =>
            {
                CurrentView = CreateRentalVM;
            });

            // Tilføj flere views og commands her
            MonthlyStatementVM = new MonthlyStatementViewModel();
            MonthlyStatementViewCommand = new RelayCommand(o =>
            {
                CurrentView = MonthlyStatementVM;
            });
        }
    }
}
