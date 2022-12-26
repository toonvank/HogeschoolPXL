﻿using System;
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

    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
              return View(await _context.Studenten.Include("Gebruiker").Include("Cursus").Include("Cursus.Vak").Include("Cursus.Vak.Handboek").ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Studenten == null)
            {
                return NotFound();
            }

            var student = await _context.Studenten.Include("Gebruiker").Include("Cursus").Include("Cursus.Vak.Handboek").Include("Cursus.Vak")
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        [Authorize(Roles = Roles.admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Create([Bind("StudentID,GebruikerID,CursusID,HandboekID, Gebruiker, Cursus,Handboek,Student")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Studenten == null)
            {
                return NotFound();
            }
            
            var student = await _context.Studenten.Include("Gebruiker").Include("Cursus").Include("Handboek").FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,GebruikerID,CursusID,HandboekID, Gebruiker, Cursus,Handboek,Student")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            return View(student);
        }

        // GET: Student/Delete/5
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Studenten == null)
            {
                return NotFound();
            }

            var student = await _context.Studenten
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Studenten == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Studenten'  is null.");
            }
            var student = await _context.Studenten.FindAsync(id);
            if (student != null)
            {
                _context.Studenten.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return _context.Studenten.Any(e => e.StudentID == id);
        }
    }
}
