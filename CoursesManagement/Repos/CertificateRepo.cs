using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Repository for managing <see cref="Certificate"/> entities.
    /// Provides queries by Course, Enrollment, and Certificate URL.
    /// (All User-related logic is commented out until the User module is added.)
    /// </summary>
    public class CertificateRepo : GenericRepo<Certificate>, ICertificateRepo
    {
        /// <summary>
        /// Initializes a new instance of the repository using the shared database context.
        /// </summary>
        public CertificateRepo(CoursesDbContext context) : base(context) { }

        // ==========================================================
        // 🔹 Get by Enrollment ID
        // ==========================================================
        /// <summary>
        /// Retrieves a certificate that belongs to a specific enrollment.
        /// </summary>
        /// <param name="enrollmentId">The unique enrollment ID.</param>
        /// <returns>The <see cref="Certificate"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<Certificate?> GetByEnrollmentAsync(Guid enrollmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.EnrollmentId == enrollmentId);
        }

        // ==========================================================
        // 🔹 Get by Course ID
        // ==========================================================
        /// <summary>
        /// Retrieves all certificates associated with a specific course.
        /// </summary>
        /// <param name="courseId">The unique course ID.</param>
        /// <returns>A list of certificates issued for that course.</returns>
        public async Task<IEnumerable<Certificate>> GetByCourseAsync(Guid courseId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.CourseId == courseId)
                .OrderByDescending(c => c.IssuedAt)
                .ToListAsync();
        }

        // ==========================================================
        // 🔹 Check if certificate exists for a course
        // ==========================================================
        /// <summary>
        /// Checks whether at least one certificate exists for a given course.
        /// </summary>
        /// <param name="courseId">The unique course ID.</param>
        /// <returns><c>true</c> if a certificate exists; otherwise, <c>false</c>.</returns>
        public async Task<bool> ExistsByCourseAsync(Guid courseId)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(c => c.CourseId == courseId);
        }

        // ==========================================================
        // 🔹 Get by Certificate URL
        // ==========================================================
        /// <summary>
        /// Retrieves a certificate by its unique public URL.
        /// </summary>
        /// <param name="certificateUrl">The public certificate URL.</param>
        /// <returns>A <see cref="Certificate"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<Certificate?> GetByUrlAsync(string certificateUrl)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CertificateUrl == certificateUrl);
        }

        // ==========================================================
        // 🔹 Get by User ID (commented until User module is merged)
        // ==========================================================
        /// <summary>
        /// Retrieves all certificates belonging to a specific user.
        /// Commented until the User entity is added to the project.
        /// </summary>
        /// <param name="userId">The unique user ID. (commented)</param>
        /// <returns>List of user certificates ordered by date issued.</returns>
        /*
        public async Task<IEnumerable<Certificate>> GetByUserAsync(Guid userId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.IssuedAt)
                .ToListAsync();
        }
        */
    }
}