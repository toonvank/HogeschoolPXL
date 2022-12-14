using HogeschoolPXL.Data;
using HogeschoolPXL.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        // HTTP POST iaction register
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            ViewData["RoleId"] = new SelectList(_context.Roles);
            if (ModelState.IsValid)
            {
                if (registerViewModel.RoleId != null)
                {
                    var identityUser = new IdentityUser();
                    identityUser.Email = registerViewModel.Email;
                    identityUser.UserName = registerViewModel.Email;
                    var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
                    // check if login is succeeded  
                    if (identityResult.Succeeded)
                    {
                        var identityRole = await _roleManager.FindByNameAsync(registerViewModel.RoleId);

                        // add role asynchronus identityuser and roleid     
                        var roleResult = await _userManager.AddToRoleAsync(identityUser, identityRole.Name);
                        // return login if result succeeded
                        if (roleResult.Succeeded)
                        {
                            return View("RegistrationCompleted");
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
            return View("Login");
        }
        #endregion
        #region identity
        [HttpGet]
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
    }
}
