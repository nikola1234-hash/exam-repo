using EasyTestMaker.Models;
using EasyTestMaker.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTestMaker.Repositories
{


    public class BaseRepository<T> : IBaseRepository<T> where T: BaseModel
    {
        private readonly IAPIService<T> service = App.GetService<IAPIService<T>>();

        public async Task<bool> CreateAsync(T entity)
        {
            var output = await service.CreateAsync(entity);
            return output;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var output = await service.DeleteAsync(id);
            return output;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var output = await service.GetAllAsync();
            return output;
        }

        public Task<T> GetByNameAsync(string name)
        {
            var output = service.GetByNameAsync(name);
            return output;
        }

        public Task<bool> UpdateAsync(int id, T entity)
        {
            var output = service.UpdateAsync(id, entity);
            return output;

        }
    }
}
