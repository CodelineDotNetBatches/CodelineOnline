namespace CoursesManagement.DTOs
{
    /// <summary>
    /// DTO for detailed enrollment information.
    /// </summary>
    public class EnrollmentDetailDto
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

        /// <summary>
        /// Title of the enrolled course.
        /// </summary>
        public string CourseTitle { get; set; } = default!;

        /// <summary>
        /// ID of the program (if enrollment is part of a program).
        /// </summary>
        public Guid? ProgramId { get; set; }

        /// <summary>
        /// Name of the program (if available).
        /// </summary>
        public string? ProgramName { get; set; }

        /// <summary>
        /// Date and time when the enrollment was created.
        /// </summary>
        public DateTime EnrolledAt { get; set; }

        /// <summary>
        /// Current enrollment status (e.g., Active, Completed, Dropped).
        /// </summary>
        public string Status { get; set; } = "Active";

        /// <summary>
        /// Optional grade achieved in the course (0-100).
        /// </summary>
        public decimal? Grade { get; set; }

        /// <summary>
        /// Optional reason for the most recent status change.
        /// </summary>
        public string? StatusChangeReason { get; set; }
    }
}
