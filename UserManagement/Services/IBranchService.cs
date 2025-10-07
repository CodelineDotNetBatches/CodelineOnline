using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDTO>> GetAllAsync();
        Task<BranchDTO?> GetByIdAsync(int id);
        Task AddAsync(BranchDTO dto);
        Task UpdateAsync(int id, BranchDTO dto);
        Task DeleteAsync(int id);
    }
}
