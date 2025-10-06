namespace CoursesManagement.DTOs
{
    /// <summary>
    /// DTO for listing enrollments in summary views.
    /// </summary>
    public class EnrollmentListDto
    {
        /// <summary>
        /// Unique identifier of the enrollment.
        /// </summary>
        public Guid EnrollmentId { get; set; }

        /// <summary>
        /// ID of the enrolled user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Name of the enrolled user.
        /// </summary>
        public string UserName { get; set; } = default!;

        /// <summary>
        /// ID of the enrolled course.
        /// </summary>
        public Guid CourseId { get; set; }

        public string CourseName { get; set; } = default!;

        /// <summary>
        /// Title of the enrolled course.
        /// </summary>
        public string CourseTitle { get; set; } = default!;

        /// <summary>
        /// Current enrollment status (e.g., Active, Completed, Dropped).
        /// </summary>
        public string Status { get; set; } = "Active";

        /// <summary>
        /// Optional reason for the most recent status change (short version).
        /// Useful in list views to quickly show context (e.g., "Dropped due to absence").
        /// </summary>
        public string? StatusChangeReason { get; set; }
    }
}
