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
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);

            if (artistDetails == null) return View("NotFound");

            return View(artistDetails);
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);
            if (artistDetails == null) return View("NotFound");
            return View(artistDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }
            await _service.UpdateAsync(id, artist);
            return RedirectToAction(nameof(Index));
        }
    }
}
