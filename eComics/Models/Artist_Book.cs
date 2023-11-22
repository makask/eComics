namespace eComics.Models
{
    public class Artist_Book
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
