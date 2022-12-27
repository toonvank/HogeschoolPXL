using HogeschoolPXL.Data;
using HogeschoolPXL.Data.DefaultData;
using HogeschoolPXL.Models;
using HogeschoolPXL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System;

namespace HogeschoolPXL.Controllers
{
    public class AccountController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        #region login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            var identityUser = await _userManager.FindByEmailAsync(login.Email);
            if (identityUser != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(identityUser.UserName, login.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Probleem met inloggen");
            return View();
        }
        #endregion
        #region register
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            ViewData["RoleId"] = new SelectList(_context.Roles);
            if (ModelState.IsValid)
            {
                if (registerViewModel.TempRole != null)
                {
                    var identityUser = new IdentityUser();
                    identityUser.Email = registerViewModel.Email;
                    identityUser.UserName = registerViewModel.Email;
                    var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
                    if (identityResult.Succeeded)
                    {

                        Gebruiker g = new Gebruiker() { Email = registerViewModel.Email, TempRole = registerViewModel.TempRole, Naam = identityUser.UserName, Voornaam = identityUser.UserName, IdentityUserID = identityUser.Id, IdentityUser = identityUser};
                        _context.Gebruiker.Add(g);
                        _context.SaveChanges();

                        var signInResult = await _signInManager.PasswordSignInAsync(identityUser.UserName, registerViewModel.Password, false, false);

                        if (signInResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Geen rol geselecteerd");
                }
            }
            return View();
        }
        #endregion
        #region logout
        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion
        #region identity
        [HttpGet]
        [Authorize(Roles = Roles.admin)]
        public IActionResult Identity()
        {  
            var identityViewModel = new IdentityViewModel();
            ViewData["RoleId"] = new SelectList(_context.Roles);
            identityViewModel.Roles = _roleManager.Roles;
            identityViewModel.Users = _userManager.Users;
            return View(identityViewModel);
        }
        #endregion
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UserClaimAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var identityUser = await _userManager.GetUserAsync(User);
                if (identityUser != null)
                    return View("UserClaim", identityUser);
            }
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(IdentityViewModel identityViewModel)
        {
            var identityUser = await _userManager.FindByIdAsync(identityViewModel.UserID);
            var roles = await _userManager.GetRolesAsync(identityUser);
            await _userManager.RemoveFromRolesAsync(identityUser, roles);
            await _userManager.AddToRoleAsync(identityUser, identityViewModel.RoleId);

            //indien een rol aan gebruiker toegewezen is veranderd status naar assigned
            foreach (var g in _context.Gebruiker.Include("IdentityUser"))
            {
                var idUser = await _userManager.FindByEmailAsync(g.Email);
                g.IdentityUser = idUser;
                if (g.IdentityUserID == identityUser.Id)
                {
                    g.TempRole = "Assigned";
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Identity");
        }
    }
}
