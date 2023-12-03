using eComics.Data.Base;
using eComics.Data.Repositories;
using eComics.Data.Repositories.Base;
using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IPublishersRepository _repository;
        public PublishersService(IPublishersRepository repository) 
        { 
            _repository = repository;
        }

        public async Task AddAsync(Publisher entity)
        {
            await _repository.AddAsync(entity); 
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Publisher> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, Publisher entity)
        {
            await _repository.UpdateAsync(id, entity);
        }
    }
}
