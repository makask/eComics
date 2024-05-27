using KooliProjekt.IntegrationTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EComics.IntegrationTests
{
    public class ArtistsControllerTests : TestBase
    {
        [Theory]
        [InlineData("/Artists/Details/100")]
        public async Task Test_Something(string url) 
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act 
            var response = await client.GetAsync(url);

            // Assert 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        }
    }
}
