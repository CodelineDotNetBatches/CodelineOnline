namespace CoursesManagement.DTOs
{
    public class UpdateCategoryDto
    {
        public string CategoryName { get; set; } = default!;
        public string? CategoryDescription { get; set; }
    }
}
