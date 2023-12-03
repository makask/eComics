using eComics.Data.Base;
using eComics.Data.Repositories.Base;
using eComics.Models;

namespace eComics.Data.Services
{
    public interface IWritersService 
    {
        Task<IEnumerable<Writer>> GetAllAsync();
        Task<Writer> GetByIdAsync(int id);
        Task AddAsync(Writer entity);
        Task UpdateAsync(int id, Writer entity);
        Task DeleteAsync(int id);
    }
}
