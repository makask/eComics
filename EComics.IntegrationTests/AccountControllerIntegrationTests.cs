using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http;
using eComics.Controllers;
using Microsoft.AspNetCore.Hosting;
using KooliProjekt.IntegrationTests.Helpers;

public class AccountControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public AccountControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Users_Returns_View_With_Users()
    {
        var response = await _client.GetAsync("/Account/Users");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("User List", responseString); // Adjust the assertion based on actual view content
    }
}
