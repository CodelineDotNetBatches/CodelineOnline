using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Repository interface for managing <see cref="Certificate"/> entities.
    /// Defines specialized queries such as by Course, Enrollment, and Certificate URL.
    /// </summary>
    public interface ICertificateRepo : IGenericRepo<Certificate>
    {
        // ==========================================================
        // 🔹 Get certificate by Enrollment ID
        // ==========================================================
        /// <summary>
        /// Retrieves a certificate belonging to a specific enrollment.
        /// </summary>
        /// <param name="enrollmentId">The unique enrollment ID.</param>
        /// <returns>
        /// A <see cref="Certificate"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        Task<Certificate?> GetByEnrollmentAsync(Guid enrollmentId);

        // ==========================================================
        // 🔹 Get certificates by Course ID
        // ==========================================================
        /// <summary>
        /// Retrieves all certificates associated with a specific course.
        /// </summary>
        /// <param name="courseId">The unique course ID.</param>
        /// <returns>
        /// A collection of certificates issued for the given course.
        /// </returns>
        Task<IEnumerable<Certificate>> GetByCourseAsync(Guid courseId);

        // ==========================================================
        // 🔹 Check if certificate exists for a course
        // ==========================================================
        /// <summary>
        /// Checks whether any certificate exists for a specified course.
        /// </summary>
        /// <param name="courseId">The unique course ID.</param>
        /// <returns>
        /// <c>true</c> if a certificate exists for the course; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> ExistsByCourseAsync(Guid courseId);

        // ==========================================================
        // 🔹 Get certificate by URL
        // ==========================================================
        /// <summary>
        /// Retrieves a certificate using its unique public URL.
        /// </summary>
        /// <param name="certificateUrl">The unique certificate URL.</param>
        /// <returns>
        /// A <see cref="Certificate"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        Task<Certificate?> GetByUrlAsync(string certificateUrl);

        // ==========================================================
        // 🔹 Get certificates by User ID (commented)
        // ==========================================================
        /// <summary>
        /// Retrieves all certificates belonging to a specific user.
        /// Commented until the User module is integrated.
        /// </summary>
        /// <param name="userId">The unique user ID. (commented)</param>
        /// <returns>
        /// A list of certificates ordered by issue date.
        /// </returns>
        /*
        Task<IEnumerable<Certificate>> GetByUserAsync(Guid userId);
        */
    }
}