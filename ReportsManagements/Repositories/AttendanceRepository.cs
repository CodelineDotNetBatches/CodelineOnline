using ReportsManagements.Models;
using Microsoft.EntityFrameworkCore;


namespace ReportsManagements.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ReportsDbContext _context;

        public AttendanceRepository(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task<AttendanceRecord?> GetByIdAsync(int id) =>
         await _context.Set<AttendanceRecord>().FindAsync(id);

        public async Task<IEnumerable<AttendanceRecord>> GetByStudentAndSessionAsync(int studentId, int sessionId) =>
        await _context.Set<AttendanceRecord>()
            .Where(a => a.StudentId == studentId && a.SessionId == sessionId)
            .ToListAsync();


        public async Task AddAsync(AttendanceRecord record)
        {
            await _context.Set<AttendanceRecord>().AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AttendanceRecord record)
        {
            _context.Set<AttendanceRecord>().Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.Set<AttendanceRecord>().FindAsync(id);
            if (record != null)
            {
                _context.Set<AttendanceRecord>().Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}
