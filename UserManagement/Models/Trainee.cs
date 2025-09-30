using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Trainee
    {
        /// <summary>
        /// Unique identifier for the trainee.
        /// </summary>
        public Guid TraineeId { get; set; }

        /// <summary>
        /// GitHub username of the trainee.
        /// </summary>
        public string GithubUsername { get; set; } = default!;

        /// <summary>
        /// Profile image file path or URL.
        /// </summary>
        public string? ProfileImage { get; set; }

        /// <summary>
        /// Educational background (degree, specialization, etc.).
        /// </summary>
        public string EducationalBackground { get; set; } = default!;

        /// <summary>
        /// CV file path or link.
        /// </summary>
        public string? TraineeCV { get; set; }

        /// <summary>
        /// Learning objectives or personal goals.
        /// </summary>
        public string? LearningObjectives { get; set; }

        /// <summary>
        /// Experience level (e.g., Beginner, Intermediate, Advanced).
        /// </summary>
        public string ExperienceLevel { get; set; } = default!;

        /// <summary>
        /// Navigation: many-to-many with batches.
        /// </summary>
        public ICollection<BatchTrainee> BatchTrainees { get; set; } = new List<BatchTrainee>();
    }
}
