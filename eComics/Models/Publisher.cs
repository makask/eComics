using eComics.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eComics.Models
{
    public class Publisher : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Logo")]
        public string Logo { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        //Relationships
        public List<Book> Books { get; set; }
    }
}
