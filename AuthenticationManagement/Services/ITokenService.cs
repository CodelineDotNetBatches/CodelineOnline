using AuthenticationManagement.Models;

namespace AuthenticationManagement.Services
{
    public interface ITokenService
    {
        (string token, DateTime expiresUtc) CreateToken(User user, string roleType);
    }
}