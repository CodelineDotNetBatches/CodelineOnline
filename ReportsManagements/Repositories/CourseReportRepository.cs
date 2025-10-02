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



    }
}
