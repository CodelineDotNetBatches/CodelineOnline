using AuthenticationManagement.DTOs;

namespace AuthenticationManagement.Services
{
    public interface IRoleService
    {
        Task<RoleDto> CreateAsync(CreateRoleDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto?> GetByIdAsync(int id);
        Task<RoleDto?> UpdateAsync(int id, UpdateRoleDto dto);
    }
}