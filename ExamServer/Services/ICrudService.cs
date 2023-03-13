using ExamServer.EntityFramework.Entities;

namespace ExamServer.Services
{
    public interface ICrudService<T> where T : BaseObject
    {
        Task<T> Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Remove(T entity);
        Task<int> Remove(int id);
        Task<T> Update(int id, T entity);
    }
}