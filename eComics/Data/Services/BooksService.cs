using eComics.Data.Base;
using eComics.Data.ViewModels;
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

        public async Task<NewBookDropdownsVM> GetNewBookDropdownsValues()
        {
            var response = new NewBookDropdownsVM()
            {
                Artists = await _context.Artists.OrderBy(n => n.FullName).ToListAsync(),
                Writers = await _context.Writers.OrderBy(n => n.FullName).ToListAsync(),
                Publishers = await _context.Publishers.OrderBy(n => n.Name).ToListAsync()
            };

            return response;
        }
    }
}
