using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Repository for managing Enrollment entities.
    /// Provides common CRUD operations and enrollment-specific queries.
    /// </summary>
    public class EnrollmentRepository : GenericRepo<Enrollment>, IEnrollmentRepository
    {
        private readonly CoursesDbContext _context;

        public EnrollmentRepository(CoursesDbContext context) : base(context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Enrollment?> GetByUserAndCourseAsync(Guid userId, Guid courseId)
        {
            return await _context.Enrollments
                .Include(e => e.User)   // Ensure User navigation is loaded
                .Include(e => e.Course) // Ensure Course navigation is loaded
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Enrollment>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId)
        {
            return await _context.Enrollments
                .Include(e => e.User)
                .Where(e => e.CourseId == courseId)
                .ToListAsync();
        }
    }
}
