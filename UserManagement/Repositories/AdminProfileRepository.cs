using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public class AdminProfileRepository
    {
        private readonly UsersDbContext _context; // Database context
        private readonly IMemoryCache _memoryCache;  // In-Memory Cache
        private readonly IDistributedCache _distributedCache;  // Distributed Cache

        private const string CacheKey = "AdminProfiles"; // Key for caching

        public AdminProfileRepository(UsersDbContext context) // Constructor injection of DbContext
        {
            _context = context;
        }


        public IEnumerable<AdminProfile> GetAll()
        {
            return _context.AdminProfiles
                           .Include(a => a.Responsibilities) // Eager load Responsibilities
                           .ToList();
        }
    }
}
