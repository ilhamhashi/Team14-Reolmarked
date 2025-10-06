using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Linq;
using Reolmarked.MVVM.Model;
using Reolmarked.MVVM.Model.Classes;
using Reolmarked.MVVM.Model.Repositories;

namespace Reolmarked.Tests
{
   
    public sealed class Test2
    {
        private SqlConnection _connection;
        private ShelfRepository _shelfRepository;
        private string _connectionString = "DinConnectionStringHer"; 

        
        public void Setup()
        {
            _connection = new SqlConnection(_connectionString);
            _shelfRepository = new ShelfRepository(_connectionString);
            _connection.Open();
        }

        
        public void Cleanup()
        {
            _connection.Close();
        }

        
        public void Add_ShouldInsertShelfSuccessfully()
        {
            
            var shelf = new Shelf(
                shelfId: 9999,
                columnIndex: 1,
                rowIndex: 1,
                arrangement: ShelfArrangement.Horizontal,
                status: ShelfStatus.Available,
                price: 100.0
            );

            
            _shelfRepository.Add(shelf);

            
            var retrieved = _shelfRepository.GetById(shelf.ShelfId);
            Assert.IsNotNull(retrieved);
            Assert.AreEqual(shelf.Price, retrieved.Price);
        }

        
        public void GetAll_ShouldReturnListOfShelves()
        {
            
            var list = _shelfRepository.GetAll().ToList();

            
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        
        public void GetById_ShouldReturnCorrectShelf()
        {
            
            int existingId = 1; 

            
            var shelf = _shelfRepository.GetById(existingId);

            
            Assert.IsNotNull(shelf);
            Assert.AreEqual(existingId, shelf.ShelfId);
        }

        
        public void Update_ShouldChangeShelfValues()
        {
            
            var shelf = new Shelf(
                shelfId: 9999,
                columnIndex: 1,
                rowIndex: 1,
                arrangement: ShelfArrangement.Horizontal,
                status: ShelfStatus.Available,
                price: 100.0
            );
            _shelfRepository.Add(shelf);

            
            shelf.Price = 200.0;
            shelf.Status = ShelfStatus.Occupied;
            _shelfRepository.Update(shelf);

            var updated = _shelfRepository.GetById(shelf.ShelfId);

            
            Assert.IsNotNull(updated);
            Assert.AreEqual(200.0, updated.Price);
            Assert.AreEqual(ShelfStatus.Occupied, updated.Status);
        }

        
        public void Delete_ShouldRemoveShelf()
        {
            
            var shelf = new Shelf(
                shelfId: 9998,
                columnIndex: 2,
                rowIndex: 2,
                arrangement: ShelfArrangement.Vertical,
                status: ShelfStatus.Available,
                price: 150.0
            );
            _shelfRepository.Add(shelf);

            
            _shelfRepository.Delete(shelf.ShelfId);
            var deleted = _shelfRepository.GetById(shelf.ShelfId);

            
            Assert.IsNull(deleted);
        }
    }
}
