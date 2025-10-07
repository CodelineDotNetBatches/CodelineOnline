using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch> AddAsync(Branch branch);
        Task DeleteAsync(int id);
        Task<int> GetActiveBranchCountAsync();
        Task<int> GetActiveBranchesCountAsync();
        Task<IEnumerable<Branch>> GetAllAsync();
        Task<int> GetBranchCountAsync();
        Task<int> GetBranchesCountAsync();
        Task<IEnumerable<Geolocation>> GetBranchGeolocationsAsync(int branchId);
        Task<Branch?> GetByIdAsync(int id);
        Task<Branch?> UpdateAsync(Branch branch);
    }
}