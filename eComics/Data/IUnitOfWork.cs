using eComics.Data.Repositories;

namespace eComics.Data
{
    public interface IUnitOfWork
    {
        IBooksRepository BooksRepository { get; }

        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
