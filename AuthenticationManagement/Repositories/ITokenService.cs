using AuthenticationManagement.Models;

namespace AuthenticationManagement.Repositories
{
    public interface ITokenService
    {
        (string token, DateTime expiresUtc) CreateToken(User user, string roleType);
    }
}