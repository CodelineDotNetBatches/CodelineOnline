using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Repository contract for Category entity.
    /// Provides specialized data-access methods for Category relationships.
    /// </summary>
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        /// <summary>
        /// Retrieves a category and its related courses.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>Category with its related courses.</returns>
        Task<Category?> GetCategoryWithCoursesAsync(Guid id);

        Task<Category?> GetCategoryWithProgramsAsync(Guid id);


        /// <summary>
        /// Retrieves a category with all related data (Programs + Courses).
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>Category entity with all relationships included.</returns>
        Task<Category?> GetCategoryFullAsync(Guid id);

        /// <summary>
        /// Retrieves a category by name (case-insensitive).
        /// </summary>
        /// <param name="name">The category name.</param>
        /// <returns>Category entity if found, otherwise null.</returns>
        Task<Category?> GetByNameAsync(string name);

    }
}
