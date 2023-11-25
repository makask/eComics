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

        public async Task AddNewBookAsync(NewBookVM data)
        {
            var newBook = new Book()
            {
                Title = data.Title,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                ReleaseDate = data.ReleaseDate,
                BookGenre = data.BookGenre,
                PublisherId = data.PublisherId,
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            foreach (var artistId in data.ArtistIds)
            {
                var newArtistBook = new Artist_Book()
                {
                    BookId = newBook.Id,
                    ArtistId = artistId
                };
                await _context.Artists_Books.AddAsync(newArtistBook);
            }
            await _context.SaveChangesAsync();

            foreach (var writerId in data.WriterIds)
            {
                var newWriterBook = new Writer_Book()
                {
                    BookId = newBook.Id,
                    WriterId = writerId
                };
                await _context.Writers_Books.AddAsync(newWriterBook);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(NewBookVM data)
        {
            var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == data.Id);

            if (dbBook != null)
            {
                dbBook.Title = data.Title;
                dbBook.Description = data.Description;
                dbBook.Price = data.Price;
                dbBook.ImageURL = data.ImageURL;
                dbBook.ReleaseDate = data.ReleaseDate;
                dbBook.BookGenre = data.BookGenre;
                dbBook.PublisherId = data.PublisherId;               
                await _context.SaveChangesAsync();
            }

            var existingArtistsDb = _context.Artists_Books.Where(n => n.BookId == data.Id).ToList();
            _context.Artists_Books.RemoveRange(existingArtistsDb);
            await _context.SaveChangesAsync();

            var existingWritersDb = _context.Writers_Books.Where(b => b.BookId == data.Id).ToList();
            _context.Writers_Books.RemoveRange(existingWritersDb);
            await _context.SaveChangesAsync();
            
            foreach (var artistId in data.ArtistIds)
            {
                var newArtistBook = new Artist_Book()
                {
                    BookId = data.Id,
                    ArtistId = artistId
                };
                await _context.Artists_Books.AddAsync(newArtistBook);
            }
            await _context.SaveChangesAsync();

            foreach (var writerId in data.WriterIds)
            {
                var newWriterBook = new Writer_Book()
                {
                    BookId = data.Id,
                    WriterId = writerId
                };
                await _context.Writers_Books.AddAsync(newWriterBook);
            }
            await _context.SaveChangesAsync();
        }
    }
}
