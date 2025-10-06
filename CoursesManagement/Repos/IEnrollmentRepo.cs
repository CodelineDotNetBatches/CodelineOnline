using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Repository interface for Enrollment entity operations.
    /// Defines custom queries in addition to generic CRUD.
    /// </summary>
    public interface IEnrollmentRepo : IGenericRepo<Enrollment>
    {
        // ==========================================================
        // 🔹 Get all enrollments by User ID
        // ==========================================================
        /// <summary>
        /// Returns all enrollments belonging to a specific user.
        /// </summary>
        Task<IEnumerable<Enrollment>> GetByUserIdAsync(Guid userId);

        // ==========================================================
        // 🔹 Get all enrollments by Course ID
        // ==========================================================
        /// <summary>
        /// Returns all enrollments for a specific course.
        /// </summary>
        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId);

        // ==========================================================
        // 🔹 Get enrollment by User + Course combination
        // ==========================================================
        /// <summary>
        /// Returns a single enrollment that matches both user and course IDs.
        /// Returns null if no record is found.
        /// </summary>
        Task<Enrollment?> GetByUserAndCourseAsync(Guid userId, Guid courseId);

        // ==========================================================
        // 🔹 Get enrollment with related entities (Course, Program, etc.)
        // ==========================================================
        /// <summary>
        /// Returns an enrollment by ID with all related entities loaded.
        /// </summary>
        Task<Enrollment?> GetByIdWithRelationsAsync(Guid id);
    }
}
