using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ReportsDbContext _context;

        public AttendanceRepository(ReportsDbContext context)
        {
            _context = context;
        }

        // Expose queryable (useful for server-side filtering/paging)
        public IQueryable<AttendanceRecord> GetQueryable()
            => _context.AttendanceRecord.AsQueryable();

        // Get by Id (with includes)
        public async Task<AttendanceRecord?> GetByIdAsync(int id) =>
            await _context.AttendanceRecord
                .Include(a => a.Geolocation)
                .Include(a => a.CapturedPhoto)
                .Include(a => a.ReasonCode)
                .FirstOrDefaultAsync(a => a.AttId == id);

        // Get by student & session
        public async Task<IEnumerable<AttendanceRecord>> GetByStudentAndSessionAsync(int studentId, int sessionId) =>
            await _context.AttendanceRecord
                .Include(a => a.Geolocation)
                .Include(a => a.CapturedPhoto)
                .Include(a => a.ReasonCode)
                .Where(a => a.StudentId == studentId && a.SessionId == sessionId)
                .ToListAsync();

        // Filter by student, session, date range, review status
        public async Task<IEnumerable<AttendanceRecord>> GetFilteredAsync(
            int? studentId, int? sessionId, DateTime? fromDate, DateTime? toDate, string? reviewStatus)
        {
            var query = _context.AttendanceRecord.AsQueryable();

            if (studentId.HasValue) query = query.Where(a => a.StudentId == studentId.Value);
            if (sessionId.HasValue) query = query.Where(a => a.SessionId == sessionId.Value);
            if (fromDate.HasValue) query = query.Where(a => a.CheckIn >= fromDate.Value);
            if (toDate.HasValue) query = query.Where(a => a.CheckIn <= toDate.Value);
            if (!string.IsNullOrWhiteSpace(reviewStatus)) query = query.Where(a => a.ReviewStatus == reviewStatus);

            return await query
                .Include(a => a.Geolocation)
                .Include(a => a.CapturedPhoto)
                .Include(a => a.ReasonCode)
                .AsNoTracking()
                .ToListAsync();
        }

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
