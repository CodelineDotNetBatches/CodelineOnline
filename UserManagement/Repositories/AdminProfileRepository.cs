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

        // -------------------
        // SYNC METHODS
        // -------------------

        //Get all AdminProfiles(sync) using IQueryable for extensibility
        public IQueryable<Admin_Profile> GetAllAdmins()
        {
            // Try to get data from Memory Cache
            if (!_memoryCache.TryGetValue(CacheKey, out List<Admin_Profile> admins))
            {
                admins = _context.AdminProfiles.Tolist(); // Load from DB (sync)

                _memoryCache.Set(CacheKey, admins); // Save to Memory Cache
            }

            return admins.AsQueryable(); // Return as IQueryable
        }


        //Get AdminProfile by Id (sync)
        public Admin_Profile GetAdminById(int id)
        {
            return _context.AdminProfiles.FirstOrDefault(a => a.AdminId == id);
        }


        // Add new AdminProfile (sync) 

        public void AddAdmin(Admin_Profile admin)
        {
            _context.AdminProfiles.Add(admin);
            _context.SaveChanges();
            _memoryCache.Remove(CacheKey); // Clear cache after insert
        }


        //-------------------
        // ASYNC METHODS
        //-------------------
        // Get all AdminProfiles (async) with Distributed Caching 

        public async Task<IEnumerable<Admin_Profile>> GetAllAdminsAsync()
        {
            // Try Distributed Cache first
            var cachedAdmins = await _distributedCache.GetStringAsync(CacheKey);

            if (!string.IsNullOrEmpty(cachedAdmins))
            {
                return JsonConvert.DeserializeObject<List<Admin_Profile>>(cachedAdmins);
            }

            // If not cached, load from DB
            var admins = await _context.AdminProfiles.ToListAsync();

            //Save in distributed cache 

            var serializedData = JsonConvert.SerializeObject(admins);
            await _distributedCache.SetStringAsync(CacheKey, serializedData);

            return admins;
        }


        // Get AdminProfile by Id (async)
        public async Task<Admin_Profile> GetAdminByIdAsync(int id)
        {
            return await _context.AdminProfiles.FirstOrDefaultAsync(a => a.AdminId == id);
        }
    }
}
