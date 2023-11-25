using eComics.Data;
using eComics.Data.Services;
using eComics.Data.ViewModels;
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

        public async Task<IActionResult> Index(string term = "", string orderBy = "", int currentPage = 1)
        {
            var allWriters = await _service.GetAllAsync();

            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var writerData = new WriterVM();

            writerData.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";

            var writers = (from writer in allWriters
                           where term == "" || writer.FullName.ToLower().StartsWith(term)
                           select new Writer
                           {
                               Id = writer.Id,
                               ProfilePictureURL = writer.ProfilePictureURL,
                               FullName = writer.FullName,
                               Bio = writer.Bio
                           });

            switch (orderBy)
            {
                case "name_desc":
                    writers = writers.OrderByDescending(w => w.FullName);
                    break;
                default:
                    writers = writers.OrderBy(w => w.FullName);
                    break;
            }

            int totalRecords = writers.Count();
            int pageSize = 6;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            writers = writers.Skip((currentPage - 1) * pageSize).Take(pageSize);
            writerData.Writers = writers;
            writerData.CurrentPage = currentPage;
            writerData.TotalPages = totalPages;
            writerData.PageSize = pageSize;
            writerData.Term = term;
            writerData.OrderBy = orderBy;
            return View(writerData);

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

        public async Task<IActionResult> Edit(int id)
        {
            var writerDetails = await _service.GetByIdAsync(id);
            if (writerDetails == null) return View("NotFound");
            return View(writerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,ProfilePictureURL,FullName,Bio")] Writer writer)
        {
            if (!ModelState.IsValid) return View(writer);
            await _service.UpdateAsync(id,writer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var writerDetails = await _service.GetByIdAsync(id);
            if (writerDetails == null) return View("NotFound");
            return View(writerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var writerDetails = await _service.GetByIdAsync(id);
            if (writerDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
