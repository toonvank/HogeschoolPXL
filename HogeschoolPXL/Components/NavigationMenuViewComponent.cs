using HogeschoolPXL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogeschoolPXL.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;
        public NavigationMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_context.VakLectoren.Include("Lector.Gebruiker").Include("Lector")
            .Select(x => x.Lector.Gebruiker.Voornaam.Substring(0,1) + ". " + x.Lector.Gebruiker.Naam)
            .Distinct()
            .OrderBy(x => x));
        }
    }
}
