using eComics.Data.Base;
using eComics.Data.Repositories.Base;
using eComics.Models;

namespace eComics.Data.Services
{
    public interface IPublishersService 
    {
        Task<IEnumerable<Publisher>> GetAllAsync();
        Task<Publisher> GetByIdAsync(int id);
        Task AddAsync(Publisher entity);
        Task UpdateAsync(int id, Publisher entity);
        Task DeleteAsync(int id);
    }
}
