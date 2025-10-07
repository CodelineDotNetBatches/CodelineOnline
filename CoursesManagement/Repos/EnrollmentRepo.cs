using Microsoft.EntityFrameworkCore;
using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Concrete repository implementation for managing enrollments.
    /// Provides specialized queries in addition to generic CRUD operations.
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
                //.Include(e => e.User) // uncomment after merge
                .Include(e => e.Program)
                .ToListAsync();
        }

        // ===============================
        // GET BY USER & COURSE
        // ===============================
        public async Task<Enrollment?> GetByUserAndCourseAsync(Guid userId, Guid courseId)
        {
            return await _dbSet
                //.Include(e => e.User)
                .Include(e => e.Course)
                .Include(e => e.Program)
                //.FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
                .FirstOrDefaultAsync(e => e.CourseId == courseId); // temporary
        }

        // ===============================
        // GET BY ID WITH RELATIONS
        // ===============================
        public async Task<Enrollment?> GetByIdWithRelationsAsync(Guid id)
        {
            return await _dbSet
                .Include(e => e.Course)
                .Include(e => e.Program)
                //.Include(e => e.User)
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);
        }

        // ===============================
        //  GET BY COURSE NAME 
        // ===============================
        /// <summary>
        /// Retrieves enrollments for a specific course name.
        /// Includes related Course and Program entities.
        /// </summary>
        /// <param name="courseName">The name of the course.</param>
        /// <returns>A list of enrollments that match the course name.</returns>
        public async Task<IEnumerable<Enrollment>> GetByCourseNameAsync(string courseName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
                return new List<Enrollment>();

            string normalized = courseName.Trim().ToLower();

            return await _dbSet
                .Include(e => e.Course)
                .Include(e => e.Program)
                //.Include(e => e.User)
                .Where(e => e.Course.CourseName.ToLower() == normalized)
                .ToListAsync();
        }
    }
}
