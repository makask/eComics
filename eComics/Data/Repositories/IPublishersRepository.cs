using eComics.Data.Base;
using eComics.Models;

namespace eComics.Data.Repositories
{
    public interface IPublishersRepository : IEntityBaseRepository<Publisher>
    {
        Task<Publisher> GetByIdAsync(int id);
    }
}
