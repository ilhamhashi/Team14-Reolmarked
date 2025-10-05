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
    public class SalesViewModel : ViewModelBase
    {
        private readonly IRepository<ItemLine> itemLineRepository = new ItemLineRepository(MainWindowViewModel.Config.GetConnectionString("DefaultConnection"));
        public ObservableCollection<ItemLine>? ItemLines { get; set; }
        public static ICollectionView? ItemLinesCollectionView { get; set; }

        private int itemId;
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; OnPropertyChanged(); }
        }

        private int saleId;
        public int SaleId
        {
            get { return saleId; }
            set { saleId = value; OnPropertyChanged(); }
        }

        private double itemLinePrice;
		public double ItemLinePrice
        {
			get { return itemLinePrice; }
			set { itemLinePrice = value; OnPropertyChanged(); }
		}

        private int itemLineQuantity;
        public int ItemLineQuantity
        {
            get { return itemLineQuantity; }
            set { itemLineQuantity = value; OnPropertyChanged(); }
        }

        private double discount;
        public double Discount
        {
            get { return discount; }
            set { discount = value; OnPropertyChanged(); }
        }

        private DateTime saleDateTime;
		public DateTime SaleDateTime
        {
			get { return saleDateTime; }
			set { saleDateTime = value; OnPropertyChanged(); }
		}

		private double total;
		public double Total
		{
			get { return total; }
			set { total = value; OnPropertyChanged(); }
		}

		private bool isPaid;
        public bool IsPaid
        {
            get { return isPaid; }
            set { isPaid = value; OnPropertyChanged(); }
        }

        private Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged(); }
        }

        private ItemLine selectedItemLine;
        public ItemLine SelectedItemLine
        {
            get { return selectedItemLine; }
            set { selectedItemLine = value; OnPropertyChanged(); }
        }

        public ICommand AddItemLineCommand { get; }
        public ICommand UpdateItemLineCommand { get; }        
		public ICommand RemoveItemLineCommand { get; }

        private bool CanAddItemLine() => true;
        private bool CanUpdateItemLine() => true;
        private bool CanRemoveItemLine() => true;

        public SalesViewModel()
        {
            ItemLines = new ObservableCollection<ItemLine>(itemLineRepository.GetAll());
            ItemLinesCollectionView = CollectionViewSource.GetDefaultView(ItemLines);

            AddItemLineCommand = new RelayCommand(_ => AddItemLine(), _ => CanAddItemLine());
            UpdateItemLineCommand = new RelayCommand(_ => UpdateItemLine(), _ => CanUpdateItemLine());
            RemoveItemLineCommand = new RelayCommand(_ => RemoveItemLine(), _ => CanRemoveItemLine());
        }

        private void AddItemLine()
        {
            // Opret itemLine-objekt
            ItemLine itemline = new ItemLine(ItemId, SaleId, ItemLineQuantity, ItemLinePrice, Discount);
            // Tilføj til database via repository
            itemLineRepository.Add(itemline);
            // Tilføj til observablecollection til UI-view
            ItemLines?.Add(itemline);
        }

        private void UpdateItemLine()
        {
            //opdater itemline i repository
            itemLineRepository.Update(SelectedItemLine);
            //vis bekræftelse 
            MessageBox.Show($"Ændringerne er gemt", "Udført", MessageBoxButton.OK, MessageBoxImage.Information);
            //nulstil felter
            SelectedItemLine = null;
        }

        private void RemoveItemLine()
        {
            itemLineRepository.Delete(SelectedItemLine.ItemId);
            ItemLines?.Remove(SelectedItemLine);
        }
    }
}
