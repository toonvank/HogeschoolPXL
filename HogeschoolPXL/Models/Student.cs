using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public int GebruikerID { get; set; }
    }
}
