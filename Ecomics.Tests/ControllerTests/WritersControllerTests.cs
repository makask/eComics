using eComics.Controllers;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using eComics.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EComics.UnitTests.ControllerTests
{
    public class WritersControllerTests
    {
        // dsad
        private readonly WritersController _controller;
        private readonly Mock<IWritersService> _mockWritersService;

        public WritersControllerTests() {
            _mockWritersService = new Mock<IWritersService>();
            _controller = new WritersController(_mockWritersService.Object);
        }

        [Fact]
        public async Task Index_returns_view_with_writerVM()
        {
            // Arrange
            var writers = new[]
            {
            new Writer { Id = 1, FullName = "Writer 1", Bio = "Bio 1" },
            new Writer { Id = 2, FullName = "Writer 2", Bio = "Bio 2" }
        };
            _mockWritersService.Setup(x => x.GetAllAsync()).ReturnsAsync(writers);

            // Act
            var result = await _controller.Index() as ViewResult;
            var model = result.Model as WriterVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Writers.Count()); // Check if there are two writers in the model
        }

        [Fact]
        public async Task Index_filters_writers_by_term()
        {
            // Arrange
            var writers = new[]
            {
            new Writer { Id = 1, FullName = "Writer 1", Bio = "Bio 1" },
            new Writer { Id = 2, FullName = "Writer 2", Bio = "Bio 2" }
            };
            _mockWritersService.Setup(x => x.GetAllAsync()).ReturnsAsync(writers);

            // Act
            var result = await _controller.Index(term: "writer 1") as ViewResult;
            var model = result.Model as WriterVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Single(model.Writers); // Only one writer with name "Writer 1" should be in the model
        }

        [Fact]
        public async Task Index_orders_writers_by_name()
        {
            // Arrange
            var writers = new[]
            {
            new Writer { Id = 1, FullName = "Writer B", Bio = "Bio B" },
            new Writer { Id = 2, FullName = "Writer A", Bio = "Bio A" }
            };
            _mockWritersService.Setup(x => x.GetAllAsync()).ReturnsAsync(writers);

            // Act
            var result = await _controller.Index(orderBy: "name_desc") as ViewResult;
            var model = result.Model as WriterVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal("name_desc", model.NameSortOrder); // Check if the order is set to "name_desc"
            Assert.Equal("Writer B", model.Writers.First().FullName); // The first writer should be "Writer B" due to descending order
        }

        [Fact]
        public async Task Index_returns_correct_page_of_writers()
        {
            // Arrange
            var writers = Enumerable.Range(1, 10).Select(i => new Writer { Id = i, FullName = $"Writer {i}", Bio = $"Bio {i}" });
            _mockWritersService.Setup(x => x.GetAllAsync()).ReturnsAsync(writers);

            // Act
            var result = await _controller.Index(currentPage: 2) as ViewResult;
            var model = result.Model as WriterVM;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.CurrentPage); // Current page should be 2
            Assert.Equal(4, model.TotalPages); // Total pages should be 4
            Assert.Equal(4, model.Writers.Count()); // There should be 4 writers in the model for the second page
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
        public void Create_returns_default_view()
        {
            // Arrange

            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact]
        public async Task Create_invalid_modelState_returns_view_with_writer_model()
        {
            // Arrange
            var writer = new Writer { FullName = "John Doe" };
           
            _controller.ModelState.AddModelError("Bio", "The Bio field is required.");

            // Act
            var result = await _controller.Create(writer) as ViewResult;
            var resultModel = result.Model as Writer;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(writer, resultModel); // Check if the returned model is the same as the input model
        }

        [Fact]
        public async Task Create_valid_modelState_redirects_to_index()
        {
            // Arrange
            var writer = new Writer { FullName = "John Doe", Bio = "Some bio" };
            _mockWritersService.Setup(x => x.AddAsync(It.IsAny<Writer>())).Returns(Task.CompletedTask);
           
            // Act
            var result = await _controller.Create(writer) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Check if it redirects to Index action
        }

        [Fact]
        public async Task Create_valid_modelState_calls_service_addAsync()
        {
            // Arrange
            var writer = new Writer { FullName = "John Doe", Bio = "Some bio" };
            _mockWritersService.Setup(x => x.AddAsync(It.IsAny<Writer>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(writer) as RedirectToActionResult;

            // Assert
            _mockWritersService.Verify(x => x.AddAsync(writer), Times.Once); // Check if AddAsync is called with the correct writer
        }

        [Fact]
        public async Task Details_returns_notFound_when_writer_not_found()
        {
            // Arrange
            int id = 1;
            _mockWritersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Writer)null);
            
            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Details_returns_view_with_writer_details()
        {
            // Arrange
            int id = 1;
            var writer = new Writer { Id = id, FullName = "John Doe", Bio = "Some bio" };
            _mockWritersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(writer);
            
            // Act
            var result = await _controller.Details(id) as ViewResult;
            var resultModel = result.Model as Writer;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(writer, resultModel); // Check if the returned model is the same as the input model
        }

        [Fact]
        public async Task Edit_returns_notFound_when_writer_not_found()
        {
            // Arrange
            int id = 1;
            _mockWritersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Writer)null);
            
            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Edit_returns_view_with_writer_details()
        {
            // Arrange
            int id = 1;
            var writer = new Writer { Id = id, FullName = "John Doe", Bio = "Some bio" };
            _mockWritersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(writer);
            
            // Act
            var result = await _controller.Edit(id) as ViewResult;
            var resultModel = result.Model as Writer;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(writer, resultModel); // Check if the returned model is the same as the input model
        }

        [Fact]
        public async Task Edit_returns_view_with_invalid_modelState()
        {
            // Arrange
            int id = 1;
            var writer = new Writer { Id = id, FullName = "John Doe" };
           
            _controller.ModelState.AddModelError("Bio", "The Bio field is required.");

            // Act
            var result = await _controller.Edit(id, writer) as ViewResult;
            var resultModel = result.Model as Writer;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(writer, resultModel); // Check if the returned model is the same as the input model
        }

        [Fact]
        public async Task Edit_redirects_to_index_after_update()
        {
            // Arrange
            int id = 1;
            var writer = new Writer { Id = id, FullName = "John Doe", Bio = "Some bio" };
            _mockWritersService.Setup(x => x.UpdateAsync(id, writer)).Returns(Task.CompletedTask);
           
            // Act
            var result = await _controller.Edit(id, writer) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Check if it redirects to Index action
        }

        [Fact]
        public async Task Edit_calls_service_updateAsync_with_correct_arguments()
        {
            // Arrange
            int id = 1;
            var writer = new Writer { Id = id, FullName = "John Doe", Bio = "Some bio" };
            _mockWritersService.Setup(x => x.UpdateAsync(id, writer)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Edit(id, writer) as RedirectToActionResult;

            // Assert
            _mockWritersService.Verify(x => x.UpdateAsync(id, writer), Times.Once); // Check if UpdateAsync is called with the correct arguments
        }

        [Fact]
        public async Task Delete_returns_notFound_when_writer_not_found()
        {
            // Arrange
            int id = 1;
            _mockWritersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Writer)null);
           
            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Delete_returns_view_with_writer_details()
        {
            // Arrange
            int id = 1;
            var writer = new Writer { Id = id, FullName = "John Doe", Bio = "Some bio" };
            _mockWritersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(writer);
           
            // Act
            var result = await _controller.Delete(id) as ViewResult;
            var resultModel = result.Model as Writer;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(writer, resultModel); // Check if the returned model is the same as the input model
        }

        [Fact]
        public async Task DeleteConfirm_returns_notFound_when_writer_not_found()
        {
            // Arrange
            int id = 1;
            _mockWritersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Writer)null);

            // Act
            var result = await _controller.DeleteConfirm(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task DeleteConfirm_deletes_writer_and_redirects_to_index()
        {
            // Arrange
            int id = 1;
            var writer = new Writer { Id = id, FullName = "John Doe", Bio = "Some bio" };
            _mockWritersService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(writer);
            _mockWritersService.Setup(x => x.DeleteAsync(id)).Returns(Task.CompletedTask);
            
            // Act
            var result = await _controller.DeleteConfirm(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Check if it redirects to Index action
            _mockWritersService.Verify(x => x.DeleteAsync(id), Times.Once); // Check if DeleteAsync is called with the correct id
        }

    }
}
