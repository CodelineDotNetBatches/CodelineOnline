using CoursesManagement.Models;
using CoursesManagement.Repos;

public interface ICourseRepo : IGenericRepo<Course>
{
    Task<IQueryable<Course>> GetCoursesByCategoryAsync(int categoryId);
    Task<IQueryable<Course>> GetCoursesByLevelAsync(LevelType level);
    Task<Course?> GetCourseWithCategoryAsync(Guid courseId);
}