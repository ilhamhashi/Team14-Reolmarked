using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Linq;
using Reolmarked.MVVM.Model;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Repositories;

namespace Reolmarked.Tests
{
    
    public sealed class Test3
    {
        private SqlConnection _connection;
        private ShelfRentalRepository _shelfRentalRepository;
        private string _connectionString = "DinConnectionStringHer"; 

        
        public void Setup()
        {
            _connection = new SqlConnection(_connectionString);
            _shelfRentalRepository = new ShelfRentalRepository(_connectionString);
            _connection.Open();
        }

        
        public void Cleanup()
        {
            _connection.Close();
        }

        
        public void Add_ShouldInsertShelfRentalSuccessfully()
        {
            
            var rental = new ShelfRental(
                shelfId: 9999,
                agreementId: 9999,
                startDate: DateTime.Now,
                endDate: null,
                isActive: true,
                price: 500.0,
                discount: 50.0
            );

            
            _shelfRentalRepository.Add(rental);

            
            var retrieved = _shelfRentalRepository.GetById(rental.AgreementId);
            Assert.IsNotNull(retrieved);
            Assert.AreEqual(rental.Price, retrieved.Price);
        }

        
        public void GetAll_ShouldReturnListOfShelfRentals()
        {
            
            var list = _shelfRentalRepository.GetAll().ToList();

            
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        
        public void GetById_ShouldReturnCorrectShelfRental()
        {
            
            int existingAgreementId = 1; 

            
            var rental = _shelfRentalRepository.GetById(existingAgreementId);

            
            Assert.IsNotNull(rental);
            Assert.AreEqual(existingAgreementId, rental.AgreementId);
        }

        
        public void Update_ShouldChangeShelfRentalValues()
        {
            
            var rental = new ShelfRental(
                shelfId: 9999,
                agreementId: 9999,
                startDate: DateTime.Now,
                endDate: null,
                isActive: true,
                price: 500.0,
                discount: 50.0
            );
            _shelfRentalRepository.Add(rental);

            
            rental.Price = 750.0;
            rental.Discount = 75.0;
            rental.isActive = false;
            _shelfRentalRepository.Update(rental);

            var updated = _shelfRentalRepository.GetById(rental.AgreementId);

            
            Assert.IsNotNull(updated);
            Assert.AreEqual(750.0, updated.Price);
            Assert.AreEqual(75.0, updated.Discount);
            Assert.IsFalse(updated.isActive);
        }

        
        public void Delete_ShouldRemoveShelfRental()
        {
            
            var rental = new ShelfRental(
                shelfId: 9998,
                agreementId: 9998,
                startDate: DateTime.Now,
                endDate: null,
                isActive: true,
                price: 300.0,
                discount: 30.0
            );
            _shelfRentalRepository.Add(rental);

            
            _shelfRentalRepository.Delete(rental.ShelfId);
            var deleted = _shelfRentalRepository.GetById(rental.AgreementId);

            
            Assert.IsNull(deleted);
        }
    }
}
