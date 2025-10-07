using CoursesManagement.Models;

namespace CoursesManagement.DTOs
{
    public class ProgramsWithCoursesDto
    {
        public Guid ProgramId { get; set; }
        public string ProgramName { get; set; } = null!;
        public string? ProgramDescription { get; set; }
        public string Roadmap { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; }

        public List<Course>? ProgramCourses { get; set; } = new();
    }
}
