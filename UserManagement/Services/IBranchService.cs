using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface IBranchService
    {
        Task AddAsync(BranchDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<BranchDTO>> GetAllAsync();
        Task<IEnumerable<BranchDTO>> GetBranchesWithMoreThanThreeBatchesAsync();
        Task<BranchDTO?> GetByIdAsync(int id);
        Task UpdateAsync(int id, BranchDTO dto);
    }
}