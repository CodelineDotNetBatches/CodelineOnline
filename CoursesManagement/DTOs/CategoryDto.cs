using CoursesManagement.DTOs;

/// <summary>
/// DTO for returning category data (Out).
/// </summary>
public class CategoryDto
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? CategoryDescription { get; set; }
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Programs this category belongs to.
    /// </summary>
    public List<ProgramDetailsDto> Programs { get; set; } = new();

    /// <summary>
    /// Courses under this category.
    /// </summary>
    public List<CourseListDto> Courses { get; set; } = new();
}