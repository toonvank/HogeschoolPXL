﻿using System;
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
              return View(await _context.Inschrijvingen.ToListAsync());
        }

        // GET: Inschrijving/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Inschrijving/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inschrijving/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InschrijvingID,StudentID,VakLectorID,AcademieJaarID")] Inschrijving inschrijving)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inschrijving);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inschrijving);
        }

        // GET: Inschrijving/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inschrijvingen == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("InschrijvingID,StudentID,VakLectorID,AcademieJaarID")] Inschrijving inschrijving)
        {
            if (id != inschrijving.InschrijvingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
