using ExamServer.EntityFramework;
using ExamServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamServer.Services
{
    public class CrudService<T> : ICrudService<T> where T : BaseObject
    {
        private readonly ExamDbContext _context;

        public CrudService(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<T> GetById(int id)
        {

            var fromDb = await _context.Set<T>().FirstOrDefaultAsync(s => s.Id == id);
            if (fromDb != null)
            {
                return fromDb;
            }
            return null;
        }

        public async Task<T> Update(T entity)
        {
            var fromDb = await _context.Set<T>().FirstOrDefaultAsync(s => s.Id == entity.Id);
            if (fromDb != null)
            {
                _context.Entry(fromDb).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return null;
        }


        public async Task<T> Add(T entity)
        {
            var obj = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return obj.Entity;

        }

        public async Task<int> Remove(T entity)
        {
            var itExists = await _context.Set<T>().FirstOrDefaultAsync(s => s.Id == entity.Id) != null;
            if (itExists)
            {
                _context.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;

        }
    }
}
