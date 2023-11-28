using eComics.Data;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using eComics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eComics.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string term = "", string orderBy = "", int currentPage = 1)
        {
            var allBooks = await _service.GetAllAsync(n => n.Publisher);

            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var bookData = new BookVM();

            bookData.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";

            var books = (from book in allBooks
                              where term == "" || book.Title.ToLower().StartsWith(term) 
                              select new Book
                              {
                                  Id = book.Id,
                                  Title = book.Title,
                                  Description = book.Description,
                                  Price = book.Price,
                                  ImageURL = book.ImageURL,
                                  ReleaseDate = book.ReleaseDate,
                                  BookGenre = book.BookGenre,
                                  Publisher = book.Publisher,
                              });

            switch (orderBy)
            {
                case "name_desc":
                    books = books.OrderByDescending(p => p.Title);
                    break;
                default:
                    books = books.OrderBy(p => p.Title);
                    break;
            }

            int totalRecords = books.Count();
            int pageSize = 6;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            books = books.Skip((currentPage - 1) * pageSize).Take(pageSize);
            bookData.Books = books;
            bookData.CurrentPage = currentPage;
            bookData.TotalPages = totalPages;
            bookData.PageSize = pageSize;
            bookData.Term = term;
            bookData.OrderBy = orderBy;
            return View(bookData);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        { 
            var bookDetails = await _service.GetBookByIdAsync(id);
            return View(bookDetails);
        }

        public async Task <ActionResult> Create()
        {
            var bookDropDownsData = await _service.GetNewBookDropdownsValues();

            ViewBag.Publishers = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
            ViewBag.Artists = new SelectList(bookDropDownsData.Artists, "Id", "FullName");
            ViewBag.Writers = new SelectList(bookDropDownsData.Writers, "Id", "FullName");

            return View(); 
        }

        [HttpPost]
        public async Task<ActionResult> Create(NewBookVM book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropDownsData = await _service.GetNewBookDropdownsValues();
                ViewBag.Publishers = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
                ViewBag.Artists = new SelectList(bookDropDownsData.Artists, "Id", "FullName");
                ViewBag.Writers = new SelectList(bookDropDownsData.Writers, "Id", "FullName");
                return View(book);
            }

            await _service.AddNewBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        { 
            var bookDetails = await _service.GetBookByIdAsync(id);
            if(bookDetails == null) return View("NotFound");

            var response = new NewBookVM()
            {
                Id = bookDetails.Id,
                Title = bookDetails.Title,
                Description = bookDetails.Description,
                Price = bookDetails.Price,
                ReleaseDate = bookDetails.ReleaseDate,
                ImageURL = bookDetails.ImageURL,
                BookGenre = bookDetails.BookGenre,
                PublisherId = bookDetails.PublisherId,
                ArtistIds = bookDetails.Artists_Books.Select(a => a.ArtistId).ToList(),
                WriterIds = bookDetails.Writers_Books.Select(w => w.WriterId).ToList()
            };

            var bookDropDownsData = await _service.GetNewBookDropdownsValues();
            ViewBag.Publishers = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
            ViewBag.Artists = new SelectList(bookDropDownsData.Artists, "Id", "FullName");
            ViewBag.Writers = new SelectList(bookDropDownsData.Writers, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewBookVM book)
        {
            if (id != book.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var bookDropDownsData = await _service.GetNewBookDropdownsValues();
                ViewBag.Publishers = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
                ViewBag.Artists = new SelectList(bookDropDownsData.Artists, "Id", "FullName");
                ViewBag.Writers = new SelectList(bookDropDownsData.Writers, "Id", "FullName");
                return View(book);
            }
            await _service.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }
    }
}
