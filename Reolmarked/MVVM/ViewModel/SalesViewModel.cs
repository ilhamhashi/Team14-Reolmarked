using Microsoft.Extensions.Configuration;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Interfaces;
using Reolmarked.MVVM.Model.Repositories;
using Reolmarked.MVVM.ViewModel.Core;
using System.Windows.Input;

namespace Reolmarked.MVVM.ViewModel
{
    public class SalesViewModel : ViewModelBase
    {
        private readonly IRepository<ItemLine> itemLineRepository = new ItemLineRepository(RentalAgreementViewModel.Config.GetConnectionString("DefaultConnection"));


		private double price;
		public double Price
		{
			get { return price; }
			set { price = value; OnPropertyChanged(); }
		}

        private int itemQuantity;
        public int ItemQuantity
        {
            get { return itemQuantity; }
            set { itemQuantity = value; OnPropertyChanged(); }
        }

        private int saleId;
        public int SaleId
        {
            get { return saleId; }
            set { saleId = value; OnPropertyChanged(); }
        }

		private DateTime saleDateTime;
		public DateTime SaleDateTime
        {
			get { return saleDateTime; }
			set { saleDateTime = value; OnPropertyChanged(); }
		}

		private double subTotal;
		public double Subtotal
		{
			get { return subTotal; }
			set { subTotal = value; OnPropertyChanged(); }
		}


		private double grandTotal;
		public double GrandTotal
		{
			get { return grandTotal; }
			set { grandTotal = value; OnPropertyChanged(); }
		}

		private bool isPaid;
        public bool IsPaid
        {
            get { return isPaid; }
            set { isPaid = value; OnPropertyChanged(); }
        }


        public ICommand AddItemCommand { get; }
        public ICommand AddItemLineCommand { get; }
        public ICommand UpdateItemCommand { get; }        
		public ICommand RemoveItemCommand { get; }


        public SalesViewModel()
        {

        }


	}
}
