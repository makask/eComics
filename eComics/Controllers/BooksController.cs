using eComics.Data;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using eComics.Models;
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

        public async Task<IActionResult> Details(int id)
        { 
            var bookDetails = await _service.GetBookByIdAsync(id);
            return View(bookDetails);
        }
    }
}
