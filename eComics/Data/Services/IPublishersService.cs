using eComics.Data.Base;
using eComics.Data.Repositories.Base;
using eComics.Models;

namespace eComics.Data.Services
{
    public interface IPublishersService : IEntityBaseRepository<Publisher>
    {
        Task<Publisher> GetPublisherByIdAsync(int id);
      
    }
}
