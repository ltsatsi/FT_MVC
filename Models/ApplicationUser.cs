using Microsoft.AspNetCore.Identity;

namespace FT1.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Extended properties
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }


        // Navigation properties
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
