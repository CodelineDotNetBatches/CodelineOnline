using CoursesManagement.Models;

namespace CoursesManagement.Repos
    {
        public interface ICategoryRepo : IGenericRepo<Category>
        {
            // Get full details (Programs + Courses)
            Task<Category?> GetCategoryFullAsync(Guid id);

            // Get category with only Courses
            Task<Category?> GetCategoryWithCoursesAsync(Guid id);

            // Get category with only Programs
            Task<Category?> GetCategoryWithProgramsAsync(Guid id);

            // Get category by Name
            Task<Category?> GetByNameAsync(string name);
        }
    }

