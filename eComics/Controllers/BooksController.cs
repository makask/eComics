using eComics.Data;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eComics.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            //var allBooks = await _context.Books.Include(p => p.Publisher).OrderBy(t => t.Title).ToListAsync();
            var allBooks = await _service.GetAllAsync(n => n.Publisher);
            return View(allBooks);
        }

        /*public async Task<IActionResult> Index(string term = "", string orderBy = "", int currentPage = 1)
        {
            var allBooks = await _service.GetAllAsync();

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

        }*/
    }
}
