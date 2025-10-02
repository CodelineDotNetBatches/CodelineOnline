using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Trainee
    {
        public int TraineeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<TraineeSkill>? TraineeSkills { get; set; }
    }
}
