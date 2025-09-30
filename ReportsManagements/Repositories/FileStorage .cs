using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ReportsManagements.Repositories
{
    public class FileStorage : IFileStorage
    {
        private readonly ReportsDbContext _context;

        public FileStorage(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.FileStorage>> GetAllAsync()
        {
            return await _context.FileStorages.ToListAsync();
        }

        public async Task<Models.FileStorage?> GetByIdAsync(int id)
        {
            return await _context.FileStorages.FindAsync(id);
        }

        public async Task AddAsync(Models.FileStorage file)
        {
            _context.FileStorages.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Models.FileStorage file)
        {
            _context.FileStorages.Update(file);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FileStorages.FindAsync(id);
            if (entity != null)
            {
                _context.FileStorages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
