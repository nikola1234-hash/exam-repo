using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTestMaker.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<bool> CreateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByNameAsync(string name);
        Task<bool> UpdateAsync(int id, T entity);
    }
}
