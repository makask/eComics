using eComics.Data;
using eComics.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eComics.Controllers
{
    public class WritersController : Controller
    {
        private readonly IWritersService _service;

        public WritersController(IWritersService service)
        {
            _service = service;    
        }

        public async Task<IActionResult> Index()
        {
            var allWriters = await _service.GetAllAsync();
            return View(allWriters);
        }
    }
}
