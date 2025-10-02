using CoursesManagement.DTOs;

namespace CoursesManagement.Services
{
    /// <summary>
    /// Service interface for handling enrollment operations.
    /// Defines methods for creating, updating, retrieving, and deleting enrollments.
    /// </summary>
    public interface IEnrollmentService
    {
        /// <summary>
        /// Retrieves all enrollments with related User, Course, and Program data.
        /// Returns summary DTOs for list views.
        /// </summary>
        Task<IEnumerable<EnrollmentListDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a single enrollment by ID with full details.
        /// Returns null if not found.
        /// </summary>
        /// <param name="enrollmentId">The enrollment ID.</param>
        Task<EnrollmentDetailDto?> GetByIdAsync(Guid enrollmentId);

        /// <summary>
        /// Retrieves a single enrollment by User and Course IDs.
        /// Returns null if not found.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="courseId">The course ID.</param>
        Task<EnrollmentDetailDto?> GetByUserAndCourseAsync(Guid userId, Guid courseId);

        /// <summary>
        /// Retrieves all enrollments for a given user.
        /// Returns summary DTOs.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        Task<IEnumerable<EnrollmentListDto>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Retrieves all enrollments for a given course.
        /// Returns summary DTOs.
        /// </summary>
        /// <param name="courseId">The course ID.</param>
        Task<IEnumerable<EnrollmentListDto>> GetByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Creates a new enrollment from a DTO.
        /// Throws exception if user is already enrolled in the course.
        /// </summary>
        /// <param name="dto">The create enrollment DTO.</param>
        Task<EnrollmentDetailDto> CreateAsync(CreateEnrollmentDto dto);

        /// <summary>
        /// Updates an enrollment (status, grade, or status change reason).
        /// Returns null if not found.
        /// </summary>
        /// <param name="enrollmentId">The enrollment ID.</param>
        /// <param name="dto">The update enrollment DTO.</param>
        Task<EnrollmentDetailDto?> UpdateAsync(Guid enrollmentId, UpdateEnrollmentDto dto);

        /// <summary>
        /// Deletes an enrollment by ID.
        /// Returns true if deleted, false if not found.
        /// </summary>
        /// <param name="enrollmentId">The enrollment ID.</param>
        Task<bool> DeleteAsync(Guid enrollmentId);
    }
}
