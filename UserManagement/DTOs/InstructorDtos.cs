using System.ComponentModel.DataAnnotations;
using UserManagement.Models;

namespace UserManagement.DTOs
{
    public class InstructorCreateDto
    {
        [Required] public int InstructorId { get; set; } // must equal existing UserId from Users module
        public string? GithubUserName { get; set; }
        [Range(0, 60)] public int Years_of_Experience { get; set; }
        public string? ProfileImage { get; set; }
        public string? InstructorCV { get; set; }
        [Required] public ExperienceLevel Experience_Level { get; set; }
        [Required] public TeachingStyle Teaching_Style { get; set; }
    }
    public class InstructorReadDto
    {
        public int InstructorId { get; set; }
        public string? GithubUserName { get; set; }
        public int Years_of_Experience { get; set; }
        public string? ProfileImage { get; set; }
        public string? InstructorCV { get; set; }
        public ExperienceLevel Experience_Level { get; set; }
        public TeachingStyle Teaching_Style { get; set; }
    }

    public class InstructorUpdateDto : InstructorCreateDto 
    {
    }
}
