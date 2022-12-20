using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class AcademieJaar
    {
        [Key]
        public int AcademieJaarID { get; set; }
        public DateTime StartDatum { get; set; }
    }
}
