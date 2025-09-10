using Reolmarked.MVVM.Model.Classes;

namespace Reolmarked.MVVM.Model.Repositories
{
    public class RentalAgreementRepository : IRepository<RentalAgreement>
    {
        private readonly string _connectionString;

        public RentalAgreementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<RentalAgreement> GetAll()
        {
            throw new NotImplementedException();
        }

        public RentalAgreement GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(RentalAgreement entity)
        {
            throw new NotImplementedException();
        }

        public void Update(RentalAgreement entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
