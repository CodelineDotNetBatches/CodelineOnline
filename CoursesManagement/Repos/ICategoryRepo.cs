using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithCoursesAsync(Guid id);
    }

}
