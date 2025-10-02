using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Repository interface for managing enrollments.
    /// Extends generic repo with enrollment-specific queries.
    /// </summary>
    public interface IEnrollmentRepository : IGenericRepo<Enrollment>
    {
        /// <summary>
        /// Finds an enrollment for a specific user and course.
        /// </summary>
        Task<Enrollment?> GetByUserAndCourseAsync(Guid userId, Guid courseId);

        /// <summary>
        /// Retrieves all enrollments for a given user.
        /// </summary>
        Task<IEnumerable<Enrollment>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Retrieves all enrollments for a given course.
        /// </summary>
        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId);
    }
}
