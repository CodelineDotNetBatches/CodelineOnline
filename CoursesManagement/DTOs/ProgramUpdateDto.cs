using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.DTOs
{
    public class ProgramUpdateDto
    {
        [Required, MaxLength(200)]
        public string ProgramName { get; set; } = null!;

        [MaxLength(500)]
        public string? ProgramDescription { get; set; }

        [Required]
        public string Roadmap { get; set; } = null!;

        // The IDs of the categories this program belongs to (Many-to-Many).
        public List<Guid> CategoryId { get; set; } = new();
    }
}
