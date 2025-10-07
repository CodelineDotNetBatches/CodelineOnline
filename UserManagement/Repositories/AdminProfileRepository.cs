using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public class AdminProfileRepository : IAdminProfileRepository
    {
        private readonly UsersDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        private const string CacheKey = "AdminProfiles";

        public AdminProfileRepository(UsersDbContext context, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _context = context;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        // ======================================================
        // CREATE
        // ======================================================

        public void AddAdminProfile(Admin_Profile admin)
        {
            _context.AdminProfiles.Add(admin);
            _context.SaveChanges();
        }

        public async Task AddAdminProfileAsync(Admin_Profile admin)
        {
            await _context.AdminProfiles.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public void AddResponsibility(Responsibility responsibility)
        {
            _context.Responsibilities.Add(responsibility);
            _context.SaveChanges();
        }

        public async Task AddResponsibilityAsync(Responsibility responsibility)
        {
            await _context.Responsibilities.AddAsync(responsibility);
            await _context.SaveChangesAsync();
        }

        // ======================================================
        // READ
        // ======================================================

        public IQueryable<Admin_Profile> GetAllAdmins()
        {
            return _context.AdminProfiles.AsQueryable();
        }

        public async Task<IQueryable<Admin_Profile>> GetAllAdminsAsync()
        {
            var admins = await _context.AdminProfiles.ToListAsync();
            return admins.AsQueryable();
        }

        public Admin_Profile GetAdminById(int id)
        {
            return _context.AdminProfiles.FirstOrDefault(a => a.AdminId == id);
        }

        public async Task<Admin_Profile> GetAdminByIdAsync(int id)
        {
            return await _context.AdminProfiles.FirstOrDefaultAsync(a => a.AdminId == id);
        }

        // ======================================================
        // UPDATE
        // ======================================================

        public void UpdateAdminProfile(Admin_Profile admin)
        {
            _context.AdminProfiles.Update(admin);
            _context.SaveChanges();
        }

        public async Task UpdateAdminProfileAsync(Admin_Profile admin)
        {
            _context.AdminProfiles.Update(admin);
            await _context.SaveChangesAsync();
        }

        public void UpdateResponsibility(Responsibility responsibility)
        {
            _context.Responsibilities.Update(responsibility);
            _context.SaveChanges();
        }

        public async Task UpdateResponsibilityAsync(Responsibility responsibility)
        {
            _context.Responsibilities.Update(responsibility);
            await _context.SaveChangesAsync();
        }

        // ======================================================
        // DELETE
        // ======================================================

        public void DeleteAdminProfile(Admin_Profile admin)
        {
            _context.AdminProfiles.Remove(admin);
            _context.SaveChanges();
        }

        public async Task DeleteAdminProfileAsync(Admin_Profile admin)
        {
            _context.AdminProfiles.Remove(admin);
            await _context.SaveChangesAsync();
        }
    }
}
