using CoursesManagement.Models;

namespace CoursesManagement.DTOs
{
    public class CourseUpdateDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = default!;
        public string? CourseDescription { get; set; }
        public LevelType CourseLevel { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
