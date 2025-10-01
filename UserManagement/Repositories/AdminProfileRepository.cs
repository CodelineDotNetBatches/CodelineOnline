using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public class AdminProfileRepository
    {
        private readonly UsersDbContext _context; // Database context
        private readonly IMemoryCache _memoryCache;  // In-Memory Cache
        private readonly IDistributedCache _distributedCache;  // Distributed Cache

        private const string CacheKey = "AdminProfiles"; // Key for caching


        public AdminProfileRepository(UsersDbContext context, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _context = context;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        //Get all AdminProfiles(sync) using IQueryable for extensibility
        public IQueryable<Admin_Profile> GetAllAdmins()
        {
            // Try to get data from Memory Cache
            if (!_memoryCache.TryGetValue(CacheKey, out List<AdminProfile> admins))
            {
            }

    }
}
