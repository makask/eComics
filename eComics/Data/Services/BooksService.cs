using eComics.Data.Base;
using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Services
{
    public class BooksService:EntityBaseRepository<Book>, IBooksService 
    {
        private readonly AppDbContext _context;
        public BooksService(AppDbContext context) : base(context) 
        { 
            _context = context;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var bookDetails = await _context.Books
                .Include(p => p.Publisher)
                .Include(ab => ab.Artists_Books).ThenInclude(a => a.Artist)
                .Include(wb => wb.Writers_Books).ThenInclude(w => w.Writer)
                .FirstOrDefaultAsync(i => i.Id == id);

            return bookDetails;
        }
    }
}
