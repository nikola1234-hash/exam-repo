using Server.EntityFramework.Entities;

namespace Server.Services
{
    public interface IGenericRepository<T> where T : BaseObject
    {
        Task<T> Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Remove(T entity);
        Task<int> Remove(int id);
        Task<T> Update(int id, T entity);
    }
}