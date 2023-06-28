using BookStore.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataLayer
{
    public class BookDBContext : DbContext
    {
        public BookDBContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Create BooksCollection Table.
        /// </summary>
        public DbSet<BookEntity> BooksCollections { get; set; }
    }
}
