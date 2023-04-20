using Bookomari.com.Data;
using Bookomari.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookomari.com.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext _context)
        {
            this._context = _context;
        }



        public async Task<IActionResult> Index()
        {
            if (!_context.Authors.Any())
            {
                //string path = @"C:\Users\BS1042\Source\Repos\Bookomari.com";
                string path = @"C:\Users\Saqlain\source\repos\Bookomari.com";
                var author1 = new Author
                {
                    AuthorName = "Md. Anwarul Habib",
                    Address = "7/13, Block-B, Lalmatia, Dhaka-1207",
                    AuthorPhoto = System.IO.File.ReadAllBytes(path + @"\AuthorImages\author1.jpg")
                };

                _context.Authors.Add(author1);
                _context.SaveChanges();

                var book1 = new Book
                {
                    BookName = "Basics of C#",
                    Language = "English",
                    BookCoverPhoto = System.IO.File.ReadAllBytes(path + @"\BookImages\book.jpg"),
                    Author = author1
                };

                _context.Books.Add(book1);
                _context.SaveChanges();
            }

            var bookList = await _context.Books
                .Include(b => b.Author)
                .ToListAsync();

            return View(bookList);
        }




        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            // retrieve all authors from the database
            var authors = _context.Authors.ToList();

            // create a new instance of the BookEditViewModel
            var model = new BookEditViewModel();

            // set the Authors property to a SelectList containing all authors
            ViewBag.Authors = new SelectList(authors, "AuthorId", "AuthorName");

            return View(model);
        }


        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookEditViewModel model)
        {
            var author = _context.Authors.Find(model.AuthorId);
            //model.Author = author;

            if (ModelState.IsValid)
            {
                var book = new Book();

                book.BookName = model.BookName;
                book.Language = model.Language;
                book.BookCoverPhoto = await model.GetImageBytesAsync();
                book.Author = author;
                book.AuthorId = model.AuthorId;
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Find the book to edit
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);

            // If the book doesn't exist, return a 404 error
            if (book == null)
            {
                return NotFound();
            }

            string fileName = "book.jpg";

            BookEditViewModel viewModel;

            using (var stream = new MemoryStream(book.BookCoverPhoto))
            {
                var file = new FormFile(stream, 0, stream.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg" // set the content type of your image here
                };

                // now you can use the IFormFile object as needed
                // Create a new view model for the edit form
                viewModel = new BookEditViewModel
                {
                    BookId = book.BookId,
                    BookName = book.BookName,
                    Language = book.Language,
                    CoverPhoto = file,
                    AuthorId = book.AuthorId
                };
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookEditViewModel model)
        {
            if (id != model.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var book = await _context.Books
                        .FirstOrDefaultAsync(b => b.BookId == id);

                if (book == null)
                {
                    return NotFound();
                }

                book.BookName = model.BookName;
                book.Language = model.Language;
                book.BookCoverPhoto = await model.GetImageBytesAsync();
                /*book.AuthorId = model.AuthorId;
                book.Author = model.Author;*/

                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
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
            if (_context.Books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Books' is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
