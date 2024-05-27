using System.Net.Http;
using System.Threading.Tasks;
using eComics.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using eComics.Controllers;
using Microsoft.Extensions.DependencyInjection;
using eComics.Data.ViewModels;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;

public class AccountControllerRegisterTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public AccountControllerRegisterTests(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Register_Post_Returns_ViewResult()
    {
        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var userManager = MockUserManager();
                var signInManager = MockSignInManager(userManager.Object);

                services.AddScoped(_ => userManager.Object);
                services.AddScoped(_ => signInManager.Object);
            });
        }).CreateClient();

        var registerVM = new RegisterVM
        {
            EmailAddress = "newuser@example.com",
            Password = "Password123!",
            FullName = "New User"
        };

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("EmailAddress", registerVM.EmailAddress),
            new KeyValuePair<string, string>("Password", registerVM.Password),
            new KeyValuePair<string, string>("FullName", registerVM.FullName)
        });

        var response = await client.PostAsync("/Account/Register", content);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("RegisterCompleted", responseString); // Adjust based on actual view name
    }

    private Mock<UserManager<ApplicationUser>> MockUserManager()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        return new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
    }

    private Mock<SignInManager<ApplicationUser>> MockSignInManager(UserManager<ApplicationUser> userManager)
    {
        var contextAccessor = new Mock<IHttpContextAccessor>();
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        return new Mock<SignInManager<ApplicationUser>>(userManager, contextAccessor.Object, claimsFactory.Object, null, null, null, null);
    }
}
