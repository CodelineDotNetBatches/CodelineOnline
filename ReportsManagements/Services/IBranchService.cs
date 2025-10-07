using ReportsManagements.Models;

namespace ReportsManagements.Services
{
    public interface IBranchService
    {
        Task<Branch> CreateBranchAsync(string name,string address);
        Task DeleteBranchAsync(int id);
        Task<IEnumerable<Branch>> GetAllBranchesAsync();
        Task<Branch?> GetBranchByIdAsync(int id);
        Task<IEnumerable<Geolocation>> GetBranchGeolocationsAsync(int branchId);
        Task<Branch?> UpdateBranchAsync(int id, Branch branch);
    }
}