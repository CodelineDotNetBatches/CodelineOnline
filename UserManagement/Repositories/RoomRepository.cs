using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly UsersDbContext _context;
        public RoomRepository(UsersDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Room> Query()
        {
            return _context.rooms.AsQueryable();
        }
        public async Task<Room?> GetByNumberAsync(string roomNumber, bool includeBranch = false)
        {
            IQueryable<Room> query = _context.rooms;
            if (includeBranch)
            {
                query = query.Include(r => r.branchs);
            }
            return await query.FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
        }
        public async Task<List<Room>> GetByBranchAsync(int branchId, bool includeBranch = false)
        {
            IQueryable<Room> query = _context.rooms.Where(r => r.BranchId == branchId);
            if (includeBranch)
            {
                query = query.Include(r => r.branchs);
            }
            return await query.ToListAsync();
        }
        public async Task<bool> ExistsAsync(string roomNumber)
        {
            return await _context.rooms.AnyAsync(r => r.RoomNumber == roomNumber);
        }
    }
        
}
