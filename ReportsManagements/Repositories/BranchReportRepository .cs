using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public class BranchReportRepository : IBranchReportRepository
    {
        private readonly ReportsDbContext _context;
        public BranchReportRepository(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task<BranchReport> AddAsync(BranchReport report)
        {
            await _context.BranchReports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task DeleteAsync(int id)
        {
            var report = await _context.BranchReports.FindAsync(id);
            if (report != null)
            {
                _context.BranchReports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BranchReport>> GetAllAsync() =>
            await _context.BranchReports.ToListAsync();

        public async Task<BranchReport?> GetByIdAsync(int id) =>
            await _context.BranchReports.FindAsync(id);

        public async Task<BranchReport?> UpdateAsync(BranchReport report)
        {
            _context.BranchReports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<BranchReport>> GetReportsByBranchIdAsync(int branchId) =>
            await _context.BranchReports
                .Where(r => r.BranchId == branchId)
                .ToListAsync();
    }
}


    

