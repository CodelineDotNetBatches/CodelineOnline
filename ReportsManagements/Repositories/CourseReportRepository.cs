using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public class CourseReportRepository : ICourseReportRepository
    {
        private readonly ReportsDbContext _context;
        public CourseReportRepository(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseReport>> GetAllAsync() =>
            await _context.CourseReports.ToListAsync();


        public async Task<CourseReport?> GetByIdAsync(int id)
        {
            return await _context.CourseReports.FindAsync(id);
        }

        public async Task<CourseReport> AddAsync(CourseReport courseReport)
        {
            _context.CourseReports.Add(courseReport);
            await _context.SaveChangesAsync();
            return courseReport;
        }

        public async Task<CourseReport?> UpdateAsync(CourseReport courseReport)
        {
            _context.CourseReports.Update(courseReport);
            await _context.SaveChangesAsync();
            return courseReport;
        }

        public async Task DeleteAsync(int id)
        {
            var courseReport = await _context.CourseReports.FindAsync(id);
            if (courseReport != null)
            {
                _context.CourseReports.Remove(courseReport);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpsertBatchAsync(IEnumerable<CourseReport> items, CancellationToken ct = default)
        {
            foreach (var i in items)
            {
                var existing = await _context.CourseReports
                    .FirstOrDefaultAsync(x => x.CourseId == i.CourseId, ct);

                if (existing is null)
                    await _context.CourseReports.AddAsync(i, ct);
                else
                {
                    existing.TotalSessions = i.TotalSessions;
                    existing.TotalStudents = i.TotalStudents;
                    existing.AverageAttendanceRate = i.AverageAttendanceRate;
                    _context.CourseReports.Update(existing);
                }
            }
            await _context.SaveChangesAsync(ct);
        }

        public IQueryable<CourseReport> Query() => _context.CourseReports.AsNoTracking();


    }
}
