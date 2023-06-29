using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity
{
    public class BookEntity
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter Title Message")]
        [StringLength(50,MinimumLength =5,ErrorMessage ="Title name is too short.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter Author name Message")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Author name is too short.")]
        [DisplayName("Author")]
        public string AuthorName { get; set; }

        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage ="Release data must be less than current date.")]
        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please enter Rating between (1-5)")]
        [Range(1,5,ErrorMessage ="Please enter rating between 1 to 5.")]
        public string Rating { get; set; }
    }

    public class DateValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime todayDate = Convert.ToDateTime(value);

            return todayDate <= DateTime.Now;
        }
    }
}
