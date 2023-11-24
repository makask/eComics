using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Services
{
    public class ArtistsService : IArtistsService
    {
        private readonly AppDbContext _context;
        public ArtistsService(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Artist artist)
        {
            _context.Artists.Add(artist);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            var result = await _context.Artists.ToListAsync();
            return result;
        }

        public Artist GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Artist Update(int id, Artist newArtist)
        {
            throw new NotImplementedException();
        }
    }
}
