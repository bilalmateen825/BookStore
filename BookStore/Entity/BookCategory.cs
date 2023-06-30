using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity
{
    public class BookCategory
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
