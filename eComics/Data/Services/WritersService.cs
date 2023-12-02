using eComics.Data.Base;
using eComics.Data.Repositories.Base;
using eComics.Models;

namespace eComics.Data.Services
{
    public class WritersService : EntityBaseRepository<Writer>, IWritersService
    {
        public WritersService(AppDbContext context) : base(context) { }
       
    }
}
