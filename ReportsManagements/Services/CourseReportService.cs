using ReportsManagements.Models;
using ReportsManagements.Repositories;

namespace ReportsManagements.Services
{
    public class CourseReportService : ICourseReportService
    {
        private readonly ICourseReportRepository _courseReportRepository;

        public CourseReportService(ICourseReportRepository courseReportRepository)
        {
            _courseReportRepository = courseReportRepository;
        }

        public Task<IEnumerable<CourseReport>> GetAllCourseReportsAsync()
        {
            return _courseReportRepository.GetAllAsync();
        }

        public Task<CourseReport?> GetReportByIdAsync(int id)
        {
            return _courseReportRepository.GetByIdAsync(id);
        }

        public Task<CourseReport> CreateReportAsync(CourseReport report)
        {
            return _courseReportRepository.AddAsync(report);
        }

        public Task<CourseReport?> UpdateReportAsync(CourseReport report)
        {
            return _courseReportRepository.UpdateAsync(report);
        }

        public Task DeleteReportByIdAsync(int id)
        {
            return _courseReportRepository.DeleteAsync(id);
        }
    }
}
