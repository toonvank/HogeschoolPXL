using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Lector
    {
        [Key]
        public int LectorID { get; set; }
        public int GebruikerID { get; set; }
        public Gebruiker? Gebruiker { get; set; }
    }
}
