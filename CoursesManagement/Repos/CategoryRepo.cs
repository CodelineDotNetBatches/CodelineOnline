using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Repository for handling Category entity database operations.
    /// Provides reusable query methods for relationships with Courses and Programs.
    /// </summary>
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        private readonly CoursesDbContext _ctx;

        public CategoryRepo(CoursesDbContext context) : base(context)
        {
            _ctx = context;
        }

        /// <summary>
        /// Gets a category including its related courses (1:M).
        /// </summary>
        public async Task<Category?> GetCategoryWithCoursesAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        /// <summary>
        /// Gets a category including its related programs (M:M).
        /// </summary>
        public async Task<Category?> GetCategoryWithProgramsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Programs)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        /// <summary>
        /// Gets a category including both programs and courses.
        /// </summary>
        public async Task<Category?> GetCategoryFullAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Programs)
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        /// <summary>
        /// Finds a category by name (case-insensitive).
        /// </summary>
        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbSet
                .Include(c => c.Programs)
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == name.ToLower());
        }
    }
}
