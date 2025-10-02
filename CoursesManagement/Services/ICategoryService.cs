using CoursesManagement.DTOs;

namespace CoursesManagement.Services
{
    /// <summary>
    /// Contract for Category business logic services.
    /// </summary>
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDetailDto?> GetCategoryByIdAsync(Guid id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);
        Task DeleteCategoryAsync(Guid id);
    }
}
