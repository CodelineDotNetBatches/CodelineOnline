using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CoursesDbContext context) : base(context) { }

        public async Task<Category?> GetCategoryWithCoursesAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }
    }
}
