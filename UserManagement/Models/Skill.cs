

namespace UserManagement.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
        public int SkillLevel { get; set; }
        public int MonthsOfExperience { get; set; }

        // Relationships
        public ICollection<TraineeSkill> TraineeSkills { get; set; } = new List<TraineeSkill>();
    }
}
