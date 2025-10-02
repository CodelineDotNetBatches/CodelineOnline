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
    }
}
