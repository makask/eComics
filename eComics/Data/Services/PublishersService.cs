using eComics.Data.Base;
using eComics.Data.Repositories.Base;
using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Services
{
    public class PublishersService: EntityBaseRepository<Publisher>, IPublishersService
    {
        private readonly new AppDbContext _context;

        public PublishersService(AppDbContext context) : base(context) 
        { 
            _context = context;
        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            var result = await _context.Publishers.Include(b => b.Books).FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }
    }
}
