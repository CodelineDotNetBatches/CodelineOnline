namespace CoursesManagement.DTOs
{
    /// <summary>
    /// DTO for returning enrollment details to clients.
    /// </summary>
    public class EnrollmentDto
    {
        public Guid EnrollmentId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;  
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = default!;
        public Guid? ProgramId { get; set; }
        public string? ProgramName { get; set; }
        public DateTime EnrolledAt { get; set; }
        public string Status { get; set; } = "Active";
        public decimal? Grade { get; set; }
    }

    /// <summary>
    /// DTO for creating a new enrollment.
    /// </summary>
    public class CreateEnrollmentDto
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public Guid? ProgramId { get; set; }
    }

    /// <summary>
    /// DTO for updating enrollment status or grade.
    /// </summary>
    public class UpdateEnrollmentDto
    {
        public string Status { get; set; } = "Active"; // Active, Completed, Dropped
        public decimal? Grade { get; set; }
    }
}
