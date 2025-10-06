using UserManagement.Models;
using Microsoft.EntityFrameworkCore;   // 👈 Needed for ToListAsync(), FindAsync(), etc.

namespace UserManagement.Repositories
{
    /// <summary>
    /// Repository implementation for batch entity.
    /// </summary>
    public class BatchRepository : IBatchRepository
    {
        private readonly UsersDbContext _context;

        public BatchRepository(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Batch>> GetAllAsync()
        {
            return await _context.Batches.ToListAsync();
        }

        public async Task<Batch?> GetByIdAsync(int id)
        {
            return await _context.Batches.FindAsync(id);
        }

        public async Task AddAsync(Batch batch)
        {
            await _context.Batches.AddAsync(batch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Batch batch)
        {
            _context.Batches.Update(batch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch != null)
            {
                _context.Batches.Remove(batch);
                await _context.SaveChangesAsync();
            }
        }
    }
}
