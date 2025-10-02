using AuthenticationManagement.Models;

namespace AuthenticationManagement.Repositories
{
    public interface IUserRepository : IGenericRepository<User> {
        Task<User?> GetByEmailAsync(string email);
    }
}
