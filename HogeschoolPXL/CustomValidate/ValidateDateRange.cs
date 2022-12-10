using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.CustomValidate
{
    public class ValidateDateRange : ValidationAttribute
    {
        public DateTime _FirstDate { get; set; }
        public DateTime _SecondDate { get; set; }
        public ValidateDateRange(string FirstDate, string SecondDate)
        {
            _FirstDate = Convert.ToDateTime(FirstDate);
            _SecondDate = Convert.ToDateTime(SecondDate);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToDateTime(value) >= _FirstDate && Convert.ToDateTime(value) <= _SecondDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("UitgifteDatum tussen 1/1/1980 en 1/1/2022");
            }
        }
    }
}
