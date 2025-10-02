using CoursesManagement.Models;

public interface ICourseRepo
{
    Task<IQueryable<Course>> GetCoursesByCategoryAsync(int categoryId);
    Task<IQueryable<Course>> GetCoursesByLevelAsync(LevelType level);
    Task<Course?> GetCourseWithCategoryAsync(Guid courseId);
}