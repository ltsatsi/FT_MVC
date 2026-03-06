using FT1.Data;
using FT1.Interfaces;
using FT1.Models;
using Microsoft.EntityFrameworkCore;

namespace FT1.Repositories
{
    public class FillUpRepo : IFillUpRepo
    {
        private readonly ApplicationDBContext context;
        public FillUpRepo(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<FillUp> CreateAsync(FillUp fillUp)
        {
            await context.FillUps.AddAsync(fillUp);
            await context.SaveChangesAsync();
            return fillUp;
        }

        public async Task<FillUp> DeleteAsync(FillUp fillUp)
        {
            context.FillUps.Remove(fillUp);
            await context.SaveChangesAsync();
            return fillUp;
        }

        public async Task<IEnumerable<FillUp>> GetAllAsync()
        {
            var fillUp = await context.FillUps
                .Include(v => v.Vehicle)
                .ToListAsync();

            return fillUp;
        }

        public async Task<FillUp> GetByIdAsync(Guid id)
        {
            var fillUp = await context.FillUps.FirstOrDefaultAsync(f => f.FillUpId == id);
            return fillUp;
        }

        public async Task<FillUp> UpdateAsync(FillUp fillUp)
        {
            context.FillUps.Update(fillUp);
            await context.SaveChangesAsync();
            return fillUp;
        }
    }
}
