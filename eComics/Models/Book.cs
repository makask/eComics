using eComics.Data.Enums;
using eComics.Data.Repositories.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eComics.Models
{
    public class Book : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Summary")]
        public string Description { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }
        [Display(Name = "Cover")]
        public string ImageURL { get; set; }
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Genre")]
        public BookGenre BookGenre { get; set; } 

        //Relationships
        public List<Artist_Book> Artists_Books { get; set; }
        public List<Writer_Book> Writers_Books { get; set; }

        //Publisher
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
    }
}
