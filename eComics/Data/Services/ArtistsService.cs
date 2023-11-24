﻿using eComics.Models;
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

        public async Task AddAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            var result = await _context.Artists.ToListAsync();
            return result;
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            var result = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            return result;
        }

        public async Task <Artist> UpdateAsync(int id, Artist newArtist)
        {
            _context.Update(newArtist);
            await _context.SaveChangesAsync();
            return newArtist;
        }
    }
}
