using eComics.Data.Enums;
using eComics.Data.Repositories;
using eComics.Data.Services;
using eComics.Data.ViewModels;
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
    public class BooksServiceTests
    {
        private readonly IBooksService _service;
        private readonly Mock<IBooksRepository> _booksRepositoryMock;

        public BooksServiceTests()
        { 
            _booksRepositoryMock = new Mock<IBooksRepository>();
            _service = new BooksService(_booksRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Correct_Artist()
        {
            // Arrange
            int artistId = 123; // Example artist ID
            var expectedArtist = new Artist { Id = artistId, FullName = "Test Artist" };

            //_booksRepositoryMock.Setup(repo => repo.GetByIdAsync(artistId)).ReturnsAsync(expectedArtist);

            _booksRepositoryMock.Setup(repo => repo.GetByIdAsync(artistId));
            // Act
            var result = await _service.GetByIdAsync(artistId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedArtist.Id, result.Id);
            Assert.Equal(expectedArtist.FullName, result.Title);
        }

        [Fact]
        public async Task GetNewBookDropdownsValues_Should_Return_Correct_Values()
        {
            // Arrange
            var expectedDropdowns = new NewBookDropdownsVM
            {
                Publishers = new List<Publisher>
            {
                new Publisher { Id = 1, Name = "Publisher1" },
                new Publisher { Id = 2, Name = "Publisher2" },
                new Publisher { Id = 3, Name = "Publisher3" }
            },
                Artists = new List<Artist>
            {
                new Artist { Id = 1, FullName = "Artist1" },
                new Artist { Id = 2, FullName = "Artist2" },
                new Artist { Id = 3, FullName = "Artist3" }
            },
                Writers = new List<Writer>
            {
                new Writer { Id = 1, FullName = "Writer1" },
                new Writer { Id = 2, FullName = "Writer2" },
                new Writer { Id = 3, FullName = "Writer3" }
            }
            };

            _booksRepositoryMock.Setup(repo => repo.GetNewBookDropdownsValues()).ReturnsAsync(expectedDropdowns);

            // Act
            var result = await _service.GetNewBookDropdownsValues();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDropdowns.Publishers, result.Publishers);
            Assert.Equal(expectedDropdowns.Artists, result.Artists);
            Assert.Equal(expectedDropdowns.Writers, result.Writers);
        }

        [Fact]
        public async Task AddNewBookAsync_Should_Call_Repository_AddNewBookAsync_Once_With_Correct_Data()
        {
            // Arrange
            var newBookData = new NewBookVM
            {
                Id = 1,
                Title = "Sample Book",
                Description = "This is a sample book description.",
                Price = 10.99,
                ImageURL = "https://example.com/sample-book-cover.jpg",
                ReleaseDate = DateTime.Now,
                BookGenre = BookGenre.Action,
                ArtistIds = new List<int> { 1, 2, 3 },
                WriterIds = new List<int> { 4, 5 },
                PublisherId = 1
            };

            // Act
            await _service.AddNewBookAsync(newBookData);

            // Assert
            _booksRepositoryMock.Verify(repo => repo.AddNewBookAsync(It.Is<NewBookVM>(data =>
                data.Id == newBookData.Id &&
                data.Title == newBookData.Title &&
                data.Description == newBookData.Description &&
                data.Price == newBookData.Price &&
                data.ImageURL == newBookData.ImageURL &&
                data.ReleaseDate == newBookData.ReleaseDate &&
                data.BookGenre == newBookData.BookGenre &&
                data.ArtistIds == newBookData.ArtistIds &&
                data.WriterIds == newBookData.WriterIds &&
                data.PublisherId == newBookData.PublisherId)), Times.Once);
        }

        [Fact]
        public async Task UpdateBookAsync_Should_Call_Repository_UpdateBookAsync_Once_With_Correct_Data()
        {
            // Arrange
            var updatedBookData = new NewBookVM
            {
                Id = 1,
                Title = "Updated Book",
                Description = "This is an updated book description.",
                Price = 15.99,
                ImageURL = "https://example.com/updated-book-cover.jpg",
                ReleaseDate = DateTime.Now.AddDays(-10),
                BookGenre = BookGenre.Horror,
                ArtistIds = new List<int> { 1, 2 },
                WriterIds = new List<int> { 3 },
                PublisherId = 2
            };

            // Act
            await _service.UpdateBookAsync(updatedBookData);

            // Assert
            _booksRepositoryMock.Verify(repo => repo.UpdateBookAsync(It.Is<NewBookVM>(data =>
                data.Id == updatedBookData.Id &&
                data.Title == updatedBookData.Title &&
                data.Description == updatedBookData.Description &&
                data.Price == updatedBookData.Price &&
                data.ImageURL == updatedBookData.ImageURL &&
                data.ReleaseDate == updatedBookData.ReleaseDate &&
                data.BookGenre == updatedBookData.BookGenre &&
                data.ArtistIds == updatedBookData.ArtistIds &&
                data.WriterIds == updatedBookData.WriterIds &&
                data.PublisherId == updatedBookData.PublisherId)), Times.Once);
        }

        [Fact]
        public async Task GetBooksWithPublishersAsync_Should_Return_Books_With_Publishers()
        {
            // Arrange
            var expectedBooks = new List<Book>
        {
            new Book { Id = 1, Title = "Book1", Publisher = new Publisher { Id = 1, Name = "Publisher1" } },
            new Book { Id = 2, Title = "Book2", Publisher = new Publisher { Id = 2, Name = "Publisher2" } },
            new Book { Id = 3, Title = "Book3", Publisher = new Publisher { Id = 3, Name = "Publisher3" } }
        };

            _booksRepositoryMock.Setup(repo => repo.GetBooksWithPublishersAsync()).ReturnsAsync(expectedBooks);

            // Act
            var result = await _service.GetBooksWithPublishersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBooks.Count, result.Count());

            // Assert each book's properties
            foreach (var expectedBook in expectedBooks)
            {
                var actualBook = result.FirstOrDefault(b => b.Id == expectedBook.Id);
                Assert.NotNull(actualBook);
                Assert.Equal(expectedBook.Id, actualBook.Id);
                Assert.Equal(expectedBook.Title, actualBook.Title);

                Assert.NotNull(actualBook.Publisher);
                Assert.Equal(expectedBook.Publisher.Id, actualBook.Publisher.Id);
                Assert.Equal(expectedBook.Publisher.Name, actualBook.Publisher.Name);
            }
        }
    }
}

