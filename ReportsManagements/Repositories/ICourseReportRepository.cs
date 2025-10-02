using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public interface ICourseReportRepository
    {
        Task<CourseReport> AddAsync(CourseReport courseReport);
        Task DeleteAsync(int id);
        Task<IEnumerable<CourseReport>> GetAllAsync();
        Task<CourseReport?> GetByIdAsync(int id);
        Task<CourseReport?> UpdateAsync(CourseReport courseReport);

        Task UpsertBatchAsync(IEnumerable<CourseReport> items, CancellationToken ct = default);
        IQueryable<CourseReport> Query();
    }
}