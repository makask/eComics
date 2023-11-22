using System.ComponentModel.DataAnnotations;

namespace eComics.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
    }
}
