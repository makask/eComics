using eComics.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eComics.Controllers
{
    public class WritersController : Controller
    {
        private readonly AppDbContext _context;

        public WritersController(AppDbContext context)
        {
            _context = context;    
        }

        public async Task<IActionResult> Index()
        {
            var allWriters = await _context.Writers.ToListAsync();
            return View();
        }
    }
}
