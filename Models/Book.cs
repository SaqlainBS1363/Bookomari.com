using System.ComponentModel.DataAnnotations;

namespace Bookomari.com.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Language { get; set; }
        public byte[] BookCoverPhoto { get; set; }
        // Foreign Key
        public int AuthorId { get; set; }
    }
}
