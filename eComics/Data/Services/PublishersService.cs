using eComics.Data.Base;
using eComics.Models;

namespace eComics.Data.Services
{
    public class PublishersService: EntityBaseRepository<Publisher>, IPublishersService
    {
        public PublishersService(AppDbContext context) : base(context) { }
    }
}
