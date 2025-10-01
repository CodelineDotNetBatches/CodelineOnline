using CoursesManagement.Models;
using CoursesManagement.Repos;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<IEnumerable<Course>> GetCoursesByLevelAsync(LevelType level);
    Task<IEnumerable<Course>> GetCoursesByCategoryAsync(int categoryId);
    Task<Course?> GetCourseWithCategoryAsync(Guid courseId);
}
