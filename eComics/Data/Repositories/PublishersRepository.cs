using eComics.Data.Base;
using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Repositories
{
    public class PublishersRepository : EntityBaseRepository<Publisher>, IPublishersRepository
    {
        public PublishersRepository(AppDbContext context) : base(context) 
        {          
        }

        public override async Task<Publisher> GetByIdAsync(int id)
        {
            var result = await _context.Publishers.Include(b => b.Books).FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }
    }
}
