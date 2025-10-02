using AuthenticationManagement.DTOs;

namespace AuthenticationManagement.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto);
    }
}