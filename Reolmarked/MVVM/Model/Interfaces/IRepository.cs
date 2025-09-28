namespace Reolmarked.MVVM.Model.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        int GetLastInsertedId();
        void Update(T entity);
        void Delete(int id);
    }
}
