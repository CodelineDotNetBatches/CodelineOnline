using Microsoft.EntityFrameworkCore;
using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Concrete repository implementation for managing enrollments.
    /// Provides specialized queries in addition to generic CRUD.
    /// </summary>
    public class EnrollmentRepo : GenericRepo<Enrollment>, IEnrollmentRepo
    {
        public EnrollmentRepo(CoursesDbContext context) : base(context) { }

        /// <inheritdoc />
        public async Task<IEnumerable<Enrollment>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .Include(e => e.Program)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .Where(e => e.CourseId == courseId)
                .Include(e => e.User)
                .Include(e => e.Program)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Enrollment?> GetByUserAndCourseAsync(Guid userId, Guid courseId)
        {
            return await _dbSet
                .Include(e => e.User)
                .Include(e => e.Course)
                .Include(e => e.Program)
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
        }
    }
}
