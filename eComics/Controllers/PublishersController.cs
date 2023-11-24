using eComics.Data;
using eComics.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eComics.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublishersService _service;

        public PublishersController(IPublishersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allPublishers = await _service.GetAllAsync();
            return View(allPublishers);
        }

        public async Task<IActionResult> Details(int id)
        { 
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }
    }
}
