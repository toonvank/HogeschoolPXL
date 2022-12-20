using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class VakLector
    {
        [Key]
        public int VakLectorID { get; set; }
        public int LectorID { get; set; }
        public int? VakId { get; set; }
        public Vak? Vak { get; set; }
        public Lector? Lector { get; set; }
    }
}
