using Bookomari.com.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookomari.com.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _DbContext;

        public AuthorController(ApplicationDbContext _DbContext)
        {
            this._DbContext = _DbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await _DbContext.Authors
                .FirstOrDefaultAsync(m => m.AuthorId == id);
                

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
    }
}
