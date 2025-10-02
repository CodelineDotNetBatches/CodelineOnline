namespace CoursesManagement.DTOs
{
    /// <summary>
    /// DTO for updating enrollment status or grade,
    /// with an optional reason for the status change.
    /// </summary>
    public class UpdateEnrollmentDto
    {
        /// <summary>
        /// The new status of the enrollment (e.g., Active, Completed, Dropped).
        /// Defaults to "Active".
        /// </summary>
        public string Status { get; set; } = "Active";

        /// <summary>
        /// Optional grade achieved in the course (0-100).
        /// </summary>
        public decimal? Grade { get; set; }

        /// <summary>
        /// Optional reason for the status change 
        /// (e.g., "Dropped due to absence", "Completed successfully").
        /// Useful for audits and reporting.
        /// </summary>
        public string? StatusChangeReason { get; set; }
    }
}
