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

namespace EComics.UnitTests.ControllerTests
{
    public class PublishersControllerTests
    {
        private readonly PublishersController _controller;
        private readonly Mock<IPublishersService> _mockPublishersService;

        public PublishersControllerTests()
        { 
            _mockPublishersService = new Mock<IPublishersService>();
            _controller = new PublishersController(_mockPublishersService.Object);
        }

        [Fact]
        public async Task Index_returns_view_with_publisher_Data()
        {
            // Arrange
            var allPublishers = new List<Publisher>
            {
            new Publisher { Id = 1, Name = "Publisher 1", Logo = "logo1.jpg", Description = "Description 1" },
            new Publisher { Id = 2, Name = "Publisher 2", Logo = "logo2.jpg", Description = "Description 2" }
            };
            
            _mockPublishersService.Setup(x => x.GetAllAsync()).ReturnsAsync(allPublishers);

            // Act
            var result = await _controller.Index() as ViewResult;
            var model = result.Model as PublisherVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(allPublishers.Count, model.Publishers.Count());
        }

        [Fact]
        public async Task Index_returns_view_with_filtered_publisher_Data()
        {
            // Arrange
            var allPublishers = new List<Publisher>
        {
            new Publisher { Id = 1, Name = "Publisher 1", Logo = "logo1.jpg", Description = "Description 1" },
            new Publisher { Id = 2, Name = "Publisher 2", Logo = "logo2.jpg", Description = "Description 2" }
        };
            
            _mockPublishersService.Setup(x => x.GetAllAsync()).ReturnsAsync(allPublishers);

            // Act
            var result = await _controller.Index("Publisher", "", 1) as ViewResult;
            var model = result.Model as PublisherVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Publishers.Count());
        }

        [Fact]
        public async Task Index_returns_view_with_sorted_publisher_data()
        {
            // Arrange
            var allPublishers = new List<Publisher>
            {
            new Publisher { Id = 1, Name = "Publisher B", Logo = "logo1.jpg", Description = "Description 1" },
            new Publisher { Id = 2, Name = "Publisher A", Logo = "logo2.jpg", Description = "Description 2" }
            };
            
            _mockPublishersService.Setup(x => x.GetAllAsync()).ReturnsAsync(allPublishers);

            // Act
            var result = await _controller.Index("", "name_desc", 1) as ViewResult;
            var model = result.Model as PublisherVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal("Publisher B", model.Publishers.First().Name);
        }

