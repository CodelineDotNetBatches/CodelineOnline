using ReportsManagements.Models;

namespace ReportsManagements.Services
{
    public interface ICourseReportService
    {
        Task<CourseReport> CreateReportAsync(CourseReport report);
        Task DeleteReportByIdAsync(int id);
        Task<IEnumerable<CourseReport>> GetAllCourseReportsAsync();
        Task<CourseReport?> GetReportByIdAsync(int id);
        Task<CourseReport?> UpdateReportAsync(CourseReport report);
    }
}