using System.ComponentModel;

namespace Bookomari.com.Models
{
    public class AuthorSearchViewModel
    {
        [DisplayName("")]
        public string AuthorSearchString { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
