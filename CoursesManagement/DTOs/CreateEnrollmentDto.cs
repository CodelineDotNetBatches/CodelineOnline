namespace CoursesManagement.DTOs
{
    /// <summary>
    /// DTO for creating a new enrollment.
    /// </summary>
    public class CreateEnrollmentDto
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public Guid? ProgramId { get; set; }
    }
}
