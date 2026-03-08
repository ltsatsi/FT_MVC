using FT1.Models;

namespace FT1.Services
{
    public class FuelService : IFuelService
    {
        public double DistanceCovered(IEnumerable<FillUp> fillUps)
        {
            var sortedFillUps = fillUps.OrderBy(f => f.Odometer).ToList();

            if (sortedFillUps.Count < 2)
                return 0;

            double distance = sortedFillUps[^1].Odometer - sortedFillUps[^2].Odometer;

            return distance;
        }

        public double FuelConsumption(IEnumerable<FillUp> fillUps)
        {
            var sortedFillUps = fillUps.OrderBy(f => f.Odometer).ToList();

            if (sortedFillUps.Count < 2)
                return 0;

            var distance = DistanceCovered(sortedFillUps);
            var latestFill = sortedFillUps.Last().Litre;

            var fuelComsumption = distance / latestFill;

            return fuelComsumption;
        }
    }
}
