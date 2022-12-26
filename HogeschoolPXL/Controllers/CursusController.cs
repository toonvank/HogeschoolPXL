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
using HogeschoolPXL.Data.DefaultData;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class CursusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cursus
        [Authorize(Roles = Roles.student + "," + Roles.admin)]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cursus.Include("Vak").Include("Vak.Handboek").ToListAsync());
        }

        // GET: Cursus/Details/5
        [Authorize(Roles = Roles.student + "," + Roles.admin)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cursus == null)
            {
                return NotFound();
            }

            var cursus = await _context.Cursus.Include("Vak").Include("Vak.Handboek")
                .FirstOrDefaultAsync(m => m.CursusId == id);
            if (cursus == null)
            {
                return NotFound();
            }

            return View(cursus);
        }

        // GET: Cursus/Create
        [Authorize(Roles = Roles.admin)]
        public IActionResult Create()
        {
            ViewData["Vak"] = new SelectList(_context.Vakken.Select(l => l.VakNaam));
            return View();
        }

        // POST: Cursus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Create([Bind("CursusId,CursusNaam,VakID,Vak")] Cursus cursus)
        {
            ViewData["Vak"] = new SelectList(_context.Vakken.Select(l => l.VakNaam));
            if (ModelState.IsValid)
            {
                if (_context.Handboeken.Any())
                {
                    var handboekid = _context.Vakken.Where(v => v.VakNaam == cursus.Vak.VakNaam).Select(v => v.HandboekID).FirstOrDefault();
                    var handboek = _context.Handboeken.Where(h => h.HandboekID == handboekid).FirstOrDefault();
                    cursus.Vak.Handboek = handboek;
                    _context.Add(cursus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Enkel creatie als er 1 handboek bestaat");
                }
            }
            return View(cursus);
        }

        // GET: Cursus/Edit/5
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Vak"] = new SelectList(_context.Vakken.Select(l => l.VakNaam));
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
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Edit(int id, [Bind("CursusId,CursusNaam,VakID,Vak")] Cursus cursus)
        {
            ViewData["Vak"] = new SelectList(_context.Vakken.Select(l => l.VakNaam));
            if (id != cursus.CursusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // find the handboekid of the selected vak
                    var handboekid = _context.Vakken.Where(v => v.VakNaam == cursus.Vak.VakNaam).Select(v => v.HandboekID).FirstOrDefault();
                    var handboek = _context.Handboeken.Where(h => h.HandboekID == handboekid).FirstOrDefault();
                    cursus.Vak.Handboek = handboek;
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
        [Authorize(Roles = Roles.admin)]
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
        [Authorize(Roles = Roles.admin)]
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
