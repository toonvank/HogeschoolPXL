using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public int GebruikerID { get; set; }
        public int CursusID { get; set; }
        public Gebruiker? Gebruiker { get; set; }
        public Cursus? Cursus { get; set; }
    }
}
