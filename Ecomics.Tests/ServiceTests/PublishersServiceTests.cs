using eComics.Data.Repositories;
using eComics.Data.Services;
using eComics.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EComics.UnitTests.ServiceTests
{
    public class PublishersServiceTests
    {
        private readonly IPublishersService _service;
        private readonly Mock<IPublishersRepository> _publishersRepositoryMock;

        public PublishersServiceTests()
        {
            _publishersRepositoryMock = new Mock<IPublishersRepository>();
            _service = new PublishersService(_publishersRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_Call_Repository_AddAsync_Once_With_Correct_Entity()
        {
            // Arrange
            var publisher = new Publisher { Name = "Publisher1" };
         
            // Act
            await _service.AddAsync(publisher);

            // Assert
            _publishersRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Publisher>(p => p.Name == publisher.Name)), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Call_Repository_DeleteAsync_Once_With_Correct_Id()
        {
            // Arrange
            int publisherId = 1; // Example publisher ID

            // Act
            await _service.DeleteAsync(publisherId);

            // Assert
            _publishersRepositoryMock.Verify(repo => repo.DeleteAsync(publisherId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Publishers()
        {
            // Arrange

            var expectedPublishers = new List<Publisher>
        {
            new Publisher { Id = 1, Name = "Publisher1" },
            new Publisher { Id = 2, Name = "Publisher2" },
            new Publisher { Id = 3, Name = "Publisher3" }
        };

            _publishersRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedPublishers);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPublishers.Count, result.Count());
            Assert.Equal(expectedPublishers, result);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Correct_Publisher()
        {
            // Arrange
            int publisherId = 123; // Example publisher ID
            var expectedPublisher = new Publisher { Id = publisherId, Name = "Test Publisher" };

            _publishersRepositoryMock.Setup(repo => repo.GetByIdAsync(publisherId)).ReturnsAsync(expectedPublisher);

            // Act
            var result = await _service.GetByIdAsync(publisherId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPublisher.Id, result.Id);
            Assert.Equal(expectedPublisher.Name, result.Name);
        }

        [Fact]
        public async Task UpdateAsync_Should_Call_Repository_UpdateAsync_Once_With_Correct_Parameters()
        {
            // Arrange
            int publisherId = 123; // Example publisher ID
            var publisher = new Publisher { Id = publisherId, Name = "Updated Publisher" };

            // Act
            await _service.UpdateAsync(publisherId, publisher);

            // Assert
            _publishersRepositoryMock.Verify(repo => repo.UpdateAsync(publisherId, publisher), Times.Once);
        }
    }
}
