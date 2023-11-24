using eComics.Models;

namespace eComics.Data.Services
{
    public interface IArtistsService
    {
        IEnumerable<Artist> GetAll();
        Artist GetById(int id);
        void Add(Artist artist);
        Artist Update(int id, Artist newArtist);
        void Delete(int id);
    }
}
