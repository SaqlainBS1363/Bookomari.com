using System.ComponentModel.DataAnnotations;

namespace Bookomari.com.Models
{
    public class AuthorEditViewModel
    {
        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Author Photo")]
        public IFormFile AuthorPhoto { get; set; }

        public async Task<byte[]> GetImageBytesAsync()
        {
            using var stream = new MemoryStream();
            await AuthorPhoto.CopyToAsync(stream);
            return stream.ToArray();
        }
    }
}
