using eComics.Data.Base;
using eComics.Data.Repositories;
using eComics.Data.Repositories.Base;
using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Services
{
    public class ArtistsService : IArtistsService
    {
        private readonly IArtistsRepository _repository;
        public ArtistsService(IArtistsRepository repository)
        { 
            _repository = repository;
        }

        public async Task AddAsync(Artist entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Artist>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Artist> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, Artist entity)
        {
            await _repository.UpdateAsync(id, entity);
        }
    }
}
