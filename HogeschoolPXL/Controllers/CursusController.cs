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
    public class CursusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cursus
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cursus.ToListAsync());
        }

        // GET: Cursus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cursus == null)
            {
                return NotFound();
            }

            var cursus = await _context.Cursus
                .FirstOrDefaultAsync(m => m.CursusId == id);
            if (cursus == null)
            {
                return NotFound();
            }

            return View(cursus);
        }

        // GET: Cursus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cursus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CursusId,CursusNaam")] Cursus cursus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cursus);
        }

        // GET: Cursus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cursus == null)
            {
                return NotFound();
            }

            var cursus = await _context.Cursus.FindAsync(id);
            if (cursus == null)
            {
                return NotFound();
            }
            return View(cursus);
        }

        // POST: Cursus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CursusId,CursusNaam")] Cursus cursus)
        {
            if (id != cursus.CursusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursusExists(cursus.CursusId))
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
            return View(cursus);
        }

        // GET: Cursus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cursus == null)
            {
                return NotFound();
            }

            var cursus = await _context.Cursus
                .FirstOrDefaultAsync(m => m.CursusId == id);
            if (cursus == null)
            {
                return NotFound();
            }

            return View(cursus);
        }

        // POST: Cursus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cursus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cursus'  is null.");
            }
            var cursus = await _context.Cursus.FindAsync(id);
            if (cursus != null)
            {
                _context.Cursus.Remove(cursus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursusExists(int id)
        {
          return _context.Cursus.Any(e => e.CursusId == id);
        }
    }
}
