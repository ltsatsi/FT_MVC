using System.ComponentModel.DataAnnotations;

namespace FT1.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Address is required.")]
        public string Email { get; set; } = null!;


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;
    }
}
