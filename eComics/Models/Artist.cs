using System.ComponentModel.DataAnnotations;

namespace eComics.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        //Relationships
        public List<Artist_Book> Artists_Books { get; set; }
    }
}
