using eComics.Data;
using eComics.Data.Services;
using eComics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
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

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")]Writer writer)
        { 
            if(!ModelState.IsValid) return View(writer);
            await _service.AddAsync(writer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var writerDetails = await _service.GetByIdAsync(id);
            if(writerDetails == null) return View("NotFound");
            return View(writerDetails);
        }
    }
}
