using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eComics.Data;
using eComics.Data.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<eComicsContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("eComicsContext") ?? throw new InvalidOperationException("Connection string 'eComicsContext' not found.")));

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("eComicsContext") ?? throw new InvalidOperationException("Connection string 'eTicketsContext' not found.")));

// Services configuration
builder.Services.AddScoped<IArtistsService, ArtistsService>();
builder.Services.AddScoped<IPublishersService, PublishersService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
