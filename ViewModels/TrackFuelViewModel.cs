using FT1.Models;
using Microsoft.AspNetCore.Identity;

namespace FT1.ViewModels
{
    public class TrackFuelViewModel
    {
        public int TotalVehicles { get; set; }
        public int TotalFillUps {  get; set; }
        public double TotalFuelPrice { get; set; }
        public double DistanceCovered { get; set; }
        public double FuelConsumption { get; set; }
        public IEnumerable<FillUp> FillUps { get; set; } = null!;
        public IEnumerable<Vehicle> Vehicles { get; set; } = null!; 
    }
}
