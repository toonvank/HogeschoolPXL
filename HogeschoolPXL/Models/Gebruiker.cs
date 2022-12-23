using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Gebruiker
    {
        [Key]
        public int GebruikerID { get; set; }
        public string? Naam { get; set; }
        public string? Voornaam { get; set; }
        public string? Email { get; set; }
        public string? IdentityUserID { get; set; }
        public IdentityUser? IdentityUser { get; set; }
        public string? TempRole { get; set; }
    }
}
