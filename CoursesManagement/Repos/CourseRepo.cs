using CoursesManagement.Models;
using CoursesManagement.Repos;
using CoursesManagement;
using Microsoft.EntityFrameworkCore;

public class CourseRepo : GenericRepo<Course>, ICourseRepo
{
    private readonly CoursesDbContext _context;

    public CourseRepo(CoursesDbContext context) : base(context)
    {
        _context = context;
    }

    // Course-specific queries:
    public async Task<IQueryable<Course>> GetCoursesByLevelAsync(LevelType level)
    {
        return _context.Courses
            .Where(c => c.CourseLevel == level);
    }

    public async Task<IQueryable<Course>> GetCoursesByCategoryAsync(int categoryId)
    {
        return _context.Courses
            .Where(c => c.CategoryId == categoryId);
    }

    public async Task<Course?> GetCourseWithCategoryAsync(Guid courseId)
    {
        return _context.Courses
            .Include(c => c.Category)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);
    }
}
