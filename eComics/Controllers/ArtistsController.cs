using eComics.Data;
using eComics.Data.Services;
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
            var data = await _service.GetAll();
            return View(data);
        }

        public IActionResult Create()
        { 
            return View();
        }
    }
}
