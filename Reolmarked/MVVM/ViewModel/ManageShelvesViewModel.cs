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
    public class ManageShelvesViewModel : ViewModelBase
    {
        private readonly IRepository<Shelf> shelfRepository = new ShelfRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        public ObservableCollection<Shelf>? Shelves { get; set; }
        public static ICollectionView? ShelvesCollectionView { get; set; }

        private int shelfId;
        public int Shelfid
        {
            get { return shelfId; }
            set { shelfId = value; OnPropertyChanged(); }
        }

        private int columnIndex;
        public int ColumnIndex
        {
            get { return columnIndex; }
            set { columnIndex = value; OnPropertyChanged(); }
        }

        private int rowIndex;
        public int RowIndex
        {
            get { return rowIndex; }
            set { rowIndex = value; OnPropertyChanged(); }
        }

        private ShelfArrangement arrangement;
        public ShelfArrangement Arrangement
        {
            get { return arrangement; }
            set { arrangement = value; OnPropertyChanged(); }
        }

        private ShelfStatus status;
        public ShelfStatus Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        private double price;   
        public double Price     
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        private Shelf selectedShelf;
        public Shelf SelectedShelf
        {
            get { return selectedShelf; }
            set { selectedShelf = value; OnPropertyChanged(); }
        }

        public ICommand AddShelfCommand { get; }
        public ICommand UpdateShelfCommand { get; }
        public ICommand RemoveShelfCommand { get; }
        private bool CanAddShelf() => true;
        private bool CanUpdateShelf() => true;
        private bool CanRemoveShelf() => true;

        public ManageShelvesViewModel()
        {
            Shelves = new ObservableCollection<Shelf>(shelfRepository.GetAll());
            ShelvesCollectionView = CollectionViewSource.GetDefaultView(Shelves);

            AddShelfCommand = new RelayCommand(_ => AddShelf(), _ => CanAddShelf());
            UpdateShelfCommand = new RelayCommand(_ => UpdateShelf(), _ => CanUpdateShelf());
            RemoveShelfCommand = new RelayCommand(_ => RemoveShelf(), _ => CanRemoveShelf());
        }
        private void AddShelf()
        {
            // Opret shelf-objekt
            Shelf shelf = new Shelf(Shelfid, ColumnIndex, RowIndex, Arrangement, Status, Price);
            // Tilføj til database via repository
            shelfRepository.Add(shelf);
            // Tilføj til observablecollection til UI-view
            Shelves?.Add(shelf);
            //vis bekræftelse
            MessageBox.Show($"Reol oprettet", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateShelf()
        {
            //opdater shelf i repository
            shelfRepository.Update(SelectedShelf);
            //vis bekræftelse 
            MessageBox.Show($"Ændringerne er gemt", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            //nulstil felter
            SelectedShelf = null;
        }

        private void RemoveShelf()
        {
            shelfRepository.Delete(SelectedShelf.ShelfId);
            Shelves?.Remove(SelectedShelf);
        }
    }
}
