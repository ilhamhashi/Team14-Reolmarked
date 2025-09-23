using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.ViewModel.Core;

namespace Reolmarked.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        //public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CreateRentalViewCommand { get; set; }
        public RentalAgreementViewModel CreateRentalVM { get; set; }
        //public DiscoveryViewModel DiscoveryVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            CreateRentalVM = new RentalAgreementViewModel();
            //DiscoveryVM = new DiscoveryViewModel();
            CurrentView = CreateRentalVM;

            CreateRentalViewCommand = new RelayCommand(o =>
            {
                CurrentView = CreateRentalVM;
            });

           /* DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryVM;
            });*/
        }
    }
}
