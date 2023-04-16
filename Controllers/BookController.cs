using Bookomari.com.Data;
using Bookomari.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookomari.com.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _DbContext;

        public BookController(ApplicationDbContext _DbContext)
        {
            this._DbContext = _DbContext;
        }
        /*
           var author1 = new Author
            {
                AuthorName = "John Doe",
                Address = "London",
                AuthorPhoto = System.IO.File.ReadAllBytes(@"C:\Users\Saqlain\source\repos\Bookomari.com\AuthorImages\author1.png")
            };

            _DbContext.Authors.Add(author1);
            _DbContext.SaveChanges();

            var book1 = new Book
            {
                BookName = "Health",
                Language = "English",
                BookCoverPhoto = System.IO.File.ReadAllBytes(@"C:\Users\Saqlain\source\repos\Bookomari.com\BookImages\book.jpg"),
                Author = author1
            };

            _DbContext.Books.Add(book1);
            _DbContext.SaveChanges();
         */

        public IActionResult Index()
        {
            IEnumerable<Book> bookList = _DbContext.Books;

            return View(bookList);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bookFromDB = _DbContext.Books.Find(id);

            if (bookFromDB == null)
            {
                return NotFound();
            }

            return View(bookFromDB);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bookFromDB = _DbContext.Books.Find(id);

            if (bookFromDB == null)
            {
                return NotFound();
            }

            return View(bookFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book obj)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Books.Update(obj);
                _DbContext.SaveChanges();
                //TempData["success"] = "Category updated succesfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _DbContext.Books == null)
            {
                return NotFound();
            }

            var book = await _DbContext.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_DbContext.Books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Books' is null.");
            }
            var book = await _DbContext.Books.FindAsync(id);
            if (book != null)
            {
                _DbContext.Books.Remove(book);
            }

            await _DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
