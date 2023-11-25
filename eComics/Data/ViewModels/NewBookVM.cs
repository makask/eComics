using eComics.Data.Enums;
using eComics.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eComics.Data.ViewModels
{
    public class NewBookVM
    {
        public int Id { get; set; }

        [Display(Name = "Book title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Display(Name = "Book summary")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Book price")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Book cover")]
        [Required(ErrorMessage = "Book cover picture is required")]
        public string ImageURL { get; set; }
        [Display(Name = "Book release date")]
        [Required(ErrorMessage = "Book release date is required")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Book genre is required")]
        public BookGenre BookGenre { get; set; }
        [Display(Name = "Select artist(s)")]
        [Required(ErrorMessage = "Artist(s) is required")]
        public List<int> ArtistIds { get; set; }
        [Display(Name = "Select writer(s)")]
        [Required(ErrorMessage = "Writer(s) is required")]
        public List<int> WriterIds { get; set; }
        [Display(Name = "Select publisher")]
        [Required(ErrorMessage = "Publisher is required")]
        public int PublisherId { get; set; }
    }
}
