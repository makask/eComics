using eComics.Data.Base;
using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Services
{
    public class ArtistsService : EntityBaseRepository<Artist>, IArtistsService
    {
        public ArtistsService(AppDbContext context) : base(context) { }        
    }
}
