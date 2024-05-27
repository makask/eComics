using eComics.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using eComics.Data.Services;
using eComics.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EComics.UnitTests.TestUtilities;
using System.Diagnostics;
using FluentAssertions;


namespace EComics.UnitTests.ControllerTests
{
    public class ArtistsControllerTests
    {
        private readonly ArtistsController _controller;
        private readonly Mock<IArtistsService> _mockArtistsService;

        public ArtistsControllerTests() 
        { 
            _mockArtistsService = new Mock<IArtistsService>();
            _controller = new ArtistsController(_mockArtistsService.Object);
        }

        [Fact]
        public async Task Index_should_return_index_view()
        {
            var result = await _controller.Index() as ViewResult;  
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" || string.IsNullOrEmpty(result.ViewName));
        }

        [Fact]
        public async Task Create_should_return_create_view()
        {
            var result = _controller.Create() as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Create" || string.IsNullOrEmpty(result.ViewName));

        }

        [Fact]
        public async Task Create_should_return_create_view_with_created_artist_when_modelstate_is_not_valid()
        {

            _controller.ModelState.AddModelError("123","333");
            
            var artist = new Artist
            { 
                ProfilePictureURL = null,
                FullName = null,
                Bio = null,
                Artists_Books = null
            };

            var result = await _controller.Create(artist) as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().BeEquivalentTo(artist);
        }

        [Fact]
        public async Task Create_should_add_artist_and_return_index_view()
        {

            var artist = new Artist
            {
                ProfilePictureURL = null,
                FullName = "Axel",
                Bio = null,
                Artists_Books = null
            };

            var result = await _controller.Create(artist) as RedirectToActionResult;
            _mockArtistsService.Verify(x => x.AddAsync(artist), Times.Once);
            result.ActionName.Should().BeEquivalentTo(nameof(Index));
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing() 
        {
            var id = (int?)null;

            var result = await _controller.Details(id) as NotFoundResult;
            
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_notfound_view_when_artistdetails_is_null() 
        { 
            // Arrange
            Artist artist = null;
            int id = 41;
            _mockArtistsService.Setup(srv => srv.GetByIdAsync(id)).ReturnsAsync(artist).Verifiable();

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "NotFound" || string.IsNullOrEmpty(result.ViewName));
            _mockArtistsService.VerifyAll();
        }

        [Fact]
        public async Task Details_should_return_artistdetails_view_when_artistdetails_exists()
        {
            // Arrange
            int id = 41;
            Artist artist = new Artist { Id = id, ProfilePictureURL="https://images/image1.jpg", FullName = "Mark Park", Bio="bio", Artists_Books=null };
            _mockArtistsService.Setup(srv => srv.GetByIdAsync(id)).ReturnsAsync(artist).Verifiable();

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Details" || string.IsNullOrEmpty(result.ViewName));
            Assert.True(result.Model == artist);
            _mockArtistsService.VerifyAll();
        }

        [Fact]
        public async Task Edit_should_return_view_notfound_when_artistDetail_is_null() 
        {
            // Arrange
            Artist artist = null;
            int id = 1;
            _mockArtistsService.Setup(srv => srv.GetByIdAsync(id)).ReturnsAsync(artist).Verifiable();

            // Act
            var result = await _controller.Details(id) as ViewResult;
            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "NotFound" || string.IsNullOrEmpty(result.ViewName));
            _mockArtistsService.VerifyAll();
        }

        [Fact]
        public async Task Edit_should_return_artistdetails_view_when_artistdetails_exists()
        {
            // Arrange
            int id = 41;
            Artist artist = new Artist { Id = id, ProfilePictureURL = "https://images/image1.jpg", FullName = "Mark Park", Bio = "bio", Artists_Books = null };
            _mockArtistsService.Setup(srv => srv.GetByIdAsync(id)).ReturnsAsync(artist).Verifiable();

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Details" || string.IsNullOrEmpty(result.ViewName));
            Assert.True(result.Model == artist);
            _mockArtistsService.VerifyAll();
        }

        [Fact]
        public async Task Edit_should_return_artist_view_when_modelstate_is_not_valid() 
        {
            _controller.ModelState.AddModelError("123", "333");

            int id = 1;
            var artist = new Artist
            {
                ProfilePictureURL = null,
                FullName = null,
                Bio = null,
                Artists_Books = null
            };

            var result = await _controller.Edit(id,artist) as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().BeEquivalentTo(artist);
        }

        [Fact]
        public async Task Edit_should_redirect_to_index_when_edit_is_complete() 
        {
            int id = 1;
            var artist = new Artist
            {
                ProfilePictureURL = null,
                FullName = "Axel",
                Bio = null,
                Artists_Books = null
            };

            var result = await _controller.Edit(1,artist) as RedirectToActionResult;
            _mockArtistsService.Verify(x => x.UpdateAsync(id,artist), Times.Once);
            result.ActionName.Should().BeEquivalentTo(nameof(Index));
        }

        [Fact]
        public async Task Delete_should_return_view_notFound_when_artistDetails_is_null() 
        {
            // Arrange
            Artist artist = null;
            int id = 1;
            _mockArtistsService.Setup(srv => srv.GetByIdAsync(id)).ReturnsAsync(artist).Verifiable();

            // Act
            var result = await _controller.Delete(id) as ViewResult;
            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "NotFound" || string.IsNullOrEmpty(result.ViewName));
            _mockArtistsService.VerifyAll();
        }

        [Fact] async Task Delete_should_return_view_artistDetails_when_artistDetails_is_valid() 
        {
            // Arrange
            int id = 41;
            Artist artist = new Artist { Id = id, ProfilePictureURL = "https://images/image1.jpg", FullName = "Mark Park", Bio = "bio", Artists_Books = null };
            _mockArtistsService.Setup(srv => srv.GetByIdAsync(id)).ReturnsAsync(artist).Verifiable();

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Details" || string.IsNullOrEmpty(result.ViewName));
            Assert.True(result.Model == artist);
            _mockArtistsService.VerifyAll();
        }

        [Fact]
        public async Task DeleteConfirmed_returns_redirectToIndex_when_artist_is_deleted()
        {
            // Arrange
            int id = 1;
        
            var artistToDelete = new Artist
            {
                ProfilePictureURL = null,
                FullName = "Axel",
                Bio = null,
                Artists_Books = null
            };

            _mockArtistsService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(artistToDelete);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.Index), result.ActionName);
            _mockArtistsService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteConfirmed_returns_notfound_when_artist_not_found()
        {
            // Arrange
            int id = 1;
            _mockArtistsService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Artist)null);
            // Act
            var result = await _controller.DeleteConfirmed(id) as ViewResult;
            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
            _mockArtistsService.Verify(x => x.DeleteAsync(id), Times.Never);
        }

        [Fact]
        public async Task Edit_returns_view_with_artist_details_when_found() 
        {
            // Arrange
            int id = 1;
            var artistDetails = new Artist
            {
                ProfilePictureURL = null,
                FullName = "Axel",
                Bio = null,
                Artists_Books = null
            };
            _mockArtistsService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(artistDetails);
            // Act
            var result = await _controller.Edit(id) as ViewResult;
            // Assert
            Assert.NotNull(result);
            Assert.Equal(artistDetails, result.Model);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public async Task Edit_returns_notFound_when_artist_not_found()
        {
            // Arrange
            int id = 1;
           
            _mockArtistsService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Artist)null);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
        }
    }
}
