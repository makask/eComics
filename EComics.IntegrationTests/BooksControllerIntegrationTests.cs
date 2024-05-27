using eComics;
using eComics.Data;
using eComics.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using eComics.Data.Enums;
using eComics.Data.ViewModels;

namespace eComics.IntegrationTests
{
    public class BooksControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BooksControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Index_ReturnsSuccessAndCorrectContentType()
        {
            // Act
            var response = await _client.GetAsync("/Books");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Details_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var response = await _client.GetAsync("/Books/Details/999");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsSuccess()
        {
            // Arrange
            var newBook = new NewBookVM
            {
                Title = "New Book",
                Description = "A new book description",
                Price = 10.99,
                ReleaseDate = DateTime.UtcNow,
                ImageURL = "http://example.com/image.jpg",
                BookGenre = BookGenre.Action,
                PublisherId = 1,
                ArtistIds = new List<int> { 1 },
                WriterIds = new List<int> { 1 }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(newBook), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/Books/Create", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(System.Net.HttpStatusCode.Redirect, response.StatusCode);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var response = await _client.GetAsync("/Books/Edit/999");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var response = await _client.GetAsync("/Books/Delete/999");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
