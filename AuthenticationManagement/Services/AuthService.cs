using AuthenticationManagement.DTOs;
using AuthenticationManagement.Models;
using AuthenticationManagement.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            // ✅ 1. Ensure unique email
            var exists = await _users.GetByEmailAsync(dto.Email);
            if (exists != null) return null;

            // ✅ 2. Find or create role
            string roleName = dto.RoleName?.Trim() ?? "User";
            var role = await _roles.Query()
                .FirstOrDefaultAsync(r => r.RoleName == roleName);

            if (role == null)
            {
                role = new Role
                {
                    RoleName = roleName,
                    Description = $"Auto-created role: {roleName}"
                };
                await _roles.AddAsync(role);
            }

            // ✅ 3. Hash password
            var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // ✅ 4. Create user
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = hashed,
                RoleID = role.RoleID,
                Status = "active",
                CreatedAt = DateTime.UtcNow
            };

            await _users.AddAsync(user);

            // ✅ 5. Load saved user + role and create token
            var saved = await _users.GetByIdAsync(user.UserID);
            var roleNameFinal = saved?.Role?.RoleName ?? "User";

            var (token, exp) = _token.CreateToken(saved!, roleNameFinal);

            return new AuthResponseDto
            {
                UserID = saved!.UserID,
                Email = saved.Email,
                FullName = $"{saved.FirstName} {saved.LastName}",
                Role = roleNameFinal,
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

            var roleName = user.Role?.RoleName ?? "User";
            var (token, exp) = _token.CreateToken(user, roleName);

            return new AuthResponseDto
            {
                UserID = user.UserID,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = roleName,
                Token = token,
                ExpiresAtUtc = exp
            };
        }
    }
}
