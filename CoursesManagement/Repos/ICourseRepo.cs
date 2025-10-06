using CoursesManagement.Models;
using CoursesManagement.Repos;

public interface ICourseRepo : IGenericRepo<Course>
{
    Task<IQueryable<Course>> GetCoursesByCategoryAsync(Guid categoryId);
    Task<IQueryable<Course>> GetCoursesByLevelAsync(LevelType level);
    Task<Course?> GetCourseWithCategoryAsync(Guid courseId);
}