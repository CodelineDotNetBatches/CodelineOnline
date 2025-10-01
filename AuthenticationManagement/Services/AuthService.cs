using AuthenticationManagement.DTOs;
using AuthenticationManagement.Models;
using AuthenticationManagement.Repositories;

namespace AuthenticationManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IRoleRepository _roles;
        private readonly ITokenService _token;

        public AuthService(IUserRepository users, IRoleRepository roles, ITokenService token)
        {
            _users = users;
            _roles = roles;
            _token = token;
        }
        // register user
        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            // unique email check
            var exists = await _users.GetByEmailAsync(dto.Email);
            if (exists != null) return null;

            // hash password
            var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = hashed,
                RoleID = dto.RoleID,
                Status = "active",
                CreatedAt = DateTime.UtcNow
            };

            await _users.AddAsync(user);

            // load role
            var saved = await _users.GetByIdAsync(user.UserID);
            var roleType = saved?.Role?.RoleType ?? "User";

            var (token, exp) = _token.CreateToken(saved!, roleType);

            return new AuthResponseDto
            {
                UserID = saved!.UserID,
                Email = saved.Email,
                FullName = $"{saved.FirstName} {saved.LastName}",
                Role = roleType,
                Token = token,
                ExpiresAtUtc = exp
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _users.GetByEmailAsync(dto.Email);
            if (user == null) return null;

            var ok = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!ok) return null;

            var roleType = user.Role?.RoleType ?? "User";
            var (token, exp) = _token.CreateToken(user, roleType);

            return new AuthResponseDto
            {
                UserID = user.UserID,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = roleType,
                Token = token,
                ExpiresAtUtc = exp
            };
        }
    }
}
