using eComics.Models;

namespace eComics.Data.ViewModels
{
    public class WriterVM
    {
        public IEnumerable<Writer> Writers { get; set; }

        public string NameSortOrder { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string Term { get; set; }

        public string OrderBy { get; set; }
    }
}
