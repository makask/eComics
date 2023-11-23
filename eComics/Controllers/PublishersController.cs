using eComics.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eComics.Controllers
{
    public class PublishersController : Controller
    {
        private readonly AppDbContext _context;

        public PublishersController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allPublishers = await _context.Publishers.ToListAsync();
            return View(allPublishers);
        }
    }
}
