using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Repository interface for managing Enrollment entities.
    /// Extends the generic repository with enrollment-specific queries.
    /// </summary>
    public interface IEnrollmentRepo : IGenericRepo<Enrollment>
    {
        /// <summary>
        /// Retrieves all enrollments for a given user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A list of Enrollment entities for the specified user.</returns>
        Task<IEnumerable<Enrollment>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Retrieves all enrollments for a given course.
        /// </summary>
        /// <param name="courseId">The course ID.</param>
        /// <returns>A list of Enrollment entities for the specified course.</returns>
        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Retrieves a single enrollment for a given user and course combination.
        /// Returns null if not found.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="courseId">The course ID.</param>
        Task<Enrollment?> GetByUserAndCourseAsync(Guid userId, Guid courseId);
    }
}
