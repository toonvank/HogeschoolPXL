using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HogeschoolPXL.CustomValidate;

namespace HogeschoolPXL.Models
{
    public class Handboek
    {
        [Key]
        public int HandboekID { get; set; }
        public string? Titel { get; set; }
        [ValidateDateRange("1/1/1980", "1/1/2022")]
        public DateTime UitgifteDatum { get; set; }
        public string? Afbeelding { get; set; }
    }
}
