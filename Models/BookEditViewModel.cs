using System.ComponentModel.DataAnnotations;

namespace Bookomari.com.Models
{
    public class BookEditViewModel
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter a book name.")]
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
    }

}
