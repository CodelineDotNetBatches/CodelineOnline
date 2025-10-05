using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ReportsManagements.Repositories
{
    public class FileStorageRepository : IFileStorageRepository
    {
        // Dependency on the database context
        private readonly ReportsDbContext _context;

        public FileStorageRepository(ReportsDbContext context)
        {
            _context = context;
        }

        // Retrieves all file storage records from the database
        public async Task<IEnumerable<Models.FileStorage>> GetAllAsync()
        {
            return await _context.FileStorages.ToListAsync();
        }

        // Retrieves a file storage record by its ID
        public async Task<Models.FileStorage?> GetByIdAsync(int id)
        {
            return await _context.FileStorages.FindAsync(id);
        }

        // Adds a new file storage record to the database
        public async Task AddAsync(Models.FileStorage file)
        {
            _context.FileStorages.Add(file);
            await _context.SaveChangesAsync();
        }

        // Updates an existing file storage record in the database
        public async Task UpdateAsync(Models.FileStorage file)
        {
            _context.FileStorages.Update(file);
            await _context.SaveChangesAsync();
        }

        // Deletes a file storage record from the database by its ID
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FileStorages.FindAsync(id); // Find the entity by ID
            if (entity != null)
            {
                _context.FileStorages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
