using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        public string? RoleId { get; set; }
    }
}
