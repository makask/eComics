using eComics.Data.Base;
using eComics.Data.Repositories.Base;
using eComics.Models;
using System.Linq.Expressions;

namespace eComics.Data.Services
{
    public interface IArtistsService 
    {
        Task<IEnumerable<Artist>> GetAllAsync(); 
        Task<Artist> GetByIdAsync(int id);
        Task AddAsync(Artist entity);
        Task UpdateAsync(int id, Artist entity);
        Task DeleteAsync(int id);
    }
}
