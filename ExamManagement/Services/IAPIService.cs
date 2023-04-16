using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTestMaker.Services
{
    public interface IAPIService<T>
    {
        Task<bool> CreateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByNameAsync(string name);
        Task<bool> UpdateAsync(int id, T entity);
    }
}