namespace UserManagement.Models;


    public class TraineeSkill
    {
        public int TraineeId { get; set; }
        public int SkillId { get; set; }

        public Trainee Trainee { get; set; }
        public Skill Skill { get; set; }
    }

