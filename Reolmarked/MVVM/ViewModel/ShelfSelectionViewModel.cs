using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.ViewModel.Core;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Reolmarked.MVVM.ViewModel
{
    public class ShelfSelectionViewModel : ViewModelBase
    {
        public ICollectionView? ShelvesCollectionView { get; set; }

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

        private Shelf selectedShelf;
        public Shelf SelectedShelf
        {
            get { return selectedShelf; }
            set { selectedShelf = value; OnPropertyChanged(); }
        }

        private Renter? selectedRenter;
        public Renter? SelectedRenter
        {
            get { return selectedRenter; }
            set { selectedRenter = value; OnPropertyChanged(); }
        }

        private bool isAddingMoreShelves;
        public bool IsAddingMoreShelves
        {
            get { return isAddingMoreShelves; }
            set { isAddingMoreShelves = value; OnPropertyChanged(); }
        }
        public ICommand IsAddingMoreShelvesCommand { get; }

        public ShelfSelectionViewModel()
        {
            ShelvesCollectionView = CollectionViewSource.GetDefaultView(ManageShelvesViewModel.ShelvesCollectionView);
            IsAddingMoreShelvesCommand = new RelayCommand(_ => ToggleAddMoreShelves(), _ => true);
        }
        private void ToggleAddMoreShelves()
        {
            IsAddingMoreShelves = !IsAddingMoreShelves;
        }

    }
}
