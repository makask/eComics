using eComics.Models;

namespace eComics.Data.ViewModels
{
    public class NewBookDropdownsVM
    {
        public NewBookDropdownsVM()
        {
            Publishers = new List<Publisher>();
            Artists = new List<Artist>();
            Writers = new List<Writer>();
        }

        public List<Publisher> Publishers { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Writer> Writers { get; set; }
    }
}
