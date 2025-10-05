using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Linq;
using Reolmarked.MVVM.Model;
using Reolmarked.MVVM.Model.Repositories;

namespace Reolmarked.Tests
{
    [TestClass]
    public sealed class Test1
    {
        private SqlConnection _connection;
        private StatementRepository _statementRepository;
        private string _connectionString = "DinConnectionStringHer"; 

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqlConnection(_connectionString);
            _statementRepository = new StatementRepository(_connectionString);
            _connection.Open();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _connection.Close();
        }

        [TestMethod]
        public void Add_ShouldInsertStatementSuccessfully()
        {
            
            var statement = new RentalStatement(
                statementId: 9999,
                date: DateTime.Now,
                totalValueSold: 1000.0,
                prepaidRent: 200.0,
                total: 1200.0,
                isPaid: false,
                agreementId: 1
            );

            
            _statementRepository.Add(statement);

            
            var retrieved = _statementRepository.GetById(statement.StatementId);
            Assert.IsNotNull(retrieved);
            Assert.AreEqual(statement.Total, retrieved.Total);
        }

        [TestMethod]
        public void GetAll_ShouldReturnListOfStatements()
        {
            
            var list = _statementRepository.GetAll().ToList();

            
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectStatement()
        {
            
            int existingId = 1; 

            
            var statement = _statementRepository.GetById(existingId);

            
            Assert.IsNotNull(statement);
            Assert.AreEqual(existingId, statement.StatementId);
        }

        [TestMethod]
        public void Update_ShouldChangeStatementValues()
        {
            
            var statement = new RentalStatement(
                statementId: 9999,
                date: DateTime.Now,
                totalValueSold: 500,
                prepaidRent: 100,
                total: 600,
                isPaid: false,
                agreementId: 1
            );
            _statementRepository.Add(statement);

            
            statement.TotalValueSold = 999.0;
            statement.Total = 1099.0;
            _statementRepository.Update(statement);

            var updated = _statementRepository.GetById(statement.StatementId);

            
            Assert.IsNotNull(updated);
            Assert.AreEqual(999.0, updated.TotalValueSold);
        }

        [TestMethod]
        public void Delete_ShouldRemoveStatement()
        {
            
            var statement = new RentalStatement(
                statementId: 9998,
                date: DateTime.Now,
                totalValueSold: 200,
                prepaidRent: 50,
                total: 250,
                isPaid: true,
                agreementId: 1
            );
            _statementRepository.Add(statement);

            
            _statementRepository.Delete(statement.StatementId);
            var deleted = _statementRepository.GetById(statement.StatementId);

            
            Assert.IsNull(deleted);
        }
    }
}
