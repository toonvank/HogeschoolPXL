using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Inschrijving
    {
        public int InschrijvingID { get; set; }
        public int StudentID { get; set; }
        public int VakLectorID { get; set; }
        public int AcademieJaarID { get; set; }
    }
}
