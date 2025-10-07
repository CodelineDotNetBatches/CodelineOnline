using CoursesManagement.DTOs;

namespace CoursesManagement.Services
{
    /// <summary>
    /// Business service interface for Category operations.
    /// Handles CRUD operations and category relationships with Programs and Courses.
    /// </summary>
    public interface ICategoryService
    {
        // =========================================================
        // GET ALL
        // =========================================================
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();

        // =========================================================
        // GET CATEGORY (By Id OR Name)
        // =========================================================
        Task<CategoryDetailDto?> GetCategoryAsync(Guid? id = null, string? name = null);

        // =========================================================
        // GET COURSES BY CATEGORY (By Id OR Name)
        // =========================================================
        Task<IEnumerable<CourseListDto>> GetCoursesByCategoryAsync(Guid? id = null, string? name = null);

        // =========================================================
        // GET PROGRAMS BY CATEGORY (By Id OR Name)
        // =========================================================
        Task<IEnumerable<ProgramDetailsDto>> GetProgramsByCategoryAsync(Guid? id = null, string? name = null);

        // =========================================================
        // CREATE
        // =========================================================
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);

        // =========================================================
        // UPDATE
        // =========================================================
        Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);

        // =========================================================
        // DELETE
        // =========================================================
        Task DeleteCategoryAsync(Guid id);
    }
}
