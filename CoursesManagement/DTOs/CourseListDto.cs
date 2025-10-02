using CoursesManagement.Models;

namespace CoursesManagement.DTOs
{
    public class CourseListDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = default!;
        public decimal Price { get; set; }
        public LevelType CourseLevel { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}
