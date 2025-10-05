using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ReportsDbContext _context;
        public BranchRepository(ReportsDbContext context)
        {
            _context = context;
        }

        // Retrieves all Branch records from the database asynchronously
        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _context.Branches.Where(b => b.IsActive).ToListAsync();
        }
        // Get by id method
        public async Task<Branch?> GetByIdAsync(int id)
        {
            // Find the branch by id
            return await _context.Branches.FindAsync(id);
        }
        // Add method
        public async Task<Branch> AddAsync(Branch branch)
        {
            // Add the new branch to the DbSet
            _context.Branches.Add(branch);
            // Save changes to the database
            await _context.SaveChangesAsync();
            return branch;
        }

        // Update method
        public async Task<Branch?> UpdateAsync(Branch branch)
        {
            // Check if the branch exists
            _context.Branches.Update(branch);
            // Save changes to the database
            await _context.SaveChangesAsync();
            return branch;
        }
        // Soft delete implementation
        public async Task DeleteAsync(int id)
        {
            // Find the branch by id
            var branch = await _context.Branches.FindAsync(id);
            // If found,remove it from the DbSet
            if (branch != null)
            {
                // For soft delete,we set IsActive to false instead of removing the record
                branch.IsActive = false;
                await _context.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<Geolocation>> GetBranchGeolocationsAsync(int branchId)
        {
            var branch = await _context.Branches
                .Include(b => b.Geolocations)
                .FirstOrDefaultAsync(b => b.BranchId == branchId);

            return branch?.Geolocations ?? new List<Geolocation>();
        }
        public async Task<int> GetBranchesCountAsync()
        {
            return await _context.Branches.CountAsync();
        }

        public async Task<int> GetActiveBranchesCountAsync()
        {
            return await _context.Branches.CountAsync(b => b.IsActive);
        }

        public Task<int> GetBranchCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetActiveBranchCountAsync()
        {
            throw new NotImplementedException();
        }
    }


}
