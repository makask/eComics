using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using eComics.Data;
using eComics.Models;
using Microsoft.AspNetCore.Identity;
using eComics.Data.Cart;
using eComics.Data.Repositories;
using eComics.Data.Services;
using eComics.Integrations.OpenMeteo;
using eComics.Integrations;
using eComics.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IArtistsService, ArtistsService>();
        services.AddScoped<IPublishersService, PublishersService>();
        services.AddScoped<IWritersService, WritersService>();
        services.AddScoped<IBooksService, BooksService>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IArtistsRepository, ArtistsRepository>();
        services.AddScoped<IWritersRepository, WritersRepository>();
        services.AddScoped<IPublishersRepository, PublishersRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWeatherClient, OpenMeteoClient>();
        services.AddScoped<IWeatherService, WeatherService>();
        //builder.Services.AddScoped<IWeatherClient, YrNoClient>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

        // Configure authentication and authorization
        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        services.AddMemoryCache();
        services.AddSession();
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });

        services.AddRouting(); // Ensure routing is added

        // Add other services
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
