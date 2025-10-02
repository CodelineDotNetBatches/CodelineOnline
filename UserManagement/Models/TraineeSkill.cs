namespace UserManagement.Models;


    public class TraineeSkill
    {
    public Guid TraineeId { get; set; }   // FIX: Guid, not int
    public int SkillId { get; set; }

    // Navigation properties
    public Trainee Trainee { get; set; }
    public Skill Skill { get; set; }
}

