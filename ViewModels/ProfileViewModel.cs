using System.ComponentModel.DataAnnotations;

namespace FT1.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;


        [Display(Name = "Last name")]
        public string? LastName { get; set; }


        [Display(Name = "Email address")]
        public string Email { get; set; } = null!;


        [Display(Name = "Joined At")]
        public DateTime CreatedOn { get; set; }
    }
}
