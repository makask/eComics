using eComics.Controllers;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using eComics.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using eComics.Data.Enums;

namespace EComics.UnitTests.ControllerTests
{
    public class BooksControllerTests
    {
        private readonly BooksController _controller;
        private readonly Mock<IBooksService> _mockBooksService;

        public BooksControllerTests() 
        { 
          _mockBooksService = new Mock<IBooksService>();  
          _controller = new BooksController(_mockBooksService.Object);
        }

        [Fact]
        public async Task Index_returns_view_with_allBooks_when_no_term_provided()
        {
            // Arrange
            var allBooks = new List<Book>
            {
            new Book { Id = 1, Title = "Book 1" },
            new Book { Id = 2, Title = "Book 2" },
            new Book { Id = 3, Title = "Book 3" }
            };
            _mockBooksService.Setup(x => x.GetBooksWithPublishersAsync()).ReturnsAsync(allBooks);

            // Act
            var result = await _controller.Index() as ViewResult;
            var model = result.Model as BookVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(allBooks.Count, model.Books.Count());
            Assert.Equal(1, model.CurrentPage); // currentPage should default to 1
        }

        [Fact]
        public async Task Index_returns_view_with_filtered_books_when_term_provided()
        {
            // Arrange
            var allBooks = new List<Book>
            {
            new Book { Id = 1, Title = "Book 1" },
            new Book { Id = 2, Title = "Book 2" },
            new Book { Id = 3, Title = "Book 3" }
            };

            _mockBooksService.Setup(x => x.GetBooksWithPublishersAsync()).ReturnsAsync(allBooks);

            // Act
            var result = await _controller.Index(term: "book 1") as ViewResult;
            var model = result.Model as BookVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Single(model.Books); // Only one book should match the term
            Assert.Equal(1, model.CurrentPage); // currentPage should default to 1
        }

        [Fact]
        public async Task Index_returns_view_with_ordered_books()
        {
            // Arrange
            var allBooks = new List<Book>
            {
            new Book { Id = 1, Title = "Book B" },
            new Book { Id = 2, Title = "Book C" },
            new Book { Id = 3, Title = "Book A" }
            };
            _mockBooksService.Setup(x => x.GetBooksWithPublishersAsync()).ReturnsAsync(allBooks);

            // Act
            var result = await _controller.Index(orderBy: "name_desc") as ViewResult;
            var model = result.Model as BookVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal("name_desc", model.OrderBy); // Should have set the orderBy parameter correctly
            Assert.Equal("Book C", model.Books.First().Title); // First book should be Book C due to ordering
        }

        [Fact]
        public async Task Details_returns_view_with_book_details_when_book_found()
        {
            // Arrange
            int id = 1;
            var bookDetails = new Book { Id = id, Title = "Test Book" };
            _mockBooksService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(bookDetails);

            // Act
            var result = await _controller.Details(id) as ViewResult;
            var model = result.Model as Book;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(bookDetails, model);
        }

        [Fact]
        public async Task Details_returns_notFound_when_book_not_found()
        {
            // Arrange
            int id = 1;
            _mockBooksService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_returns_view_with_dropdown_data()
        {
            // Arrange
            var bookDropDownsData = new NewBookDropdownsVM
            {
                Publishers = new List<Publisher> { new Publisher { Id = 1, Name = "Publisher 1" } },
                Artists = new List<Artist> { new Artist { Id = 1, FullName = "Artist 1" } },
                Writers = new List<Writer> { new Writer { Id = 1, FullName = "Writer 1" } }
            };
            _mockBooksService.Setup(x => x.GetNewBookDropdownsValues()).ReturnsAsync(bookDropDownsData);

            // Act
            var result = await _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Assert ViewBag data
            Assert.NotNull(_controller.ViewBag.Publishers);
            Assert.IsType<SelectList>(_controller.ViewBag.Publishers);
            var publishersSelectList = _controller.ViewBag.Publishers as SelectList;
            Assert.Equal(bookDropDownsData.Publishers.Count, publishersSelectList.Count());

            Assert.NotNull(_controller.ViewBag.Artists);
            Assert.IsType<SelectList>(_controller.ViewBag.Artists);
            var artistsSelectList = _controller.ViewBag.Artists as SelectList;
            Assert.Equal(bookDropDownsData.Artists.Count, artistsSelectList.Count());

            Assert.NotNull(_controller.ViewBag.Writers);
            Assert.IsType<SelectList>(_controller.ViewBag.Writers);
            var writersSelectList = _controller.ViewBag.Writers as SelectList;
            Assert.Equal(bookDropDownsData.Writers.Count, writersSelectList.Count());

            // Assert View
            Assert.Null(result.ViewName); // View name should be default
        }

        [Fact]
        public async Task Create_returns_view_with_invalid_modelState()
        {
            // Arrange
            var bookDropDownsData = new NewBookDropdownsVM
            {
                Publishers = new List<Publisher> { new Publisher { Id = 1, Name = "Publisher 1" } },
                Artists = new List<Artist> { new Artist { Id = 1, FullName = "Artist 1" } },
                Writers = new List<Writer> { new Writer { Id = 1, FullName = "Writer 1" } }
            };
            _mockBooksService.Setup(x => x.GetNewBookDropdownsValues()).ReturnsAsync(bookDropDownsData);

            _controller.ModelState.AddModelError("Title", "Title is required");

            var book = new NewBookVM();

            // Act
            var result = await _controller.Create(book) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Assert ViewBag data
            Assert.NotNull(_controller.ViewBag.Publishers);
            Assert.IsType<SelectList>(_controller.ViewBag.Publishers);
            var publishersSelectList = _controller.ViewBag.Publishers as SelectList;
            Assert.Equal(bookDropDownsData.Publishers.Count, publishersSelectList.Count());

            Assert.NotNull(_controller.ViewBag.Artists);
            Assert.IsType<SelectList>(_controller.ViewBag.Artists);
            var artistsSelectList = _controller.ViewBag.Artists as SelectList;
            Assert.Equal(bookDropDownsData.Artists.Count, artistsSelectList.Count());

            Assert.NotNull(_controller.ViewBag.Writers);
            Assert.IsType<SelectList>(_controller.ViewBag.Writers);
            var writersSelectList = _controller.ViewBag.Writers as SelectList;
            Assert.Equal(bookDropDownsData.Writers.Count, writersSelectList.Count());

            // Assert View
            Assert.Equal(book, result.Model); // Model should be the same as input
            Assert.Null(result.ViewName); // View name should be default
        }

        [Fact]
        public async Task Create_redirects_to_index_with_valid_modelState()
        {
            // Arrange
            var book = new NewBookVM { Title = "Test Book" };

            // Act
            var result = await _controller.Create(book) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.Index), result.ActionName);
        }

