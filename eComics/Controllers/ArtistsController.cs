using eComics.Data;
using eComics.Data.Services;
using eComics.Data.Static;
using eComics.Data.ViewModels;
using eComics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eComics.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ArtistsController : Controller
    {
        private readonly IArtistsService _service;

        public ArtistsController(IArtistsService service)
        {
           _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string term = "", string orderBy= "", int currentPage = 1)
        {
            var allArtists = await _service.GetAllAsync();

            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var artistData = new ArtistVM();

            artistData.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";

            var artists = (from artist in allArtists
                           where term == "" || artist.FullName.ToLower().StartsWith(term)
                           select new Artist
                           { 
                               Id = artist.Id,
                               ProfilePictureURL = artist.ProfilePictureURL,
                               FullName = artist.FullName,
                               Bio = artist.Bio
                           });

            switch (orderBy)
            {
                case "name_desc":
                    artists = artists.OrderByDescending(a => a.FullName);
                    break;               
                default:
                    artists = artists.OrderBy(a => a.FullName);
                    break;
            }
            
            int totalRecords = artists.Count();            
            int pageSize = 6;           
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);     
            artists = artists.Skip((currentPage - 1) * pageSize).Take(pageSize);
            artistData.Artists = artists;
            artistData.CurrentPage = currentPage;
            artistData.TotalPages = totalPages;
            artistData.PageSize = pageSize;
            artistData.Term = term;
            artistData.OrderBy = orderBy;
            return View(artistData);

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

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var artistDetails = await _service.GetByIdAsync(id.Value);

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

        public async Task<IActionResult> Delete(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);
            if (artistDetails == null) return View("NotFound");
            return View(artistDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);
            if (artistDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
