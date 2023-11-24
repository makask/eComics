using eComics.Data;
using eComics.Data.Services;
using eComics.Models;
using Microsoft.AspNetCore.Mvc;

namespace eComics.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistsService _service;

        public ArtistsController(IArtistsService service)
        {
           _service = service;
        }
        public async Task <IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }
            await _service.AddAsync(artist);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);

            if (artistDetails == null) return View("Empty");

            return View(artistDetails);
            
        }
    }
}
