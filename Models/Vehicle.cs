using System.ComponentModel.DataAnnotations;

namespace FT1.Models
{
    public class Vehicle
    {
        [Key]
        public Guid VehicleId { get; set; }
        public string Registration { get; set; } = null!;   
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }


        // Navigation properties
        public Guid Id { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public FillUp? FillUp { get; set; } 
    }
}
