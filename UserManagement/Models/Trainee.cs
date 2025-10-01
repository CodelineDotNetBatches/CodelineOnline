using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Trainee
    {
        public Guid TraineeId { get; set; }
        public string GithubUsername { get; set; } = default!;
        public string? ProfileImage { get; set; }
        public string EducationalBackground { get; set; } = default!;
        public string? TraineeCV { get; set; }
        public string? LearningObjectives { get; set; }
        public string ExperienceLevel { get; set; } = default!;

        public ICollection<BatchTrainee> BatchTrainees { get; set; } = new List<BatchTrainee>();
    }
}
