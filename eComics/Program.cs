using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eComics.Data;
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<eComicsContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("eComicsContext") ?? throw new InvalidOperationException("Connection string 'eComicsContext' not found.")));

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
