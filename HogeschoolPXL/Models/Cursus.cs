using HogeschoolPXL.CustomValidate;
using HogeschoolPXL.Data;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Cursus
    {
        [Key]
        public int CursusId { get; set; }
        public string? CursusNaam { get; set; }
        public int HandboekID { get; set; }
        public Handboek? Handboek { get; set; }
    }
}
