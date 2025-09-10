using Reolmarked.MVVM.Model.Classes;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class Shelf_RentalAgreementRepository : IRepository<Shelf_RentalAgreement>
    {
        private readonly string _connectionString;

        public Shelf_RentalAgreementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Shelf_RentalAgreement> GetAll()
        {
            throw new NotImplementedException();
        }

        public Shelf_RentalAgreement GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Shelf_RentalAgreement entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Shelf_RentalAgreement entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
