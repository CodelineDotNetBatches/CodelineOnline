using AuthenticationManagement.DTOs;

namespace AuthenticationManagement.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
    }
}