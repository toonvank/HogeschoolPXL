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
    public class VakLectorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VakLectorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VakLector
        public async Task<IActionResult> Index()
        {
              return View(_context.VakLectoren.Include("Lector").Include("Vak").ToList());
        }

        // GET: VakLector/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VakLectoren == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLectoren.Include("Lector").Include("Vak").FirstOrDefaultAsync(m => m.VakLectorID == id);
            vakLector.Lector.Gebruiker = _context.Gebruiker.Find(vakLector.Lector.GebruikerID);
            if (vakLector == null)
            {
                return NotFound();
            }

            return View(vakLector);
        }

        // GET: VakLector/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VakLector/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VakLectorID,LectorID,VakId,Vak,Lector")] VakLector vakLector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vakLector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vakLector);
        }

        // GET: VakLector/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VakLectoren == null)
            {
                return NotFound();
            }
            
            var vakLector = await _context.VakLectoren.Include("Lector").Include("Vak").Include("Handboek").FirstOrDefaultAsync(m => m.VakLectorID == id);
            vakLector.Lector.Gebruiker = _context.Gebruiker.Find(vakLector.Lector.GebruikerID);

            if (vakLector == null)
            {
                return NotFound();
            }
            return View(vakLector);
        }

        // POST: VakLector/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VakLectorID,LectorID,VakId,Vak,Lector,GebruikerID")] VakLector vakLector)
        {
            if (id != vakLector.VakLectorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vakLector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakLectorExists(vakLector.VakLectorID))
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
            return View(vakLector);
        }

        // GET: VakLector/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VakLectoren == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLectoren
                .FirstOrDefaultAsync(m => m.VakLectorID == id);
            if (vakLector == null)
            {
                return NotFound();
            }

            return View(vakLector);
        }

        // POST: VakLector/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VakLectoren == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VakLectoren'  is null.");
            }
            var vakLector = await _context.VakLectoren.FindAsync(id);
            if (vakLector != null)
            {
                _context.VakLectoren.Remove(vakLector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakLectorExists(int id)
        {
          return _context.VakLectoren.Any(e => e.VakLectorID == id);
        }
    }
}
