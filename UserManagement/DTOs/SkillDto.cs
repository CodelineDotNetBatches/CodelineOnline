namespace UserManagement.DTOs;


    public class SkillDto
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
        public string SkillLevel { get; set; } = string.Empty;
        public int MonthsOfExperience { get; set; }
    }

