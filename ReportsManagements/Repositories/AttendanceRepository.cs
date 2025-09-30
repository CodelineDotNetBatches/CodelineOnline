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

        // Get all records as IQueryable (make esay in server side filtering, paging etc)
        public IQueryable<AttendanceRecord> GetQueryable() => _context.AttendanceRecord.AsQueryable();
        public async Task<AttendanceRecord?> GetByIdAsync(int id) =>
          await _context.AttendanceRecord
              .Include(a => a.Geolocation)
              .Include(a => a.CapturedPhoto)
              .Include(a => a.ReasonCode)
              .FirstOrDefaultAsync(a => a.AttId == id);
        public async Task<IEnumerable<AttendanceRecord>> GetByStudentAndSessionAsync(int studentId, int sessionId) =>
           await _context.AttendanceRecord
               .Include(a => a.Geolocation)
               .Include(a => a.CapturedPhoto)
               .Include(a => a.ReasonCode)
               .Where(a => a.StudentId == studentId && a.SessionId == sessionId)
               .ToListAsync();


        // Add
        public async Task AddAsync(AttendanceRecord record)
        {
            await _context.AttendanceRecord.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        // Update
        public async Task UpdateAsync(AttendanceRecord record)
        {
            _context.AttendanceRecord.Update(record);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            var record = await _context.AttendanceRecord.FindAsync(id);
            if (record != null)
            {
                _context.AttendanceRecord.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}
