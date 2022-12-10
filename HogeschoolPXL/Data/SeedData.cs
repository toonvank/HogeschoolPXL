using HogeschoolPXL.Models;
using Microsoft.EntityFrameworkCore;

namespace HogeschoolPXL.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!context.Studenten.Any())
                {
                    Gebruiker g = new Gebruiker() {GebruikerID = 0, Naam = "Van Kimmenade", Voornaam = "Anton", Email = "toonvankimmenade@gmail.com"};
                    Gebruiker g2 = new Gebruiker() { Naam = "Palmaers", Voornaam = "Kristof", Email = "palmaerskristof@gmail.com" };
                    Student s = new Student() {StudentID = 0, GebruikerID = 0};
                    context.Gebruiker.Add(g);
                    context.Gebruiker.Add(g2);
                    context.Studenten.Add(s);
                    context.SaveChanges();
                }
                if (!context.Lectoren.Any())
                {
                    Lector l = new Lector {GebruikerID = 1, LectorID = 0};
                    context.Lectoren.Add(l);
                    context.SaveChanges();
                }
                if (!context.Handboeken.Any())
                {
                    Handboek h = new Handboek {Titel = "C# Web 1", UitgifteDatum = DateTime.Now, HandboekID = 0,};
                    context.Handboeken.Add(h);
                    context.SaveChanges();
                }
                if (!context.Vakken.Any())
                {
                    Vak v = new Vak {VakId = 0, VakNaam = "C# Web 1", HandboekID = 0, Studiepunten = 6};
                    context.Vakken.Add(v);
                    context.SaveChanges();
                }
                if (!context.VakLectoren.Any())
                {
                    VakLector v = new VakLector {LectorID = 0, VakId = 0, VakLectorID = 0,};
                    context.VakLectoren.Add(v);
                    context.SaveChanges();
                }
                if (!context.AcademieJaren.Any())
                {
                    AcademieJaar a = new AcademieJaar {AcademieJaarID = 0, StartDatum = new DateTime(2021, 9, 20)};
                    context.AcademieJaren.Add(a);
                    context.SaveChanges();
                }
                if (!context.Inschrijvingen.Any())
                {
                    Inschrijving i = new Inschrijving{AcademieJaarID = 0, InschrijvingID = 0, VakLectorID = 0, StudentID = 0};
                    context.Inschrijvingen.Add(i);
                    context.SaveChanges();
                }
            }
        }
    }
}
