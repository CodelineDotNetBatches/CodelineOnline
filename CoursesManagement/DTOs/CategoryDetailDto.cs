namespace CoursesManagement.DTOs
{
    public class CategoryDetailDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public string? CategoryDescription { get; set; }

        // Outgoing nested data
        public IEnumerable<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
