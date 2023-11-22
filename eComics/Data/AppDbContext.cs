using eComics.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace eComics.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist_Book>().HasKey(ab => new
            { 
                ab.ArtistId,
                ab.BookId
            });

            modelBuilder.Entity<Artist_Book>().HasOne(b => b.Book).WithMany(ab => ab.Artists_Books).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<Artist_Book>().HasOne(a => a.Artist).WithMany(ab => ab.Artists_Books).HasForeignKey(a => a.ArtistId);

            modelBuilder.Entity<Writer_Book>().HasKey(wb => new
            {
                wb.WriterId,
                wb.BookId
            }); ;

            modelBuilder.Entity<Writer_Book>().HasOne(b => b.Book).WithMany(wb => wb.Writers_Books).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<Writer_Book>().HasOne(w => w.Writer).WithMany(wb => wb.Writers_Books).HasForeignKey(w => w.WriterId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Artist_Book> Artists_Books { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Writer_Book> Writers_Books { get; set; }
    }
}
