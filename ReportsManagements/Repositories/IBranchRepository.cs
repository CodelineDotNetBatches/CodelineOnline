using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch> AddAsync(Branch branch);
        Task DeleteAsync(int id);
        Task<IEnumerable<Branch>> GetAllAsync();
        Task<Branch?> GetByIdAsync(int id);
        Task<Branch?> UpdateAsync(Branch branch);
        Task<IEnumerable<Geolocation>> GetBranchGeolocationsAsync(int branchId);
    }
}