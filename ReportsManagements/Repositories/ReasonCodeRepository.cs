using ReportsManagements.Models;
using Microsoft.EntityFrameworkCore;

namespace ReportsManagements.Repositories
{
    public class ReasonCodeRepository : IReasonCodeRepository
    {
        // Dependency on the database context
        private readonly ReportsDbContext _context;

        public ReasonCodeRepository(ReportsDbContext context)
        {
            _context = context;
        }

        // Retrieves all reason codes from the database
        public async Task<IEnumerable<Models.ReasonCode>> GetAllAsync()
        {
            return await _context.ReasonCodes.ToListAsync();
        }

        // Retrieves a reason code by its ID
        public async Task<Models.ReasonCode> GetByIdAsync(int id)
        {
            return await _context.ReasonCodes.FindAsync(id);
        }

        // Adds a new reason code to the database
        public async Task AddAsync(Models.ReasonCode reasonCode)
        {
            _context.ReasonCodes.Add(reasonCode);
            await _context.SaveChangesAsync();
        }

        // Updates an existing reason code in the database
        public async Task UpdateAsync(Models.ReasonCode reasonCode)
        {
            _context.ReasonCodes.Update(reasonCode);
            await _context.SaveChangesAsync();
        }

        // Deletes a reason code from the database by its ID
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ReasonCodes.FindAsync(id); // Find the entity by ID
            if (entity != null)
            {
                _context.ReasonCodes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

