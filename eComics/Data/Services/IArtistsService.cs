using eComics.Models;

namespace eComics.Data.Services
{
    public interface IArtistsService
    {
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist> GetByIdAsync(int id);
        Task AddAsync(Artist artist);
        Artist Update(int id, Artist newArtist);
        void Delete(int id);
    }
}