        [Fact]
        public async Task Edit_returns_notFound_when_book_not_found()
        {
            // Arrange
            int id = 1;
     
            _mockBooksService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Edit_Returns_View_With_Book_Data()
        {
            // Arrange
            int id = 1;
            var bookDetails = new Book
            {
                Id = id,
                Title = "Test Book",
                Description = "Test Description",
                Price = 9.99,
                ReleaseDate = new System.DateTime(2022, 5, 15),
                ImageURL = "test.jpg",
                BookGenre = BookGenre.SciFi, // Use the enum value instead of a string
                PublisherId = 1,
                Artists_Books = new List<Artist_Book> { new Artist_Book { ArtistId = 1 }, new Artist_Book { ArtistId = 2 } },
                Writers_Books = new List<Writer_Book> { new Writer_Book { WriterId = 1 }, new Writer_Book { WriterId = 2 } }
            };
            _mockBooksService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(bookDetails);

            // Act
            var result = await _controller.Edit(id) as ViewResult;
            var model = result.Model as Book; // Adjust model type to Book
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);

            // Assert ViewModel data
            Assert.Equal(bookDetails.Id, model.Id);
            Assert.Equal(bookDetails.Title, model.Title);
            Assert.Equal(bookDetails.Description, model.Description);
            Assert.Equal(bookDetails.Price, model.Price);
            Assert.Equal(bookDetails.ReleaseDate, model.ReleaseDate);
            Assert.Equal(bookDetails.ImageURL, model.ImageURL);
            Assert.Equal(bookDetails.BookGenre, model.BookGenre);
            Assert.Equal(bookDetails.PublisherId, model.PublisherId);
            Assert.Equal(bookDetails.Artists_Books.Select(a => a.ArtistId), model.Artists_Books.Select(a => a.ArtistId)); // Adjust for list comparison
            Assert.Equal(bookDetails.Writers_Books.Select(w => w.WriterId), model.Writers_Books.Select(w => w.WriterId)); // Adjust for list comparison
        }

        [Fact]
        public async Task Edit_returns_notFound_when_id_does_not_match_book_id()
        {
            // Arrange
            int id = 1;
            var book = new NewBookVM { Id = 2 }; // Id doesn't match

            // Act
            var result = await _controller.Edit(id, book) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Edit_returns_view_with_invalid_modelState()
        {
            // Arrange
            int id = 1;
            var bookDropDownsData = new NewBookDropdownsVM
            {
                Publishers = new List<Publisher> { new Publisher { Id = 1, Name = "Publisher 1" } },
                Artists = new List<Artist> { new Artist { Id = 1, FullName = "Artist 1" } },
                Writers = new List<Writer> { new Writer { Id = 1, FullName = "Writer 1" } }
            };
            _mockBooksService.Setup(x => x.GetNewBookDropdownsValues()).ReturnsAsync(bookDropDownsData);
            var book = new NewBookVM { Id = id };
            _controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await _controller.Edit(id, book) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Assert ViewBag data
            Assert.NotNull(_controller.ViewBag.Publishers);
            Assert.IsType<SelectList>(_controller.ViewBag.Publishers);
            var publishersSelectList = _controller.ViewBag.Publishers as SelectList;
            Assert.Equal(bookDropDownsData.Publishers.Count, publishersSelectList.Count());

            Assert.NotNull(_controller.ViewBag.Artists);
            Assert.IsType<SelectList>(_controller.ViewBag.Artists);
            var artistsSelectList = _controller.ViewBag.Artists as SelectList;
            Assert.Equal(bookDropDownsData.Artists.Count, artistsSelectList.Count());

            Assert.NotNull(_controller.ViewBag.Writers);
            Assert.IsType<SelectList>(_controller.ViewBag.Writers);
            var writersSelectList = _controller.ViewBag.Writers as SelectList;
            Assert.Equal(bookDropDownsData.Writers.Count, writersSelectList.Count());

            // Assert View
            Assert.Equal(book, result.Model); // Model should be the same as input
            Assert.Null(result.ViewName); // View name should be default
        }

        [Fact]
        public async Task Edit_redirects_to_index_with_valid_modelState()
        {
            // Arrange
            int id = 1;
            var book = new NewBookVM { Id = id };

            // Act
            var result = await _controller.Edit(id, book) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.Index), result.ActionName);
        }

    }
}
