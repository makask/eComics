using eComics.Data.Base;
using eComics.Models;

namespace eComics.Data.Services
{
    public class BooksService:EntityBaseRepository<Book>, IBooksService 
    {
        public BooksService(AppDbContext context) : base(context) { }
        
    }
}
