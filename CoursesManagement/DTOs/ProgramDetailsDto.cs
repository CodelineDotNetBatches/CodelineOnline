namespace CoursesManagement.DTOs
{
    public class ProgramDetailsDto
    {
        public Guid ProgramId { get; set; }
        public string ProgramName { get; set; } = null!;
        public string? ProgramDescription { get; set; }
        public string Roadmap { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; }

        
        public List<Guid> CategoryIds { get; set; } = new();
        public List<Guid> CourseIds { get; set; } = new();

       
        public List<string>? CategoryNames { get; set; }
        public List<string>? CourseNames { get; set; }

        
        public int EnrollmentsCount { get; set; }
    }
}
