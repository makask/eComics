using eComics.Data.Base;
using eComics.Data.ViewModels;
using eComics.Models;

namespace eComics.Data.Services
{
    public interface IBooksService : IEntityBaseRepository<Book>
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<NewBookDropdownsVM> GetNewBookDropdownsValues();
    }
}
