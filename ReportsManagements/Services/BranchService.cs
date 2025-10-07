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

        public async Task<Branch> CreateBranchAsync(string name, string address)
        {
            var branch = new Branch
            {
                Name = name,
                Address = address,
                IsActive = true
            };

            await _repo.AddAsync(branch);
            return branch;
        }


        public async Task<Branch?> UpdateBranchAsync(int id, Branch branch)
        {
            var existingBranch = await _repo.GetByIdAsync(id);
            if (existingBranch == null || existingBranch.BranchId != id)
                return null;

            existingBranch.Name = branch.Name;
            existingBranch.Address = branch.Address;
            existingBranch.IsActive = branch.IsActive;

            return await _repo.UpdateAsync(existingBranch);
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



