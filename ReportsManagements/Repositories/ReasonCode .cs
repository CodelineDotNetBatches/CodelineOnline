using ReportsManagements.Models;
using Microsoft.EntityFrameworkCore;

namespace ReportsManagements.Repositories
{
    public class ReasonCode : IReasonCode
    {
        private readonly ReportsDbContext _context;

        public ReasonCode(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.ReasonCode>> GetAllAsync()
        {
            return await _context.ReasonCodes.ToListAsync();
        }

        public async Task<Models.ReasonCode> GetByIdAsync(int id)
        {
            return await _context.ReasonCodes.FindAsync(id);
        }

        public async Task AddAsync(Models.ReasonCode reasonCode)
        {
            _context.ReasonCodes.Add(reasonCode);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Models.ReasonCode reasonCode)
        {
            _context.ReasonCodes.Update(reasonCode);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ReasonCodes.FindAsync(id);
            if (entity != null)
            {
                _context.ReasonCodes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
