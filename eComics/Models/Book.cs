using eComics.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eComics.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime ReleaseDate { get; set; }
        public BookGenre BookGenre { get; set; } 

    }
}
