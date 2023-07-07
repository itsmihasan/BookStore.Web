using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Web.Models;
using Microsoft.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace BookStore.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var shelves = await _context.Shelves.ToListAsync();
            ViewData["ShelfList"] = new SelectList(shelves, "ShelfId", "Code");
            var racks = await _context.Racks.ToListAsync();
            ViewData["RackList"] = new SelectList(racks, "RackId", "Code");
            var books = await _context.Books.FromSqlRaw("EXEC [dbo].[GetBooks]")
                .ToListAsync();
            return View(books);
        }

        // For Book Details PDF
        public async Task<IActionResult> ExportToPdfAsync(int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            var book = await GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var document = new Document();
            var stream = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            document.Add(new Paragraph($"Book Code: {book.Code}"));
            document.Add(new Paragraph($"Book Name: {book.Name}"));
            document.Add(new Paragraph($"Author: {book.Author}"));
            document.Add(new Paragraph($"Year of Publish: {book.YearOfPublish}"));
            document.Add(new Paragraph($"Price: {book.Price}"));


            document.Close();

            Response.ContentType = "application/pdf";
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{book.Name}_details.pdf\"");

            await Response.Body.WriteAsync(stream.GetBuffer());

            return new EmptyResult();
        }

        //GET: Book Details for PDF
        private async Task<Book> GetBookById(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return null;
            }

            // Prepare the parameters for the stored procedure
            var parameters = new[]
            {
                new SqlParameter("@BookId", id)
            };

            // Execute the stored procedure to retrieve the book details
            var book = _context.Books
                .FromSqlRaw("EXEC GetBookDetails @BookId", parameters)
                .AsEnumerable()
                .FirstOrDefault();
            
            return book;
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameters = new[]
            {
                new SqlParameter("@BookId", id)
            };

            var book = _context.Books
                .FromSqlRaw("EXEC GetBookDetails @BookId", parameters)
                .AsEnumerable()
                .FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            var shelfId = book.ShelfId;
            var shelf = await _context.Shelves
                .Include(s => s.Rack)
                .FirstOrDefaultAsync(m => m.ShelfId == shelfId);
            book.Shelf = shelf;

            return View(book);
        }

        public async Task<IActionResult> GetShelves(int rackId)
        {
            var shelves = await _context.Shelves.AsNoTracking().Where(s => s.RackId == rackId)
                                          .Select(s => new { value = s.ShelfId, text = s.Code })
                                          .ToListAsync();

            return Json(shelves);
        }


        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["RackId"] = new SelectList(_context.Racks, "RackId", "Code");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Author,YearOfPublish,IsAvailable,Price,ShelfId")] Book book)
        {
            ModelState.Remove("Shelf");
            if (ModelState.IsValid)
            {

                var parameters = new[]
                {
                    new SqlParameter("@Code", book.Code),
                    new SqlParameter("@Name", book.Name),
                    new SqlParameter("@Author", book.Author),
                    new SqlParameter("@YearOfPublish", book.YearOfPublish),
                    new SqlParameter("@IsAvailable", book.IsAvailable),
                    new SqlParameter("@Price", book.Price),
                    new SqlParameter("@ShelfId", book.ShelfId)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.CreateBook @Code, @Name, @Author, @YearOfPublish, @IsAvailable, @Price, @ShelfId", parameters);

                return RedirectToAction(nameof(Index));
            }

            ViewData["ShelfId"] = new SelectList(_context.Shelves, "ShelfId", "ShelfId", book.ShelfId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Shelf.Rack)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            var racks = await _context.Racks.AsNoTracking().ToListAsync();
            var shelves = await _context.Shelves.AsNoTracking().ToListAsync();

            ViewData["RackList"] = new SelectList(racks, "RackId", "Code");
            ViewData["ShelfList"] = new SelectList(shelves, "ShelfId", "Code");

            // Add the existing rack ID to the ViewBag
            ViewData["SelectedRackId"] = book.Shelf.RackId;
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Code,Name,Author,YearOfPublish,IsAvailable,Price,ShelfId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }
            ModelState.Remove("Shelf");
            if (ModelState.IsValid)
            {
                try
                {

                    var parameters = new[]
                    {
                        new SqlParameter("@BookId", book.BookId),
                        new SqlParameter("@Code", book.Code),
                        new SqlParameter("@Name", book.Name),
                        new SqlParameter("@Author", book.Author),
                        new SqlParameter("@YearOfPublish", book.YearOfPublish),
                        new SqlParameter("@IsAvailable", book.IsAvailable),
                        new SqlParameter("@Price", book.Price),
                        new SqlParameter("@ShelfId", book.ShelfId)
                    };

                    await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateBook @BookId, @Code, @Name, @Author, @YearOfPublish, @IsAvailable, @Price, @ShelfId", parameters);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShelfId"] = new SelectList(_context.Shelves, "ShelfId", "ShelfId", book.ShelfId);
            return View(book);
        }


        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Shelf.Rack)
                .FirstOrDefaultAsync(m => m.BookId == id);

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
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[DeleteBook] {id}");

            return RedirectToAction(nameof(Index));
        }


        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
