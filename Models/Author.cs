namespace Bookomari.com.Models
{
    public class Author
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Address { get; set; }
        public byte[] AuthorPhoto { get; set; }
        public List<Book> Books { get; set; }

    }
}
