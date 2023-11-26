using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eComics.Data;
using eComics.Data.Services;
using eComics.Data.Cart;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<eComicsContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("eComicsContext") ?? throw new InvalidOperationException("Connection string 'eComicsContext' not found.")));

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("eComicsContext") ?? throw new InvalidOperationException("Connection string 'eTicketsContext' not found.")));

// Services configuration
builder.Services.AddScoped<IArtistsService, ArtistsService>();
builder.Services.AddScoped<IPublishersService, PublishersService>();
builder.Services.AddScoped<IWritersService, WritersService>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Seed database
AppDbInitializer.Seed(app);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
