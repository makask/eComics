using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eComics.Data;
using eComics.Data.Services;
using eComics.Data.Cart;
using eComics.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using eComics.Data.Repositories;
using eComics.Integrations.OpenMeteo;
using eComics.Services;
using eComics.Integrations;
using eComics.Integrations.YrNoClient;

//var yrNoClient = new YrNoClient();
//return;

var builder = WebApplication.CreateBuilder(args);

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("eComicsContext") ?? throw new InvalidOperationException("Connection string 'eComicsContext' not found.")));

// Services configuration
builder.Services.AddScoped<IArtistsService, ArtistsService>();
builder.Services.AddScoped<IPublishersService, PublishersService>();
builder.Services.AddScoped<IWritersService, WritersService>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IArtistsRepository, ArtistsRepository>();
builder.Services.AddScoped<IWritersRepository, WritersRepository>();
builder.Services.AddScoped<IPublishersRepository, PublishersRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IWeatherClient,OpenMeteoClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
//builder.Services.AddScoped<IWeatherClient, YrNoClient>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

// Configure authentication and authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options => 
{ 
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Seed database
AppDbInitializer.Seed(app);
//Seed users and roles
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

// Authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
