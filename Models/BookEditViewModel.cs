using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Bookomari.com.Models
{
    public class BookEditViewModel
    {
        public int BookId { get; set; }

        public string BookName { get; set; }

        public string Language { get; set; }

        [Display(Name = "Book Cover Photo")]
        public IFormFile CoverPhoto { get; set; }

        public async Task<byte[]> GetImageBytesAsync()
        {
            using var stream = new MemoryStream();
            await CoverPhoto.CopyToAsync(stream);
            return stream.ToArray();
        }
        public Author Author { get; set; }
        public int AuthorId { get; set; }

    }
}
