using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Vak
    {
        [Key]
        public int VakId { get; set; }
        public string? VakNaam { get; set; }
        public int Studiepunten { get; set; }
        public int HandboekID { get; set; }
    }
}
