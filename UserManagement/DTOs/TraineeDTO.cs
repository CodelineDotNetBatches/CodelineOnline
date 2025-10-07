using UserManagement.Models;

namespace UserManagement.DTOs
{

    /// <summary>
    /// DTO for transferring trainee data.
    /// </summary>
    public class TraineeDTO
    {
        public int TraineeId { get; set; }
        public string GithubUsername { get; set; } = default!;
        public string? ProfileImage { get; set; }
        public string EducationalBackground { get; set; } = default!;
        public string? TraineeCV { get; set; }
        public string? LearningObjectives { get; set; }
        public string ExperienceLevel { get; set; } = default!;


        public int Years_of_Experience { get; set; }
        public LearningStyle Learning_Style { get; set; }
        public StudyFocus Study_Focus { get; set; }

    }
}
