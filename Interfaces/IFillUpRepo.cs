using FT1.Models;

namespace FT1.Interfaces
{
    public interface IFillUpRepo
    {
        Task<IEnumerable<FillUp>> GetAllAsync();
        Task<FillUp> GetByIdAsync(Guid id);
        Task<FillUp> CreateAsync(FillUp fillUp);
        Task<FillUp> UpdateAsync(FillUp fillUp);    
        Task<FillUp> DeleteAsync(FillUp fillUp);
    }
}
