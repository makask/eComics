using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class AccountControllerLogoutTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public AccountControllerLogoutTests(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Logout_Post_Returns_RedirectToActionResult()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsync("/Account/Logout", null);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("Redirecting", responseString); 
    }
}
