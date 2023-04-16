using System.ComponentModel.DataAnnotations;

namespace Bookomari.com.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public string Address { get; set; }
        public byte[] AuthorPhoto { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
