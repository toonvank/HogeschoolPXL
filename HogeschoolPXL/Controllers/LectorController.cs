using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using Microsoft.AspNetCore.Authorization;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class LectorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LectorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lector
        public async Task<IActionResult> Index()
        {
              return View(await _context.Lectoren.ToListAsync());
        }

        // GET: Lector/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lectoren == null)
            {
                return NotFound();
            }

            var lector = await _context.Lectoren
                .FirstOrDefaultAsync(m => m.LectorID == id);
            if (lector == null)
            {
                return NotFound();
            }

            return View(lector);
        }

        // GET: Lector/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lector/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LectorID,GebruikerID")] Lector lector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lector);
        }

        // GET: Lector/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lectoren == null)
            {
                return NotFound();
            }

            var lector = await _context.Lectoren.FindAsync(id);
            if (lector == null)
            {
                return NotFound();
            }
            return View(lector);
        }

        // POST: Lector/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LectorID,GebruikerID")] Lector lector)
        {
            if (id != lector.LectorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectorExists(lector.LectorID))
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
            return View(lector);
        }

        // GET: Lector/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lectoren == null)
            {
                return NotFound();
            }

            var lector = await _context.Lectoren
                .FirstOrDefaultAsync(m => m.LectorID == id);
            if (lector == null)
            {
                return NotFound();
            }

            return View(lector);
        }

        // POST: Lector/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lectoren == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lectoren'  is null.");
            }
            var lector = await _context.Lectoren.FindAsync(id);
            if (lector != null)
            {
                _context.Lectoren.Remove(lector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LectorExists(int id)
        {
          return _context.Lectoren.Any(e => e.LectorID == id);
        }
    }
}
