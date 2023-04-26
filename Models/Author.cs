using System.ComponentModel.DataAnnotations;

namespace Bookomari.com.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        public string Address { get; set; }
        [Display(Name = "Author Photo")]
        public byte[] AuthorPhoto { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
