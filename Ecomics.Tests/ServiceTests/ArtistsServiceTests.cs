using eComics.Data.Repositories;
using eComics.Data.Services;
using eComics.Models;
using Microsoft.AspNetCore.Routing;
using Moq;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EComics.UnitTests.ServiceTests
{
    public class ArtistsServiceTests
    {
        private readonly IArtistsService _service;
        private readonly Mock<IArtistsRepository> _artistsRepositoryMock;

        public ArtistsServiceTests() 
        { 
            _artistsRepositoryMock = new Mock<IArtistsRepository>();
            _service = new ArtistsService( _artistsRepositoryMock.Object );
        }

        [Fact]
        public async Task AddAsync_Should_Call_Repository_AddAsync_Once()
        {
            // Arrange
            var artist = new Artist { /* Initialize artist properties */ };

            // Act
            await _service.AddAsync(artist);

            // Assert
            _artistsRepositoryMock.Verify(repo => repo.AddAsync(artist), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Call_Repository_DeleteAsync_Once()
        {
            // Arrange
            int artistId = 123; // Example artist ID

            // Act
            await _service.DeleteAsync(artistId);

            // Assert
            _artistsRepositoryMock.Verify(repo => repo.DeleteAsync(artistId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Artists()
        {
            // Arrange
            var expectedArtists = new List<Artist>
        {
            new Artist { Id = 1, FullName = "Artist1" },
            new Artist { Id = 2, FullName = "Artist2" },
            new Artist { Id = 3, FullName = "Artist3" }
        };

            _artistsRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedArtists);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedArtists.Count, result.Count());
            Assert.Equal(expectedArtists, result);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Correct_Artist()
        {
            // Arrange
            int artistId = 123; // Example artist ID
            var expectedArtist = new Artist { Id = artistId, FullName = "Test Artist" };

            _artistsRepositoryMock.Setup(repo => repo.GetByIdAsync(artistId)).ReturnsAsync(expectedArtist);

            // Act
            var result = await _service.GetByIdAsync(artistId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedArtist.Id, result.Id);
            Assert.Equal(expectedArtist.FullName, result.FullName);
        }

        [Fact]
        public async Task UpdateAsync_Should_Call_Repository_UpdateAsync_Once_With_Correct_Parameters()
        {
            // Arrange
            int artistId = 123; // Example artist ID
            var artist = new Artist { Id = artistId, FullName = "Updated Artist" };

            // Act
            await _service.UpdateAsync(artistId, artist);

            // Assert
            _artistsRepositoryMock.Verify(repo => repo.UpdateAsync(artistId, artist), Times.Once);
        }
    }
}
