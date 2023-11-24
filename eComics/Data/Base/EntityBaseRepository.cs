using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
       
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
        
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
      

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