        [Fact]
        public async Task Details_returns_notFound_when_publisher_not_found()
        {
            // Arrange
            int id = 1;
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Publisher)null);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Details_returns_view_with_publisher_details()
        {
            // Arrange
            int id = 1;
            var publisher = new Publisher { Id = id, Name = "Publisher 1", Logo = "logo.jpg", Description = "Description 1" };
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.Details(id) as ViewResult;
            var model = result.Model as Publisher;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(publisher.Id, model.Id);
            Assert.Equal(publisher.Name, model.Name);
            Assert.Equal(publisher.Logo, model.Logo);
            Assert.Equal(publisher.Description, model.Description);
        }

        [Fact]
        public void Create_returns_viewResult()
        {
            // Arrange
      
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_returns_view_with_invalid_modelState()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Name is required"); // Adding ModelState error

            var publisher = new Publisher { Name = "Test Publisher", Description = "Test Description" };

            // Act
            var result = await _controller.Create(publisher) as ViewResult;
            var model = result.Model as Publisher;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisher, model); // Model should be the same as input
        }

        [Fact]
        public async Task Create_redirects_to_index_with_valid_modelState()
        {
            // Arrange
            var publisher = new Publisher { Name = "Test Publisher", Description = "Test Description" };

            // Act
            var result = await _controller.Create(publisher) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.Index), result.ActionName); // Should redirect to Index action
        }

        [Fact]
        public async Task Create_adds_publisher_with_valid_modelState()
        {
            // Arrange
            var publisher = new Publisher { Name = "Test Publisher", Description = "Test Description" };

            // Act
            var result = await _controller.Create(publisher) as RedirectToActionResult;

            // Assert
            _mockPublishersService.Verify(x => x.AddAsync(publisher), Times.Once); // Verify that AddAsync was called with the correct publisher
        }

        [Fact]
        public async Task Edit_returns_notFound_when_publisher_not_found()
        {
            // Arrange
            int id = 1;
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Publisher)null);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Edit_returns_view_with_publisher_details()
        {
            // Arrange
            int id = 1;
            var publisher = new Publisher { Id = id, Name = "Publisher 1", Description = "Description 1" };
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.Edit(id) as ViewResult;
            var model = result.Model as Publisher;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(publisher.Id, model.Id);
            Assert.Equal(publisher.Name, model.Name);
            Assert.Equal(publisher.Description, model.Description);
        }

        [Fact]
        public async Task Edit_returns_view_with_invalid_modelState()
        {
            // Arrange
            int id = 1;
            _controller.ModelState.AddModelError("Name", "Name is required"); // Adding ModelState error

            var publisher = new Publisher { Id = id, Name = "Test Publisher", Description = "Test Description" };

            // Act
            var result = await _controller.Edit(id, publisher) as ViewResult;
            var model = result.Model as Publisher;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisher, model); // Model should be the same as input
        }

        [Fact]
        public async Task Edit_redirects_to_index_with_valid_modelState()
        {
            // Arrange
            int id = 1;
            var publisher = new Publisher { Id = id, Name = "Test Publisher", Description = "Test Description" };

            // Act
            var result = await _controller.Edit(id, publisher) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.Index), result.ActionName); // Should redirect to Index action
        }

        [Fact]
        public async Task Edit_returns_view_with_same_publisher_when_id_Mismatch()
        {
            // Arrange
            int id = 1;
            var publisher = new Publisher { Id = 2, Name = "Test Publisher", Description = "Test Description" };

            // Act
            var result = await _controller.Edit(id, publisher) as ViewResult;
            var model = result.Model as Publisher;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisher, model); // Model should be the same as input
        }

        [Fact]
        public async Task Edit_calls_updateAsync_with_correct_publisher()
        {
            // Arrange
            int id = 1;
            var publisher = new Publisher { Id = id, Name = "Test Publisher", Description = "Test Description" };

            // Act
            var result = await _controller.Edit(id, publisher) as RedirectToActionResult;

            // Assert
            _mockPublishersService.Verify(x => x.UpdateAsync(id, publisher), Times.Once); // Verify that UpdateAsync was called with the correct id and publisher
        }

        [Fact]
        public async Task Delete_returns_notFound_when_publisher_not_found()
        {
            // Arrange
            int id = 1;
           
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Publisher)null);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Delete_returns_view_with_publisher_details()
        {
            // Arrange
            int id = 1;
            var publisher = new Publisher { Id = id, Name = "Publisher 1", Description = "Description 1" };
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.Delete(id) as ViewResult;
            var model = result.Model as Publisher;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(publisher.Id, model.Id);
            Assert.Equal(publisher.Name, model.Name);
            Assert.Equal(publisher.Description, model.Description);
        }

        [Fact]
        public async Task DeleteConfirmed_returns_notFound_when_publisher_not_found()
        {
            // Arrange
            int id = 1;
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Publisher)null);

            // Act
            var result = await _controller.DeleteConfirmed(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task DeleteConfirmed_deletes_publisher_and_redirects_to_index()
        {
            // Arrange
            int id = 1;
          
            var publisher = new Publisher { Id = id, Name = "Test Publisher", Description = "Test Description" };

            // Setup service mock to return a publisher when GetByIdAsync is called
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.Index), result.ActionName); // Should redirect to Index action
            _mockPublishersService.Verify(x => x.DeleteAsync(id), Times.Once); // Verify that DeleteAsync was called with the correct id
        }

        [Fact]
        public async Task PublishersBooks_returns_notFound_when_publisher_not_found()
        {
            // Arrange
            int id = 1;
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Publisher)null);

            // Act
            var result = await _controller.PublishersBooks(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task PublishersBooks_returns_view_with_publishersBooksVM()
        {
            // Arrange
            int id = 1;
            var publisher = new Publisher
            {
                Id = id,
                Name = "Publisher 1",
                Logo = "logo.jpg",
                Books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Description = "Description 1" },
                new Book { Id = 2, Title = "Book 2", Description = "Description 2" }
            }
            };
            
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.PublishersBooks(id) as ViewResult;
            var model = result.Model as PublishersBooksVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(publisher.Id, model.Id);
            Assert.Equal(publisher.Name, model.PublisherName);
            Assert.Equal(publisher.Logo, model.PublisherLogo);
            Assert.Equal(2, model.Books.Count()); // Checking the number of books
        }

        [Fact]
        public async Task PublishersBooks_returns_books_with_term_filtered()
        {
            // Arrange
            int id = 1;
            string term = "book";
            var publisher = new Publisher
            {
                Id = id,
                Name = "Publisher 1",
                Logo = "logo.jpg",
                Books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Description = "Description 1" },
                new Book { Id = 2, Title = "Another Book", Description = "Description 2" },
                new Book { Id = 3, Title = "Book 2", Description = "Description 3" }
            }
            };
            
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.PublishersBooks(id, term) as ViewResult;
            var model = result.Model as PublishersBooksVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Books.Count()); // Only two books contain 'book' in their title
            Assert.True(model.Books.All(b => b.Title.ToLower().Contains(term))); // All titles should contain the term
        }

        [Fact]
        public async Task PublishersBooks_returns_books_ordered_by_title_desc()
        {
            // Arrange
            int id = 1;
            string orderBy = "name_desc";
            var publisher = new Publisher
            {
                Id = id,
                Name = "Publisher 1",
                Logo = "logo.jpg",
                Books = new List<Book>
            {
                new Book { Id = 1, Title = "Book C", Description = "Description 1" },
                new Book { Id = 2, Title = "Book B", Description = "Description 2" },
                new Book { Id = 3, Title = "Book A", Description = "Description 3" }
            }
            };
            
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.PublishersBooks(id, orderBy) as ViewResult;
            var model = result.Model as PublishersBooksVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal("name_desc", model.NameSortOrder); // Ensure order is set to "name_desc"
            Assert.Equal(3, model.Books.Count()); // Checking the number of books
            Assert.Equal("Book C", model.Books.First().Title); // First book should be Book C
        }

        [Fact]
        public async Task PublishersBooks_returns_books_paginated()
        {
            // Arrange
            int id = 1;
            int pageSize = 2;
            int currentPage = 2;
            var publisher = new Publisher
            {
                Id = id,
                Name = "Publisher 1",
                Logo = "logo.jpg",
                Books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Description = "Description 1" },
                new Book { Id = 2, Title = "Book 2", Description = "Description 2" },
                new Book { Id = 3, Title = "Book 3", Description = "Description 3" },
                new Book { Id = 4, Title = "Book 4", Description = "Description 4" }
            }
            };
            
            _mockPublishersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.PublishersBooks(id, currentPage: currentPage) as ViewResult;
            var model = result.Model as PublishersBooksVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(currentPage, model.CurrentPage); // Should be the second page
            Assert.Equal(2, model.Books.Count()); // Should contain only 2 books for the second page
        }
    }
}

