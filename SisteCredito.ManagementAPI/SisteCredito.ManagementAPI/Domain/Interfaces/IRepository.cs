namespace SisteCredito.ManagementAPI.Domain.Interfaces
{
    public interface IRepository
    {
        public interface IRepository<T> where T : class
        {
            Task<IEnumerable<T>> GetAll();
            Task<T> GetById(int id);
            Task Add(T entity);
            Task Update(T entity);
            Task Delete(int id);
            Task <List<T>> GetByDateRange();
        }
    }
}
