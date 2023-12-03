using eComics.Data.Base;
using eComics.Models;

namespace eComics.Data.Repositories
{
    public class WritersRepository : EntityBaseRepository<Writer>, IWritersRepository
    {
        public WritersRepository(AppDbContext context) : base(context)
        {          
        }
    }
}
