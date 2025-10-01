namespace CoursesManagement.DTOs
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; } = default!;
        public string? CategoryDescription { get; set; }
        public Guid ProgramId { get; set; }
    }
}
