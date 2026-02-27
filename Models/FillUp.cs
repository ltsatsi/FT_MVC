namespace FT1.Models
{
    public class FillUp
    {
        public Guid FillUpId { get; set; }
        public string StationName { get; set; } = null!;
        public double Price { get; set; }
        public double Litre { get; set; }
        public double Odometer { get; set; }    
        public DateTime DateOfFill { get; set; }    
        public DateTime? CreatedOn { get; set; }


        // Navigation properties
        public Vehicle Vehicle { get; set; } = new();
    }
}
