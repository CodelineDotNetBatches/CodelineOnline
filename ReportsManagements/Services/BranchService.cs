using ReportsManagements.Models;
using ReportsManagements.Repositories;

namespace ReportsManagements.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repo;
        public BranchService(IBranchRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Branch>> GetAllBranchesAsync() =>
           await _repo.GetAllAsync();

        public async Task<Branch> CreateBranchAsync(string name)
        {
            var branch = new Branch { Name = name, IsActive = true };
            return await _repo.AddAsync(branch);
        }

        public async Task<Branch?> UpdateBranchAsync(int id, Branch branch)
        {
            if (id != branch.BranchId) return null;
            return await _repo.UpdateAsync(branch);
        }

        public async Task DeleteBranchAsync(int id) =>
            await _repo.DeleteAsync(id);

        public async Task<IEnumerable<Geolocation>> GetBranchGeolocationsAsync(int branchId) =>
            await _repo.GetBranchGeolocationsAsync(branchId);

        public async Task<Branch?> GetBranchByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

    }
}



