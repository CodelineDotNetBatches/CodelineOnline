using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.DTOs
{
    /// <summary>
    /// DTO for updating an existing category.
    /// </summary>
    public class UpdateCategoryDto
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required, MaxLength(200)]
        public string CategoryName { get; set; } = null!;

        [MaxLength(500)]
        public string? CategoryDescription { get; set; }

        /// <summary>
        /// The IDs of the programs this category belongs to (Many-to-Many).
        /// </summary>
        public List<Guid> ProgramIds { get; set; } = new();
    }
}
