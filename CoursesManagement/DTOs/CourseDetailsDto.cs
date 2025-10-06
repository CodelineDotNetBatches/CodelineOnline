using CoursesManagement.Models;

namespace CoursesManagement.DTOs
{
    public class CourseDetailsDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = default!;
        public string? CourseDescription { get; set; }
        public LevelType CourseLevel { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        // Related entities simplified (instead of full navigation props)
        public string CategoryName { get; set; } = default!;

        // Example future expansion
         public List<string> ProgramNames { get; set; } = new();
        // public List<string> LessonTitles { get; set; } = new();
    }
}
