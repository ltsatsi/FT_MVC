using FT1.Data;
using FT1.Interfaces;
using FT1.Models;
using Microsoft.EntityFrameworkCore;

namespace FT1.Repositories
{
    public class VehicleRepo : IVehicleRepo
    {
        private readonly ApplicationDBContext context;
        public VehicleRepo(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> CreateAsync(Vehicle vehicle)
        {
            await context.Vehicles.AddAsync(vehicle);   
            await context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> DeleteAsync(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            var vehicles = await context.Vehicles
                .Include(f => f.FillUp)
                .ToListAsync();

            return vehicles;
        }

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            var vehicle = await context.Vehicles.FirstOrDefaultAsync(f => f.VehicleId == id);
            return vehicle;
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            context.Vehicles.Update(vehicle);
            await context.SaveChangesAsync();
            return vehicle;
        }
    }
}
