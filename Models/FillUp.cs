using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FT1.Models
{
    public class FillUp
    {
        [Key]
        public Guid FillUpId { get; set; }
        public string StationName { get; set; } = null!;
        public double Price { get; set; }
        public double Litre { get; set; }
        public double Odometer { get; set; }    
        public DateTime DateOfFill { get; set; }    
        public DateTime? CreatedOn { get; set; }


        // Navigation properties
        [ForeignKey("VehicleId")]
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
