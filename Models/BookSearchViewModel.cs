using System.ComponentModel;

namespace Bookomari.com.Models
{
    public class BookSearchViewModel
    {
        [DisplayName("")]
        public string BookSearchString { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
