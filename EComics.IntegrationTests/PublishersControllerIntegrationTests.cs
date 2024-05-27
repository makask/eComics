using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using eComics;
using eComics.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace eComics.IntegrationTests
{
    public class PublishersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PublishersControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Index_ReturnsSuccessAndCorrectContentType()
        {
            // Act
            var response = await _client.GetAsync("/Publishers");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Details_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var response = await _client.GetAsync("/Publishers/Details/999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
