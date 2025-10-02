using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using UserManagement.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;



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
                admins = _context.AdminProfiles.ToList(); // Load from DB (sync)

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

        public void AddAdminProfile(Admin_Profile admin)
        {
            _context.AdminProfiles.Add(admin);
            _context.SaveChanges();
            _memoryCache.Remove(CacheKey); // Clear cache after insert
        }


        //Add a new  Responsibility (sync)
        public void AddResponsibility(Responsibility responsibility)
        {
            _context.Responsibilities.Add(responsibility);   // Add new record
            _context.SaveChanges();                          // Save to DB
        }

        //-------------------
        // ASYNC METHODS
        //-------------------
        // Get all AdminProfiles (async) with Distributed Caching 

        public async Task<IQueryable<Admin_Profile>> GetAllAdminsAsync()
        {
            // Try Distributed Cache first
            var cachedAdmins = await _distributedCache.GetStringAsync(CacheKey);

            List<Admin_Profile> admins;

            if (!string.IsNullOrEmpty(cachedAdmins))
            {
                admins = JsonSerializer.Deserialize<List<Admin_Profile>>(cachedAdmins);
            }
            else
            {
                // If not cached, load from DB
                admins = await _context.AdminProfiles.ToListAsync();

                // Save in distributed cache 
                var serializedData = JsonSerializer.Serialize(admins);
                await _distributedCache.SetStringAsync(CacheKey, serializedData);
            }

            // Convert List to IQueryable before returning
            return admins.AsQueryable();
        }



        // Get AdminProfile by Id (async)
        public async Task<Admin_Profile> GetAdminByIdAsync(int id)
        {
            return await _context.AdminProfiles.FirstOrDefaultAsync(a => a.AdminId == id);
        }

        // Add new AdminProfile (async)
        public async Task AddAdminProfileAsync(Admin_Profile admin)
        {
            await _context.AdminProfiles.AddAsync(admin);
            await _context.SaveChangesAsync();

            // Clear distributed cache
            await _distributedCache.RemoveAsync(CacheKey);
        }

    }

}
