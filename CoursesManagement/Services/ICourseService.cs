using CoursesManagement.Models;
using CoursesManagement.DTOs;

namespace CoursesManagement.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(Guid id);
        Task<IEnumerable<Course>> GetCoursesByLevelAsync(LevelType level);
        Task<IEnumerable<Course>> GetCoursesByCategoryAsync(Guid categoryId);
        Task<Course?> GetCourseWithCategoryAsync(Guid id);

        Task<Course> AddCourseAsync(CourseCreateDto dto);
        Task<Course?> UpdateCourseAsync(CourseUpdateDto dto);
        Task DeleteCourseAsync(Guid id);
        Task<Course?> GetCourseWithEnrollmentListAsync(Guid courseId);
        Task<Course?> GetCourseByNameAsync(string courseName);


    }
}
