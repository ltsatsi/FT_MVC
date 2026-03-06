using FT1.Models;

namespace FT1.Interfaces
{
    public interface IVehicleRepo
    {
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<Vehicle> GetByIdAsync(Guid id);
        Task<Vehicle> CreateAsync(Vehicle vehicle);
        Task<Vehicle> UpdateAsync(Vehicle vehicle);
        Task<Vehicle> DeleteAsync(Vehicle vehicle);
    }
}
