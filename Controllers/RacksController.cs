using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Web.Models;
using Microsoft.Data.SqlClient;

namespace BookStore.Web.Controllers
{
    public class RacksController : Controller
    {
        private readonly LibraryContext _context;

        public RacksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Racks
        public async Task<IActionResult> Index()
        {
            if (_context.Racks == null)
            {
                return Problem("Entity set 'LibraryContext.Racks' is null.");
            }

            var racks = await _context.Racks.FromSqlRaw("EXEC GetRacks").ToListAsync();
            return View(racks);
        }


        // GET: Racks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Racks == null)
            {
                return NotFound();
            }

            // Prepare the parameters for the stored procedure
            var parameters = new[]
            {
                new SqlParameter("@RackId", id)
            };

            // Execute the stored procedure to retrieve the rack details
            var rack = _context.Racks
                .FromSqlRaw("EXEC GetRackDetails @RackId", parameters)
                .AsEnumerable()
                .FirstOrDefault();

            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }


        // GET: Racks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Racks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code")] Rack rack)
        {
            if (ModelState.IsValid)
            {
                // Call the stored procedure to create the Rack
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC CreateRack {rack.Code}");

                return RedirectToAction(nameof(Index));
            }

            return View(rack);
        }

        // GET: Racks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Racks == null)
            {
                return NotFound();
            }

            var rack = await _context.Racks.FindAsync(id);
            if (rack == null)
            {
                return NotFound();
            }
            return View(rack);
        }


        // POST: Racks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RackId,Code")] Rack rack)
        {
            if (id != rack.RackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Call the stored procedure to update the Rack
                    await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC UpdateRack {rack.RackId}, {rack.Code}");

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RackExists(rack.RackId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(rack);
        }

        // GET: Racks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Racks == null)
            {
                return NotFound();
            }

            var rack = await _context.Racks
                .FirstOrDefaultAsync(m => m.RackId == id);
            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }

        // POST: Racks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Racks == null)
        //    {
        //        return Problem("Entity set 'LibraryContext.Racks'  is null.");
        //    }
        //    var rack = await _context.Racks.FindAsync(id);
        //    if (rack != null)
        //    {
        //        _context.Racks.Remove(rack);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool RackExists(int id)
        {
            return (_context.Racks?.Any(e => e.RackId == id)).GetValueOrDefault();
        }
    }
}
