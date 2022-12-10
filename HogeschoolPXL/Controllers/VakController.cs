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
    public class VakController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VakController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vak
        public async Task<IActionResult> Index()
        {
              return View(await _context.Vakken.ToListAsync());
        }

        // GET: Vak/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        // GET: Vak/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VakId,VakNaam,Studiepunten,HandboekID")] Vak vak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vak);
        }

        // GET: Vak/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken.FindAsync(id);
            if (vak == null)
            {
                return NotFound();
            }
            return View(vak);
        }

        // POST: Vak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VakId,VakNaam,Studiepunten,HandboekID")] Vak vak)
        {
            if (id != vak.VakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakExists(vak.VakId))
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
            return View(vak);
        }

        // GET: Vak/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        // POST: Vak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vakken == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vakken'  is null.");
            }
            var vak = await _context.Vakken.FindAsync(id);
            if (vak != null)
            {
                _context.Vakken.Remove(vak);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakExists(int id)
        {
          return _context.Vakken.Any(e => e.VakId == id);
        }
    }
}
