using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Web.Models;

namespace BookStore.Web.Controllers
{
    public class ShelvesController : Controller
    {
        private readonly LibraryContext _context;

        public ShelvesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Shelves
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Shelves.Include(s => s.Rack);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Shelves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shelves == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelves
                .Include(s => s.Rack)
                .FirstOrDefaultAsync(m => m.ShelfId == id);
            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        // GET: Shelves/Create
        public IActionResult Create()
        {
            ViewData["RackId"] = new SelectList(_context.Racks, "RackId", "RackId");
            return View();
        }

        // POST: Shelves/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShelfId,Code,RackId")] Shelf shelf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shelf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RackId"] = new SelectList(_context.Racks, "RackId", "RackId", shelf.RackId);
            return View(shelf);
        }

        // GET: Shelves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shelves == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelves.FindAsync(id);
            if (shelf == null)
            {
                return NotFound();
            }
            ViewData["RackId"] = new SelectList(_context.Racks, "RackId", "RackId", shelf.RackId);
            return View(shelf);
        }

        // POST: Shelves/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShelfId,Code,RackId")] Shelf shelf)
        {
            if (id != shelf.ShelfId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shelf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelfExists(shelf.ShelfId))
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
            ViewData["RackId"] = new SelectList(_context.Racks, "RackId", "RackId", shelf.RackId);
            return View(shelf);
        }

        // GET: Shelves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shelves == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelves
                .Include(s => s.Rack)
                .FirstOrDefaultAsync(m => m.ShelfId == id);
            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        // POST: Shelves/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Shelves == null)
        //    {
        //        return Problem("Entity set 'LibraryContext.Shelves'  is null.");
        //    }
        //    var shelf = await _context.Shelves.FindAsync(id);
        //    if (shelf != null)
        //    {
        //        _context.Shelves.Remove(shelf);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ShelfExists(int id)
        {
            return (_context.Shelves?.Any(e => e.ShelfId == id)).GetValueOrDefault();
        }
    }
}
