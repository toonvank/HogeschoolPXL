using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace HogeschoolPXL.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("td", Attributes = "temp-role")]
    public class TempRoleTagHelper : TagHelper
    {
        [HtmlAttributeName("temp-role")]
        public IdentityUser User { get; set; }

        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        ApplicationDbContext _context;

        public TempRoleTagHelper(ApplicationDbContext context,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //eerst geprobeerd in linq maar werkte niet
            foreach (var item in _context.Gebruiker.Include("IdentityUser"))
            {
                if (item.IdentityUser != null)
                {
                    if (item.IdentityUser.Id == User.Id)
                    {
                        output.Content.SetContent(item.TempRole);
                    }
                    else
                    {
                        output.Content.SetContent("Geen gekozen rol");
                    }
                }
            }
        }
    }
}
