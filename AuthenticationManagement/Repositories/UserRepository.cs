using AuthenticationManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationManagement.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AuthenticationDbContext _context;

        public UserRepository(AuthenticationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)  // optional: include role info
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
