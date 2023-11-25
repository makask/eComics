using eComics.Data;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using eComics.Models;
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
        /*public async Task<IActionResult> Index()
        {
            var allPublishers = await _service.GetAllAsync();
            return View(allPublishers);
        }*/

        public async Task<IActionResult> Index(string term = "", string orderBy = "", int currentPage = 1)
        {
            var allPublishers = await _service.GetAllAsync();

            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var publisherData = new PublisherVM();

            publisherData.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";

            var publishers = (from publisher in allPublishers
                           where term == "" || publisher.Name.ToLower().StartsWith(term)
                           select new Publisher
                           {
                               Id = publisher.Id,
                               Logo = publisher.Logo,
                               Name = publisher.Name,
                               Description = publisher.Description
                           });

            switch (orderBy)
            {
                case "name_desc":
                    publishers = publishers.OrderByDescending(p => p.Name);
                    break;
                default:
                    publishers = publishers.OrderBy(p => p.Name);
                    break;
            }

            int totalRecords = publishers.Count();
            int pageSize = 6;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            publishers = publishers.Skip((currentPage - 1) * pageSize).Take(pageSize);
            publisherData.Publishers = publishers;
            publisherData.CurrentPage = currentPage;
            publisherData.TotalPages = totalPages;
            publisherData.PageSize = pageSize;
            publisherData.Term = term;
            publisherData.OrderBy = orderBy;
            return View(publisherData);

        }

        public async Task<IActionResult> Details(int id)
        { 
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        public IActionResult Create()
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Publisher publisher)
        { 
            if(!ModelState.IsValid) return View(publisher);

            await _service.AddAsync(publisher);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Logo,Name,Description")] Publisher publisher)
        {
            if (!ModelState.IsValid) return View(publisher);

            if (id == publisher.Id)
            {
                await _service.UpdateAsync(id, publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");

            await _service.DeleteAsync(id); 
            return RedirectToAction(nameof(Index));
        }

    }
}
