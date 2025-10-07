using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public interface IBranchReportRepository
    {
        Task<BranchReport> AddAsync(BranchReport report);
        Task DeleteAsync(int id);
        Task<IEnumerable<BranchReport>> GetAllAsync();
        Task<BranchReport?> GetByIdAsync(int id);
        Task<IEnumerable<BranchReport>> GetReportsByBranchIdAsync(int branchId);
        Task<BranchReport?> UpdateAsync(BranchReport report);
    }
}