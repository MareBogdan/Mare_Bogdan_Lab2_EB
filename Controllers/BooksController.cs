using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mare_Bogdan_Lab2_EB.Data;
using Mare_Bogdan_Lab2_EB.Models;

namespace Mare_Bogdan_Lab2_EB.Controllers
{
    public class BooksController : Controller
    {
        private readonly Mare_Bogdan_Lab2_EBContext _context;

        public BooksController(Mare_Bogdan_Lab2_EBContext context)
        {
            _context = context;
        }

        // GET: Books
        // GET: Books
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["AuthorSortParm"] = sortOrder == "Author" ? "author_desc" : "Author";

            ViewData["CurrentFilter"] = searchString;


            var books = from b in _context.Book
                        join a in _context.Author on b.AuthorID equals a.ID
                        select new BookViewModel
                        {
                            ID = b.ID,
                            Title = b.Title,
                            Price = b.Price,
                            FullName = a.FirstName + " " + a.LastName
                        };

            // FILTRARE după titlu (dacă avem ceva în searchString)
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            // SORTARE (la fel ca înainte)
            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "Price":
                    books = books.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    books = books.OrderByDescending(b => b.Price);
                    break;
                case "Author":
                    books = books.OrderBy(b => b.FullName);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.FullName);
                    break;
                default:
                    books = books.OrderBy(b => b.Title); // default: sortare după Title
                    break;
            }


            return View(await books.AsNoTracking().ToListAsync());
        }



        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)            // aducem și Genre
                .Include(b => b.Author)           // aducem și Author
                .Include(b => b.Orders)           // aducem toate Orders ale cărții
                    .ThenInclude(o => o.Customer) // și pentru fiecare Order, aducem Customer
                .AsNoTracking()                   // doar afișare, fără tracking
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name");
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "LastName");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Price,GenreID,AuthorID")] Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                              "Try again, and if the problem persists.");
            }

            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", book.GenreID);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "LastName", book.AuthorID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", book.GenreID);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "LastName", book.AuthorID);
            return View(book);
        }

        // POST: Books/Edit/5  (varianta EditPost din Lab3)
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book.FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "",
                s => s.AuthorID, s => s.Title, s => s.Price, s => s.GenreID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                  "Try again, and if the problem persists");
                }
            }

            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", bookToUpdate.GenreID);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "LastName", bookToUpdate.AuthorID);
            return View(bookToUpdate);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}
