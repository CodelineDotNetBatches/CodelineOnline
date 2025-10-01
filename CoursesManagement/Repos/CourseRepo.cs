using CoursesManagement.Models;
using CoursesManagement.Repos;
using CoursesManagement;
using Microsoft.EntityFrameworkCore;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    private readonly CoursesDbContext _context;

    public CourseRepository(CoursesDbContext context) : base(context)
    {
        _context = context;
    }

    // Course-specific queries:
    public async Task<IEnumerable<Course>> GetCoursesByLevelAsync(LevelType level)
    {
        return await _context.Courses
            .Where(c => c.CourseLevel == level)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesByCategoryAsync(int categoryId)
    {
        return await _context.Courses
            .Where(c => c.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<Course?> GetCourseWithCategoryAsync(Guid courseId)
    {
        return await _context.Courses
            .Include(c => c.Category)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);
    }
}
