using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        Task<Category?> GetCategoryWithCoursesAsync(Guid id);
        Task SaveAsync();
    }

}
