using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO for creating a new category.
/// </summary>
public class CreateCategoryDto
{
    [Required, MaxLength(200)]
    public string CategoryName { get; set; } = null!;

    [MaxLength(500)]
    public string? CategoryDescription { get; set; }

    /// <summary>
    /// The IDs of the programs this category belongs to (Many-to-Many).
    /// </summary>
    public List<Guid> ProgramIds { get; set; } = new();
}