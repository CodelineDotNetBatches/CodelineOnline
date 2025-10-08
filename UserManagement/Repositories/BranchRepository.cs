using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        private readonly UsersDbContext _db;

        public BranchRepository(UsersDbContext db) : base(db)
        {
            _db = db;
        }

       
        public IQueryable<Branch> Query(bool includeNavs = false)
        {
            IQueryable<Branch> q = _db.branchs.AsQueryable();

            if (includeNavs)
            {
                q = q
                    .Include(b => b.branchPNs)
                    .Include(b => b.rooms)
                    .Include(b => b.Admin_Profiles)
                    .Include(b => b.batches)
                    .Include(b => b.instructors)
                    .Include(b => b.trainees);
            }

            return q;
        }

       
        public async Task<Branch?> GetByIdWithNavsAsync(int id)
        {
            return await _db.branchs
                .Include(b => b.branchPNs)
                .Include(b => b.rooms)
                .Include(b => b.Admin_Profiles)
                .Include(b => b.batches)
                .Include(b => b.instructors)
                .Include(b => b.trainees)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BranchId == id);
        }

       
        public async Task<bool> BranchNameExistsAsync(string branchName, int? excludeId = null)
        {
            var q = _db.branchs.AsQueryable();

            if (excludeId.HasValue)
                q = q.Where(b => b.BranchId != excludeId.Value);

            return await q.AnyAsync(b => b.BranchName == branchName);
        }

       
        public async Task AddPhoneAsync(int branchId, int phoneNumber)
        {
            // Optional: prevent duplicates
            bool exists = await _db.Set<BranchPN>()
                .AnyAsync(p => p.BranchId == branchId && p.PhoneNumber == phoneNumber);

            if (!exists)
            {
                await _db.Set<BranchPN>().AddAsync(new BranchPN
                {
                    BranchId = branchId,
                    PhoneNumber = phoneNumber
                });

                await _db.SaveChangesAsync();
            }
        }

        
        public async Task RemovePhoneAsync(int branchId, int phoneNumber)
        {
            var pn = await _db.Set<BranchPN>()
                .FirstOrDefaultAsync(p => p.BranchId == branchId && p.PhoneNumber == phoneNumber);

            if (pn != null)
            {
                _db.Remove(pn);
                await _db.SaveChangesAsync();
            }
        }

        
    }
}
