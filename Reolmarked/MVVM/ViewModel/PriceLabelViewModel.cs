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
    public class PriceLabelsViewModel : ViewModelBase
    {
        private readonly IRepository<Item> itemRepository = new ItemRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        public ObservableCollection<Item>? Items { get; set; }
        public static ICollectionView? ItemsCollectionView { get; set; }

        private int itemId;
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; OnPropertyChanged(); }
        }

        private Shelf selectedShelf;
        public Shelf SelectedShelf
        {
            get { return selectedShelf; }
            set { selectedShelf = value; OnPropertyChanged(); }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        private string barcodeImage;
        public string BarcodeImage
        {
            get { return barcodeImage; }
            set { barcodeImage = value; OnPropertyChanged(); }
        }

        private Item selectedItem;
        public Item SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        public ICommand AddItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        private bool CanAddItem() => true;
        private bool CanRemoveItem() => true;


        public PriceLabelsViewModel()
        {
            Items = new ObservableCollection<Item>(itemRepository.GetAll());
            ItemsCollectionView = CollectionViewSource.GetDefaultView(Items);

            AddItemCommand = new RelayCommand(_ => AddItem(), _ => CanAddItem());
            RemoveItemCommand = new RelayCommand(_ => RemoveItem(), _ => CanRemoveItem());
        }    

        private void AddItem()
        {
            // Opret item-objekt
            Item item = new Item(SelectedShelf.ShelfId, Price, BarcodeImage);
            // Tilføj til database via repository
            itemRepository.Add(item);
            // Tilføj til observablecollection til UI-view
            Items.Add(item);
            //vis bekræftelse
            MessageBox.Show($"Vare oprettet", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RemoveItem()
        {
            itemRepository.Delete(SelectedItem.ItemId);
            Items.Remove(SelectedItem);
        }
    }
}
