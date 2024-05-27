using System;
using eComics.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace KooliProjekt.IntegrationTests.Helpers
{
    public abstract class TestBase : IDisposable
    {
        public WebApplicationFactory<Startup> Factory { get; }
        public AppDbContext DbContext { get; set; }

        public TestBase()
        {
            Factory = new TestApplicationFactory<Startup>();
            DbContext = (AppDbContext)Factory.Services.GetService(typeof(AppDbContext));
            DbContext.Database.EnsureCreated();
        }        

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
        }

        // Add you other helper methods here
    }
}