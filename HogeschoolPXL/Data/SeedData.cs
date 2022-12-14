using HogeschoolPXL.Data.DefaultData;
using HogeschoolPXL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace HogeschoolPXL.Data
{
    public static class SeedData
    {
        //identity initaliseren
        static ApplicationDbContext? _context;
        static RoleManager<IdentityRole>? _roleManager;
        static UserManager<IdentityUser>? _userManager;
        private static async Task VoegRolToeAsync(RoleManager<IdentityRole> _roleManager, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                IdentityRole role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
        }
        private static async Task VoegRollenToeAsync(ApplicationDbContext __context, RoleManager<IdentityRole> _roleManager)
        {
            if (!__context.Roles.Any())
            {
                await VoegRolToeAsync(_roleManager, Roles.admin);
                await VoegRolToeAsync(_roleManager, Roles.student);
                await VoegRolToeAsync(_roleManager, Roles.lector);
            }
        }
        private static async Task CreateIdentityRecordAsync(string userName, string email, string pwd, string role)
        {

            if (_userManager != null && await _userManager.FindByEmailAsync(email) == null && await _userManager.FindByNameAsync(userName) == null)
            {
                var identityUser = new IdentityUser() { Email = email, UserName = userName };
                var result = await _userManager.CreateAsync(identityUser, pwd);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, role);
                }
            }
        }
        public static async Task EnsurePopulatedAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                _userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await VoegRollenToeAsync(_context, _roleManager);
                await CreateIdentityRecordAsync(Roles.student, "student@pxl.be", "Student123!", Roles.student);
                await CreateIdentityRecordAsync(Roles.admin, "admin@pxl.be", "Admin456!", Roles.admin);
                VoegStandaardDataToe();
            }
        }
        public static void VoegStandaardDataToe()
        {
            if (!_context.Studenten.Any())
            {
                Gebruiker g = new Gebruiker() { GebruikerID = 0, Naam = "Van Kimmenade", Voornaam = "Anton", Email = "toonvankimmenade@gmail.com" };
                Gebruiker g2 = new Gebruiker() { Naam = "Palmaers", Voornaam = "Kristof", Email = "palmaerskristof@gmail.com" };
                Student s = new Student() { StudentID = 0, GebruikerID = 0 };
                _context.Gebruiker.Add(g);
                _context.Gebruiker.Add(g2);
                _context.Studenten.Add(s);
                //_context.SaveChanges();
            }
            if (!_context.Lectoren.Any())
            {
                Lector l = new Lector { GebruikerID = 1, LectorID = 0 };
                _context.Lectoren.Add(l);
                //_context.SaveChanges();
            }
            if (!_context.Handboeken.Any())
            {
                Handboek h = new Handboek { Titel = "C# Web 1", UitgifteDatum = DateTime.Now, HandboekID = 0, };
                _context.Handboeken.Add(h);
                //_context.SaveChanges();
            }
            if (!_context.Vakken.Any())
            {
                Vak v = new Vak { VakId = 0, VakNaam = "C# Web 1", HandboekID = 0, Studiepunten = 6 };
                _context.Vakken.Add(v);
                //_context.SaveChanges();
            }
            if (!_context.VakLectoren.Any())
            {
                VakLector v = new VakLector { LectorID = 0, VakId = 0, VakLectorID = 0, };
                _context.VakLectoren.Add(v);
                //_context.SaveChanges();
            }
            if (!_context.AcademieJaren.Any())
            {
                AcademieJaar a = new AcademieJaar { AcademieJaarID = 0, StartDatum = new DateTime(2021, 9, 20) };
                _context.AcademieJaren.Add(a);
                //_context.SaveChanges();
            }
            if (!_context.Inschrijvingen.Any())
            {
                Inschrijving i = new Inschrijving { AcademieJaarID = 0, InschrijvingID = 0, VakLectorID = 0, StudentID = 0 };
                _context.Inschrijvingen.Add(i);
            }
            _context.SaveChanges();
        }
    }
}
