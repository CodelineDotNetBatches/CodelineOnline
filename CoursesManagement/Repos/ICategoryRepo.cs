using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface ICategoryRepository : IGenericRepo<Category>
    {
        Task<Category?> GetCategoryWithCoursesAsync(Guid id);
        Task SaveAsync();
    }

}
