using eComics.Data.Base;
using eComics.Data.Enums;
using eComics.Data.Repositories.Base;
using eComics.Data.ViewModels;
using eComics.Models;
using System.Linq.Expressions;

namespace eComics.Data.Repositories
{
    public interface IBooksRepository: IEntityBaseRepository<Book>
    {
        Task<NewBookDropdownsVM> GetNewBookDropdownsValues();
        Task AddNewBookAsync(NewBookVM data);
        Task UpdateBookAsync(NewBookVM data);
        Task<IEnumerable<Book>> GetBooksWithPublishersAsync();
    }
}
