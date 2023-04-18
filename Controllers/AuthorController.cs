using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookomari.com.Data;
using Bookomari.com.Models;

namespace Bookomari.com.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
            if (!_context.Authors.Any())
            {
                string path = @"C:\Users\BS1042\Source\Repos\Bookomari.com";
                /*string path = @"C:\Users\Saqlain\source\repos\Bookomari.com";
                var author1 = new Author
                {
                    AuthorName = "GG Ctan",
                    Address = "Paris",
                    AuthorPhoto = System.IO.File.ReadAllBytes(path + @"\AuthorImages\author1.png")
                };

                _context.Authors.Add(author1);
                _context.SaveChanges();*/

                var book1 = new Book
                {
                    BookName = "WQERQ",
                    Language = "Chinese",
                    BookCoverPhoto = System.IO.File.ReadAllBytes(path + @"\BookImages\book.jpg"),
                    Author = await _context.Authors.FirstOrDefaultAsync(m => m.AuthorId == 5)
                };

                _context.Books.Add(book1);
                _context.SaveChanges();
            }

            var authorList = await _context.Authors
                .Include(b => b.Books)
                .ToListAsync();

            return View(authorList);
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(b => b.Books)
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = new Author();

                author.AuthorName = model.AuthorName;
                author.Address = model.Address;
                author.AuthorPhoto = await model.GetImageBytesAsync();

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Find the author to edit
            Author author = await _context.Authors.FindAsync(id);

            // If the author doesn't exist, return a 404 error
            if (author == null)
            {
                return NotFound();
            }

            string fileName = "author.jpg";

            AuthorEditViewModel viewModel;

            using (var stream = new MemoryStream(author.AuthorPhoto))
            {
                var file = new FormFile(stream, 0, stream.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg" // set the content type of your image here
                };

                // now you can use the IFormFile object as needed
                // Create a new view model for the edit form
                viewModel = new AuthorEditViewModel
                {
                    AuthorId = author.AuthorId,
                    AuthorName = author.AuthorName,
                    Address = author.Address,
                    AuthorPhoto = file
                };
            }
            return View(viewModel);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorEditViewModel model)
        {
            if (id != model.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var author = await _context.Authors
                        .Include(b => b.Books)
                        .FirstOrDefaultAsync(b => b.AuthorId == id);

                if (author == null)
                {
                    return NotFound();
                }

                author.AuthorName = model.AuthorName;
                author.Address = model.Address;
                author.AuthorPhoto = await model.GetImageBytesAsync();

                _context.Authors.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Authors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Authors'  is null.");
            }
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
          return (_context.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}
