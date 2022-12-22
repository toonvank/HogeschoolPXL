using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Inschrijving
    {
        [Key]
        public int InschrijvingID { get; set; }
        public int StudentID { get; set; }
        public int AcademieJaarID { get; set; }
        public Student? Student { get; set; }
        public VakLector? VakLector { get; set; }
        public AcademieJaar? AcademieJaar { get; set; }
    }
}
