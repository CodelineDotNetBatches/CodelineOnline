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

        // ===============================
        // GET BY USER ID
        // ===============================
        public async Task<IEnumerable<Enrollment>> GetByUserIdAsync(Guid userId)
        {
            // Note: User module not yet merged, so UserId exists only later
            return await _dbSet
                //.Where(e => e.UserId == userId) // uncomment after merge
                .Include(e => e.Course)
                .Include(e => e.Program)
                .ToListAsync();
        }

        // ===============================
        // GET BY COURSE ID
        // ===============================
        public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .Where(e => e.CourseId == courseId)
                //.Include(e => e.User)  // uncomment after merge
                .Include(e => e.Program)
                .ToListAsync();
        }

        // ===============================
        // GET BY USER & COURSE
        // ===============================
        public async Task<Enrollment?> GetByUserAndCourseAsync(Guid userId, Guid courseId)
        {
            return await _dbSet
                //.Include(e => e.User) // uncomment after merge
                .Include(e => e.Course)
                .Include(e => e.Program)
                //.FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
                .FirstOrDefaultAsync(e => e.CourseId == courseId); // temporary version
        }

        // ===============================
        // GET BY ID WITH RELATIONS
        // ===============================
        public async Task<Enrollment?> GetByIdWithRelationsAsync(Guid id)
        {
            return await _dbSet
                .Include(e => e.Course)
                .Include(e => e.Program)
                //.Include(e => e.User) // uncomment after merge
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);
        }
    }
}
