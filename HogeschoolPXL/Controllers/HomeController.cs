using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using HogeschoolPXL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(HomeViewModel homeViewModel)
        {
            // find student by name linq query
            foreach (var item in _context.Studenten.Include("Gebruiker"))
            {
                if (item.Gebruiker.Naam.ToLower().Contains(homeViewModel.searchQuery.ToLower()))
                {
                    return RedirectToAction("Details", "Student", new { id = item.GebruikerID });
                }
                else
                {
                    ModelState.AddModelError("", "Not found");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}