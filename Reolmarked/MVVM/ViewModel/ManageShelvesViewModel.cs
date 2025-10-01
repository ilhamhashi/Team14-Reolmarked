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
            //LoadAllShelves();
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

        private void LoadAllShelves()
        {
            IList<int> columnIndexes = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 2, 3, 4, 2, 3, 4, 2, 3, 4, 5, 6, 7, 8, 2, 3, 4, 5, 6, 7, 8, 2, 3, 4, 5, 6, 7, 8, 2, 3, 4, 5, 6, 7, 8, 2, 3, 4, 5, 6, 7, 8, 2, 3, 4, 5, 6, 7, 8, 2, 3, 4, 5, 6, 2, 3, 4, 5, 6, 2, 3, 5, 6 };
            IList<int> rowIndexes = new List<int> {13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 12, 12, 12, 12 };
            int j = 1;
            for (int i = 0; i < 80; i++) 
            {
                var shelf = new Shelf(j++, columnIndexes[i], rowIndexes[i], ShelfArrangement.ShelvesOnly, ShelfStatus.Available, 850);
                shelfRepository.Add(shelf);
            }            
        }
    }
}
