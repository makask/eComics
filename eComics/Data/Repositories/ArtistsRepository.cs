using eComics.Data.Base;
using eComics.Models;

namespace eComics.Data.Repositories
{
    public class ArtistsRepository : EntityBaseRepository<Artist>, IArtistsRepository
    {
        public ArtistsRepository(AppDbContext context) : base(context) 
        {           
        }
    }
}
