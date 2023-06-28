using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity
{
    public class BookEntity
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string Rating { get; set; }
    }
}
