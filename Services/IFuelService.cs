using FT1.Models;

namespace FT1.Services
{
    public interface IFuelService
    {
        double DistanceCovered(IEnumerable<FillUp> fillUps);
        double FuelConsumption(IEnumerable<FillUp> fillUps);
    }
}
