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
            if (!_context.Inschrijvingen.Any())
            {
                Handboek ha = new Handboek { Titel = "C# Web 1", UitgifteDatum = new DateTime(2019, 05, 09, 09, 15, 00), Afbeelding = "https://static.packt-cdn.com/products/9781800567184/cover/smaller"};
                _context.Handboeken.Add(ha);
                _context.SaveChanges();

                Gebruiker ge = new Gebruiker() { Naam = "Palmaers", Voornaam = "Kristof", Email = "palmaerskristof@gmail.com" };
                Lector le = new Lector { Gebruiker = ge };
                Vak va = new Vak { VakNaam = "C# Web 1", Handboek = ha, Studiepunten = 6 };

                VakLector v = new VakLector { Lector = le, Vak = va };
                _context.VakLectoren.Add(v);
                _context.SaveChanges();

                Cursus c = new Cursus { CursusNaam = "Graduaat Programmeren", Vak = va};
                _context.Cursus.Add(c);
                _context.SaveChanges();

                Gebruiker g = new Gebruiker() { Naam = "Van Kimmenade", Voornaam = "Anton", Email = "toonvankimmenade@gmail.com" };
                Student s = new Student() { Gebruiker = g,  Cursus = c};
                _context.Add(s);
                _context.SaveChanges();

                AcademieJaar a = new AcademieJaar { StartDatum = new DateTime(2021, 9, 20) };
                _context.AcademieJaren.Add(a);
                _context.SaveChanges();

                Inschrijving i = new Inschrijving { VakLector = v, AcademieJaar = a, Student = s };
                _context.Inschrijvingen.Add(i);
                _context.SaveChanges();
            }
        }
    }
}
