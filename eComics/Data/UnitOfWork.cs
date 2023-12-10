using eComics.Data.Repositories;

namespace eComics.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IBooksRepository BooksRepository { get; private set; }

        public UnitOfWork(AppDbContext context, IBooksRepository booksRepository)
        {
            _context = context;
            BooksRepository = booksRepository;
        }

        public async Task BeginTransaction()
        { 
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync(); 
        }
    }
}
