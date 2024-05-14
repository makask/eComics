using eComics.Data.Repositories;
using eComics.Data.Services;
using eComics.Models;
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
    public class WritersServiceTests
    {
        private readonly IWritersService _service;
        private readonly Mock<IWritersRepository> _writersRepositoryMock;
        public WritersServiceTests() 
        { 
            _writersRepositoryMock = new Mock<IWritersRepository>();
            _service = new WritersService(_writersRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_Call_Repository_AddAsync_Once_With_Correct_Entity()
        {
            // Arrange
            var writer = new Writer { FullName = "Writer1" };

            // Act
            await _service.AddAsync(writer);

            // Assert
            _writersRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Writer>(w => w.FullName == writer.FullName)), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Call_Repository_DeleteAsync_Once_With_Correct_Id()
        {
            // Arrange
            int writerId = 1; // Example writer ID

            // Act
            await _service.DeleteAsync(writerId);

            // Assert
            _writersRepositoryMock.Verify(repo => repo.DeleteAsync(writerId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Writers()
        {
            // Arrange
            var expectedWriters = new List<Writer>
        {
            new Writer { Id = 1, FullName = "Writer1" },
            new Writer { Id = 2, FullName = "Writer2" },
            new Writer { Id = 3, FullName = "Writer3" }
        };

            _writersRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedWriters);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedWriters.Count, result.Count());
            Assert.Equal(expectedWriters, result);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Correct_Writer()
        {
            // Arrange
            int writerId = 123; // Example writer ID
            var expectedWriter = new Writer { Id = writerId, FullName = "Test Writer" };

            _writersRepositoryMock.Setup(repo => repo.GetByIdAsync(writerId)).ReturnsAsync(expectedWriter);

            // Act
            var result = await _service.GetByIdAsync(writerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedWriter.Id, result.Id);
            Assert.Equal(expectedWriter.FullName, result.FullName);
        }

        [Fact]
        public async Task UpdateAsync_Should_Call_Repository_UpdateAsync_Once_With_Correct_Parameters()
        {
            // Arrange
            int writerId = 123; // Example writer ID
            var writer = new Writer { Id = writerId, FullName = "Updated Writer" };

            // Act
            await _service.UpdateAsync(writerId, writer);

            // Assert
            _writersRepositoryMock.Verify(repo => repo.UpdateAsync(writerId, writer), Times.Once);
        }
    }
}
