using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    public class CategoryRepository : GenericRepo<Category>, ICategoryRepository
    {
        protected readonly CoursesDbContext _ctx;

        public CategoryRepository(CoursesDbContext context) : base(context)
        {
            _ctx = context;
        }

        public async Task<Category?> GetCategoryWithCoursesAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
