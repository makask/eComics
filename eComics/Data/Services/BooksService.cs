using eComics.Data.Base;
using eComics.Data.Repositories;
using eComics.Data.ViewModels;
using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Services
{
    public class BooksService : IBooksService
    {

        private readonly IBooksRepository _repository;
        public BooksService(IBooksRepository repository) 
        {
            _repository = repository;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<NewBookDropdownsVM> GetNewBookDropdownsValues()
        {
            return await _repository.GetNewBookDropdownsValues();
        }

        public async Task AddNewBookAsync(NewBookVM data)
        {
            await _repository.AddNewBookAsync(data);
        }

        public async Task UpdateBookAsync(NewBookVM data)
        {         
            await _repository.UpdateBookAsync(data);
        }

        public async Task<IEnumerable<Book>> GetBooksWithPublishersAsync()
        {
            return await _repository.GetBooksWithPublishersAsync();
        }
    }
}