using System.ComponentModel.DataAnnotations;

namespace FT1.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First name")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "First name must be between 3 to 20 characters.")]
        public string FirstName { get; set; } = null!;


        [Display(Name = "Last name")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Last name must be between 3 to 20 characters.")]
        public string? LastName { get; set; }


        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required.")]
        [Display(Name = "Email address")]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;


        [Required(ErrorMessage = "Confirm Password is required.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
