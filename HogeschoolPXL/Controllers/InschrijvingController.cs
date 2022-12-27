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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class InschrijvingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InschrijvingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inschrijving
        public async Task<IActionResult> Index()
        {
            //get properties from url
            var lector = Request.Query["category"];
            if (lector.Count >0)
            {
                var list = await _context.Inschrijvingen.Include("Student").Include("VakLector").Include("AcademieJaar").Include("Student.Gebruiker").Include("Student.Cursus").Include("Student.Cursus.Vak.Handboek").Include("Student.Cursus.Vak").Include("VakLector.Lector.Gebruiker").ToListAsync();
                return View(list.Where(x => x.VakLector.Lector.Gebruiker.Naam == lector));
            }
            else
            {
                return View(await _context.Inschrijvingen.Include("Student").Include("VakLector").Include("AcademieJaar").Include("Student.Gebruiker").Include("Student.Cursus").Include("Student.Cursus.Vak.Handboek").Include("Student.Cursus.Vak").Include("VakLector.Lector.Gebruiker").ToListAsync());
            }
        }

        // GET: Inschrijving/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inschrijvingen == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen.Include("Student").Include("VakLector").Include("AcademieJaar").Include("Student.Gebruiker").Include("Student.Cursus").Include("Student.Cursus.Vak").Include("Student.Cursus.Vak.Handboek").Include("VakLector.Lector.Gebruiker")
                .FirstOrDefaultAsync(m => m.InschrijvingID == id);
            inschrijving.Student.Gebruiker = _context.Gebruiker.Find(inschrijving.Student.GebruikerID);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        // GET: Inschrijving/Create
        [Authorize(Roles = Roles.admin)]
        public IActionResult Create()
        {
            ViewData["VakLectoren"] = new SelectList(_context.VakLectoren.Include("Lector").Include("Lector.Gebruiker").Select(l => l.Lector.Gebruiker.Naam + " " + l.Lector.Gebruiker.Voornaam));
            ViewData["Cursussen"] = new SelectList(_context.Cursus.Select(l => l.CursusNaam));
            return View();
        }

        // POST: Inschrijving/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Create([Bind("InschrijvingID,StudentID,VakLectorID,AcademieJaarID,VakLector,Student,AcademieJaar,Cursus,CursusID")] Inschrijving inschrijving)
        {
            if (ModelState.IsValid)
            {
                if (_context.Studenten.Any())
                {
                    inschrijving.Student.Cursus = _context.Cursus.FirstOrDefault(c => c.CursusNaam == inschrijving.Student.Cursus.CursusNaam);
                    // find vak by vaknaam in context and set it to the student
                    inschrijving.Student.Cursus.Vak = _context.Vakken.FirstOrDefault();
                    inschrijving.Student.Cursus.Vak.Handboek = _context.Handboeken.FirstOrDefault();
                    inschrijving.VakLector = _context.VakLectoren.Include("Lector").Include("Lector.Gebruiker").FirstOrDefault(l => l.Lector.Gebruiker.Naam + " " + l.Lector.Gebruiker.Voornaam == inschrijving.VakLector.Lector.Gebruiker.Naam + " " + inschrijving.VakLector.Lector.Gebruiker.Voornaam);
                    _context.Add(inschrijving);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Enkel toegelaten als er studenten zijn en een cursus");
                }
            }
            return View(inschrijving);
        }

        // GET: Inschrijving/Edit/5
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Edit(int? id)
        {

            ViewData["VakLectoren"] = new SelectList(_context.VakLectoren.Include("Lector").Include("Lector.Gebruiker").Select(l => l.Lector.Gebruiker.Naam + " " + l.Lector.Gebruiker.Voornaam));
            ViewData["Cursussen"] = new SelectList(_context.Cursus.Select(l => l.CursusNaam));
            if (id == null || _context.Inschrijvingen == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen.Include("Student").Include("VakLector").Include("AcademieJaar").Include("Student.Gebruiker").Include("Student.Cursus").Include("Student.Cursus.Vak.Handboek").Include("Student.Cursus.Vak").Include("VakLector.Lector.Gebruiker").FirstOrDefaultAsync(m => m.InschrijvingID == id);
            inschrijving.Student.Gebruiker = _context.Gebruiker.Find(inschrijving.Student.GebruikerID);
            if (inschrijving == null)
            {
                return NotFound();
            }
            return View(inschrijving);
        }

        // POST: Inschrijving/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Edit(int id, [Bind("InschrijvingID,StudentID,VakLectorID,AcademieJaarID,Student,AcademieJaar,Cursus, CursusID,Handboek,HandboekID")] Inschrijving inschrijving)
        {
            if (id != inschrijving.InschrijvingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    inschrijving.Student.Cursus.Vak = _context.Vakken.FirstOrDefault();
                    inschrijving.Student.Cursus.Vak.Handboek = _context.Handboeken.FirstOrDefault();
                    //inschrijving.VakLector = _context.VakLectoren.Include("Lector").Include("Lector.Gebruiker").FirstOrDefault(l => l.Lector.Gebruiker.Naam + " " + l.Lector.Gebruiker.Voornaam == inschrijving.VakLector.Lector.Gebruiker.Naam + " " + inschrijving.VakLector.Lector.Gebruiker.Voornaam);
                    inschrijving.Student.Cursus = _context.Cursus.FirstOrDefault(c => c.CursusNaam == inschrijving.Student.Cursus.CursusNaam);
                    _context.Update(inschrijving);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InschrijvingExists(inschrijving.InschrijvingID))
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
            return View(inschrijving);
        }

        // GET: Inschrijving/Delete/5
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inschrijvingen == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen
                .FirstOrDefaultAsync(m => m.InschrijvingID == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        // POST: Inschrijving/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inschrijvingen == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Inschrijvingen'  is null.");
            }
            var inschrijving = await _context.Inschrijvingen.FindAsync(id);
            if (inschrijving != null)
            {
                _context.Inschrijvingen.Remove(inschrijving);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InschrijvingExists(int id)
        {
          return _context.Inschrijvingen.Any(e => e.InschrijvingID == id);
        }
    }
}
