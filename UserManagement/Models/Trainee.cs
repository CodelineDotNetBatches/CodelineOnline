using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Trainee
    {
        public Guid TraineeId { get; set; }   // PK is Guid
        public string GithubUsername { get; set; } = default!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? EducationalBackground { get; set; }
        public string? ExperienceLevel { get; set; }
        public string? LearningObjectives { get; set; }
        public string? ProfileImage { get; set; }
        public string? TraineeCV { get; set; }

        // Navigation
        public ICollection<BatchTrainee> BatchTrainees { get; set; } = new List<BatchTrainee>();
        public ICollection<TraineeSkill> TraineeSkills { get; set; } = new List<TraineeSkill>();
    }
}
