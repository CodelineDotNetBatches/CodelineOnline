using AuthenticationManagement.Models;

namespace AuthenticationManagement.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuthenticationDbContext context) : base(context)
        {
        }
    }
}
