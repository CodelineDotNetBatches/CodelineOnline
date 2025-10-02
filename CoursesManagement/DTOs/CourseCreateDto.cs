using CoursesManagement.Models;

namespace CoursesManagement.DTOs
{
    public class CourseCreateDto
    {
        public string CourseName { get; set; } = default!;
        public string? CourseDescription { get; set; }
        public LevelType CourseLevel { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Just ID, not full Category object
    }
}
