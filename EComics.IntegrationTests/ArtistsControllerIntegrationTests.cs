using eComics;
using eComics.Data;
using eComics.Data.Services;
using eComics.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class ArtistsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public ArtistsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Index_ReturnsSuccessAndCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/Artists");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task Details_ReturnsNotFound_ForInvalidId()
    {
        // Act
        var response = await _client.GetAsync("/Artists/Details/999");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsSuccess()
    {
        // Arrange
        var newArtist = new Artist
        {
            FullName = "New Artist",
            ProfilePictureURL = "http://example.com/image.jpg",
            Bio = "New artist bio"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/Artists/Create", newArtist);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal(System.Net.HttpStatusCode.Redirect, response.StatusCode);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_ForInvalidId()
    {
        // Act
        var response = await _client.GetAsync("/Artists/Edit/999");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_ForInvalidId()
    {
        // Act
        var response = await _client.GetAsync("/Artists/Delete/999");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

}
