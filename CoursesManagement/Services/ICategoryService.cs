using CoursesManagement.DTOs;

namespace CoursesManagement.Services
{
    /// <summary>
    /// Contract for Category business logic services.
    /// Defines all methods for managing categories, including
    /// related Courses and Programs.
    /// </summary>
    public interface ICategoryService
    {
        // =========================================================
        // Core CRUD Operations
        // =========================================================

        /// <summary>
        /// Retrieves all categories, including their programs and courses.
        /// </summary>
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();

        /// <summary>
        /// Retrieves a specific category by its unique identifier,
        /// including all related programs and courses.
        /// </summary>
        Task<CategoryDetailDto?> GetCategoryByIdAsync(Guid id);

        /// <summary>
        /// Creates a new category and links it to the specified programs (if provided).
        /// </summary>
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);

        /// <summary>
        /// Updates an existing category and re-assigns its programs if provided.
        /// </summary>
        Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);

        /// <summary>
        /// Deletes a category if it has no related courses.
        /// </summary>
        Task DeleteCategoryAsync(Guid id);

        /// <summary>
        /// Retrieves a category by its name (case-insensitive).
        /// Includes both programs and courses.
        /// </summary>
        Task<CategoryDetailDto?> GetCategoryByNameAsync(string name);

        /// <summary>
        /// Retrieves all courses that belong to a specific category.
        /// </summary>
        Task<IEnumerable<CourseListDto>> GetCoursesByCategoryAsync(Guid categoryId);

        /// <summary>
        /// Retrieves all programs linked to a specific category.
        /// </summary>
        Task<IEnumerable<ProgramDetailsDto>> GetProgramsByCategoryAsync(Guid categoryId);
    }
}
