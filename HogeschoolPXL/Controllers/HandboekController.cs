using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models;

namespace HogeschoolPXL.Controllers
{
    public class HandboekController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HandboekController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Handboek
        public async Task<IActionResult> Index()
        {
              return View(await _context.Handboeken.ToListAsync());
        }

        // GET: Handboek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Handboeken == null)
            {
                return NotFound();
            }

            var handboek = await _context.Handboeken
                .FirstOrDefaultAsync(m => m.HandboekID == id);
            if (handboek == null)
            {
                return NotFound();
            }

            return View(handboek);
        }

        // GET: Handboek/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Handboek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HandboekID,Titel,UitgifteDatum,Afbeelding")] Handboek handboek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(handboek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(handboek);
        }

        // GET: Handboek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Handboeken == null)
            {
                return NotFound();
            }

            var handboek = await _context.Handboeken.FindAsync(id);
            if (handboek == null)
            {
                return NotFound();
            }
            return View(handboek);
        }

        // POST: Handboek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HandboekID,Titel,UitgifteDatum,Afbeelding")] Handboek handboek)
        {
            if (id != handboek.HandboekID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(handboek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HandboekExists(handboek.HandboekID))
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
            return View(handboek);
        }

        // GET: Handboek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Handboeken == null)
            {
                return NotFound();
            }

            var handboek = await _context.Handboeken
                .FirstOrDefaultAsync(m => m.HandboekID == id);
            if (handboek == null)
            {
                return NotFound();
            }

            return View(handboek);
        }

        // POST: Handboek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Handboeken == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Handboeken'  is null.");
            }
            var handboek = await _context.Handboeken.FindAsync(id);
            if (handboek != null)
            {
                _context.Handboeken.Remove(handboek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HandboekExists(int id)
        {
          return _context.Handboeken.Any(e => e.HandboekID == id);
        }
    }
}
