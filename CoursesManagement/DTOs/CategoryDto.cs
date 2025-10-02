namespace CoursesManagement.DTOs
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public string? CategoryDescription { get; set; }
    }
}
