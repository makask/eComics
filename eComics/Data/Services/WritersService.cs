using eComics.Data.Base;
using eComics.Data.Repositories;
using eComics.Data.Repositories.Base;
using eComics.Models;

namespace eComics.Data.Services
{
    public class WritersService : IWritersService
    {
        private readonly IWritersRepository _repository;
        public WritersService(IWritersRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Writer entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Writer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Writer> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, Writer entity)
        {
            await _repository.UpdateAsync(id, entity);
        }
    }
}
