using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        private readonly CoursesDbContext _context;

        public CategoryRepo(CoursesDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category?> GetCategoryFullAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Programs)
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category?> GetCategoryWithCoursesAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category?> GetCategoryWithProgramsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Programs)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbSet
                .Include(c => c.Programs)
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == name.ToLower());
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
